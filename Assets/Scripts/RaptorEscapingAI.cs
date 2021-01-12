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
    Vector3 lastPos;

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
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(agent.velocity,new Vector3(0f,0f,0f))<0.1)
            anim.Play("Base Layer.Armature|Velociraptor_Idle");
        else
            anim.Play("Base Layer.Armature|Velociraptor_Run");
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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == threat)
            die();
    }
}
