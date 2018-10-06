using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : PNJ {

    [Header("Dialogs")]
    public string shovelText;


    public override string GetText()
    {
        AddInteraction();
        return shovelText;
    }

    public override void AddInteraction()
    {
        dialogController.AddEvent(events[0]);

    }

    public override void EndDialog()
    {
      
    }

    public void Reset()
    {
        gameObject.SetActive(true);
    }

}
