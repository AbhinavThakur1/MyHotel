using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraAI : MonoBehaviour
{
    [SerializeField] TMP_Text money, money1;
    GameObject Camera;
    EventSystem es;
    float x = 0;
    float y = 0;
    Vector3 direction;

    void Start()
    {
        Camera = FindFirstObjectByType<Camera>().gameObject;
        es = FindFirstObjectByType<EventSystem>();
    }

    void Update()
    {
        money.text = "Money: " + FindFirstObjectByType<TashCityManager>().money.ToString();
        money1.text = "Money: " + FindFirstObjectByType<TashCityManager>().money.ToString();
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
            Camera.transform.position += direction.normalized;
            if (Camera.transform.position.z > 60)
            {
                Camera.transform.position -= new Vector3(0f, 0f, direction.normalized.z);
            }
            else if (Camera.transform.position.z < -60)
            {
                Camera.transform.position -= new Vector3(0f, 0f, direction.normalized.z);
            }
            if (Camera.transform.position.x > 28)
            {
                Camera.transform.position -= new Vector3(direction.normalized.x, 0f, 0f);
            }
            else if (Camera.transform.position.x < -28)
            {
                Camera.transform.position -= new Vector3(direction.normalized.x, 0f, 0f);
            }
        }
    }
}
