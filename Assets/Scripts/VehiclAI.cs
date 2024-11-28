using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class VehiclAI : MonoBehaviour
{

    [SerializeField] GameObject trash;
    TashCityManager manager;
    GameObject[] path;
    NavMeshAgent nav;
    int checkpoint = 0;
    int trashpath = 1;
    float time;
    trashAI trashAi;

    void Start()
    {
        manager = FindFirstObjectByType<TashCityManager>();
        PathSelecter();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if(path != null)
        {
            PathChecker(checkpoint);
        }
    }

    void PathSelecter()
    {
        if (manager.trash1 != 2)
        {
            path = manager.trashpath1;
            trashpath = 1;
        }
        else if (manager.trash2 != 2)
        {
            path = manager.trashpath2;
            trashpath = 2;
        }
        else if (manager.trash3 != 2)
        {
            path = manager.trashpath3;
            trashpath = 3;
        }
        else if (manager.trash4 != 2)
        {
            path = manager.trashpath4;
            trashpath = 4;
        }
        else
        {
            path = null;
        }
        manager.stage = trashpath;
        PlayerPrefs.SetInt("stage", trashpath);
        foreach (trashAI t in FindObjectsOfType<trashAI>())
        {
            if(t.trashpathnumber == trashpath)
            {
                trashAi = t;
            }
        }
    }

    private void PathChecker(int checkPoint)
    {
        nav.SetDestination(path[checkPoint].transform.position);
        if (nav.pathEndPosition == transform.position)
        {
            PathSelecter();
            time += Time.deltaTime;
            if (checkpoint == 3)
            {
                if (!trash.activeSelf)
                {
                    trash.SetActive(true);
                }
            }
            if (checkpoint  < 4)
            {
                if (time >= 2)
                {
                    Debug.Log("works;");
                    trashAi.count += 1;
                    trashAi.CountChecker();
                    checkpoint += 1;
                    time = 0;
                }
            }
            else
            {
                if (time >= 2)
                {
                    trashAi.count += 1;
                    trashAi.CountChecker();
                    PathSelecter();
                    manager.money += 5;
                    PlayerPrefs.SetInt("money", manager.money);
                    manager.MoneySound();
                    checkpoint = 0;
                    trash.SetActive(false);
                    time = 0;
                }
            }
        }
    }
}
