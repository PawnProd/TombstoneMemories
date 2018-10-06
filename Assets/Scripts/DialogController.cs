using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogController : MonoBehaviour {

    public GameObject dialogBoxPrefab;
    public GameObject buttonPrefab;

    [HideInInspector]
    public DialogueSystem dialogBoxSystem;


    public void CreateDialogBox(Vector3 position)
    {
        dialogBoxSystem = Instantiate(dialogBoxPrefab, transform).GetComponent<DialogueSystem>();
        dialogBoxSystem.GetComponent<RectTransform>().anchoredPosition = position;
    }

    public void DoubleDialog(bool continueAfterInte = false)
    {
        dialogBoxSystem.continueAfterInteraction = continueAfterInte;
    }

    public void AddText(string text)
    {
        dialogBoxSystem.InitDialog(text);
    }

    public void AddEvent(Interaction interaction)
    {
        dialogBoxSystem.CreateButton(buttonPrefab, interaction.interactionText, interaction.interactionsEvent);
        dialogBoxSystem.getInteraction = true;
    }

    public void ValidateChoice()
    {
        dialogBoxSystem.SelectButton();
    }

    public bool DialogIsRunning()
    {
        return (dialogBoxSystem != null);
    }

    public void NextDialogue()
    {
        dialogBoxSystem.NextDialogue();
    }

    public bool InInteraction()
    {
        if (DialogIsRunning())
            return dialogBoxSystem.inInteraction;
        else
            return false;
    }
}
