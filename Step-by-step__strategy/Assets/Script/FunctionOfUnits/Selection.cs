using UnityEngine;

public class Selection : MonoBehaviour
{
    Outline outline;
    [SerializeField] static GameObject SelectedUnit;
    [SerializeField] Selection PreviouseUnit;
    void Start()
    {
       outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            SelectedUnit = null;
            _Selection();
        }
    }
    public void _Selection()
    {
        if (SelectedUnit == this.gameObject)
        {
            outline.OutlineColor = Color.red;
            outline.enabled = true;
            if (this.GetComponent<Building>() == null)
                this.GetComponent<ShowRadiusMovement>().ShowOrHideRadiuse();
        }
        else 
        {
            outline.OutlineColor = Color.white;
            outline.enabled = false;
            if (this.GetComponent<Building>() == null)
                this.GetComponent<ShowRadiusMovement>().ShowOrHideRadiuse();
        }
    }   

    public void OnMouseEnter()
    {
        if(!SelectedUnit == this.gameObject) outline.enabled = true;
    }
    public void OnMouseExit()
    {
        if (!SelectedUnit == this.gameObject) outline.enabled = false;
    }
    public void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectedUnit != null) PreviouseUnit = SelectedUnit.GetComponent<Selection>();
            SelectedUnit = null;
            SelectedUnit = gameObject;
            if (this.GetComponent<Building>() == null)
            {
                this.GetComponent<BaseUnit>().GetPositionUnit();
                if (PreviouseUnit != null)
                {
                    PreviouseUnit._Selection();
                    PreviouseUnit.GetComponent<ShowRadiusMovement>().ResetRadiuse();
                }
            }
            else
            {
                if (PreviouseUnit != null)
                {
                    PreviouseUnit._Selection();
                }
            }
            _Selection();
        }
    }
    public GameObject _SelectionUnit
    { 
        get { return SelectedUnit; }
    }
}
