using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;

public class TashCityManager : MonoBehaviour
{
    [SerializeField] GameObject Game, shop;
    [SerializeField] TMP_Text message;
    [SerializeField] AudioClip moneyclip;
    int vehicleCount = 1;
    public int money = 0;
    [SerializeField] GameObject trashtruck,spawnpoint;
    const int VEHICLECOST = 10;
    public GameObject[] trashpath1;
    public GameObject[] trashpath2;
    public GameObject[] trashpath3;
    public GameObject[] trashpath4;
    public int stage = 1;
    public int trash1;
    public int trash2;
    public int trash3;
    public int trash4;

    private void Start()
    {
        if (PlayerPrefs.HasKey("stage"))
        {
            stage = PlayerPrefs.GetInt("stage");
        }
        else
        {
            PlayerPrefs.SetInt("stage", stage);
        }
        if (PlayerPrefs.HasKey("truck"))
        {
            vehicleCount = PlayerPrefs.GetInt("truck");
        }
        else
        {
            PlayerPrefs.SetInt("truck", vehicleCount);
        }
        if (PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        else
        {
            PlayerPrefs.SetInt("money", money);
        }
        vehicalSpawner(vehicleCount);
    }

    public void trash(int number, int count)
    {
        if (number == 1)
        {
            trash1 = count;
        }
        else if (number == 2)
        {
            trash2 = count;
        }
        else if (number == 3)
        {
            trash3 = count;
        }
        else if (number == 4)
        {
            trash4 = count;
        }
    }

    public void SOundOnOff()
    {
        if (GetComponent<AudioSource>().isActiveAndEnabled)
        {
            GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            GetComponent<AudioSource>().enabled = true;
        }
    }

    public void vehicalSpawner(int number)
    {
        if (number == 1)
        {
            Spawner();
        }
        if(number == 2)
        {
            Spawner();
            Invoke("Spawner", 5f);
        }
    }

    public void Spawner()
    {
        Instantiate(trashtruck, spawnpoint.transform.position, spawnpoint.transform.rotation);
    }

    
    public void Shop()
    {
        if (vehicleCount < 2 && money > VEHICLECOST)
        {
            vehicleCount += 1;
            PlayerPrefs.SetInt("truck", vehicleCount);
            money -= VEHICLECOST;
            Invoke("Spawner", 5f);
            message.text = "Will deliver in 5 seconds!!";
            Invoke("Cleartext", 5f);
            MoneySound();
            PlayerPrefs.SetInt("money", money);
        }
        else
        {
            if (vehicleCount >= 2)
            {
                message.text = "Max Vehicle value reached!!";
                Invoke("Cleartext", 2f);
            }
            else if(money < VEHICLECOST)
            {
                message.text = "Not Enough Money!!";
                Invoke("Cleartext", 2f);
            }
        }
    }
    public void BtnClicked()
    {
        if (Game.activeSelf)
        {
            Game.SetActive(false);
            shop.SetActive(true);
        }
        else
        {
            Game.SetActive(true);
            shop.SetActive(false);
        }
    }

    void Cleartext()
    {
        message.text = "";
    }

    public void MoneySound()
    {
        GetComponent<AudioSource>().PlayOneShot(moneyclip);
    }
}
