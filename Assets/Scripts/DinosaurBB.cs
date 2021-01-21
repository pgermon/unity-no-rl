﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurBB : MonoBehaviour
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
    protected GameObject currentPredator = null;

    [SerializeField]
    protected List<string> preys_names;
    protected List<GameObject> preys;
    protected GameObject currentPrey = null;

    protected ParticleSystem[] blood;


    /* Dinosaur methods */
    public virtual void attack(){
        this.anim.Play("Base Layer.Attack");
        this.is_attacking = true;
    }

    public virtual void growUp(float g){
        this.gameObject.transform.localScale += new Vector3(g, g, g);
    }

    public virtual void die(){
        Debug.Log("die");
        this.anim.Play("Base Layer.Die");
        Collider[] colliders = this.gameObject.GetComponents<BoxCollider>();
        for(int i = 0; i < colliders.Length; i++){
            colliders[i].enabled = false;
        }

        // When the dino dies, it sends a message to alert its predators and preys
        foreach(GameObject predator in this.predators){
            if(predator != null){
                predator.gameObject.GetComponent<DinosaurBB>().OnMessageDie(this.gameObject);
            }
        }
        foreach(GameObject prey in this.preys){
            if(prey != null){
                prey.gameObject.GetComponent<DinosaurBB>().OnMessageDie(this.gameObject);
            }
        }

        
        Destroy(this.gameObject, 1.5f);
        enabled = false;
    }

    protected virtual void Start(){

        this.predators = new List<GameObject>();
        this.preys = new List<GameObject>();

        this.anim = this.gameObject.GetComponent<Animator>();
        if(this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>() != null){
            this.agent = this.gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        }

        blood = this.gameObject.GetComponentsInChildren<ParticleSystem>();
        foreach (var stain in blood)
        {
            stain.Stop();
        }
    }

    protected virtual void Update(){
        //this.health -= 0.0001f;

        if(this.health <= 0){
            this.die();
        }

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

        if(this.agent != null && !this.is_attacking){
            if (Vector3.Distance(agent.velocity, new Vector3(0f, 0f, 0f)) < 0.1){
                this.anim.Play("Base Layer.Idle");
            }
            else{
                this.anim.Play("Base Layer.Run");
            }
        }
    }


    // Called by the DetectionHandlerBB script when the detection collider is triggered
    public void OnDetection(Collider other){

        if(this.predators != null && this.preys != null){

            // Case a new predator is detected
            if(this.predators_names.Contains(other.gameObject.name.Split('(')[0])){

                // the detected predator is added to the list of predators if it is not already in it
                if(!this.predators.Contains(other.gameObject)){
                    this.predators.Add(other.gameObject);
                    Debug.Log(other.gameObject.name + " set as predator of " + this.gameObject.name);
                }

                // the detected predator is set as current predator if there is not already one
                if(this.currentPredator == null){
                    this.currentPredator = other.gameObject;
                }

                // if the detected predator is closer than the current one, it is set as the current predator
                else{
                    float current_dist = Vector3.Distance(this.gameObject.transform.position, this.currentPredator.gameObject.transform.position);
                    float other_dist = Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.position);
                    if(other_dist < current_dist){
                        this.currentPredator = other.gameObject;
                    }
                }
            }

            // Case a prey is detected
            else if(this.preys_names.Contains(other.gameObject.name.Split('(')[0])){

                // the detected prey is added to the list of preys if it is not already in it
                if(!this.preys.Contains(other.gameObject)){
                    this.preys.Add(other.gameObject);
                    Debug.Log(other.gameObject.name + " set as prey of " + this.gameObject.name);
                }

                // the detected prey is set as current prey if there is not already one
                if(this.currentPrey == null){
                    this.currentPrey = other.gameObject;
                }

                // if the detected prey is closer than the current one, it is set as the current prey
                else{
                    float current_dist = Vector3.Distance(this.gameObject.transform.position, this.currentPrey.gameObject.transform.position);
                    float other_dist = Vector3.Distance(this.gameObject.transform.position, other.gameObject.transform.position);
                    if(other_dist < current_dist){
                        this.currentPrey = other.gameObject;
                    }
                }
            }
        }
    }


    // Called by the HeadCollisionHandlerBB script when the head collider is triggered
    public void OnHeadCollision(Collider other){

        // if the dino is currently attacking and other is part of its list of preys, the dino deals damage to the other
        if(this.is_attacking && this.preys.Contains(other.gameObject)){
            Debug.Log(this.gameObject.name + " attacks " + other.gameObject.name);
            this.is_attacking = false;
            other.gameObject.GetComponent<DinosaurBB>().decreaseHealth(0.1f, this.gameObject);

            // if the dino kills other, it grows up and regens its health
            if(other.gameObject.GetComponent<DinosaurBB>().getHealth() <= 0){
                other.gameObject.GetComponent<DinosaurBB>().die();
                this.preys.Remove(other.gameObject);
                this.growUp(0.05f);
                this.increaseHealth(0.5f);

                if(this.currentPrey == other.gameObject){
                    this.currentPrey = null;
                }
            }
        }
    }

    // Message sent by another dinosaur when it dies
    public void OnMessageDie(GameObject other){

        Debug.Log("OnMessageDie received by " + this.gameObject.name + " from " + other.gameObject.name);

        if(this.predators.Contains(other.gameObject)){
            this.predators.Remove(other.gameObject);
        }
        else if(this.preys.Contains(other.gameObject)){
            this.preys.Remove(other.gameObject);
        }

        if(this.currentPredator == other.gameObject){
            this.currentPredator = null;
        }
        else if(this.currentPrey == other.gameObject){
            this.currentPrey = null;
        }
    }


    /* GETTERS AND SETTERS */

    // is_attacking

    public virtual bool getIsAttacking(){
        return this.is_attacking;
    }

    public virtual void setIsAttacking(bool b){
        this.is_attacking = b;
    }

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
        //this.runFrom(enemy);
    }

    // Speed
    public virtual float getSpeed(){
        return this.speed;
    }
    
    public virtual void setSpeed(float new_speed){
        this.speed = new_speed;
        this.anim.speed = new_speed;
    }

    public virtual GameObject getCurrentPredator(){
        return this.currentPredator;
    }

    public virtual GameObject getCurrentPrey(){
        return this.currentPrey;
    }
    
}
