using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Animator anim;
    Vector3 rot = Vector3.zero;
    float rotSpeed = 80f;
    float movementSpeed = 10f;
    Vector3 size = new Vector3(0.2f, 0.2f, 0.2f);
    public Camera mycam;
    private Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {

        anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
        gameObject.transform.localScale = size;
        body = gameObject.GetComponent<Rigidbody>();

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
        if (anim.IsInTransition(0))
        {
            anim.Play("Idle");
        }
        if (Input.GetKey(KeyCode.Z))
        {
  
            anim.Play("Run");
             body.MovePosition(transform.position + transform.forward * Time.deltaTime * movementSpeed);
            //body.transform.position += transform.forward * Time.deltaTime * movementSpeed;

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
            body.MovePosition(transform.position - transform.forward * Time.deltaTime * movementSpeed/2);

        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            anim.Play("Idle");
        }
        if (Input.GetKey(KeyCode.Space))
        {
    
            if (anim.IsInTransition(0) || (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                anim.Play("Attack");
            }
            
        }
        if (Input.GetKey(KeyCode.O))
        {
            //test changement de taille
            gameObject.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);

        }


    }


}
