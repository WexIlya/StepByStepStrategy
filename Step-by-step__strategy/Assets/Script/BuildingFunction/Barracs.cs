using Unity.VisualScripting;
using UnityEngine;

public class Barracs : MonoBehaviour
{
    [SerializeField] ResuorceManager Wallet;
    [SerializeField] GameObject UnitTire1;
    [SerializeField] GameObject UnitTire2;
    [SerializeField] GameObject UnitTire3;
    [SerializeField] GridManager GR;
    [SerializeField] SpawnUnitsManager SUM;

    private void Start()
    {
        Wallet = FindAnyObjectByType<ResuorceManager>();
        GR = FindAnyObjectByType<GridManager>();
        SUM = FindAnyObjectByType<SpawnUnitsManager>();
    }

    public void Update()
    {
        if (FindAnyObjectByType<Selection>()._SelectionUnit == this.gameObject)
            this.transform.GetChild(1).gameObject.SetActive(true);
        else
            this.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void SpawnUnitTire1()
    {
        if (this.gameObject.tag == "Player")
        {
            if (Wallet._GoldPlayer >= 3)
            {
                Wallet._GoldPlayer -= 3;
                Wallet.ChangeTextGold();
                var Tire1 = Instantiate(UnitTire1);
                UnitTire1.GetComponent<BaseUnit>()._Grave = SUM.Graves.Find(p => p.name == SUM.FirstPlayerChoice + "_grave").gameObject;
                var SpawnTile = GR.GetTileSpawnToPlayer();
                SpawnTile.SetUnit(Tire1.GetComponent<BaseUnit>());
                Tire1.tag = "Player";
            }
        }
        else
        {
            if (Wallet._GoldEnemy >= 3)
            {
                Wallet._GoldEnemy -= 3;
                Wallet.ChangeTextGold();
                var Tire1 = Instantiate(UnitTire1);
                UnitTire1.GetComponent<BaseUnit>()._Grave = SUM.Graves.Find(p => p.name == SUM.SecondPlayerChoice + "_grave").gameObject;
                var SpawnTile = GR.GetTileSpawnToEnemy();
                SpawnTile.SetUnit(Tire1.GetComponent<BaseUnit>());
                Tire1.tag = "Enemy";
            }
        }
    }
    public void SpawnUnitTire2()
    {
        if (this.gameObject.tag == "Player")
        {
            if (Wallet._GoldPlayer >= 5)
            {
                Wallet._GoldPlayer -= 5;
                Wallet.ChangeTextGold();
                var Tire1 = Instantiate(UnitTire2);
                UnitTire2.GetComponent<BaseUnit>()._Grave = SUM.Graves.Find(p => p.name == SUM.FirstPlayerChoice + "_grave").gameObject;
                var SpawnTile = GR.GetTileSpawnToPlayer();
                SpawnTile.SetUnit(Tire1.GetComponent<BaseUnit>());
                Tire1.tag = "Player";
            }
        }
        else
        {
            if (Wallet._GoldEnemy >= 5)
            {
                Wallet._GoldEnemy -= 5;
                Wallet.ChangeTextGold();
                var Tire1 = Instantiate(UnitTire2);
                UnitTire2.GetComponent<BaseUnit>()._Grave = SUM.Graves.Find(p => p.name == SUM.SecondPlayerChoice + "_grave").gameObject;
                var SpawnTile = GR.GetTileSpawnToEnemy();
                SpawnTile.SetUnit(Tire1.GetComponent<BaseUnit>());
                Tire1.tag = "Enemy";
            }
        }
    }
    public void SpawnUnitTire3()
    {
        if (this.gameObject.tag == "Player")
        {
            if (Wallet._GoldPlayer >= 8)
            {
                Wallet._GoldPlayer -= 8;
                Wallet.ChangeTextGold();
                var Tire1 = Instantiate(UnitTire3);
                UnitTire3.GetComponent<BaseUnit>()._Grave = SUM.Graves.Find(p => p.name == SUM.FirstPlayerChoice + "_grave").gameObject;
                var SpawnTile = GR.GetTileSpawnToPlayer();
                SpawnTile.SetUnit(Tire1.GetComponent<BaseUnit>());
                Tire1.tag = "Player";
            }
        }
        else
        {
            if (Wallet._GoldEnemy >= 8)
            {
                Wallet._GoldEnemy -= 8;
                Wallet.ChangeTextGold();
                var Tire1 = Instantiate(UnitTire3);
                UnitTire3.GetComponent<BaseUnit>()._Grave = SUM.Graves.Find(p => p.name == SUM.SecondPlayerChoice + "_grave").gameObject;
                var SpawnTile = GR.GetTileSpawnToEnemy();
                SpawnTile.SetUnit(Tire1.GetComponent<BaseUnit>());
                Tire1.tag = "Enemy";
            }
        }
    }
}
