using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DinosaurAbstract : MonoBehaviour
{
    /* Dinosaur properties */
    [SerializeField]
    protected float health = 1.0f;
    protected float speed;
    protected Animator anim;
    protected UnityEngine.AI.NavMeshAgent agent;

    /* Anim states */
    protected bool is_attacking = false;

    [SerializeField]
    protected List<string> predators_names;
    protected List<GameObject> predators;

    [SerializeField]
    protected List<string> preys_names;
    protected List<GameObject> preys;

    /* Dinosaur methods */
    public abstract void runTo(Vector3 position);

    public virtual void growUp(float g){
        this.gameObject.transform.localScale += new Vector3(g, g, g);
    }

    public virtual void attack(){
        this.anim.Play("Base Layer.Attack");
    }

    public virtual void die(){
        this.anim.Play("Base Layer.Die");
        Destroy(this.gameObject, 3.0f);
        enabled = false;
    }

    public virtual void runFrom(GameObject predator)
    {
        if(agent.enabled){
            Vector3 dirToThreat = transform.position - predator.transform.position;
            Vector3 newPos = transform.position + dirToThreat;
            agent.SetDestination(newPos);
        }
    }

    public virtual void chasePrey(GameObject prey){
        if(agent.enabled){
            agent.SetDestination(prey.transform.position);
        }
    }

    protected virtual void Start(){
        this.predators = new List<GameObject>();
        this.preys = new List<GameObject>();
    }

    protected virtual void Update(){
        this.health -= 0.0001f;

        if(this.health <= 0){
            this.die();
        }

        if(!this.is_attacking && this.anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack")){
            this.is_attacking = true;
        }

        if(this.is_attacking && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack")){
            this.is_attacking = false;
        }
    }


    // Called by the DetectionHandler script when the detection collider is triggered
    public void OnDetection(Collider other){

        if(!this.predators.Contains(other.gameObject)){

            // if other's name is in the list of predators it is added to the predators
            if(this.predators_names.Contains(other.gameObject.name.Split(' ')[0])){
                this.predators.Add(other.gameObject);
                Debug.Log(other.gameObject.name.Split(' ')[0] + " added to predators of " + this.gameObject.name);
            }

            // if other's name is in the list of preys it is added to the preys
            else if(this.preys_names.Contains(other.gameObject.name.Split(' ')[0])){
                this.preys.Add(other.gameObject);
                Debug.Log(other.gameObject.name.Split(' ')[0] + " added to preys of " + this.gameObject.name);
            }
        }
    }

    // Called by the HeadCollisionHandler script when the head collider is triggered
    public void OnHeadCollision(Collider other){

        // if the dino is currently attacking and other is part of its prey array, the dino deals damage to the other
        if(this.is_attacking && this.preys.Contains(other.gameObject)){
            this.is_attacking = false;
            other.gameObject.GetComponent<DinosaurAbstract>().decreaseHealth(0.1f, this.gameObject);

            // if the dino kills other, it grows up and regens its health
            if(other.gameObject.GetComponent<DinosaurAbstract>().getHealth() <= 0){
                this.preys.Remove(other.gameObject);
                this.growUp(0.05f);
                this.increaseHealth(0.5f);
            }
        }
    }


    /* GETTERS AND SETTERS */

    // Health
    public virtual float getHealth(){
        return this.health;
    }

    public virtual void setHealth(float h){
        this.health = h;
    }

    public virtual void increaseHealth(float i){
        this.health += i;
        if(this.health > 1f){
            this.health = 1f;
        }
    }

    public virtual void decreaseHealth(float i, GameObject enemy){
        this.health -= i;
        this.runFrom(enemy);
    }

    // Speed
    public virtual float getSpeed(){
        return this.speed;
    }
    
    public virtual void setSpeed(float new_speed){
        this.speed = new_speed;
        this.anim.speed = new_speed;
    }
}
