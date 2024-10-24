using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static event Action<GameState> OnGameStateChanged;
    public GameState State;
    [SerializeField] ResuorceManager RM;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UpdateGameState(GameState.GenerateGrid);
    }

    public void UpdateGameState(GameState newState)
    { 
        State = newState;
        switch (newState)
        {
            case GameState.SelectRace:
                break;
            case GameState.GenerateGrid:
                GridManager.Instance.GenerateGrid();
                break;
            case GameState.SpawnPlayerUnits:
                SpawnUnitsManager.Instance.SpawnPlayerUnits();  
                break;
            case GameState.SpawnEnemyUnits:
                SpawnUnitsManager.Instance.SpawnEnemyUnits();
                break;
            case GameState.PlayerTurn:
                Debug.Log(State);
                RM = FindAnyObjectByType<ResuorceManager>();
                var AllUnits = FindObjectsOfType<BaseUnit>();
                var AllBuilding = FindObjectsOfType<Building>();
                for (int i = 0; i < AllUnits.Length; i++)
                {
                    AllUnits[i].GetComponent<ShowRadiusMovement>().ResetRadiuse();
                    if (AllUnits[i].tag == "Enemy")
                    {
                        AllUnits[i].GetComponent<Collider>().enabled = false;
                        AllUnits[i].GetComponent<Selection>().enabled = false;
                        AllUnits[i].GetComponent<Outline>().enabled = false;
                    }
                    AllUnits[i].GetComponent<BaseUnit>()._TimeToAction = 2;
                    if (AllUnits[i].tag == "Player")
                    {
                        AllUnits[i].transform.GetChild(1).GetComponent<HPBarUI>().ChageTimeToAction();
                        AllUnits[i].GetComponent<Collider>().enabled = true;
                        AllUnits[i].GetComponent<Selection>().enabled = true;
                    }
                }
                for (int i = 0; i < AllBuilding.Length; i++)
                {
                    if (AllBuilding[i].tag == "Enemy")
                    {
                        AllBuilding[i].GetComponent<Outline>().enabled = false;
                        AllBuilding[i].GetComponent<Collider>().enabled = false;
                        AllBuilding[i].GetComponent<Selection>().enabled = false;
                    }
                    if (AllBuilding[i].tag == "Player")
                    {
                        AllBuilding[i].GetComponent<Collider>().enabled = true;
                        AllBuilding[i].GetComponent<Selection>().enabled = true;
                        if (AllBuilding[i].GetComponent<Gold>() != null)
                        {
                            AllBuilding[i].GetComponent<Gold>().GainGoldPlayer();
                            RM.TextGold();
                        }
                    }
                }
                    break;
            case GameState.Enemyturn:
                Debug.Log(State);
                AllUnits = FindObjectsOfType<BaseUnit>();
                AllBuilding = FindObjectsOfType<Building>();
                for (int i = 0; i < AllUnits.Length; i++)
                {
                    AllUnits[i].GetComponent<ShowRadiusMovement>().ResetRadiuse();
                    AllUnits[i].GetComponent<BaseUnit>()._TimeToAction = 2;
                    if (AllUnits[i].tag == "Enemy")
                    {
                        AllUnits[i].transform.GetChild(1).GetComponent<HPBarUI>().ChageTimeToAction();
                        AllUnits[i].GetComponent<Collider>().enabled = true;
                        AllUnits[i].GetComponent<Selection>().enabled = true;
                    }
                    if (AllUnits[i].tag == "Player")
                    {
                        AllUnits[i].GetComponent<Collider>().enabled = false;
                        AllUnits[i].GetComponent<Selection>().enabled = false;
                        AllUnits[i].GetComponent<Outline>().enabled = false;
                    }
                }
                for (int i = 0; i < AllBuilding.Length; i++)
                {
                    if (AllBuilding[i].tag == "Enemy")
                    {
                        AllBuilding[i].GetComponent<Collider>().enabled = true;
                        AllBuilding[i].GetComponent<Selection>().enabled = true;
                        if (AllBuilding[i].GetComponent<Gold>() != null)
                        {
                            AllBuilding[i].GetComponent<Gold>().GainGoldEnemy();
                            RM.TextGold();
                        }
                    }
                    if (AllBuilding[i].tag == "Player")
                    {
                        AllBuilding[i].GetComponent<Outline>().enabled = false;
                        AllBuilding[i].GetComponent<Collider>().enabled = false;
                        AllBuilding[i].GetComponent<Selection>().enabled = false;
                    }
                }
                break;
            case GameState.Victory:
                break;
            case GameState.Lose:
                break;
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState
    { 
        SelectRace = 0,
        GenerateGrid = 1,
        SpawnPlayerUnits = 2,
        SpawnEnemyUnits = 3,
        PlayerTurn = 4,
        Enemyturn = 5,
        Victory = 6,
        Lose = 7
    }
}
