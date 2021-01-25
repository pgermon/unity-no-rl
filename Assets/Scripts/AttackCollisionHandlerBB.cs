using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollisionHandlerBB : MonoBehaviour
{
    [SerializeField]
    private DinosaurBB dino;
    public Rigidbody body;

    void OnTriggerEnter(Collider other){
        if (dino.getIsAttacking() && other.gameObject.layer == LayerMask.NameToLayer("Dino"))
        {
            dino.OnSuccessfulAttack(other);
        }
    

    }
}
