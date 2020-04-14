using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
///<summary> 
/// This class is used to trigger an audio sound whenever the object collides with something.
///</summary> 
public class CollisionAudio : MonoBehaviour
{
    [Tooltip("Audio OneShot to be played durring a collision event.")] [SerializeField] private AudioClip collisionAudio = null;


    private AudioSource audioSource;
    private Rigidbody rigidBody;
    private float startTime;


    void Start()
    {
        try
        {
            rigidBody = GetComponent<Rigidbody>();
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
        float timeSinceStart = Time.time - startTime;
        if (timeSinceStart > 2) {
            audioSource.mute = false;
        }
        bool audioReady = collisionAudio != null && audioSource != null;
        if (audioReady)
        {
            audioSource.PlayOneShot(collisionAudio);
        }
        else
        {
            Debug.LogError("GameObject Missing AudioSource or AudioClip.");
        }
    }
}
