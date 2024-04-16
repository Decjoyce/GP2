using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AntPuzzleGame : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;

    [SerializeField] GameObject breadcrumb;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                agent.destination = hit.point;
                Instantiate(breadcrumb, hit.point + Vector3.up * 2, Random.rotation);
            }
        }
    }
}
