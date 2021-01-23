using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Transform cam;

    void Start(){
        cam = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.LookAt(this.transform.position + cam.forward);
    }
}
