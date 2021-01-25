using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawnBB : MonoBehaviour
{

    public GameObject raptor;
    public GameObject para;
    public GameObject tric;
    public GameObject steg;
    public GameObject area;
    public GameObject player;

    public int MAX_RAPTORS = 1;
    public int MAX_PARAS = 1;
    public int MAX_TRICS = 1;
    public int MAX_STEGS = 1;

    void Start()
    {
        StartCoroutine("SpawnDino");
    }

    System.Collections.IEnumerator SpawnDino()
    {
        if (player != null) {
            GameObject go_player = Instantiate(player, GetRandomPosition(), Quaternion.identity) as GameObject;
            yield return null;
        }
        
        for (int i = 0; i < MAX_PARAS; i++)
        {
            GameObject go_para = Instantiate(para, GetRandomPosition(), Quaternion.identity) as GameObject;
            BehaviorExecutor para_executor = go_para.GetComponent<BehaviorExecutor>();
            //para_executor.SetBehaviorParam("wanderArea", area);
            yield return null;
        }
        
        for (int i = 0; i < MAX_RAPTORS; i++)
        {
            GameObject go_raptor = Instantiate(raptor, GetRandomPosition(), Quaternion.identity) as GameObject;
            BehaviorExecutor raptor_executor = go_raptor.GetComponent<BehaviorExecutor>();
            //raptor_executor.SetBehaviorParam("wanderArea", area);
            yield return null;
        }

        for (int i = 0; i < MAX_TRICS; i++)
        {
            GameObject go_tric = Instantiate(tric, GetRandomPosition(), Quaternion.identity) as GameObject;
            BehaviorExecutor tric_executor = go_tric.GetComponent<BehaviorExecutor>();
            //tric_executor.SetBehaviorParam("wanderArea", area);
            yield return null;
        }

        for (int i = 0; i < MAX_STEGS; i++)
        {
            GameObject go_steg = Instantiate(steg, GetRandomPosition(), Quaternion.identity) as GameObject;
            BehaviorExecutor steg_executor = go_steg.GetComponent<BehaviorExecutor>();
            //steg_executor.SetBehaviorParam("wanderArea", area);
            yield return null;
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
