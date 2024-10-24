using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBarUI : MonoBehaviour
{
    [SerializeField] Image HPBar;
    [SerializeField] Image IconAction;
    [SerializeField] Camera cam;
    float maxhp;
    float hp;
    float maxAction;
    float action;
    private void Start()
    {
        cam = Camera.main;
        maxhp = this.transform.parent.GetComponent<BaseUnit>()._MaxHP;
        hp = this.transform.parent.GetComponent<BaseUnit>()._HP;
        maxAction = this.transform.parent.GetComponent<BaseUnit>()._MaxTimeToAction;
        action = this.transform.parent.GetComponent<BaseUnit>()._TimeToAction;
    }

    private void Update()
    {
        this.transform.LookAt(cam.transform.position);
    }
    public void ChangeHPUI()
    {
        hp = this.transform.parent.GetComponent<BaseUnit>()._HP;
        HPBar.fillAmount = hp / maxhp;  
    }
    public void ChageTimeToAction()
    {
        action = this.transform.parent.GetComponent<BaseUnit>()._TimeToAction;
        IconAction.fillAmount = action / maxAction;
    }
}
