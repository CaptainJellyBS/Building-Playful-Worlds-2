using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShatterMirror : Mirror
{
    public GameObject[] objectsToBreak;
    public GameObject[] shards;
    bool activated = false;


    public override void ActivateMirrorEffect(GameObject player)
    {
        if(!gameObject.activeInHierarchy) { return; }
        StartCoroutine(Countdown());
    }

    /// <summary>
    /// Coroutine that makes it so the player has to at least kinda look in the mirror instead of it instantly breaking
    /// </summary>
    /// <returns></returns>
    IEnumerator Countdown()
    {
        activated = true;
        yield return new WaitForSeconds(0.5f);
        if (!activated) { yield break; }
        foreach (GameObject go in objectsToBreak)
        {
                go.SetActive(false);
        }

        foreach(GameObject go in shards)
        {
            go.SetActive(true);
        }
        gameObject.SetActive(false);
    }
    public override void DeactivateMirrorEffect(GameObject player)
    {
        activated = false;
    }
}