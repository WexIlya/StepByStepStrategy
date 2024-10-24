using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTurnButton : MonoBehaviour
{
    public static SwitchTurnButton Instance;
    public GameManager GM;
    private void Awake()
    {
        Instance = this;
        GM = FindAnyObjectByType<GameManager>();
    }

    public void Switch()
    {
        if (GM.State == GameManager.GameState.PlayerTurn) GameManager.Instance.UpdateGameState(GameManager.GameState.Enemyturn);
        else GameManager.Instance.UpdateGameState(GameManager.GameState.PlayerTurn);
    }
}
