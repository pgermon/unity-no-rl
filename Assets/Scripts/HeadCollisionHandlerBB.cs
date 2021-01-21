using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionHandlerBB : MonoBehaviour
{
    [SerializeField]
    private DinosaurBB dino;

    void OnTriggerEnter(Collider other){
        dino.OnHeadCollision(other);
    }
}
