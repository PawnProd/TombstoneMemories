using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectre : PNJ {

    [Header("Dialogs")]
    public string firstTimeText;
    public string idleText;
    public string getPieceText;
    public string idleGetPieceText;
    public string gardienNotMoveText;

    [Header("States")]
    public bool firstTime = true;
    public bool getPiece = false;

    public void Reset()
    {
        firstTime = true;
        getPiece = false;
        transform.parent.gameObject.SetActive(true);
    }


    public void GivePiece()
    {
        getPiece = true;
        dialogController.AddText(getPieceText);
        dialogController.DoubleDialog(false);
        GameController.Instance.MoveGardien();
    }

    public override string GetText()
    {
        if (firstTime)
        {
            return firstTimeText;
        }
        else if (!getPiece)
        {
            AddInteraction();
            return idleText;
        }
        else
        {
            if (GameController.Instance.gardien.assomer)
            {
                return gardienNotMoveText;
            }
            else
            {
                return idleGetPieceText;
            }

        }
    }

    public override void AddInteraction()
    {
        if (GameController.Instance.player.InventoryContainItem("Piece"))
        {
            dialogController.AddEvent(events[0]);
            dialogController.DoubleDialog(true);
        }
    }

    public override void EndDialog()
    {
        if (firstTime)
        {
            firstTime = false;
        }
    }
}
