using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crossRotationInWallHole : MonoBehaviour {
	public float rotateSpeed = 20F;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
	}
}
