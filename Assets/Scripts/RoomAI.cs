using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomAI : MonoBehaviour
{
    public bool occupied = false;
    [SerializeField] GameObject roof,text;
    public bool clean = true;
    const int ROOMCHARGES = 2;
    [SerializeField] GameObject npc;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<NPCAI>() != null)
        {
            if (!occupied && !other.gameObject.GetComponent<NPCAI>().leaving)
            {
                Invoke("Leave", 15f);
                FindFirstObjectByType<AImanager>().money += ROOMCHARGES;
                FindFirstObjectByType<AImanager>().MoneySoundEffect();
                PlayerPrefs.SetInt("money", FindFirstObjectByType<AImanager>().money);
                occupied = true;
                clean = false;
                roof.SetActive(true);
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.GetComponent<PlayerController>() != null)
        {
            if (!clean && !occupied)
            {
                clean = true;
                roof.SetActive(true);
                text.SetActive(false);
            }
        }
    }

    private void Leave()
    {
        GameObject npcLeaving =Instantiate(npc, gameObject.transform.position , Quaternion.identity);
        npcLeaving.GetComponent<NavMeshAgent>().SetDestination(GameObject.FindGameObjectWithTag("Respawn").transform.position);
        npcLeaving.GetComponent<NPCAI>().leaving = true;
        npcLeaving.GetComponent<NPCAI>().canTake = false;
        occupied = false;
        roof.SetActive(false);
        text.SetActive(true);
    }
}
