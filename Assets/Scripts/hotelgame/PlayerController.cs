using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    float x;
    float y;
    public bool canGive = true;
    float speed = 8f;
    int drag = 8;
    float horizontal;
    float vertical;
    Vector3 direction;
    [SerializeField] Transform playerobj;
    [SerializeField] Animator animator;
    EventSystem es;
    Vector2 startingpos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.drag = drag;
        es = FindFirstObjectByType<EventSystem>();
        Input.multiTouchEnabled = false;
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.touchCount > 0)
        {
            if (!es.IsPointerOverGameObject(Input.touches[0].fingerId))
            {
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    x = 0; y = 0;
                }
                else
                {
                    x += Input.GetTouch(0).deltaPosition.x;
                    y += Input.GetTouch(0).deltaPosition.y;
                }
            }
            else
            {
                x = 0; y = 0;
            }
        }
        direction = Vector3.forward * y + x * Vector3.right;
        if (direction.magnitude > 0)
        {
            animator.SetBool("Walk", true);
            rb.AddForce(direction.normalized * (speed * 200f) * Time.deltaTime, ForceMode.Force);
            playerobj.forward = Vector3.Lerp(playerobj.forward, direction.normalized, 6f * Time.deltaTime);
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("KeyCheck"))
        {
            canGive = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("KeyCheck"))
        {
            canGive = false;
        }
    }

}
