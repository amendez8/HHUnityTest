using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;
    public GameObject Rock;
    public GameObject Bow;
    public GameObject Controls;
    public GameObject winPanel;
    public GameObject losePanel;
    public Button rockAttackButton;
    public Button rockEquipButton;
    public Button arrowAttackButton;
    public Button arrowEquipButton;
    public Text healthPoints;
    private Text rockAmmo;
    private Text bowAmmo;


    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        // assigning text component of the ammo
        rockAmmo = rockEquipButton.transform.Find("Ammo").GetComponent<Text>();
        bowAmmo = arrowEquipButton.transform.Find("Ammo").GetComponent<Text>();

        GameStart();
    }

    public void GameStart()
    {
        // deactivate both panels on game start/restart and activate controls.
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        Controls.SetActive(true);
    }

    // equiping and unequiping
    public void EquipRock()
    {
        // if the player has rock ammo
        if (int.Parse(rockAmmo.text.ToString()) > 0)
        {
            Rock.SetActive(true);
            rockAttackButton.gameObject.SetActive(true);
            Bow.SetActive(false);
            arrowAttackButton.gameObject.SetActive(false);
        }
    }

    // equiping and unequiping
    public void EquipBow()
    {
        // if the player has bow ammo
        if (int.Parse(bowAmmo.text.ToString()) > 0)
        {
            Rock.SetActive(false);
            rockAttackButton.gameObject.SetActive(false);
            Bow.SetActive(true);
            arrowAttackButton.gameObject.SetActive(true);
        }
    }

    public void IncreaseRockAmmo()
    {
        rockAmmo.text = (int.Parse(rockAmmo.text.ToString()) + 1).ToString();
    }

    public void DecreaseRockAmmo()
    {
        // reduce ammo by 1
        rockAmmo.text = (int.Parse(rockAmmo.text.ToString()) - 1).ToString();

        // if ammo is empty, call function to deactive weapon
        if (int.Parse(rockAmmo.text.ToString()) <= 0)
        {
            AmmoEmpty();
        }
    }

    public void IncreaseBowAmmo()
    {
        bowAmmo.text = (int.Parse(bowAmmo.text.ToString()) + 1).ToString();
    }

    public void DecreaseBowAmmo()
    {
        // reduce ammo by 1
        bowAmmo.text = (int.Parse(bowAmmo.text.ToString()) - 1).ToString();

        // if ammo is empty, call function to deactive weapon
        if (int.Parse(bowAmmo.text.ToString()) <= 0)
        {
            AmmoEmpty();
        }
    }

    public void AmmoEmpty()
    {
        // if bow has no more ammo, deactivate bow/arrow and it's attack button
        if(int.Parse(bowAmmo.text.ToString()) <= 0)
        {
            Bow.SetActive(false);
            arrowAttackButton.gameObject.SetActive(false);
        }

        // if rock has no more ammo, deactivate rock and it's attack button
        if (int.Parse(rockAmmo.text.ToString()) <= 0)
        {
            Rock.SetActive(false);
            rockAttackButton.gameObject.SetActive(false);
        }
    }

    public void DecreaseHealth()
    {
        healthPoints.text = (int.Parse(healthPoints.text.ToString()) - 1).ToString();

        if (int.Parse(healthPoints.text.ToString()) <= 0)
        {
            LoseFunction();
        }
    }

    public void WinFunction()
    {
        winPanel.SetActive(true);
        Controls.SetActive(false);
    }

    public void LoseFunction()
    {
        losePanel.SetActive(true);
        Controls.SetActive(false);
    }
}
