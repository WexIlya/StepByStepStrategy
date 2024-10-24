using TMPro;
using UnityEngine;

public class ResuorceManager : MonoBehaviour
{
    [SerializeField] int GoldPlayer;
    [SerializeField] int GoldEnemy;
    [SerializeField] GameManager GM;
    [SerializeField] GameObject TextGoldPlayer;
    [SerializeField] GameObject TextGoldEnemy;
    [SerializeField] GameObject TextPlayerGold;
    [SerializeField] GameObject TextEnemyGold;

    private void Start()
    {
        GM = FindAnyObjectByType<GameManager>();
    }

    public void ChangeTextGold()
    {
        TextPlayerGold.GetComponent<TextMeshProUGUI>().text = "Gold: " + GoldPlayer;
        TextEnemyGold.GetComponent<TextMeshProUGUI>().text = "Gold: " + GoldEnemy;
    }

    public void TextGold()
    {
        if (GM.State == GameManager.GameState.PlayerTurn)
        {
            ChangeTextGold();
            TextGoldPlayer.SetActive(true);
            TextGoldEnemy.SetActive(false);
        }
        if (GM.State == GameManager.GameState.Enemyturn)
        {
            ChangeTextGold();
            TextGoldPlayer.SetActive(false);
            TextGoldEnemy.SetActive(true);
        }
    }

    public int _GoldPlayer
    { 
        get { return GoldPlayer; }
        set { GoldPlayer = value; }
    }
    public int _GoldEnemy
    {
        get { return GoldEnemy; }
        set { GoldEnemy = value; }
    }
}
