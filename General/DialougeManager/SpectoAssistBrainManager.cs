using System;
using System.Collections.Generic;
using RTLTMPro;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpectoAssistBrainManager : MonoBehaviour
{
    public List<DialogueNode> DialogueNodes;
    public DialogueNode currentNode;
    public DialogueManager dialogueManager;

    [Header("UI")] public bool isDialogueShowing;
    public GameObject dialoguePanel;
    

    private void Update()
    {
        Debug.Log("Count: " + DialogueNodes.Count);
        Debug.Log("DialogueManager isShowing: " + dialogueManager.isShowing);

        if (DialogueNodes.Count > 0 && !dialogueManager.isShowing)
        {
            dialogueManager.StartDialogue(DialogueNodes);
            Debug.Log("shiiiiiiiiiiiiiiiiiiiigt");            
            DialogueNodes.Clear();
        }
    }
}

/* public void ShowNode()
 {
     // اجرای eventهای ورود به نود
     if (currentNode.onEnterEvents != null)
     {
         foreach (var e in currentNode.onEnterEvents)
             e.Execute();
     }

     dialogueText.text = currentNode.text;

     // پاک کردن دکمه‌های قبلی
     foreach (Transform child in choicesParent)
         Destroy(child.gameObject);

     // ساخت دکمه‌ها
     if (currentNode != null && currentNode.choices[0].choiceText != "")
     {
         for (int i = 0; i < currentNode.choices.Count; i++)
         {
             int index = i;
             var choice = currentNode.choices[i];
             Button btn = Instantiate(choiceButtonPrefab, choicesParent);
             btn.GetComponentInChildren<TextMeshProUGUI>().text = choice.choiceText;

             btn.onClick.AddListener(() => SelectChoice(index));
         }
     }

 }

 public void SelectChoice(int index)
 {
     var choice = currentNode.choices[index];

     // اجرای eventهای انتخاب
     if (choice.onSelectEvents != null)
     {
         foreach (var e in choice.onSelectEvents)
             e.Execute();
     }

     currentNode = choice.nextNode;
     ShowNode();
 }
}*/
