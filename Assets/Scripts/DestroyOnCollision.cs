using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        /*if(other.gameObject.layer == LayerMask.NameToLayer("Dino")){
            Debug.Log("Destroy Dino !!!!!!!!!!!!!!!!!!!!!!!!!!");
            Debug.Log("dino position y = " + other.gameObject.transform.position.y);
        }*/
        Destroy(other.gameObject);
    }
}
