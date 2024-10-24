using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMenu : MonoBehaviour
{
    [SerializeField] GameObject MainMenu;
    void Start()
    {
        Instantiate(MainMenu);
    }
}
