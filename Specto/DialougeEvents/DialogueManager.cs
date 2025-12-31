using System.Collections.Generic;
using RTLTMPro;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class DialogueManager : MonoBehaviour
{
    public RTLTextMeshPro dialogueText;
    public GameObject panel; // پنل دیالوگ
    public Image assistAvatar;


    public List<DialogueNode> dialogues; // لیست دیالوگ‌ها از Inspector
    private int currentIndex = 0;
    public bool isShowing = false;

    private void Awake()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (isShowing && Input.GetKeyDown(KeyCode.Return))
        {
            ShowNext();
        }
    }

    public void StartDialogue(List<DialogueNode> dialogueList)
    {
        if (dialogueList.Count == 0) return;

        dialogues = new List<DialogueNode>(dialogueList);
        currentIndex = 0;
        panel.SetActive(true);
        isShowing = true;
        ShowNext();
    }

    private void ShowNext()
    {
        if (currentIndex >= dialogues.Count)
        {
            EndDialogue();
            return;
        }

        dialogueText.text = dialogues[currentIndex].text;
        if(dialogues[currentIndex].sprite != null)
            assistAvatar.sprite = dialogues[currentIndex].sprite;
        currentIndex++;
    }

    private void EndDialogue()
    {
        panel.SetActive(false);
        isShowing = false;
    }
}