using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] GameObject SUM;
    void Awake()
    {
        Instantiate(SUM);
    }
}
