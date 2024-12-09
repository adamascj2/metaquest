using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
 
public class Vitoriax: MonoBehaviour
{
 public AudioSource  MyAudioSource;
   
   void Start()
   {
     MyAudioSource = GetComponent<AudioSource>();
     
    }

    void OnTriggerEnter(Collider other)
    {
      if(other.gameObject.name == "robot"){
        MyAudioSource.Play();
      }
     }
}

