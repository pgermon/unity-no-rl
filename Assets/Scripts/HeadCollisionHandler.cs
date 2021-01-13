using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionHandler : MonoBehaviour
{
    [SerializeField]
    private DinosaurAbstract dino;

    void OnTriggerEnter(Collider other){
        dino.OnHeadCollision(other);
    }
}
