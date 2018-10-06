using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Oracle : PNJ {

    [Header("State")]
    public bool firstTime = true;
    public bool giveFragment = false;
    public bool waitEndFragmentDialog = false;

    [Header("Dialogs")]
    public string firsTimeText;
    public string beforeGiveFragment;
    public string getPiece;
    public string idleText;



    public void GiveFragment()
    {
        giveFragment = true;
        dialogController.AddText(getPiece);
        dialogController.DoubleDialog(false);
        waitEndFragmentDialog = true;
        
    }

    public void Reload()
    {
        dialogController.dialogBoxSystem.CloseDialog();
    }

    public override string GetText()
    {
        if (firstTime)
        {
            return firsTimeText;

        }
        else if (!giveFragment)
        {
            AddInteraction();
            return beforeGiveFragment;
        }
        else if (!waitEndFragmentDialog)
        {
            AddInteraction();
            return idleText;
        }
        else
        {
            return "";
        }
    }

    public override void AddInteraction()
    {
        
        if (GameController.Instance.player.InventoryContainItem("Piece"))
        {
            dialogController.AddEvent(events[0]);
            dialogController.DoubleDialog(true);
        }
        dialogController.AddEvent(events[1]);
    }

    public override void EndDialog()
    {
        if (waitEndFragmentDialog)
        {
            waitEndFragmentDialog = false;
            GameController.Instance.SetFragment();
        }
        if (firstTime)
        {
            firstTime = false;
            AthManager.Instance.SetGoalText("Trouver les fragments d'âme : 0/2");
        }
    }
}
