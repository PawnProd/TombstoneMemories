using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PNJ : MonoBehaviour
{
    public DialogController dialogController;

    public List<Interaction> events;

    public Vector3 dialoBoxPos;

    public bool interactWithPlayer = false;

    private void Start()
    {
        dialogController = GameController.Instance.dialogController;
    }

    public abstract string GetText();

    public abstract void AddInteraction();

    public abstract void EndDialog();

    public void Interact()
    {
        if (!dialogController.DialogIsRunning())
        {
            dialogController.CreateDialogBox(dialoBoxPos);
            interactWithPlayer = true;
            string text = GetText();

            if (text != null)
                dialogController.AddText(text);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().lastTriggerEnter = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().lastTriggerEnter = null;
        }
    }
}

[System.Serializable]
public class Interaction
{
    public string interactionText;
    public UnityEvent interactionsEvent;
}
