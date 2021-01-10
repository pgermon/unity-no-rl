using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator anim;
    Vector3 rot = Vector3.zero;
    float rotSpeed = 40f;
    float movementSpeed = 10f;
    public Camera mycam;

    // Start is called before the first frame update
    void Start()
    {

        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
        

    }

    // Update is called once per frame
    void Update()
    {
        CheckKey();
        gameObject.transform.eulerAngles = rot;
        transform.LookAt(mycam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y*2, Input.mousePosition.x)));


    }

    void CheckKey()
    {
        // Walk
        if (Input.GetKey(KeyCode.Z))
        {
  
            anim.Play("Run");
            this.transform.position += transform.forward * Time.deltaTime * movementSpeed;

        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            anim.Play("Idle");
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rot[1] -= rotSpeed * Time.fixedDeltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            rot[1] += rotSpeed * Time.fixedDeltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
      
            anim.Play("Walk");
            this.transform.position -= transform.forward * Time.deltaTime * movementSpeed/2;

        }
        if (Input.GetKey(KeyCode.Space))
        {
            anim.Play("Attack");
        }

    }


}
