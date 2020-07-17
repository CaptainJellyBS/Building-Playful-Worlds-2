using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingMirror : Mirror
{
    public Transform target;
    public float rotSpeed;
    float speed;

    public void Start()
    {
        speed = 0;
    }

    public void Update()
    {
        target.Rotate(Vector3.forward, speed * Time.deltaTime);
    }

    public override void ActivateMirrorEffect(GameObject player)
    {
        speed = rotSpeed;
    }

    public override void DeactivateMirrorEffect(GameObject player)
    {
        speed = 0;
    }
}