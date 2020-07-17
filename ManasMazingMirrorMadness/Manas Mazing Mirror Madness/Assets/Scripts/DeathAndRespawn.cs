using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAndRespawn : MonoBehaviour
{
    public Transform respawnPoint;


    public void Kill(GameObject player)
    {
        player.transform.position = respawnPoint.position;
    }
}
