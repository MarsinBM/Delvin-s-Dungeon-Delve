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

    [SerializeField] Sprite attackUp;
    [SerializeField] Sprite evadeUp;
    [SerializeField] Sprite healthUp;
    [SerializeField] Sprite hpPotion;
    [SerializeField] Sprite sdPotion;
    [SerializeField] Sprite shield;

    private Player player;

    void Start()
    {
        chosenUpgrade = Random.Range(1, 7);
        SetUpgrade(chosenUpgrade);

        GameObject ThePlayer = GameObject.Find("Player");
        player = ThePlayer.GetComponent<Player>();
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
                uName.text = "HOLY SHIELD";
                break;
        }
    }

    public void OnClickUpgrade()
    {
        switch (chosenUpgrade)
        {
            case 1:
                UpgradeManager.instance.AttackUp();
                break;
            case 2:
                UpgradeManager.instance.EvadeUp();
                break;
            case 3:
                UpgradeManager.instance.HealthUp();
                break;
            case 4:
                UpgradeManager.instance.GiveHealthPotion();
                break;
            case 5:
                UpgradeManager.instance.GiveSpeedPotion();
                break;
            case 6:
                UpgradeManager.instance.HolyShield();
                break;
        }
    }

    public void ClosePanel()
    {
        Destroy(UpgradePanel);
        player.isUpgradeOpen = false;
    }

}
