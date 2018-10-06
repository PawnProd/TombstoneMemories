using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Colliding")]
    public GameObject lastTriggerEnter;

    [Header("Movement")]
    public float horizontal;
    public float vertical;
    public float moveSpeed;

    [Header("Inventory")]
    public string[] inventory;

    [Header("States")]
    public bool canMove = true;
    public bool firstInteraction = true;
    public bool isTeleported = false;

	// Use this for initialization
	void Start () {
        inventory = new string[5];
	}
	
	// Update is called once per frame
	void Update () {
       
        horizontal = Input.GetAxisRaw(GameController.Instance.horizontal);
        vertical = Input.GetAxisRaw(GameController.Instance.vertical);
        if(canMove)
        {
            if (horizontal > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (horizontal < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
     
        if (lastTriggerEnter != null)
        {
            if (Input.GetButtonDown(GameController.Instance.action))
            {
                if(!GameController.Instance.dialogController.DialogIsRunning())
                {
                    if (lastTriggerEnter.GetComponent<PNJ>() && lastTriggerEnter.activeSelf)
                    {
                        canMove = false;
                        PNJ pnj = lastTriggerEnter.GetComponent<PNJ>();
                        pnj.Interact();
                    }
                    else if (lastTriggerEnter.GetComponent<TeleportToZone>() && canMove)
                    {
                        lastTriggerEnter.GetComponent<TeleportToZone>().Teleport();
                        canMove = false;
                    }
                }
                else
                {
                    if (!GameController.Instance.dialogController.InInteraction())
                        GameController.Instance.dialogController.NextDialogue();
                    else
                    {
                        GameController.Instance.dialogController.ValidateChoice();
                        firstInteraction = true;
                    }
                        
                }
  
            }
            else if(GameController.Instance.dialogController.InInteraction())
            {
                if(firstInteraction)
                {
                    GameController.Instance.dialogController.dialogBoxSystem.MoveSelector(0);
                    firstInteraction = false;
                }
                if (vertical == -1)
                {
                    GameController.Instance.dialogController.dialogBoxSystem.MoveSelector(1);
                }
                else if(vertical == 1)
                {
                    GameController.Instance.dialogController.dialogBoxSystem.MoveSelector(0);
                }
                
            }
        }
    }

    public void ClearInventory()
    {
        for(int i = 0; i < inventory.Length; ++i)
        {
            inventory[i] = null;
            AthManager.Instance.RemoveItem(i);
        }
    }

    public void AddToInventory(Sprite objSprite)
    {
        int i = 0;
        bool find = false;
        while (i < inventory.Length && !find)
        {
            if (inventory[i] == null)
            {
                inventory[i] = objSprite.name;
                find = true;
            }
            ++i;
        }
        AthManager.Instance.AddItem(i-1, objSprite);
        
    }

    public void RemoveFromInventory(Sprite objSprite)
    {
        int i = 0;
        bool find = false;
        while(i < inventory.Length && !find)
        {
            if(inventory[i] == objSprite.name)
            {
                inventory[i] = null;
                find = true;
            }
            ++i;
        }

        AthManager.Instance.RemoveItem(i - 1);
    }

    public bool InventoryContainItem(string itemName)
    {
        int i = 0;
        bool find = false;

        while(i < inventory.Length && !find)
        {
            if(inventory[i] == itemName)
            {
                find = true;
            }
            ++i;
        }

        return find;
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            Vector2 movement = new Vector2(horizontal, vertical);
            transform.Translate(movement * moveSpeed * Time.fixedDeltaTime);
        }
      

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.GetComponent<SpriteRenderer>())
        {
            int order = collision.GetComponent<SpriteRenderer>().sortingOrder;
            if (collision.transform.position.y > transform.position.y)
            {
                GetComponent<SpriteRenderer>().sortingOrder = order + 1;

            }
            else
            {
                GetComponent<SpriteRenderer>().sortingOrder = order - 1;
            }
        }
    }
}
