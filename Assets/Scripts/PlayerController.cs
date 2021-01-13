using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : DinosaurAbstract
{

    Vector3 rot = Vector3.zero;
    float rotSpeed = 80f;
    float movementSpeed = 10f;
    Vector3 size = new Vector3(0.2f, 0.2f, 0.2f);
    public Camera mycam;
    private Rigidbody body;


    public override void runTo(Vector3 position)
    {
    }


    // Start is called before the first frame update
    void Start()
    {

        this.anim = gameObject.GetComponent<Animator>();
        gameObject.transform.eulerAngles = rot;
        gameObject.transform.localScale = size;
        body = gameObject.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        CheckKey();
        gameObject.transform.eulerAngles = rot;
        transform.LookAt(mycam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y*2, Input.mousePosition.x)));


    }

    void CheckKey()
    {
        // Walk
        if (this.anim.IsInTransition(0))
        {
            this.anim.Play("Base Layer.Idle");
        }
        if (Input.GetKey(KeyCode.Z))
        {
  
            this.anim.Play("Base Layer.Run");
             body.MovePosition(transform.position + transform.forward * Time.deltaTime * movementSpeed);
            //body.transform.position += transform.forward * Time.deltaTime * movementSpeed;

        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            this.anim.Play("Base Layer.Idle");
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
      
            this.anim.Play("Base Layer.Walk");
            body.MovePosition(transform.position - transform.forward * Time.deltaTime * movementSpeed/2);

        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            this.anim.Play("Base Layer.Idle");
        }
        if (Input.GetKey(KeyCode.Space))
        {
    
            if (this.anim.IsInTransition(0) || (this.anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")))
            {
                this.attack();
            }
            
        }
        if (Input.GetKey(KeyCode.O))
        {
            //test changement de taille
            gameObject.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);

        }
    }

    void OnTriggerEnter(Collider other){
        if(this.is_attacking && Array.IndexOf(this.prey, other.gameObject) > -1){
            this.is_attacking = false;
            other.gameObject.GetComponent<DinosaurAbstract>().increaseHealth(-0.1f);
            if(other.gameObject.GetComponent<DinosaurAbstract>().getHealth() <= 0){
                this.growUp(0.05f);
                this.increaseHealth(0.5f);
            }
        }
        
    }


}
