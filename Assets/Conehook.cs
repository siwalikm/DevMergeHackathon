using System.Linq;
using Assets;
using UnityEngine;

public class Conehook : MonoBehaviour {

    private GameObject camera;

    public bool isReleased;
    private GameObject player;
    private GameObject hook1;
    private GameObject hook2;
    private player playerObj;
    public float positionX;
    private FixedJoint joint;
    private Collider playerCollider;
    public bool isLatched;
    public bool isRetreating;
    private int touchStatus; //0-no touch,1-touching,2-touch released

    private Rigidbody rb;
    // Use this for initialization

    public void ResetHook()
    {
        rb.isKinematic = true;
        transform.localPosition = new Vector3(positionX,-2f,4);
        transform.localRotation = Quaternion.Euler(new Vector3(-10,1,0));
        isReleased = false;
        isLatched = false;
        isRetreating = false;
        Destroy(joint);
    }
    
    void Start () {

        camera = GameObject.FindGameObjectsWithTag("MainCamera").Single();
        isReleased = false;
        var rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectsWithTag("Player").Single();
        hook1 = GameObject.FindGameObjectsWithTag("Hook").Single();
        hook2 = GameObject.FindGameObjectsWithTag("Hook2").Single();
        playerObj = player.GetComponent<player>();
        playerCollider = player.GetComponent<Collider>();
        joint = gameObject.GetComponent<FixedJoint>();
        isLatched = false;
        isRetreating = false;
        touchStatus = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    if (!isReleased)
	    {
	        //transform.position = camera.transform.position;
	        //transform.rotation = camera.transform.rotation;

	    }
	    if (Input.touchCount == 0)
	    {
	        if (touchStatus == (int)TouchStatus.Touching)
	        {
	            touchStatus = (int)TouchStatus.Released;
	        }
	        else
	        {
	            touchStatus = (int)TouchStatus.NoTouch;
	        }
	    }
	    if (touchStatus == (int)TouchStatus.Released || Input.GetMouseButtonDown(0) )
        {
            if (isLatched || isReleased)
            {
                isRetreating = true;
                rb.isKinematic = true;
            }
            else
            {
                isReleased = true;
                rb = GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.AddForce(transform.forward * 320, ForceMode.Impulse);
            }
        }
	    if (isRetreating)
	    {
            //transform.LookAt(player.transform);
            //Vector3 direction = transform.position - new Vector3(player.transform.position.x + positionX, player.transform.position.y, player.transform.position.z);
            //rb.AddForce(transform.forward * 80, ForceMode.Force);
	        var start = transform.position;
	        playerObj.speed = 0;
	        var target = new Vector3(player.transform.position.x + positionX, player.transform.position.y,
	            player.transform.position.z);
	        float speed = 320;
            transform.position = Vector3.MoveTowards(start, target, speed * Time.deltaTime);
	        if (transform.position == target)
	        {
	            ResetHook();
            }

        }
    }


    void OnCollisionEnter(Collision c)
    {
        if (c.collider != playerCollider)
        {
            isReleased = false;
            isLatched = true;
            rb.isKinematic = true;
            var midPoint = Vector3.Lerp(hook1.transform.position, hook2.transform.position, 0.5f);
            playerObj.isInMotion = true;
            playerObj.targetLocation = midPoint;
            playerObj.speed = 10;
        }
    }
}
