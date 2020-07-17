using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{

    public Camera[] cameras;

    public GameObject[] mirrors;

    // Use this for initialization
    void Start()
    {
        if(cameras.Length != mirrors.Length) { throw new System.ArgumentException("Amount of cameras and amount of camera materials must be the same."); }
        for (int i = 0; i < cameras.Length; i++)
        {
            SetupCamera(cameras[i], mirrors[i].GetComponent<Renderer>().material);
        }
    }

    void SetupCamera(Camera cam, Material mat)
    {
        if (cam.targetTexture != null)
        {
            cam.targetTexture.Release();
        }
        cam.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        mat.mainTexture = cam.targetTexture;
    }
}
