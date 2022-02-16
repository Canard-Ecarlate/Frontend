using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Game : MonoBehaviour
{
    [SerializeField] private Image defaultCardOverlay, myCardOverlay;
    [SerializeField] private Image turnPointer, pullsShuffle;
    [SerializeField] private Image previousCardOne, previousCardTwo, previousCardThree;
    [SerializeField] private Text pullsEnd, effectText;
    [SerializeField] private Image tempBoomerang;
    
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void StopShowingCard(Button b)
    {
        b.gameObject.SetActive(false);
    }

    public void ShowMyCard(Image i)
    {
        myCardOverlay.sprite = i.sprite;
        myCardOverlay.gameObject.SetActive(true);
    }
    
    public void ShowACard(Image i)
    {
        defaultCardOverlay.sprite = i.sprite;
        defaultCardOverlay.gameObject.SetActive(true);
        //TODO? Add a sleep then call stopShowingCard()
    }

    public void AnnounceEffect(string s)
    {
        effectText.text = s;
    }

    public void UpdatePreviousCards(Image i)
    {
        previousCardOne.sprite = previousCardTwo.sprite;
        previousCardTwo.sprite = previousCardThree.sprite;
        previousCardThree.sprite = i.sprite;
    }

    public void Countdown()
    {
        int count = int.Parse(pullsEnd.text);
        count--;
        pullsEnd.text = count.ToString();
    }

    //Simulate drawing a boomerang from Seb.
    public void DrawCardFromSeb()
    {
        ShowACard(tempBoomerang);
        AnnounceEffect("Seb could draw one of his own cards!");
        turnPointer.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,110);
        pullsShuffle.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,160);
        UpdatePreviousCards(tempBoomerang);
        Countdown();
    }
}
