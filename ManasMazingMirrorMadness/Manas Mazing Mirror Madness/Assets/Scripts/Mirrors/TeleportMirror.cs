using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportMirror : Mirror
{
    public Transform targetMirror;
    public override void ActivateMirrorEffect(GameObject player)
    {
            player.transform.position += (targetMirror.transform.position -transform.position);
    }
}
