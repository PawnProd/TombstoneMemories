using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cierge : PNJ {

    [Header("State")]
    public bool giveFragment = false;
    public bool waitEndFragmentDialog = false;

    [Header("Dialogs")]
    public string idleText;
    public string getFragment;
    public string afterFragmentText;


    public override string GetText()
    {
        if (!giveFragment)
        {
            AddInteraction();
            return idleText;
        }
        else
        {
            return afterFragmentText;
        }
    }

    public override void AddInteraction()
    {
        if (GameController.Instance.player.InventoryContainItem("Torche"))
        {
            dialogController.AddEvent(events[0]);
            dialogController.DoubleDialog(true);
        }
    }

    public override void EndDialog()
    {
        if (waitEndFragmentDialog)
        {
            waitEndFragmentDialog = false;
            GameController.Instance.SetFragment();
        }

    }

    public void GiveFragment()
    {
        giveFragment = true;
        dialogController.AddText(getFragment);
        dialogController.DoubleDialog(false);
        waitEndFragmentDialog = true;

    }
}
