using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AthManager : MonoBehaviour {

    public static AthManager Instance { private set; get; }

    public Text goalText;

    public Animator fadeImgAnimator;

    public bool endFadeIn = false;

    public Transform inventorySlots;

	// Use this for initialization
	void Awake () {
        Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
        if(fadeImgAnimator.GetCurrentAnimatorStateInfo(0).IsName("EndFadeIn"))
        {
            endFadeIn = true;
        }
        else if (fadeImgAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeOut"))
        {
            endFadeIn = false;
        }
    }

    public void SetGoalText(string newGoal)
    {
        goalText.text = newGoal;
    }

    public void AddItem(int indexSlot, Sprite objSprite)
    {
        Transform slot = inventorySlots.transform.GetChild(indexSlot);
        slot.GetChild(0).GetComponent<Image>().sprite = objSprite;
        slot.GetChild(0).gameObject.SetActive(true);
       
    }

    public void RemoveItem(int indexSlot)
    {
        Transform slot = inventorySlots.transform.GetChild(indexSlot);
        slot.GetChild(0).gameObject.SetActive(false);

    }

    public void FadeIn()
    {
        fadeImgAnimator.SetBool("FadeOut", false);
        fadeImgAnimator.SetBool("FadeIn", true);
    }

    public void FadeOut()
    {
        endFadeIn = false;
        fadeImgAnimator.SetBool("FadeIn", false);
        fadeImgAnimator.SetBool("FadeOut", true);
    }
}
