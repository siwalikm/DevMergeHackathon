using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class hook : MonoBehaviour
{

    public bool towardsCube;
    public bool towardsPlayer;
    public bool onPlayer;
    public bool onCube;
    public float speed;
    public Vector3 targetLocation;
    private GameObject player;
    private player playerObj;

    // Use this for initialization
    void Start()
    {
        towardsCube = false;
        towardsPlayer = false;
        onPlayer = true;
        onCube = false;
        player = GameObject.FindGameObjectsWithTag("Player").SingleOrDefault();
        playerObj = player.GetComponent<player>();
    }

    // Update is called once per frame
	void Update () {
	    if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
	    {
	        Debug.Log("Touched");
	    }
	    if (towardsCube)
	    {
	        var start = transform.position;
	        transform.position = Vector3.MoveTowards(start, targetLocation, speed * Time.deltaTime);
	        if (transform.position == targetLocation)
	        {
	            onCube = true;
	            towardsCube = false;
	            playerObj.isInMotion = true;
	            playerObj.targetLocation = transform.position;
	        }
        }
	    if (towardsPlayer)
	    {
	        var start = transform.position;
	        transform.position = Vector3.MoveTowards(start, targetLocation, speed * Time.deltaTime);
	        if (transform.position == targetLocation)
	        {
	            onPlayer = true;
	            towardsPlayer = false;
	        }
	    }
	    if (onPlayer)
	    {
	        transform.position = player.transform.position;

	    }
    }

}
