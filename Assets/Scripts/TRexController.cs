﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRexController : DinosaurAbstract
{


    public override void runTo(Vector3 position)
    {
    }

    protected override void Start()
    {   
        base.Start();
        anim = GetComponent<Animator>();
    }


    protected override void Update()
    {
        base.Update();
    }
}
