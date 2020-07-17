using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openAngle;
    public float closedAngle;

    public void OpenDoor()
    {
        transform.rotation = Quaternion.AngleAxis(openAngle, Vector3.up);
    }

    public void CloseDoor()
    {
        transform.rotation = Quaternion.AngleAxis(closedAngle, Vector3.up);
    }
}
