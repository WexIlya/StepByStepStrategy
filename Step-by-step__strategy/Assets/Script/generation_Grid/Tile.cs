using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Tile : MonoBehaviour
{
    public BaseUnit OccupieUnit;
    public Building OccupieBuilding;
    ShowRadiusMovement zone;
    [SerializeField] bool Walkable;
    [SerializeField] GridManager GM;
    private void Start()
    {
        GM = FindAnyObjectByType<GridManager>();
    }
    public void SetUnit(BaseUnit unit)
    {
        if (unit.OccupieTile != null) unit.OccupieTile.OccupieUnit = null;
        unit.transform.position = new Vector3 (this.transform.position.x, 0.65f, this.transform.position.z);
        OccupieUnit = unit;
        unit.OccupieTile = this;
    }
    public void SetBuilding(GameObject build)
    {
        if (build.GetComponent<Building>()._OccupieTile != null) build.GetComponent<Building>()._OccupieTile.OccupieUnit = null;
        build.transform.position = new Vector3(this.transform.position.x, 0.65f, this.transform.position.z);
        OccupieBuilding = build.GetComponent<Building>();
        build.GetComponent<Building>()._OccupieTile = this;
    }

    public void OnMouseDown()
    {
        var SelectedUnit = FindAnyObjectByType<Selection>()._SelectionUnit;
        if (SelectedUnit != null && SelectedUnit.GetComponent<ShowRadiusMovement>().ZoneOfMovement.Length > 0)
        {
            for (int i = 0; i < SelectedUnit.GetComponent<ShowRadiusMovement>().ZoneOfMovement.Length; i++)
            {
                if (this.transform.position == SelectedUnit.GetComponent<ShowRadiusMovement>().ZoneOfMovement[i].transform.position)
                {
                    if (this.OccupieUnit == null && this.OccupieBuilding == null)
                    {
                        GM.GetEndUnitXZ(this.GetComponent<GridStats>()._X, this.GetComponent<GridStats>()._Z);
                        SelectedUnit.GetComponent<BaseUnit>().GetPositionUnit();
                        GM.FindDistance = true;
                    }
                }
            }
        }
    }

    public void OnMouseOver()
    {
        var SelectedUnit = FindAnyObjectByType<Selection>()._SelectionUnit;
        if (Input.GetMouseButtonDown(1))
        {
            if (this.OccupieUnit != null || this.OccupieBuilding != null)
            {
                if(SelectedUnit.GetComponent<DeliverDamageToMelee>() != null)
                    SelectedUnit.GetComponent<DeliverDamageToMelee>().DillerDamageMelly(OccupieUnit.gameObject, SelectedUnit);
                if(SelectedUnit.GetComponent<DeliverDamageToRange>() != null)
                    SelectedUnit.GetComponent<DeliverDamageToRange>().DillerDamageRange(OccupieUnit.gameObject, SelectedUnit);
            }
        }
    }

    public void OnMouseEnter()
    {
        this.transform.GetChild(2).GetComponent<SelectionPlit>().RedPlit();
        
    }
    public void OnMouseExit()
    {
        this.transform.GetChild(2).GetComponent<SelectionPlit>().WhitePlit();
    }

    public bool _isWalkable
    { get { return Walkable; } set { Walkable = value; } }
}
