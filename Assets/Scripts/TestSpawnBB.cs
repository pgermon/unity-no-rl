using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnBB : MonoBehaviour
{

    public GameObject raptor;
    public GameObject para;
    public GameObject area;
    public GameObject player;

    private bool player_spawned = false;

    public int MAX_RAPTORS = 1;
    private int raptors_count = 0;

    public int MAX_PARAS = 1;
    private int paras_count = 0;

    void Start()
    {
        InvokeRepeating("SpawnDino", 0f, 1.0f / 1000.0f);
    }

    void SpawnDino()
    {
        if(!player_spawned){
            GameObject go_player = Instantiate(player, GetRandomPosition(), Quaternion.identity) as GameObject;
            player_spawned = true;
        }
        

        if(paras_count < MAX_PARAS)
        {
            GameObject go_para = Instantiate(para, GetRandomPosition(), Quaternion.identity) as GameObject;
            BehaviorExecutor para_executor = go_para.GetComponent<BehaviorExecutor>();
            para_executor.SetBehaviorParam("wanderArea", area);
            paras_count++;
        }
        
        else if(raptors_count < MAX_RAPTORS)
        {
            GameObject go_raptor = Instantiate(raptor, GetRandomPosition(), Quaternion.identity) as GameObject;
            BehaviorExecutor raptor_executor = go_raptor.GetComponent<BehaviorExecutor>();
            raptor_executor.SetBehaviorParam("wanderArea", area);
            raptors_count++;
        }
        else{    
            CancelInvoke();
        }

    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition = Vector3.zero;
        BoxCollider boxCollider = area.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            randomPosition = 
                new Vector3(
                    Random.Range(area.transform.position.x - area.transform.localScale.x * boxCollider.size.x * 0.5f,
                                 area.transform.position.x + area.transform.localScale.x * boxCollider.size.x * 0.5f),
                    area.transform.position.y,
                    Random.Range(area.transform.position.z - area.transform.localScale.z * boxCollider.size.z * 0.5f,
                                 area.transform.position.z + area.transform.localScale.z * boxCollider.size.z * 0.5f));
        }

        return randomPosition;
    }
}
