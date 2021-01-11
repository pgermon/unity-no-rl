using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{


    private void OnTriggerEnter(Collider coll)
    {
        //Debug.Log(coll.gameObject.name);
        if (coll.gameObject.tag != "Ground" && coll.gameObject.name != "Player-TRex")
        {
            Destroy(this.gameObject);
        }

     

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
