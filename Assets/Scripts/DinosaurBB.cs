using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinosaurBB : MonoBehaviour
{
    /* Dinosaur properties */
    [SerializeField]
    protected float init_max_health, max_health, health = 1.0f;
    //protected float health;

    [SerializeField]
    protected DinoHealthBar health_bar;

    protected bool is_player = false;
    protected float speed;
    protected Animator anim;
    protected UnityEngine.AI.NavMeshAgent agent;
    protected ParticleSystem[] blood;

    /* Dino states */
    protected bool is_attacking = false;
    protected bool is_walking = false;
    protected bool is_running = false;
    protected bool is_wandering = false;

    [SerializeField]
    protected List<string> predators_names;
    protected List<GameObject> predators;
    protected List<GameObject> predators_in_range;

    [SerializeField]
    protected GameObject currentPredator = null;

    [SerializeField]
    protected List<string> preys_names;
    protected List<GameObject> preys;
    protected List<GameObject> preys_in_range;

    [SerializeField]
    protected GameObject currentPrey = null;

    protected List<GameObject> herd;
    protected List<GameObject> herd_in_range;


    /* Dinosaur methods */
    public virtual bool attack(GameObject prey){

        this.anim.Play("Base Layer.Attack");

        this.is_attacking = true;
        if(prey != null){
            //Debug.Log(this.gameObject.name + " tries to attack " + prey.gameObject.name);
            if(this.gameObject.name.Split('(')[0] != "StegosaurusBB"){
                this.transform.LookAt(prey.transform);
                //Debug.Log("lookAt");
            }
            
            return true;
        }
        
        return false;
    }

    public virtual void growUp(float g){
        this.gameObject.transform.localScale += new Vector3(g, g, g);
    }

    public virtual void die(){
        Debug.Log(this.gameObject.name + " died");
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        
        if(!is_player){
            this.gameObject.GetComponent<BehaviorExecutor>().enabled = false;
            this.agent.enabled = false;
        }

        this.anim.Play("Base Layer.Die");
        Collider[] colliders = this.gameObject.GetComponents<BoxCollider>();
        for(int i = 0; i < colliders.Length; i++){
            colliders[i].enabled = false;
        }

        // When the dino dies, it sends a message to alert its herd-mates, predators and preys

        foreach(GameObject mate in this.herd){
            if(mate != null){
                mate.gameObject.GetComponent<DinosaurBB>().OnMessageDie(this.gameObject);
            }
        }
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

       
        Destroy(this.gameObject, 4.0f);
        enabled = false;
    }

    protected virtual void Start() {

        this.max_health = this.init_max_health * this.transform.localScale[0];
        this.health = this.max_health;
        health_bar.SetMaxHealth(this.max_health);

        this.predators = new List<GameObject>();
        this.predators_in_range = new List<GameObject>();

        this.preys = new List<GameObject>();
        this.preys_in_range = new List<GameObject>();

        this.herd = new List<GameObject>();
        this.herd_in_range = new List<GameObject>();

        this.anim = this.gameObject.GetComponent<Animator>();
        if(!is_player){
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
        //this.speed *= this.health;
        
        this.health_bar.SetHealth(this.health);

        if(this.health <= 0){
            this.die();
        }

        // sets the current predator and prey, which are the closest among the ones in range
        selectCurrentPredator();
        selectCurrentPrey();
        
        // update bleeding according to health
        //UpdateBlood();

        /*bool is_anim_attack = this.anim.GetCurrentAnimatorStateInfo(0).IsName("Base Layer.Attack");
        if(!this.is_attacking && is_anim_attack){
            this.is_attacking = true;
        }*/

        if(this.is_attacking && anim.GetAnimatorTransitionInfo(0).IsName("Attack -> Idle")){
            this.is_attacking = false;
        }


        if(this.agent != null && !this.is_attacking){
            speed = Vector3.Distance(agent.velocity, new Vector3(0f, 0f, 0f));
            if (speed < 0.1){
                this.anim.Play("Base Layer.Idle");
            }
            else if(speed < 6){
                this.anim.Play("Base Layer.Walk");
                //this.is_walking = true;
            }
            else{
                this.anim.Play("Base Layer.Run");
                //this.is_running = true;
            }
        }
    }

    public void UpdateBlood(){
        if(blood.Length != 0){
            if (this.health<0.8f * max_health)
            {
                if (!blood[0].isPlaying)
                {
                    blood[0].Play();
                }
                if (!blood[1].isPlaying)
                {
                    blood[1].Play();
                }
                
            }
            if (this.health < 0.5f * max_health)
            {
               if (!blood[2].isPlaying)
                {
                    blood[2].Play();
                }
                if (!blood[3].isPlaying)
                {
                    blood[3].Play();
                }
            }
            if (this.health < 0.4f * max_health)
            {
               if (!blood[4].isPlaying)
                {
                    blood[3].Play();
                }
               if (!blood[5].isPlaying)
                {
                    blood[5].Play();
                }
            }
        }
    }


    // Called by the DetectionHandlerBB script when another dinosaur enters the detection range collider
    public void OnEnterDetection(Collider other){

        if(this.predators != null && this.preys != null && this.herd != null){

            // Case a new dino of the same species is detected
            if(this.gameObject.name == other.gameObject.name && !this.herd.Contains(other.gameObject)){
                this.herd.Add(other.gameObject);
                this.herd_in_range.Add(other.gameObject);
                //Debug.Log(other.gameObject.name + " set as herd-mate of " + this.gameObject.name);
            }

            // Case a new predator is detected
            else if(this.predators_names.Contains(other.gameObject.name.Split('(')[0])){

                // the detected predator is added to the list of predators if it is not already in it
                if(!this.predators.Contains(other.gameObject)){
                    this.predators.Add(other.gameObject);
                    this.predators_in_range.Add(other.gameObject);
                    //Debug.Log(other.gameObject.name + " set as predator of " + this.gameObject.name);
                }
                // else it is added to the list of predators in range
                else{
                    this.predators_in_range.Add(other.gameObject);
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
                    this.preys_in_range.Add(other.gameObject);
                    //Debug.Log(other.gameObject.name + " set as prey of " + this.gameObject.name);
                }
                // else it is added to the list of preys in range
                else{
                    this.preys_in_range.Add(other.gameObject);
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

    // Called by the DetectionHandlerBB script when another dinosaur exits the detection range collider
    public void OnExitDetection(Collider other){

        // If another dino leaves the detection range, it is removed from the corresponding list
        if(this.gameObject.name == other.gameObject.name){
            this.herd_in_range.Remove(other.gameObject);
        }
        else if(this.predators_names.Contains(other.gameObject.name.Split('(')[0])){
            this.predators_in_range.Remove(other.gameObject);
        }
        else if(this.preys_names.Contains(other.gameObject.name.Split('(')[0])){
            this.preys_in_range.Remove(other.gameObject);
        }
    }


    // Called by the HeadCollisionHandlerBB script when the head collider is triggered during an attack
    public void OnSuccessfulAttack(Collider other){

        // if the other dino is part of its list of preys, the dino deals damage to the other
        if(this.preys.Contains(other.gameObject)){
            Debug.Log(this.gameObject.name + " deals " + 0.2f * this.transform.localScale[0] + " damages to " + other.gameObject.name);
            this.is_attacking = false;
            other.gameObject.GetComponent<DinosaurBB>().decreaseHealth(0.2f * this.transform.localScale[0]);

            // if the dino kills other, it grows up and regens its health
            if(other.gameObject.GetComponent<DinosaurBB>().getHealth() <= 0){
                Debug.Log(this.gameObject.name + " killed " + other.gameObject.name);
                other.gameObject.GetComponent<DinosaurBB>().die();
                this.preys.Remove(other.gameObject);
                this.preys_in_range.Remove(other.gameObject);

                this.growUp(0.07f * other.gameObject.transform.localScale[0]);
                this.max_health = this.init_max_health * this.transform.localScale[0];
                this.increaseHealth(0.2f * other.gameObject.GetComponent<DinosaurBB>().getMaxHealth());
                this.health_bar.SetMaxHealth(this.max_health);

                if(this.currentPrey == other.gameObject){
                    this.currentPrey = null;
                }
            }
        }

        // if the other dino is part of its list of predators, the dino deals damage to the other
        else if(this.predators.Contains(other.gameObject)){
            Debug.Log(this.gameObject.name + " strikes back at " + other.gameObject.name);
            this.is_attacking = false;
            other.gameObject.GetComponent<DinosaurBB>().decreaseHealth(0.1f * this.transform.localScale[0]);
        }
    }

    // Message sent by another dinosaur when it dies
    public void OnMessageDie(GameObject other){

        string other_name = other.gameObject.name.Split('(')[0];
        //Debug.Log("OnMessageDie received by " + this.gameObject.name + " from " + other_name);

        // if the message sender is a herd-mate, it is removed from the corresponding lists
        if(this.gameObject.name == other.gameObject.name){
            this.herd.Remove(other.gameObject);
            this.herd_in_range.Remove(other.gameObject);
        }

        // if the message sender is a predator, it is removed from the corresponding lists
        else if(this.predators_names.Contains(other_name)){
            this.predators.Remove(other.gameObject);
            this.predators_in_range.Remove(other.gameObject);

            if(this.currentPredator == other.gameObject){
                this.currentPredator = null;
                selectCurrentPredator();
            }
        }

        // if the message sender is a prey, it is removed from the corresponding lists
        else if(this.preys_names.Contains(other_name)){
            this.preys.Remove(other.gameObject);
            this.preys_in_range.Remove(other.gameObject);

            if(this.currentPrey == other.gameObject){
                this.currentPrey = null;
                selectCurrentPrey();
            }
        }
    }


    public void OnHerdAlert(GameObject mate, GameObject other){
        string other_name = other.gameObject.name.Split('(')[0];
        //Debug.Log("OnHerdAlert received by " + this.gameObject.name + " from " + mate.gameObject.name + " about " + other_name);
        
        if(this.predators_names.Contains(other_name) && !this.predators.Contains(other.gameObject)){
            this.predators.Add(other);
            this.predators_in_range.Add(other);
            if(this.is_wandering){
                this.currentPredator = other.gameObject;
            }
            //Debug.Log(other_name + " set as predator of " + this.gameObject.name);
        }
        else if(this.preys_names.Contains(other_name) && !this.preys.Contains(other.gameObject)){
            this.preys.Add(other.gameObject);
            this.preys_in_range.Add(other.gameObject);
            if(this.is_wandering){
                this.currentPredator = other.gameObject;
            }
            //Debug.Log(other_name + " set as prey of " + this.gameObject.name);
        }
    }


    /* GETTERS AND SETTERS */

    /* is_attacking */
    public virtual bool getIsAttacking(){
        return this.is_attacking;
    }

    public virtual void setIsAttacking(bool b){
        this.is_attacking = b;
    }

    /* is_wandering */
        public virtual bool getIsWandering(){
        return this.is_wandering;
    }

    public virtual void setIsWandering(bool b){
        this.is_wandering = b;
    }

    /* Health */
    public virtual float getHealth(){
        return this.health;
    }

    public virtual void setHealth(float h){
        this.health = h;
    }

    public virtual float getMaxHealth(){
        return this.max_health;
    }

    public virtual void increaseHealth(float i){
        this.health += i;
        if(this.health > this.max_health){
            this.health = this.max_health;
        }
        this.health_bar.SetHealth(this.health);
    }

    public virtual void decreaseHealth(float i){
        this.health -= i;
        if(this.health < 0){
            this.health = 0;
        }
        this.health_bar.SetHealth(this.health);

    }

    /* speed */
    public virtual float getSpeed(){
        return this.speed;
    }
    
    public virtual void setSpeed(float new_speed){
        this.speed = new_speed;
        this.anim.speed = new_speed;
    }

    /* Herd */
    public virtual List<GameObject> getHerdInRange(){
        return this.herd_in_range;
    }

    /* Predators */
    public virtual GameObject getCurrentPredator(){
        return this.currentPredator;
    }

    /** @brief Désigne le prédateur.
     *
     * Définit le {@link currentPredator} comme le prédateur le plus proche.
     * Le prédateur actuel peut être remplacé par un prédateur plus proche.
     * 
     * Les prédateurs potentiels sont enregistrés par {@link OnEnterDetection(Collider)} dans {@link predators_in_range}.
     */
    public virtual void selectCurrentPredator(){
        
        float current_predator_dist = Mathf.Infinity;

        if(currentPredator != null){
            current_predator_dist = Vector3.Distance(this.gameObject.transform.position, this.currentPredator.gameObject.transform.position);
        }

        for(int i = 0; i < this.predators_in_range.Count; i++){

            if(predators_in_range[i] != null){

                float dist = Vector3.Distance(this.gameObject.transform.position, predators_in_range[i].gameObject.transform.position);
                if(dist < current_predator_dist){
                    this.currentPredator = predators_in_range[i].gameObject;
                    current_predator_dist = dist;
                }
            } 
        }
    }

    /* Preys */

    public virtual GameObject getCurrentPrey(){
        return this.currentPrey;
    }

    /** @brief Choisit une proie.
     *
     * Choisit la proie la plus proche et actualise {@link currentPrey}.
     */
    public virtual void selectCurrentPrey(){
        
        float current_prey_dist = Mathf.Infinity;

        if(currentPrey != null){
            current_prey_dist = Vector3.Distance(this.gameObject.transform.position, this.currentPrey.gameObject.transform.position);
        }
        
        for(int i = 0; i < this.preys_in_range.Count; i++){

            if(preys_in_range[i] != null){

                float dist = Vector3.Distance(this.gameObject.transform.position, preys_in_range[i].gameObject.transform.position);
                if(dist < current_prey_dist){
                    this.currentPrey = preys_in_range[i].gameObject;
                    current_prey_dist = dist;
                }
            }
        }
    }
    
}
