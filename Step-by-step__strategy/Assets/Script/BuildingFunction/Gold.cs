using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour
{
    [SerializeField] int GainPlayer;
    [SerializeField] int GainEnemy;
    [SerializeField] int ExtraGainPlayer = 0;
    [SerializeField] int ExtraGainEnemy = 0;
    [SerializeField] int ExtraGainHuman;
    [SerializeField] int ExtraGainGolem;    
    [SerializeField] ResuorceManager Wallet;
    [SerializeField] SpawnUnitsManager SUM;
    private void Start()
    {
        SUM = FindAnyObjectByType<SpawnUnitsManager>();
        Wallet = FindAnyObjectByType<ResuorceManager>();
        ExtraGainHuman = 2;
        ExtraGainGolem = 3;
    }

    public void Update()
    {
        if(FindAnyObjectByType<Selection>()._SelectionUnit == this.gameObject)
            this.transform.GetChild(1).gameObject.SetActive(true);
        else
            this.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void ImproveGoldGain()
    {
        if (this.gameObject.tag == "Player")
        {
            if (Wallet._GoldPlayer >= 5)
            {
                Wallet._GoldPlayer -= 5;
                Wallet.ChangeTextGold();
                ValueExtraGain();
                TakeExtraGain();
            }
        }
        else
        {
            if (Wallet._GoldEnemy >= 5)
            {
                Wallet._GoldEnemy -= 5;
                Wallet.ChangeTextGold();
                ValueExtraGain();
                TakeExtraGain();
            }
        }
    }
    public void TakeExtraGain()
    {
        if (this.gameObject.tag == "Player")
            GainPlayer += ExtraGainPlayer;
        else
            GainEnemy += ExtraGainEnemy;
    }

    public void ValueExtraGain()
    {
        if (this.gameObject.tag == "Player")
        {
            if (this.gameObject.name == "Golem_gold(Clone)")
                ExtraGainPlayer = ExtraGainGolem;
            if (this.gameObject.name == "Human_gold(Clone)")
                ExtraGainPlayer = ExtraGainHuman;
        }
        if (this.gameObject.tag == "Enemy")
        {
            if (this.gameObject.name == "Golem_gold(Clone)")
                ExtraGainEnemy = ExtraGainGolem;
            if (this.gameObject.name == "Human_gold(Clone)")
                ExtraGainEnemy = ExtraGainHuman;
        }
    }

    public void GainGoldPlayer()
    {
        Wallet = FindAnyObjectByType<ResuorceManager>();
        Wallet._GoldPlayer += GainPlayer;
    }

    public void GainGoldEnemy()
    {
        Wallet = FindAnyObjectByType<ResuorceManager>();
        Wallet._GoldEnemy += GainEnemy; 
    }
}
