using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RaptorEscapingAI : MonoBehaviour, DinosaurInterface
{
    public float speed;
    Animator anim;
    public GameObject threat;
    NavMeshAgent agent;

    public void runTo(Vector3 position) 
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

    public void die()
    {
        anim.Play("Base Layer.Armature|Velociraptor_Death");
        Destroy(this.gameObject,1.13f);
    }
    //Calls growUp is the target dies
    public void attack(DinosaurInterface target) { }
    public void growUp() { }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        runFrom();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == threat)
            die();
    }
}
