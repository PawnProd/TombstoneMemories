using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cave : PNJ
{

    public Transform teleportPoint;

    public GameObject info;

    public bool playerIsEnter;
    public bool unlocked = false;

    public string gardienNoMoved;
    public string noKey;


    private void Update()
    {
        if (AthManager.Instance.endFadeIn && playerIsEnter)
        {
            GameController.Instance.TeleportPlayer(teleportPoint.position);
            playerIsEnter = false;
        }
    }

    public override string GetText()
    {
        if (!GameController.Instance.gardien.moved)
        {
            return gardienNoMoved;
        }
        else if (!unlocked)
        {
            AddInteraction();
            return noKey;
        }
        else
        {
            Teleport();
            return "";
        }
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

    public void Reset()
    {
        unlocked = false;
    }

    public void UnlockCave()
    {
        unlocked = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().lastTriggerEnter = gameObject;
            playerIsEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().lastTriggerEnter = null;
            playerIsEnter = false;
        }
    }

    public void Teleport()
    {
        AthManager.Instance.FadeIn();
    }
}

