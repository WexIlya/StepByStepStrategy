using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnUnitsManager : MonoBehaviour
{
    public static SpawnUnitsManager Instance;
    public List<ScriptaleUnits> Units;
    public List<GameObject> Buildings;
    public List<GameObject> Graves;
    public string FirstPlayerChoice;
    public string SecondPlayerChoice;
    private void Awake()
    {
        Instance = this;

        Units = Resources.LoadAll<ScriptaleUnits>("Units").ToList();
        Graves = Resources.LoadAll<GameObject>("Graves").ToList();
        Buildings = Resources.LoadAll<GameObject>("Buildings").ToList();
    }

    public void FirstPlayer(string race)
    {
        FirstPlayerChoice = race;
        Debug.Log(FirstPlayerChoice);
    }

    public void SecondPlayer(string race)
    {
        SecondPlayerChoice = race;
        Debug.Log(SecondPlayerChoice);
    }

    public void SpawnPlayerUnits()
    {
        var PlayerUnitsCount = 2;
        for (int i = 0; i < PlayerUnitsCount; i++)
        {
            var UnitPrefab = Units.Find(p => p.UnitPrefab.name == FirstPlayerChoice+"_simple").UnitPrefab;
            var SpawnPlayerUnit = Instantiate(UnitPrefab);
            SpawnPlayerUnit._Grave = Graves.Find(p => p.name == FirstPlayerChoice + "_grave").gameObject;
            ChangeRaceUnitsTagPlayer(SpawnPlayerUnit);
            var RandomSpawnTile = GridManager.Instance.GetTileSpawnToPlayer();
            RandomSpawnTile.SetUnit(SpawnPlayerUnit);
        }
        var PlayerBuildingCount = 1;
        for (int i = 0; i < PlayerBuildingCount; i++)
        {
            var BuildingPrefab = Buildings.Find(p => p.gameObject.name == FirstPlayerChoice + "_armor").gameObject;
            var SpawnPlayerBuilding = Instantiate(BuildingPrefab);
            ChangeRaceBuildingTagPlayer(SpawnPlayerBuilding);
            var RandomSpawnTile = GridManager.Instance.GetTileSpawnToBuildingPlayer();
            RandomSpawnTile.SetBuilding(SpawnPlayerBuilding);

            BuildingPrefab = Buildings.Find(p => p.gameObject.name == FirstPlayerChoice + "_gold").gameObject;
            SpawnPlayerBuilding = Instantiate(BuildingPrefab);
            ChangeRaceBuildingTagPlayer(SpawnPlayerBuilding);
            RandomSpawnTile = GridManager.Instance.GetTileSpawnToBuildingPlayer();
            RandomSpawnTile.SetBuilding(SpawnPlayerBuilding);

            BuildingPrefab = Buildings.Find(p => p.gameObject.name == FirstPlayerChoice + "_baracs").gameObject;
            SpawnPlayerBuilding = Instantiate(BuildingPrefab);
            ChangeRaceBuildingTagPlayer(SpawnPlayerBuilding);
            RandomSpawnTile = GridManager.Instance.GetTileSpawnToBuildingPlayer();
            RandomSpawnTile.SetBuilding(SpawnPlayerBuilding);
        }

        GameManager.Instance.UpdateGameState(GameManager.GameState.SpawnEnemyUnits);
    }
    public void SpawnEnemyUnits()
    {
        var EnemyUnitsCount = 2;
        for (int i = 0; i < EnemyUnitsCount; i++)
        {
            var UnitPrefab = Units.Find(p => p.UnitPrefab.name == SecondPlayerChoice+"_simple").UnitPrefab;
            var SpawnEnemyUnit = Instantiate(UnitPrefab);
            SpawnEnemyUnit._Grave = Graves.Find(p => p.name == SecondPlayerChoice + "_grave").gameObject;
            ChangeRaceUnitsTagEnemy(SpawnEnemyUnit);
            var RandomSpawnTile = GridManager.Instance.GetTileSpawnToEnemy();
            RandomSpawnTile.SetUnit(SpawnEnemyUnit);
        }
        var EnemyBuildingCount = 1;
        for (int i = 0; i < EnemyBuildingCount; i++)
        {
            var BuildingPrefab = Buildings.Find(p => p.gameObject.name == SecondPlayerChoice + "_armor").gameObject;
            var SpawnPlayerBuilding = Instantiate(BuildingPrefab);
            ChangeRaceBuildingTagEnemy(SpawnPlayerBuilding);
            var RandomSpawnTile = GridManager.Instance.GetTileSpawnToBuildingEnemy();
            RandomSpawnTile.SetBuilding(SpawnPlayerBuilding);

            BuildingPrefab = Buildings.Find(p => p.gameObject.name == SecondPlayerChoice + "_gold").gameObject;
            SpawnPlayerBuilding = Instantiate(BuildingPrefab);
            ChangeRaceBuildingTagEnemy(SpawnPlayerBuilding);
            RandomSpawnTile = GridManager.Instance.GetTileSpawnToBuildingEnemy();
            RandomSpawnTile.SetBuilding(SpawnPlayerBuilding);

            BuildingPrefab = Buildings.Find(p => p.gameObject.name == SecondPlayerChoice + "_baracs").gameObject;
            SpawnPlayerBuilding = Instantiate(BuildingPrefab);
            ChangeRaceBuildingTagEnemy(SpawnPlayerBuilding);
            RandomSpawnTile = GridManager.Instance.GetTileSpawnToBuildingEnemy();
            RandomSpawnTile.SetBuilding(SpawnPlayerBuilding);
        }
        GameManager.Instance.UpdateGameState(GameManager.GameState.PlayerTurn);
    }
    public void ChangeRaceUnitsTagPlayer(BaseUnit unit)
    {
        unit.tag = "Player";
    }
    public void ChangeRaceBuildingTagPlayer(GameObject build)
    {
        build.tag = "Player";
    }
    public void ChangeRaceUnitsTagEnemy(BaseUnit unit)
    {
        unit.tag = "Enemy";
    }
    public void ChangeRaceBuildingTagEnemy(GameObject build)
    {
        build.tag = "Enemy";
    }

    public string _FirstPlayerChoice
    {
        get { return FirstPlayerChoice; }
    }
    public string _SecondPlayerChoice
    {
        get { return SecondPlayerChoice; }
    }
}
