using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerBB : DinosaurBB
{

    Vector3 rot = Vector3.zero;
    float rotSpeed = 80f;
    float movementSpeed = 10f;
    Vector3 size = new Vector3(0.3f, 0.3f, 0.3f);
    public Camera cam;
    private Rigidbody body;
    public MouseLook mouseLook = new MouseLook();
    protected float boostTime = 5f;
    protected float coolDown = 20f;
    protected float timeStamp, timeStamp2 = 0f;
    private new SkinnedMeshRenderer renderer;
    public Material MaterialBoost, MaterialN;
    private bool boosting = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        this.is_player = true;
        mouseLook.Init (this.transform, cam.transform);
        gameObject.transform.eulerAngles = rot;
        gameObject.transform.localScale = size;
        body = gameObject.GetComponent<Rigidbody>();
        renderer = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        
    }
 
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        RotateView();
        CheckKey();

        this.gameObject.transform.eulerAngles = rot;

        if (boosting && Time.time >= timeStamp - coolDown + boostTime  )
        {
            //Debug.Log(timeStamp + "  "  +  Time.time);
            this.movementSpeed /= 2;
            Material[] mats = renderer.materials;
            mats[2] = MaterialN;
            renderer.materials = mats;
            boosting = true;
            boosting = false;
        }

        //transform.LookAt(cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y*2, Input.mousePosition.x)));
    }

    private void RotateView()
    {
        //avoids the mouse looking if the game is effectively paused
        if (Mathf.Abs(Time.timeScale) < float.Epsilon) return;

        // get the rotation before it's changed
        float oldYRotation = transform.eulerAngles.y;

        mouseLook.LookRotation (transform, cam.transform);

        /*if (m_IsGrounded || advancedSettings.airControl)
        {*/
            // Rotate the rigidbody velocity to match the new direction that the character is looking
            Quaternion velRotation = Quaternion.AngleAxis(transform.eulerAngles.y - oldYRotation, Vector3.up);
            body.velocity = velRotation*body.velocity;
        //}
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
                this.attack(null);
            }
            
        }
        if (Input.GetKey(KeyCode.E))
        {
            
            //Debug.Log(timeStamp +" " + Time.time);
            if (timeStamp <= Time.time)
            {
                this.movementSpeed *= 2f;
                timeStamp = Time.time + coolDown;
                Material[] mats = renderer.materials;
                mats[2] = MaterialBoost;
                renderer.materials = mats;
                boosting = true;

            }
            //test changement de taille
            //gameObject.transform.localScale += new Vector3(0.001f, 0.001f, 0.001f);
            

        }
    }
}
