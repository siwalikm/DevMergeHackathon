using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class player : MonoBehaviour
{

    public bool isInMotion;
    public Vector3 targetLocation;
    public float speed;
    private GameObject hook1;
    private GameObject hook2;
    private Conehook hook1Obj;
    private Conehook hook2Obj;
    private List<Collider> hookColliders;
    public bool playerIdle;
    private GameObject camera;
    private GameObject cube;
    public bool collided;
    public DateTime motionStartTime;
    public float waitTime;
    private bool timeSet;
    public float health;
    private int frameCounter;
    public int frameLimit;
    private bool initialPositionStored;
    private Vector3 hook1Pos;
    private Vector3 hook2Pos;


    private Quaternion cameraRot;
    //private GameObject actualPlayer;

    //Use this for initialization

    void Start()
    {
        //actualPlayer = GameObject.FindGameObjectsWithTag("ActualPlayer").Single();
        camera = GameObject.FindGameObjectWithTag("MainCamera");
        hook1 = GameObject.FindGameObjectsWithTag("Hook").Single();
        hook2 = GameObject.FindGameObjectsWithTag("Hook2").Single();
        hook1Obj = hook1.GetComponent<Conehook>();
        hook2Obj = hook2.GetComponent<Conehook>();
        hookColliders = new List<Collider>();
        hookColliders.Add(hook1.GetComponent<Collider>());
        hookColliders.Add(hook2.GetComponent<Collider>());
        playerIdle = true;
        cameraRot = new Quaternion();
        collided = false;
        health = 10;
    }

    //void Awake()
    //{
    //    camera = GameObject.FindGameObjectWithTag("MainCamera");
    //    cameraRot = camera.transform.rotation;
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        //actualPlayer.transform.position = transform.position + transform.forward * -0.5f;
        if (isInMotion)
        {
            if (!timeSet)
            {
                motionStartTime = DateTime.Now;
                timeSet = true;
            }
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = false;
            Vector3 direction = targetLocation - transform.position;
            rb.AddForce(direction * speed, ForceMode.Force);
            //var start = transform.position;
            //transform.position = Vector3.MoveTowards(start, targetLocation, speed * Time.deltaTime);
            if (transform.position == targetLocation)
            {
                isInMotion = false;
                playerIdle = true;
                hook1Obj.ResetHook();
                hook2Obj.ResetHook();
                rb.isKinematic = true;
            }
            playerIdle = false;
            var timeSinceMovement = (DateTime.Now - motionStartTime).TotalMilliseconds;
            if (timeSinceMovement > waitTime)
                collided = false;
        }

    }

    void Update()
    {

        frameCounter++;
        if (frameCounter > frameLimit && health < 10)
        {
            health++;
            frameCounter = 0;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (!hookColliders.Contains(c.collider) && !playerIdle && !collided)
        {
            timeSet = false;
            collided = true;
            var rb = GetComponent<Rigidbody>();
            rb.isKinematic = true;
            isInMotion = false;
            hook1Obj.ResetHook();
            hook2Obj.ResetHook();
            if (health > 0)
                health--;
        }
    }
}
