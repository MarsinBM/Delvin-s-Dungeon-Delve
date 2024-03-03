using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    // Variables
    private int chosenUpgrade;

    [SerializeField] Image uIcon;
    [SerializeField] TMP_Text uName;
    [SerializeField] GameObject UpgradePanel;

    public UpgradeManager umanager;

    [SerializeField] Sprite attackUp;
    [SerializeField] Sprite evadeUp;
    [SerializeField] Sprite healthUp;
    [SerializeField] Sprite hpPotion;
    [SerializeField] Sprite sdPotion;
    [SerializeField] Sprite shield;

    void Start()
    {
        chosenUpgrade = Random.Range(1, 7);
        SetUpgrade(chosenUpgrade);  
    }

    void SetUpgrade(int upgrade)
    {
        switch (upgrade)
        {
            case 1:
                uIcon.sprite = attackUp;
                uName.text = "ATTACK UP";
                break;
            case 2:
                uIcon.sprite = evadeUp;
                uName.text = "EVADE UP";
                break;
            case 3:
                uIcon.sprite = healthUp;
                uName.text = "HEALTH UP";
                break;
            case 4:
                uIcon.sprite = hpPotion;
                uName.text = "HEALTH POTION";
                break;
            case 5:
                uIcon.sprite = sdPotion;
                uName.text = "SPEED POTION";
                break;
            case 6:
                uIcon.sprite = shield;
                uName.text = "HOLY SHIED";
                break;
        }
    }

    public void OnClickUpgrade()
    {
        switch (chosenUpgrade)
        {
            case 1:
                umanager.AttackUp();
                break;
            case 2:
                umanager.EvadeUp();
                break;
            case 3:
                umanager.HealthUp();
                break;
            case 4:
                umanager.GiveHealthPotion();
                break;
            case 5:
                umanager.GiveSpeedPotion();
                break;
            case 6:
                umanager.HolyShield();
                break;
        }
    }

    public void ClosePanel()
    {
        Destroy(UpgradePanel);
    }

}
