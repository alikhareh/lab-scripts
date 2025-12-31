using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class NodeManager : MonoBehaviour
{

    public UnityEvent action, outAction;
    public GraphicRaycaster raycaster;   // روی Canvas قرار بده

    public Color highlightColor = Color.black;

    public Transform cam, image;
    public EventSystem eventSystem;      // در صحنه باید وجود داشته باشد
    public string targetTag = "Node"; // آیتم UI باید این Tag را داشته باشد
    public float durationSpeed = 1.5f;
    
    private GameObject currentUI;
    Coroutine rotateRoutine;


    void Update()
    {
        // مرکز صفحه
        Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
        PointerEventData pointerData = new PointerEventData(eventSystem)
        {
            position = screenCenter
        };

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(pointerData, results);

        // آیا آیتم مورد نظر برخورد کرده؟
        GameObject hitObj = null;

        foreach (var result in results)
        {
            if (result.gameObject.CompareTag(targetTag))
            {
                hitObj = result.gameObject;
                break;
            }
        }

        if (hitObj != null)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                DoAction();
            }

            if (currentUI != hitObj)
            {
                ExitUI();
                EnterUI(hitObj);
            }
        }
        else
        {
            if (currentUI != null)
            {
                ExitUI();
                if (outAction != null)
                {
                    outAction.Invoke();
                }
            }
        }
    }

    void LateUpdate()
    {
        image.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);
    }

    void DoAction()
    {
        // عمل دلخواه اینجا
        action.Invoke();
    }

    void EnterUI(GameObject target)
    {
        currentUI = target;
        image.GetComponent<Image>().color = highlightColor;
        Debug.Log("UI وارد محدوده شد");
            
    }

    void ExitUI()
    {
        image.GetComponent<Image>().color = Color.white;
        Debug.Log("UI از محدوده خارج شد");
        currentUI = null;
    }
}
