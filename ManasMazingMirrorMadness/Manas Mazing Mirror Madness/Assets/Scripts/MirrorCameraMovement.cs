using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorCameraMovement : MonoBehaviour
{
    public Transform mirror;
    public Transform playerCamera;
    public bool horizontal = false;
    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void OnPreCull()
    {
        cam.ResetWorldToCameraMatrix();
        cam.ResetProjectionMatrix();
        cam.projectionMatrix = cam.projectionMatrix * Matrix4x4.Scale(new Vector3(-1, 1, 1));
    }

    // Set it to true so we can watch the flipped Objects
    void OnPreRender()
    {
        GL.invertCulling = true;
    }

    // Set it to false again because we dont want to affect all other cammeras.
    void OnPostRender()
    {
        GL.invertCulling = false;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 depth = mirror.transform.forward;
        Vector3 sideways = mirror.transform.right;
        Vector3 vert = mirror.transform.up;

        Vector3 offset = playerCamera.position - mirror.position;
        Vector3 posOff = mirror.position - offset;

        Vector3 reflectionBase = Vector3.Reflect(Vector3.Reflect(offset, depth), sideways);
        //Vector3 reflectionBase = Vector3.Reflect(offset, vert);

        if (!horizontal)
        {
            transform.position = mirror.position - reflectionBase;
            Quaternion temp = Quaternion.LookRotation(Vector3.Reflect(playerCamera.forward, mirror.up), Vector3.up);
            transform.rotation = Quaternion.Euler(playerCamera.rotation.eulerAngles.x, temp.eulerAngles.y , playerCamera.rotation.eulerAngles.z);
        }
        else
        {
            transform.position = new Vector3(playerCamera.position.x, posOff.y, playerCamera.position.z);
            transform.rotation = Quaternion.Euler(playerCamera.rotation.eulerAngles.x + 180, playerCamera.rotation.eulerAngles.y, playerCamera.rotation.eulerAngles.z);
        }

        Camera camera = GetComponent<Camera>();
    }

}
