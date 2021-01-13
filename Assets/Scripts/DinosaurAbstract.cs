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

    public virtual void growUp(float g){
        this.gameObject.transform.localScale += new Vector3(g, g, g);
    }

    public virtual void attack(){
        this.anim.Play("Base Layer.Attack");
    }

    public virtual void die(){
        this.anim.Play("Base Layer.Die");
        Destroy(this.gameObject, 2.0f);
        enabled = false;
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

    // Speed
    public virtual float getSpeed(){
        return this.speed;
    }
    
    public virtual void setSpeed(float new_speed){
        this.speed = new_speed;
        this.anim.speed = new_speed;
    }
}
