using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandler : MonoBehaviour
{
    [SerializeField]
    private DinosaurAbstract dino;

    void OnTriggerEnter(Collider other){
        dino.OnDetection(other);
    }
}
