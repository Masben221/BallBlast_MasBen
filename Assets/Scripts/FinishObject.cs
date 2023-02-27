using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishObject : MonoBehaviour
{
  AudioSource audioSource;
  [SerializeField] AudioClip finishlySound;
  void Start()
    {
      audioSource=GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
void OnCollisionEnter (Collision collision) 
    { 
      Finish (collision.gameObject);
    }
   
void Finish(GameObject LandingPadObj)
  {
    if(audioSource.isPlaying==false)
     {
      audioSource.PlayOneShot(finishlySound);
     }
    LandingPadObj.GetComponent<BoxCollider>().enabled = false;
  }
}