using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexController : MonoBehaviour, DinosaurInterface
{
    public float speed;
    Animator anim;

    public void die()
    {
        anim.Play("Base Layer.Armature|TRex_Death");
        Destroy(this);
    }

    public void runTo(Vector3 position)
    {
        float x = 1;
    }

    public void setSpeed(float new_speed)
    {
        this.speed = new_speed;
        anim.speed = new_speed;
    }

    //Calls growUp is the target dies
    public void attack(DinosaurInterface target)
    {
        float x = 1;
    }

    public void growUp()
    {
        float size = 1;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (Input.GetButtonDown("Jump"))
            die();
    }
}
