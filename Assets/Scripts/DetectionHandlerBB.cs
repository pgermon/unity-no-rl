using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionHandlerBB : MonoBehaviour
{
    [SerializeField]
    private DinosaurBB dino;

    void OnTriggerEnter(Collider other){
        dino.OnDetection(other);
    }
}
