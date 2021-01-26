using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandlerBB : MonoBehaviour
{
    [SerializeField]
    private DinosaurBB dino;

    void OnTriggerEnter(Collider other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Dino") 
            || other.gameObject.layer == LayerMask.NameToLayer("DinoPlayer")){
            dino.OnEnterDetection(other);
        }
    }

    void OnTriggerExit(Collider other){
        if(other.gameObject.layer == 12){
            dino.OnExitDetection(other);
        }
    }
}
