using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DinosaurInterface
{
    void runTo(Vector3 position);
    void die();
    //Calls growUp is the target dies
    void attack(DinosaurInterface target);
    void growUp();
}
