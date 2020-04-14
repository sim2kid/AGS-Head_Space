using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{
    [Tooltip("Audio OneShot to be played durring a collision event.")] [SerializeField] private AudioClip collisionAudio = null;

    private AudioSource audioSource;
    private Rigidbody rb;

    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            rb = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }
        catch (Exception e)
        {
            Debug.LogError("There is not Rigidbody and/or AudioSource component on the game object.\n" + e);
        }
        audioSource.mute = true;
        startTime = Time.time;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (Time.time - startTime > 2) {
            audioSource.mute = false;
        }
        if (collisionAudio != null && audioSource != null)
        {
            audioSource.PlayOneShot(collisionAudio);
        }
        else
        {
            Debug.LogError("GameObject Missing AudioSource or AudioClip.");
        }
    }
}
