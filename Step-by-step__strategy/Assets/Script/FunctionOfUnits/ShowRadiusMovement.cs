using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRadiusMovement : MonoBehaviour
{
    [SerializeField] float Radiuse = 1f;
    public Collider[] ZoneOfMovement;
    [SerializeField] Tile[] AllTile;
    [SerializeField] LayerMask tile;
    [SerializeField] Canvas Plit;
    private void Start()
    {
        AllTile = FindObjectsOfType<Tile>();
    }
    public void ShowOrHideRadiuse()
    {
        if (FindAnyObjectByType<Selection>()._SelectionUnit != null)
        {
            var SelectedUnit = FindAnyObjectByType<Selection>()._SelectionUnit;
            ZoneOfMovement = Physics.OverlapSphere(SelectedUnit.transform.position, Radiuse, tile);
            for (int i = 0; i < ZoneOfMovement.Length; i++)
            {
                ZoneOfMovement[i].gameObject.transform.GetChild(2).gameObject.SetActive(true);
            }
        }
        else 
        {
            for (int i = 0; i < AllTile.Length; i++)
            {
                if (AllTile[i]._isWalkable == true)
                    AllTile[i].gameObject.transform.GetChild(2).gameObject.SetActive(false);
            }
        }
    }

    public void ResetRadiuse()
    {
        for (int i = 0; i < AllTile.Length; i++)
        {
            if (AllTile[i]._isWalkable == true)
                AllTile[i].gameObject.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
