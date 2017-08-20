using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class distnceText : MonoBehaviour
{

    private GameObject distanceText;
    private GameObject healthText;
    private GameObject player;
    private Text textComponent;
    private Text healthTextComponent;
    private player playerObj;

    private MeshRenderer reticleMaterial;
    private MeshRenderer healthReticleMaterial;
    // Use this for initialization
    void Start () {
	    distanceText = GameObject.FindGameObjectWithTag("DistanceText");
	    healthText = GameObject.FindGameObjectWithTag("HealthText");
	    player = GameObject.FindGameObjectWithTag("Player");
        playerObj = player.GetComponent<player>();
        textComponent = distanceText.GetComponent<Text>();
        healthTextComponent = healthText.GetComponent<Text>();
        reticleMaterial = GetComponent<MeshRenderer>();
        healthReticleMaterial = GetComponent<MeshRenderer>();

    }
	
	// Update is called once per frame
	void Update ()
	{
	    var distance = reticleMaterial.material.GetFloat("_DistanceInMeters");
	    healthTextComponent.text = String.Format("Health-{0}",playerObj.health);

        textComponent.text = string.Format("Distance-{0}",distance);

	}
}
