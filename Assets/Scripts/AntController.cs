using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class AntController : MonoBehaviour
{
    [SerializeField] GameObject text;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
            Destroy(other.gameObject);

        if (other.CompareTag("Obstacle"))
            text.SetActive(true);
    }
}
