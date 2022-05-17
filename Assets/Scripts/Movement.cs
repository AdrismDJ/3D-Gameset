using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
      // PARAMETERS - for tuning, typically set in the editor 
      // CACHE - e.g. references for readability or speed 
      // STATE - private instance (member) variables 

     [SerializeField] float mainThrust = 2000f;
     [SerializeField] float rotationThrust = 500f;
     [SerializeField] AudioClip mainEngine; 

     [SerializeField] ParticleSystem mainEngineParticles; 
     [SerializeField] ParticleSystem leftThrusterParticles; 
     [SerializeField] ParticleSystem rightThrusterParticles; 

     Rigidbody rocketrigidbody; 
     AudioSource audioSource; 

     bool isAlive; 

    // Start is called before the first frame update
    void Start()
    {
        rocketrigidbody = GetComponent<Rigidbody>(); 
        audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust(); 
        ProcessRotation(); 
    }

    void ProcessThrust()
    {
       if (Input.GetKey(KeyCode.Space)) 
       {
          StartThrusting(); 
       } 
       else 
       {  
          StopThrusting(); 
       }
    }

    void ProcessRotation() 
    {
       if (Input.GetKey(KeyCode.A)) 
       {
         RotateLeft(); 
       }
       else if (Input.GetKey(KeyCode.D)) 
       {
          RotateRight(); 
       } 
       else if (Input.GetKey(KeyCode.W)) 
       { 
          RotateForward(); 
       } 
       else if (Input.GetKey(KeyCode.S)) 
       {
          RotateBackwards(); 
       }  
       else 
       {
          StopRotating(); 
       }
    }

    void StartThrusting()
    {
      rocketrigidbody.AddRelativeForce( 0, 1 * mainThrust * Time.deltaTime, .5f * mainThrust * Time.deltaTime); 

      if (!audioSource.isPlaying)
      {
        audioSource.PlayOneShot(mainEngine);
      } 
      if (!mainEngineParticles.isPlaying)
      {
        mainEngineParticles.Play();
      } 
    }

    void StopThrusting() 
    {
      audioSource.Stop();
      mainEngineParticles.Stop(); 
    }

    void RotateLeft() 
    {
        ApplyRotationLeftRight(rotationThrust); 
         if (!rightThrusterParticles.isPlaying)
         {
            rightThrusterParticles.Play();
         } 
    }

    void RotateRight()
    {
         ApplyRotationLeftRight(-rotationThrust);
         if (!leftThrusterParticles.isPlaying)
         {
            leftThrusterParticles.Play();
         }  
    }

    void RotateForward() 
    {
        ApplyRotationUpDown(rotationThrust); 
         if (!rightThrusterParticles.isPlaying)
         {
            rightThrusterParticles.Play();
         } 
    }

    void RotateBackwards() 
    {
        ApplyRotationUpDown(-rotationThrust); 
         if (!rightThrusterParticles.isPlaying)
         {
            rightThrusterParticles.Play();
         } 
    }


    void StopRotating() {
          leftThrusterParticles.Stop();
          rightThrusterParticles.Stop();
    }

     void ApplyRotationLeftRight(float rotationThisFrame)
     {
         rocketrigidbody.freezeRotation = true; // freezing rotation so we can manually rotate. 
         transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); 
         rocketrigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over. 
     }

     void ApplyRotationUpDown(float rotationThisFrame)
     {
         rocketrigidbody.freezeRotation = true; // freezing rotation so we can manually rotate. 
         transform.Rotate(Vector3.right * rotationThisFrame * Time.deltaTime); 
         rocketrigidbody.freezeRotation = false; // unfreezing rotation so the physics system can take over. 
     }

}
