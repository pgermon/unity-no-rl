﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaptorEscapingAI : DinosaurAbstract
{
    
    public GameObject threat;
    NavMeshAgent agent;
    Vector3 lastPos;

    public override void runTo(Vector3 position) 
    {
        Vector3 dest = threat.GetComponent<Collider>().ClosestPoint(agent.transform.position);
        agent.SetDestination(dest);
    }

    public void runFrom()
    {
        Vector3 dirToThreat = transform.position - threat.transform.position;
        Vector3 newPos = transform.position + dirToThreat;
        agent.SetDestination(newPos);
    }

    public override void die()
    {
        this.anim.Play("Die");
        Destroy(this.gameObject, 2.0f);
        enabled = false;
    }

    //Calls growUp is the target dies
    public override void attack(){}
    public override void growUp(){}

    // Start is called before the first frame update
    void Start()
    {
        this.anim = GetComponent<Animator>();
        this.anim.Play("Idle");
        agent = GetComponent<NavMeshAgent>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Vector3.Distance(agent.velocity, new Vector3(0f,0f,0f))<0.1)
            this.anim.Play("Idle");
        else
            this.anim.Play("Run");
        if (Vector3.Distance(transform.position, threat.transform.position)<25f)
            runFrom();
        else
        {
            float rd = Random.value;
            if (rd < 0.02) 
            {
                Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-30.0f, 30.0f), transform.up) * transform.forward;
                targetDir = transform.position + targetDir.normalized * 25;
                agent.SetDestination(targetDir);
            }
        }
        lastPos = transform.position;
    }

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == threat.gameObject){
            Debug.Log("TriggerEnter die");
            this.die();
        }
    }*/
}
