using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance { private set; get; }
    public Player player;
    public Vector3 playerSpawnPosition;
    public GameObject endGamePanel;
    public DialogController dialogController;

    public bool keyboard = true;

    public int nbFragmentGet = 0;

    [Header("Input Name")]
    public string horizontal;
    public string vertical;
    public string action;
    public string undo;

    [Header("Triggers")]
    public Oracle oracle;
    public Shovel shovel1;
    public Shovel shovel2;
    public Cadavre cadavre;
    public Gardien gardien;
    public Spectre spectre;
    public Torche torche;
    public Gate gate;
    public Eboulement eboulement;
    public Cierge cierge;
    public Faucheuse faucheuse;
    public Cave cave;

    [Header("Evt Info")]
    public Transform newGardienPosition;
    public Transform newFaucheusePosition;


    public bool resetGame = false;


    private AthManager athManager;
	// Use this for initialization
	void Awake () {
        Instance = this;

	}

    private void Start()
    {
        playerSpawnPosition = player.transform.position;
        athManager = AthManager.Instance;
    }
    // Update is called once per frame
    void Update () {
		if(athManager.endFadeIn && resetGame)
        {
            player.transform.position = playerSpawnPosition;
            StartCoroutine(CO_WaitTeleport());
            resetGame = false;
        }
	}

    public void SetInput()
    {
        if (keyboard)
        {
            horizontal = InputName.Keyboard_Horizontal;
            vertical = InputName.Keyboard_Vertical;
            action = InputName.Keyboard_Action;
            undo = InputName.Keyboard_Undo;
        }
        else
        {
            horizontal = InputName.Joystick_Horizontal;
            vertical = InputName.Joystick_Vertical;
            action = InputName.Joystick_Action;
            undo = InputName.Joystick_Undo;
        }
    }

    public void EndGame()
    {
        endGamePanel.GetComponent<Animator>().SetBool("FadeIn", true);
    }

    public void SetFragment()
    {
        ++nbFragmentGet;
        athManager.SetGoalText("Trouver les fragments d'âme : " + nbFragmentGet + "/2");
        if(nbFragmentGet >= 2)
        {
            athManager.SetGoalText("Trouver le lieu de vôtre mort.");
        }
    }

    public void FindDeadZone()
    {
        if(nbFragmentGet >= 2)
        {
            athManager.SetGoalText("Trouver la paix.");
        }
    }

    public void ResetGameState()
    {
        athManager.FadeIn();

        //Reset Gardien
        gardien.Reset();

        // Reset faucheuse
        faucheuse.Reset();

        // Reset cave
        cave.Reset();

        // Spectre Reset
        spectre.Reset();

        //Torche Reset
        torche.Reset();

        //Gate Reset
        gate.Reset();

        // Shovel 1 & 2 Reset
        shovel1.Reset();
        shovel2.Reset();

        // Cadavre Reset
        cadavre.Reset();

        // Eboulement Reset
        eboulement.Reset();

        player.ClearInventory();

        resetGame = true;

    }

    public void MoveGardien()
    {
        if(!gardien.assomer)
        {
            gardien.transform.parent.position = newGardienPosition.position;
            gardien.dialoBoxPos = new Vector3(-12.2f, 9.38f, 0);
            gardien.moved = true;
        } 
    }

    public void MoveFaucheuse()
    {
        if(nbFragmentGet >= 2)
        {
            faucheuse.transform.parent.position = newFaucheusePosition.position;
            faucheuse.dialoBoxPos = new Vector3(-317.9898f, -170.05f, 0);
            faucheuse.moved = true; 
        }
       
    }

    public void TeleportPlayer(Vector3 position)
    {
        if(!player.isTeleported)
        {
            player.isTeleported = true;
            athManager.endFadeIn = false;
            player.transform.position = position;
            StartCoroutine(CO_WaitTeleport());
        }
       
    }

    IEnumerator CO_WaitTeleport()
    {
        yield return new WaitForSeconds(1.5f);
        athManager.FadeOut();

        yield return new WaitForSeconds(1f);
        player.canMove = true;
        player.isTeleported = false;
    }
}

public static class InputName
{
    public static string Keyboard_Horizontal = "Horizontal";
    public static string Keyboard_Vertical = "Vertical";
    public static string Keyboard_Action = "Action";
    public static string Keyboard_Undo = "Undo";

    public static string Joystick_Horizontal = "J_Horizontal";
    public static string Joystick_Vertical = "J_Vertical";
    public static string Joystick_Action = "J_Action";
    public static string Joystick_Undo = "J_Undo";
}
