using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DialogueSystem : MonoBehaviour {

    public Queue<string> textes = new Queue<string>();
    public TextMeshProUGUI dialogText;
    public GameObject imgDialog;
    public GameObject interactionPanel;
    public GameObject selector;

    public int selectorIndex = 0;

    public bool getInteraction = false;
    public bool inInteraction = false;

    public bool continueAfterInteraction = false;

    public void InitDialog(string text)
    {
        CutText(text);
        
        NextDialogue();
    }

    public void CloseDialog()
    {
        GameController.Instance.player.canMove = true;
        GameController.Instance.player.lastTriggerEnter.GetComponent<PNJ>().EndDialog();
        Destroy(gameObject);
    }

    public void HideDialog()
    {
        dialogText.gameObject.SetActive(false);
        imgDialog.SetActive(false);
    }

    public void HideInteraction()
    {
        interactionPanel.SetActive(false);
        selector.SetActive(false);
        inInteraction = false;
        getInteraction = false;

        foreach(Transform child in interactionPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void CreateButton(GameObject prefabButton, string buttonText, UnityEvent buttonEvents)
    {
        Button button = Instantiate(prefabButton, interactionPanel.transform).GetComponent<Button>();
        button.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = buttonText;
        button.onClick.AddListener(delegate { OnClickButton(buttonEvents); });
    }
   

    public void OnClickButton(UnityEvent events)
    {
        events.Invoke();
    }

    public void ShowDialog()
    {
        imgDialog.SetActive(true);
        dialogText.gameObject.SetActive(true);
    }
    public void ShowPanelInteraction()
    {
        Debug.Log("Show Interaction ! ");
        HideDialog();
        interactionPanel.SetActive(true);
        selector.SetActive(true);
        inInteraction = true;
        MoveSelector(0);
    }

    public void CloseInteraction()
    {
        interactionPanel.SetActive(false);
        selector.SetActive(false);
        inInteraction = false;
        getInteraction = false;
    }

    public void ChangeInteraction()
    {
        interactionPanel.transform.GetChild(1).gameObject.SetActive(false);
        interactionPanel.transform.GetChild(0).gameObject.SetActive(true);
        selectorIndex = 0;
    }

    public void MoveSelector(int index)
    {
        if (interactionPanel.transform.GetChild(index).gameObject.activeSelf)
        {
            selectorIndex = index;
            selector.GetComponent<RectTransform>().anchoredPosition = interactionPanel.transform.GetChild(index).GetComponent<RectTransform>().anchoredPosition;
            
        }
           
    }

    public void SelectButton()
    {
        if (!continueAfterInteraction)
            CloseDialog();
        else
        {
            HideInteraction();
            ShowDialog();
        }
            

        interactionPanel.transform.GetChild(selectorIndex).GetComponent<Button>().onClick.Invoke() ;
    }


    public void CutText(string text)
    {
        int startIndex = 0;
        int length = 60;
        int endIndex = 60;

        while(startIndex + length <= text.Length && startIndex != -1)
        {
            int correctLength = text.IndexOf(" ", endIndex) - startIndex;

            if(correctLength < 0)
            {
                correctLength = text.Length - startIndex;
            }

            string cuttedString = text.Substring(startIndex, correctLength) + " ...";
            if(text.IndexOf(" ", endIndex) == -1)
            {
                startIndex = text.Length;
            }
            else
            {
                startIndex = text.IndexOf(" ", endIndex) + 1;
            }
             
            endIndex += length;
            textes.Enqueue(cuttedString);
        }

        textes.Enqueue(text.Substring(startIndex, text.Length - startIndex));
    }

    public void NextDialogue()
    {
        if(textes.Count > 0)
        {
            string texte = textes.Dequeue();
            if(string.IsNullOrEmpty(texte))
            {
                CloseDialog();
            }
            else
            {
                dialogText.text = texte;
            }

        }
        else if(!getInteraction)
        {
            CloseDialog();
        }
        else
        {
            ShowPanelInteraction();
        }
           
    }

}
