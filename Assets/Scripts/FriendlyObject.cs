using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyObject : MonoBehaviour
{
  Rigidbody rigidBody;
  AudioSource audioSource;
  [SerializeField] AudioClip friendlySound;
  [SerializeField] ParticleSystem FriendlyParticles;

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
       Friendly();
       print("Friendly");
    }
   // void OnTriggerEnter(Collider other) 
    //{
     //   Destroy(other.gameObject);
      //  Friendly();
    //}
void Friendly()
  {
    audioSource.Stop();
    audioSource.PlayOneShot(friendlySound);
    FriendlyParticles.Play();
  }


}
