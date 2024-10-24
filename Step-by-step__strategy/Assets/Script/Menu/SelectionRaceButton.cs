using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionRaceButton : MonoBehaviour
{
    [SerializeField] string FirstPlayerChoice;
    [SerializeField] string SecondPlayerChoice;
    [SerializeField] SpawnUnitsManager sum;
    [SerializeField] GameObject ButtonStart;
    [SerializeField] GameObject Text1;
    [SerializeField] GameObject Text2;
    [SerializeField] int FightScene;
    public void StartChoosingRace()
    { 
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(true);
    }

    public void SelectGolem() 
    {
        if (FirstPlayerChoice == "")
        {
            FirstPlayerChoice = "Golem";
            sum.FirstPlayer(FirstPlayerChoice);
            Text1.gameObject.SetActive(false);
            Text2.gameObject.SetActive(true);
        }
        else
        {
            SecondPlayerChoice = "Golem";
            sum.SecondPlayer(SecondPlayerChoice);
            ButtonStart.gameObject.SetActive(true);
        }
    }
    public void SelectHuman() 
    {
        if (FirstPlayerChoice == "")
        {
            FirstPlayerChoice = "Human";
            sum.FirstPlayer(FirstPlayerChoice);
            Text1.gameObject.SetActive(false);
            Text2.gameObject.SetActive(true);
        }
        else
        {
            SecondPlayerChoice = "Human";
            sum.SecondPlayer(SecondPlayerChoice);
            ButtonStart.gameObject.SetActive(true);
        }
    }
    public void StartGame()
    {
        SceneManager.LoadScene(FightScene);
    }
}
