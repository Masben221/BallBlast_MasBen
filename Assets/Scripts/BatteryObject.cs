using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryObject : MonoBehaviour
{
  Rigidbody rigidBody;
  AudioSource audioSource;
  [SerializeField] AudioClip batterySound;
  [SerializeField] ParticleSystem BatteryParticles;

    // Start is called before the first frame update
    void Start()
    {
       audioSource=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
void OnCollisionEnter (Collision other) 
    { 
       Battery();
       print("Battery!");
    }
  
void Battery()
  {
    audioSource.Stop();
    audioSource.PlayOneShot(batterySound);
    BatteryParticles.Play();
  }


}
