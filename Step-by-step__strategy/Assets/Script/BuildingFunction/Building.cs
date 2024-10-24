using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] float HP;
    [SerializeField] float MaxHP;
    [SerializeField] float ResistPhys;
    [SerializeField] Tile OccupieTile;

    private void Update()
    {
        if (this.HP <= 0)
            DestroyBuilding();
    }
    public void TakeDamage(float damage)
    {
        HP -= damage * ResistPhys;
    }
    public void DestroyBuilding()
    {
        Destroy(gameObject);
    }
    public Tile _OccupieTile
    {
        get { return OccupieTile; }
        set { OccupieTile = value; }
    }
}
