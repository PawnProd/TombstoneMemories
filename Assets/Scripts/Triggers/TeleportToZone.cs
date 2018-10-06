using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportToZone : MonoBehaviour {

    public Transform teleportPoint;

    public GameObject info;

    public bool playerIsEnter;

    private void Update()
    {
        if (AthManager.Instance.endFadeIn && playerIsEnter)
        {
            GameController.Instance.TeleportPlayer(teleportPoint.position);
            playerIsEnter = false;
        }
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
