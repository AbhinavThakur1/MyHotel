using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCAI : MonoBehaviour
{
    public bool leaving = false;
    public bool roomTaken = false;
    public bool canTake = false;
    NavMeshAgent nav;
    AImanager aiManager;
    RoomAI roomAI;
    Animator animator;
    void Start()
    {
        aiManager = FindFirstObjectByType<AImanager>();
        animator = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        if (!leaving)
        {
            nav.SetDestination(GameObject.FindGameObjectWithTag("FirstStop").transform.position);
        }
    }

    void Update()
    {
        if (nav.pathEndPosition == transform.position)
        {
            animator.SetBool("Walk", false);
        }
        else
        {
            animator.SetBool("Walk", true);
        }
        if (leaving)
        {
            if (nav.pathEndPosition == transform.position)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (!roomTaken)
            {
                foreach (RoomAI room in FindObjectsByType<RoomAI>(FindObjectsSortMode.None))
                {
                    if (!room.occupied && room.clean)
                    {
                        if (roomAI == null)
                        {
                            roomAI = room;
                        }
                        else
                        {
                            if (Vector3.Distance(transform.position, roomAI.transform.position) > Vector3.Distance(transform.position, room.transform.position))
                            {
                                roomAI = room;
                            }
                        }
                    }
                }
            }
            if (roomAI != null)
            {
                if (!roomAI.occupied && roomAI.clean)
                {
                    if (nav.pathEndPosition == transform.position)
                    {
                        canTake = true;
                    }
                    if (canTake && FindFirstObjectByType<PlayerController>().canGive)
                    {
                        roomTaken = true;
                        nav.SetDestination(roomAI.transform.position);
                    }
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.GetComponent<NPCAI>() != null)
        {
            GetComponent<CapsuleCollider>().isTrigger = true;
        }
        else
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
        }
        if (collision.collider.CompareTag("FirstStop"))
        {
            canTake = true;
        }
        else
        {
            canTake = false;
        }
    }


}
