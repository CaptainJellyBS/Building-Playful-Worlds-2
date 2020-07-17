using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorHandler : MonoBehaviour
{
    public GameObject[] mirrors;
    public GameObject player;
    public GameObject curMirror;
    bool hasActivated = false;
    
    void Start()
    {
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject lookingAtMirrors = PlayerLookingAtMirrors();
        if(lookingAtMirrors && !hasActivated)
        {
            lookingAtMirrors.GetComponent<Mirror>().ActivateMirrorEffect(player);
            hasActivated = true;
            curMirror = lookingAtMirrors;
            //TeleportPlayer();
        }
    }
    public static bool IsVisibleFrom(Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    /// <summary>
    /// Returns true if a player is currently looking at a mirror, returns false otherwise.
    /// </summary>
    /// <returns></returns>
    public GameObject PlayerLookingAtMirrors()
    {

        foreach (GameObject go in mirrors)
        {
            Renderer renderer = go.GetComponent<Renderer>();
            bool vis = IsVisibleFrom(renderer, Camera.main);

            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(new Ray(go.transform.position, Camera.main.transform.position - go.transform.position), out hit))
            {
                Debug.DrawRay(go.transform.position, hit.point);
                if (hit.collider.gameObject.CompareTag("Player") && vis)
                {
                    return go;
                }
            }

        }
        if(curMirror)
        {
            curMirror.GetComponent<Mirror>().DeactivateMirrorEffect(player);
            curMirror = null;
        }
        hasActivated = false;
        return null;
    }
}
