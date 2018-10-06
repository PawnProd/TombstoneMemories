using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public bool selectCommand = false;
    public bool onMenu = true;

    public GameObject panelCommand;
    public GameObject menu;
    public GameObject selector;

    public RectTransform selectorPos1;
    public RectTransform selectorPos2;

    int index = 1;

	// Update is called once per frame
	void Update () {
		if(selectCommand)
        {
            if(Input.GetButtonDown(InputName.Joystick_Action))
            {
                GameController.Instance.keyboard = false;
                selectCommand = false;
                GameController.Instance.player.canMove = true;
                panelCommand.GetComponent<Animator>().SetBool("FadeOut", true);
                GameController.Instance.SetInput();
                GameController.Instance.player.enabled = true;
            }
            else if(Input.GetButtonDown(InputName.Keyboard_Action))
            {
                GameController.Instance.keyboard = true;
                selectCommand = false;
                GameController.Instance.player.canMove = true;
                panelCommand.GetComponent<Animator>().SetBool("FadeOut", true);
                GameController.Instance.SetInput();
                GameController.Instance.player.enabled = true;
            }

        }

        if(onMenu)
        {
            float vertical = Input.GetAxisRaw(InputName.Joystick_Vertical);

            if (vertical != 0)
            {
                if (!selector.activeSelf)
                {
                    selector.SetActive(true);
                }

                if (vertical > 0)
                {
                    index = 1;
                    selector.GetComponent<RectTransform>().anchoredPosition = selectorPos1.anchoredPosition;
                }
                else
                {
                    index = 2;
                    selector.GetComponent<RectTransform>().anchoredPosition = selectorPos2.anchoredPosition;
                }
            }

            if(Input.GetButtonDown(InputName.Joystick_Action))
            {
                if (index == 1)
                {
                    menu.SetActive(false);
                    panelCommand.SetActive(true);
                    selectCommand = true;
                    onMenu = false;
                }
                else
                {
                    Quit();
                }
            }
        }
       
    }

    public void UnloadMenu()
    {
        onMenu = false;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void SelectCommand()
    {
        selectCommand = true;
    }
}
