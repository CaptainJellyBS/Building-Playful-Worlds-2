using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMirror : Mirror
{
    public GameObject[] redObjects;
    public GameObject[] blueObjects;

    public bool toggle = true;

    public void Start()
    {
        foreach (GameObject go in redObjects)
        {
            go.SetActive(toggle);
        }
        foreach (GameObject go in blueObjects)
        {
            go.SetActive(!toggle);
        }
    }
    public override void ActivateMirrorEffect(GameObject player)
    {
        toggle = !toggle;
        foreach (GameObject go in redObjects)
        {
            //If the player is further away from the mirror than the object, do not toggle
            if (Vector3.Distance(go.transform.position, transform.position) < Vector3.Distance(player.transform.position, transform.position))
            { continue; }

            go.SetActive(toggle);
        }
        foreach(GameObject go in blueObjects)
        {
            //If the player is further away from the mirror than the object, do not toggle

            if (Vector3.Distance(go.transform.position, transform.position) < Vector3.Distance(player.transform.position, transform.position))
            { continue; }

            go.SetActive(!toggle);
        }
    }
}