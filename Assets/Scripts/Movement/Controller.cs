using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Controller : MonoBehaviour
{
    public bool canRun;
    public float speed;
    public float runSpeed;
    [HideInInspector] float tspeed;
    public float stepInterval;
    public float stepIntervalRun;
    [HideInInspector] float tinterval;

    [HideInInspector] public AudioClip[] footstepSounds;
    public AudioClip[] walkSounds;
    public AudioClip[] runSounds;
    private bool isMoving;
    private bool shouldPlay;

    private void Awake()
    {
        shouldPlay = true;
        tspeed = speed;
        tinterval = stepInterval;
        footstepSounds = walkSounds;
    }
    void Update()
    {
        if(PlayerBoxCollider.speed != 0)
            speed = PlayerBoxCollider.speed;
        if(PlayerBoxCollider.interval != 0)
        {
            stepInterval = PlayerBoxCollider.interval;
        }
        if (canRun)
        {
            tspeed = Input.GetButton("Run") ? runSpeed : speed;
            tinterval = Input.GetButton("Run") ? stepIntervalRun : stepInterval;
            footstepSounds = Input.GetButton("Run") ? runSounds : walkSounds;
        }
        var movement = Input.GetAxis("Horizontal");
        transform.position += tspeed * Time.deltaTime * new Vector3(movement, 0, 0);

        if (!Mathf.Approximately(0, movement))
        {
            transform.rotation = movement > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;
            isMoving = true;
        }
        else isMoving = false;

        //PlayFootStepAudio();

        //Debug.Log(shouldPlay);
        if (isMoving && shouldPlay)
        {
            StartCoroutine(StepWait());
        }
    }

    IEnumerator StepWait()
    {
        PlayFootStepAudio();
        yield return new WaitForSeconds(tinterval);
        shouldPlay = true; 
    }
    private void PlayFootStepAudio()
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, footstepSounds.Length);
        Sound footstepSound = Array.Find(AudioManager.Instance.sounds, sound => sound.name == "Footstep");
        footstepSound.clip = footstepSounds[n];
        footstepSound.source.PlayOneShot(footstepSound.clip);
        // move picked sound to index 0 so it's not picked next time
        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = footstepSound.clip;
        shouldPlay = false;
    }
}
