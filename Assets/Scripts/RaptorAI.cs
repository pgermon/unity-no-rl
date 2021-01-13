using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaptorAI : DinosaurAbstract
{
    
    //public GameObject threat;
    NavMeshAgent agent;
    Vector3 lastPos;
    protected ParticleSystem[] blood;

    public override void runTo(Vector3 position) 
    {
        /*Vector3 dest = threat.GetComponent<Collider>().ClosestPoint(agent.transform.position);
        agent.SetDestination(dest);*/
    }

    public void runFrom(GameObject predator)
    {
        if(agent.enabled){
            Vector3 dirToThreat = transform.position - predator.transform.position;
            Vector3 newPos = transform.position + dirToThreat;
            agent.SetDestination(newPos);
        }
    }

    public void chasePrey(GameObject prey){
        if(agent.enabled){
            agent.SetDestination(prey.transform.position);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("ok");
        this.anim = GetComponent<Animator>();
        this.anim.Play("Base Layer.Idle");
        agent = GetComponent<NavMeshAgent>();
        lastPos = transform.position;
        blood = GetComponentsInChildren<ParticleSystem>();
        foreach (var stain in blood)
        {
            stain.Stop();
        }
      
    }

    // Update is called once per frame
    protected override void Update()
    {
        
        base.Update();

        this.speed *= this.health;
        if(blood.Length == 6){
            if (this.health<0.8)
            {
                blood[0].Play();
                blood[1].Play();
            }
            if (this.health < 0.5)
            {
                blood[2].Play();
                blood[3].Play();
            }
            if (this.health < 0.4)
            {
                blood[4].Play();
                blood[5].Play();
            }
        }


        if(!this.is_attacking){
            if (Vector3.Distance(agent.velocity, new Vector3(0f, 0f, 0f)) < 0.1){
                this.anim.Play("Base Layer.Idle");
            }
            else{
                this.anim.Play("Base Layer.Run");
            }
        }


        /* Run from predators */
        bool run = false;
        foreach(GameObject predator in this.predators){
            if (Vector3.Distance(this.transform.position, predator.transform.position) < 25f){
                //Debug.Log("Running from predator");
                run = true;
                runFrom(predator);
            }
        }

        /* Chase prey */
        bool chase = false;
        foreach (GameObject prey in this.preys){
            if(Vector3.Distance(this.transform.position, prey.transform.position) < 10f){
                Debug.Log("Attacking prey / is_attacking = " + this.is_attacking);
                chase = true;
                if(!this.is_attacking){
                    attack();
                }
                chasePrey(prey);
            }
            else if(Vector3.Distance(this.transform.position, prey.transform.position) < 25f){
                Debug.Log("Chasing prey");
                chase = true;
                chasePrey(prey);
            }
        }
        
        if(!run && !chase){
            float rd = Random.value;
            if (agent.enabled && rd < 0.02) 
            {
                //Debug.Log("random direction");
                Vector3 targetDir = Quaternion.AngleAxis(Random.Range(-30.0f, 30.0f), transform.up) * transform.forward;
                targetDir = transform.position + targetDir.normalized * 25;
                agent.SetDestination(targetDir);
            }
        }
        lastPos = transform.position;
    }
}
