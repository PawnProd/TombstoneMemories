using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gardien : PNJ {

    [Header("Dialogs")]
    public string idleText;
    public string assomerText;
    public string spectreText;

    [Header("States")]
    public bool waitGetKeys = false;
    public bool assomer = false;
    public bool moved = false;

    private Vector3 spawnPosition;
    private Vector3 spawnDialogBoxPos;


    private void Start()
    {
        spawnPosition = transform.position;
        spawnDialogBoxPos = dialoBoxPos;
    }

    public void AssomerGardien()
    {
        dialogController.AddText(assomerText);
        dialogController.DoubleDialog(false);
        waitGetKeys = true;
    }

    public void Reset()
    {
        waitGetKeys = false;
        assomer = false;
        moved = false;
        transform.parent.position = spawnPosition;
        dialoBoxPos = spawnDialogBoxPos;
    }

    public override string GetText()
    {
        if(!assomer)
        {
            AddInteraction();
            if (!moved)
            {
                return idleText;
            }
            else
            {
                return spectreText;
            }
        }
        else
        {
            return "";
        }
    }

    public override void AddInteraction()
    {
        if (GameController.Instance.player.InventoryContainItem("Shovel"))
        {
            dialogController.DoubleDialog(true);
            dialogController.AddEvent(events[0]);
        }  
    }

    public override void EndDialog()
    {
        if (waitGetKeys)
        {
            waitGetKeys = false;
            assomer = true;
        }
    }



}
