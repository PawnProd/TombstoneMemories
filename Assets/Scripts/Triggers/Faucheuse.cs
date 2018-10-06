using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faucheuse : PNJ {

    [Header("State")]
    public bool firstTime = true;
    public bool moved = false;

    [Header("Dialogs")]
    public string firsTimeText;
    public string fragmentText;
    public string endText;

    private Vector3 spawnPosition;
    private Vector3 spawnDialogBoxPos;


    private void Start()
    {
        spawnPosition = transform.position;
        spawnDialogBoxPos = dialoBoxPos;
    }


    public void Reset()
    {
        moved = false;
        dialoBoxPos = spawnDialogBoxPos;
        transform.parent.position = spawnPosition;
    }

    public override string GetText()
    {
        if (firstTime && !moved)
        {
            return firsTimeText;
        }
        else if (GameController.Instance.nbFragmentGet >= 2)
        {
            if (!GameController.Instance.cadavre.burned)
            {
                return fragmentText;
            }
            else
            {
                AddInteraction();
                return endText;
            }
        }
        else
        {
            return "";
        }
    }

    public override void AddInteraction()
    {
        dialogController.AddEvent(events[0]);
    }

    public override void EndDialog()
    {
        if (firstTime)
        {
            firstTime = false;
        }
    }

}
