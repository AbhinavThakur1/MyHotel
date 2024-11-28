using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashAI : MonoBehaviour
{
    public int trashpathnumber;
    public int count;
    [SerializeField] GameObject innertrash, outertrash;

    private void Start()
    {
        if(PlayerPrefs.GetInt("stage") > trashpathnumber)
        {
            count = 10;
            CountChecker();
        }
    }
    public void CountChecker()
    {
        if (count == 5)
        {
            innertrash.SetActive(false);
            FindFirstObjectByType<TashCityManager>().trash(trashpathnumber,1);
            FindFirstObjectByType<TashCityManager>().money += 5;
            PlayerPrefs.SetInt("money", FindFirstObjectByType<TashCityManager>().money);
        }
        else if (count == 10)
        {
            innertrash.SetActive(false);
            outertrash.SetActive(false);
            FindFirstObjectByType<TashCityManager>().trash(trashpathnumber, 2);
            FindFirstObjectByType<TashCityManager>().money += 5;
            PlayerPrefs.SetInt("money", FindFirstObjectByType<TashCityManager>().money);
        }
    }
}
