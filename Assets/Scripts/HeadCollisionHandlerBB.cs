using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionHandlerBB : MonoBehaviour
{
    [SerializeField]
    private DinosaurBB dino;
    public Rigidbody body;


    void OnTriggerEnter(Collider other){
        if (other.gameObject.layer == LayerMask.NameToLayer("Dino"))
        {
            dino.OnHeadCollision(other);
            body.isKinematic = true;
            //Debug.Log("Touched DINO");
        }
    

    }
    void OnTriggerExit(Collider other)
    {

        body.isKinematic = false;
    }
}
