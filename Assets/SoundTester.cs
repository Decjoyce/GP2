using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] float vol;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            EnemyController.instance.SoundHeard(transform.position, source.volume);
        }
    }
}
