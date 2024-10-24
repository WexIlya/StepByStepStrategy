using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] ResuorceManager Wallet;

    private void Start()
    {
        Wallet = FindAnyObjectByType<ResuorceManager>();
    }

    public void ImproveHP()
    { 
        
    }
    public void ImproveDamageMelle()
    { 
        
    }
    public void ImproveDamageRange()
    {

    }
}
