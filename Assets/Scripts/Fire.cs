using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] CampFire campFire;

    private void OnTriggerEnter(Collider other)
    {
        if (campFire.currentHealth > 0 && other.CompareTag("Interactable") && other.GetComponent<Interaction_Pickup>().displayName == "Stick")
        {
            campFire.IncreaseHealth(50);
            Destroy(other.gameObject);
        }
    }
}
