using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cadavre : PNJ {

    [Header("State")]
    public bool getPiece = true;
    public bool burned = false;

    [Header("Dialogs")]
    public string getPieceText;
    public string notGetPieceText;
    public string burnText;

    public void Reset()
    {
        gameObject.SetActive(true);
        getPiece = true;
        burned = false;
    }

    public void GetPiece()
    {
        getPiece = false;
    }

    public void Burn()
    {
        burned = true;
        gameObject.SetActive(false);
        dialogController.AddText(burnText);
        GameController.Instance.MoveFaucheuse();
    }

    public override string GetText()
    {
        if (getPiece)
        {
            AddInteraction();
            return getPieceText;

        }
        else
        {
            AddInteraction();
            return notGetPieceText;
        }
    }

    public override void AddInteraction()
    {
        if (GameController.Instance.player.InventoryContainItem("Torche"))
        {
            dialogController.AddEvent(events[0]);
        }
        if(getPiece)
            dialogController.AddEvent(events[1]);
    }

    public override void EndDialog()
    {
        
    }
}
