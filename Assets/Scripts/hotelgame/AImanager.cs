using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AImanager : MonoBehaviour
{
    [SerializeField] GameObject[] allRooms;
    int roomBought = 0;
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject spawnObject;
    [SerializeField] GameObject gameUI, shopUI;
    [SerializeField] TMP_Text message, money1,money2;
    [SerializeField] AudioClip moneySound;
    [SerializeField] AudioSource audioSource;
    public RoomAI[] rooms;
    public int money;
    const int ROOMBUYCHARGES = 20;
    float time;
    RoomAI roomopen;

    private void Start()
    {
        if (PlayerPrefs.HasKey("rooms"))
        {
            roomBought = PlayerPrefs.GetInt("rooms");
        }
        else
        {
            PlayerPrefs.SetInt("rooms",roomBought);
        }
        if (PlayerPrefs.HasKey("money"))
        {
            money = PlayerPrefs.GetInt("money");
        }
        else
        {
            PlayerPrefs.SetInt("money", money);
        }
        for (int i = 0; i <= roomBought; i++)
        {
            allRooms[i].SetActive(true);
        }
    }

    void FixedUpdate()
    {
        time += Time.deltaTime;
        if (time >= 2f)
        {
            time = 0;
            NpcSpawnner();
        }
        money1.text = "Money: " + money.ToString();
        money2.text = "Money: " + money.ToString();
    }

    public void NpcSpawnner()
    {
        rooms = FindObjectsByType<RoomAI>(FindObjectsSortMode.None);
        foreach (RoomAI room in rooms)
        {
            if (room != null)
            {
                if (!room.occupied && room.clean)
                {
                    roomopen = room;
                }
            }
        }
        if (roomopen != null)
        {
            if (FindFirstObjectByType<NPCAI>() == null)
            {
                Instantiate(spawnObject, spawnPoint.position, Quaternion.identity);
            }
        }
    }

    public void PanelChange()
    {
        if (gameUI.activeSelf)
        {
            gameUI.SetActive(false);
            shopUI.SetActive(true);
        }
        else
        {
            gameUI.SetActive(true);
            shopUI.SetActive(false );
        }
    }

    public void Shop()
    {
        if(money >= ROOMBUYCHARGES && roomBought < 5)
        {
            money -= ROOMBUYCHARGES;
            MoneySoundEffect();
            PlayerPrefs.SetInt("money", money);
            roomBought += 1;
            allRooms[roomBought].SetActive(true);
            PlayerPrefs.SetInt("rooms", roomBought);
        }
        else if(money < ROOMBUYCHARGES)
        {
            message.text = "Not enough money!!";
            Invoke("TextReset", 2f);
        }
        else if(roomBought == 5)
        {
            message.text = "Maximum room reached!!";
            Invoke("TextReset", 2f);
        }
    }

    void TextReset()
    {
        message.text = "";
    }

    public void SoundONOff()
    {
        if (audioSource.isActiveAndEnabled)
        {
            audioSource.enabled = false;
        }
        else
        {
            audioSource.enabled = true;
        }
    }

    public void MoneySoundEffect()
    {
        if (audioSource.isActiveAndEnabled)
        {
            audioSource.PlayOneShot(moneySound);
        }
    }
}
