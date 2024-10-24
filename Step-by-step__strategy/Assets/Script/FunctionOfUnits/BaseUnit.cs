using System.Collections.Generic;
using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField] float MaxHP = 20f;
    [SerializeField] float HP;
    [SerializeField] int MaxTimeToAction = 2;
    [SerializeField] int TimeToAction = 2;
    [SerializeField] float ResistPhys = 0.2f;
    [SerializeField] float ResistMagyck = 0.2f;
    [SerializeField] float DamagePhys = 5f;
    [SerializeField] float DamageMagyck = 0f;
    [SerializeField] float DistanceToEnemy;
    [SerializeField] bool IsMoving = false;
    [SerializeField] GameObject Target;
    [SerializeField] GridManager GM;
    [SerializeField] GameObject Grave;
    HPBarUI hpbar;
    int i = 0;
    float Distance;
    float MaxDistance = .1f;
    public Tile OccupieTile;
    public int PositionUnitX;
    public int PositionUnitZ;
    public List<GameObject> PathForUnit;
    public int TargetTileX;
    public int TargetTileZ;

    private void Start()
    {
        HP = MaxHP;
        GM = FindAnyObjectByType<GridManager>();
        hpbar = this.transform.GetChild(1).GetComponent<HPBarUI>();
        if (PathForUnit == null) return;
    }
    private void Update()
    {
        if (IsMoving)
        {
            transform.position = Vector3.Lerp(transform.position, Target.transform.position, 5 * Time.deltaTime);
            TargetCheker();
        }
        if (this.HP <= 0)
        {
            Death();
        }
    }
    public void GetPositionUnit()
    {
        GM.GetStartUnitXZ(OccupieTile.GetComponent<GridStats>()._X, OccupieTile.GetComponent<GridStats>()._Z);
    }

    public void GetPath(List<GameObject> pathForUnit)
    { 
        PathForUnit = pathForUnit;
    }

    public void TakeDamage(float damage)
    {
        HP -= damage;
        hpbar.ChangeHPUI();
    }
    public void Death()
    {
        Instantiate(Grave, this.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void MoveUnit()
    {
        if (TimeToAction > 0)
        {
            Target = PathForUnit[i].transform.Find("WayPoint").gameObject;
            IsMoving = true;
            Selection[] ListofanotherUnit = FindObjectsOfType<Selection>();
            for (int i = 0; i < ListofanotherUnit.Length; i++)
            {
                ListofanotherUnit[i].GetComponent<Selection>().enabled = false;
                ListofanotherUnit[i].GetComponent<Collider>().enabled = false;
            }
            TimeToAction -= 1;
            hpbar.ChageTimeToAction();
        }
    }
    public void TargetCheker()
    {
        Distance = Vector3.Distance(transform.position, Target.transform.position);
        if (i < PathForUnit.Count)
        {
            if (Distance <= MaxDistance)
            {
                i++;
                Target = PathForUnit[i].transform.Find("WayPoint").gameObject;
            }
        }
        else
        {
            IsMoving = false;
            PathForUnit[i-1].GetComponent<Tile>().SetUnit(this);
            this.GetComponent<ShowRadiusMovement>().ResetRadiuse();
            this.GetComponent<ShowRadiusMovement>().ShowOrHideRadiuse();
            GM.GetStartUnitXZ(OccupieTile.GetComponent<GridStats>()._X, OccupieTile.GetComponent<GridStats>()._Z);
            PathForUnit.Clear();
            i = 0;
            Selection[] ListofanotherUnit = FindObjectsOfType<Selection>();
            for (int i = 0; i < ListofanotherUnit.Length; i++)
            {
                if (ListofanotherUnit[i].tag == this.tag)
                {
                    ListofanotherUnit[i].GetComponent<Selection>().enabled = true;
                    ListofanotherUnit[i].GetComponent<Collider>().enabled = true;
                }
            }
        }
    }

    public float _MaxHP
    {
        get { return MaxHP; }
    }
    public float _HP
    {
        get { return HP; }
    }
    public float _DamagePhys
    {
        get { return DamagePhys; }
    }

    public int _TimeToAction
    {
        get { return TimeToAction; }
        set { TimeToAction = value; }
    }
    public int _MaxTimeToAction
    {
        get { return MaxTimeToAction; }
    }
    public float _DistanceToEnemy
    {
        get { return DistanceToEnemy; }
    }
    public GameObject _Grave
    {
        get { return Grave; }
        set { Grave = value; }
    }
}
