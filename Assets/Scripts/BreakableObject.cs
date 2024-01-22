using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] GameObject gibbedObject;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(gibbedObject, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
