using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexController : DinosaurAbstract
{

    public override void die()
    {
        anim.Play("Die");
        Destroy(this.gameObject, 2.0f);
        enabled = false;
    }

    public override void runTo(Vector3 position)
    {
    }


    //Calls growUp is the target dies
    public override void attack()
    {
    }

    public override void growUp()
    {
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    protected override void Update()
    {
        base.Update();
    }
}
