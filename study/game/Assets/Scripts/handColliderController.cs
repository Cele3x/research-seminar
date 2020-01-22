using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handColliderController : MonoBehaviour
{
    public AudioClip slapSound;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.other.gameObject.tag == "Wasp")
        {
            PlaySlapSound();
        }
    }
    
    public void PlaySlapSound()
    {
        _audioSource.PlayOneShot(slapSound, 1.0F);
    }
}
