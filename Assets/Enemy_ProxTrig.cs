using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_ProxTrig : MonoBehaviour
{
    EnemyController controller;

    private void Start()
    {
        controller = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player1") || other.CompareTag("Player2"))
        {
            controller.PlayerClose(other.transform);
        }
    }
}
