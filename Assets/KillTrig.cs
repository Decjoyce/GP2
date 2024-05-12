using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTrig : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            other.GetComponent<PlayerStats>().Die();
        }
    }
}
