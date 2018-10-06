using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : PNJ {

    public string idleText;

    public void Reset()
    {
        transform.parent.gameObject.SetActive(true);
    }


    public override string GetText()
    {
        AddInteraction();
         return idleText;
    }

    public override void AddInteraction()
    {
        if (GameController.Instance.player.InventoryContainItem("Key"))
        {
            dialogController.AddEvent(events[0]);
        }
    }

    public override void EndDialog()
    {

    }
}
