using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class coneComponent : MonoBehaviour
{

    private GameObject camera;
    private GameObject hook1;
    private GameObject hook2;
    private Conehook hook1Obj;
    private Conehook hook2Obj;

    // Use this for initialization
    void Start()
    {

        camera = GameObject.FindGameObjectsWithTag("MainCamera").Single();
        hook1 = GameObject.FindGameObjectsWithTag("Hook").Single();
        hook2 = GameObject.FindGameObjectsWithTag("Hook2").Single();
        hook1Obj = hook1.GetComponent<Conehook>();
        hook2Obj = hook2.GetComponent<Conehook>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!(hook1Obj.isReleased || hook2Obj.isReleased))
        {
            transform.rotation = camera.transform.rotation;
            transform.position = camera.transform.position;
        }
    }
}
