using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AI;

public class NavMeshGenerator : MonoBehaviour
{
    public int c = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (c < 5)
            c++;
        else
        {
            NavMeshBuilder.BuildNavMesh();
            Destroy(this);
        }
    }
}
