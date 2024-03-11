using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    Rigidbody rb;

    AudioSource source;
    [SerializeField] AudioClip[] soundEffects;

    [SerializeField] bool alertEnemy;

    bool allowSounds;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameManager.instance.allowSounds)
        {
            source.PlayOneShot(soundEffects[0]);
            if(alertEnemy)
                EnemyController.instance.SoundHeard(transform.position, source.volume);
        }
    }
}
