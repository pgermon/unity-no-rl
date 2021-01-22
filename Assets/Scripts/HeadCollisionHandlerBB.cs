using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionHandlerBB : MonoBehaviour
{
    [SerializeField]
    private DinosaurBB dino;
    public Rigidbody body;

    //private Collider dino_attacked;


    void OnTriggerEnter(Collider other){
        if (/*dino_attacked == null &&*/ dino.getIsAttacking() && other.gameObject.layer == LayerMask.NameToLayer("Dino"))
        {
            //dino_attacked = other;
            dino.OnSuccessfulAttack(other);
            body.isKinematic = true;
            //Debug.Log("Touched DINO");
        }
    

    }
    void OnTriggerExit(Collider other)
    {
        /*if(dino_attacked == other){
            dino_attacked = null;
        }*/
        
        body.isKinematic = false;
    }
}
