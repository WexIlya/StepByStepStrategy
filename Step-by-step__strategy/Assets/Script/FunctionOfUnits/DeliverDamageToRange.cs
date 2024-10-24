using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverDamageToRange : MonoBehaviour
{
    [SerializeField] float DTE;
    [SerializeField] int TTA;
    [SerializeField] float Dmg;
    [SerializeField] HPBarUI hpbar;
    private void Update()
    {
        DTE = this.GetComponent<BaseUnit>()._DistanceToEnemy;
        TTA = this.GetComponent<BaseUnit>()._TimeToAction;
        Dmg = this.GetComponent<BaseUnit>()._DamagePhys;
        hpbar = this.transform.GetChild(1).GetComponent<HPBarUI>();
    }
    public void DillerDamageRange(GameObject enemyunit, GameObject selectedunit)
    {
        DTE = Vector3.Distance(selectedunit.transform.position, enemyunit.transform.position);
        if (TTA > 0)
        {
            if (selectedunit.GetComponent<Selection>()._SelectionUnit != null && DTE > 1.5f && enemyunit.tag != this.tag && enemyunit.GetComponent<BaseUnit>() != null)
            {
                enemyunit.GetComponent<BaseUnit>().TakeDamage(Dmg);
                TTA -= 1;
                this.GetComponent<BaseUnit>()._TimeToAction = TTA;
                hpbar.ChageTimeToAction();
            }
            if (selectedunit.GetComponent<Selection>()._SelectionUnit != null && DTE > 1.5f && enemyunit.tag != this.tag && enemyunit.GetComponent<Building>() != null)
            {
                enemyunit.GetComponent<Building>().TakeDamage(Dmg);
                TTA -= 1;
                this.GetComponent<BaseUnit>()._TimeToAction = TTA;
                hpbar.ChageTimeToAction();
            }
        }
    }
}
