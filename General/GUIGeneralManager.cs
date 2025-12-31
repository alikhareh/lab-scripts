using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class CenterScreenUIInteractor : MonoBehaviour
{
    public Camera cam;
    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;

    public string targetTag = "Node";
    public Animator animator;

    private GameObject currentUI;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector2 center = new Vector2(Screen.width / 2f, Screen.height / 2f);

        PointerEventData data = new PointerEventData(eventSystem);
        data.position = center;

        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(data, results);

        GameObject hit = null;

        foreach (var r in results)
        {
            if (r.gameObject.CompareTag(targetTag))
            {
                hit = r.gameObject;
                break;
            }
        }

        if (hit != null)
        {
            if (currentUI != hit)
            {
                ExitUI();
                EnterUI(hit);
            }

            if (Input.GetMouseButtonDown(0))
            {
                ExecuteEvents.Execute(
                    hit,
                    new PointerEventData(eventSystem),
                    ExecuteEvents.submitHandler
                );
            }
        }
        else
        {
            if (currentUI != null)
                ExitUI();
        }
    }

    void EnterUI(GameObject ui)
    {
        currentUI = ui;
        if (animator) animator.SetBool("isSelected", true);
    }

    void ExitUI()
    {
        if (animator) animator.SetBool("isSelected", false);
        currentUI = null;
    }
}