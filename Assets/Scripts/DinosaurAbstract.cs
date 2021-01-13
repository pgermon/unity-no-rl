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

    /* Anim states */
    protected bool is_attacking = false;

    [SerializeField]
    protected GameObject[] predators;

    [SerializeField]
    protected GameObject[] prey;

    /* Dinosaur methods */
    public abstract void runTo(Vector3 position);
    public abstract void attack();
    public abstract void growUp();
    public abstract void die();


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
    }

    // Speed
    public virtual float getSpeed(){
        return this.speed;
    }
    
    public virtual void setSpeed(float new_speed){
        this.speed = new_speed;
        this.anim.speed = new_speed;
    }

    protected virtual void Update(){
        this.health -= 0.0001f;

        if(this.health <= 0){
            this.die();
        }
    }
}
