
using System.Linq;
using UnityEngine;

public class line : MonoBehaviour
{
    private GameObject cylinder;
    private GameObject player;
    private GameObject hook;
    // Use this for initialization
    void Start()
    {

        player = GameObject.FindGameObjectsWithTag("Player").Single();
        hook = GameObject.FindGameObjectsWithTag("Hook").Single();
        cylinder = GameObject.FindGameObjectsWithTag("Cylinder").Single();
    }

    // Update is called once per frame
    void Update()
    {
        var dist = Vector3.Distance(player.transform.position, hook.transform.position) / 2;
        //cylinder.transform.position = player.transform.position;
        cylinder.transform.LookAt(hook.transform.position);
        cylinder.transform.Rotate(Vector3.right, 90);
        cylinder.transform.localScale = Vector3.one;
        cylinder.transform.localScale = new Vector3(0.05f / cylinder.transform.lossyScale.x, dist / cylinder.transform.lossyScale.y, 0.05f / cylinder.transform.lossyScale.z);
        //cylinder.transform.lossyScale = new Vector3(0.1f, dist, 0.1f);
        cylinder.transform.position = (player.transform.position + cylinder.transform.up * dist);
        //cylinder.transform.position += new Vector3(cylinder.transform.position.x+0.5f, cylinder.transform.position.y, cylinder.transform.position.z);
        //cylinder.transform.position += cylinder.transform.right * 5;
    }
}
