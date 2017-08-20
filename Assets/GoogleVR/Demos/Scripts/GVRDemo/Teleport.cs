// Copyright 2014 Google Inc. All rights reserved.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using UnityEngine;

using System.Collections;
using System.Linq;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class Teleport : MonoBehaviour
{
    private Vector3 startingPosition;

    public Material inactiveMaterial;
    public Material gazedAtMaterial;
    public float speed;
    private bool isInMotion;
    private Vector3 start;
    private Vector3 end;
    private GameObject player;
    private GameObject hook;
    private hook hookObj;
    void Start()
    {
        startingPosition = transform.localPosition;
        SetGazedAt(false);
        isInMotion = false;
        player = GameObject.FindGameObjectsWithTag("Player").SingleOrDefault();
        hook = GameObject.FindGameObjectsWithTag("Hook").SingleOrDefault();
        hookObj = hook.GetComponent<hook>();
    }

    public void SetGazedAt(bool gazedAt)
    {
        if (inactiveMaterial != null && gazedAtMaterial != null)
        {
            GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
            return;
        }
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
    }

    public void Reset()
    {
        transform.localPosition = startingPosition;
    }

    public void Recenter()
    {
#if !UNITY_EDITOR
    GvrCardboardHelpers.Recenter();
#else
        GvrEditorEmulator emulator = FindObjectOfType<GvrEditorEmulator>();
        if (emulator == null)
        {
            return;
        }
        emulator.Recenter();
#endif  // !UNITY_EDITOR
    }

    public void TeleportRandomly()
    {
        if (hookObj.onCube)
        {
            hookObj.onCube = false;
            hookObj.towardsPlayer = true;
            hookObj.targetLocation = player.transform.position;
        }
        else if (hookObj.onPlayer)
        {
            hookObj.onPlayer = false;
            hookObj.towardsCube = true;
            hookObj.targetLocation = transform.position;
        }
        else if (hookObj.towardsCube)
        {
            hookObj.towardsCube = false;
            hookObj.towardsPlayer = true;
            hookObj.targetLocation = player.transform.position;

        }
        //isInMotion = false;
        //isInMotion = true;
        //end = transform.position;
        //start = player.transform.position;
        //Vector3.Lerp(player.transform.position,transform.localPosition,Time.time/time);
        //Vector3 direction = Random.onUnitSphere;
        //direction.y = Mathf.Clamp(direction.y, 0.5f, 1f);
        //float distance = 2 * Random.value + 1.5f;
        //transform.localPosition = direction * distance;
    }

    void Update()
    {
        //if (isInMotion)
        //{
        //    start = player.transform.position;
        //    player.transform.position = Vector3.MoveTowards(start, end, speed * Time.deltaTime);
        //    if (player.transform.position == end)
        //    {
        //        isInMotion = false;
        //    }
        //}
    }

    void OnTriggerEnter(Collider other)
    {
        //isInMotion = false;
    }
}
