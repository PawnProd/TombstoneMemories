using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torche : PNJ {

    [Header("Dialogs")]
    public string torcheText;


    public override string GetText()
    {
        AddInteraction();
        return torcheText;
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
