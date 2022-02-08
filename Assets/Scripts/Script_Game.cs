using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneScript : MonoBehaviour
{
    [SerializeField] private Button defaultCardOverlay;
    [SerializeField] private Button myCardOverlay;
    [SerializeField] private Text effectText;
    [SerializeField] private Image tempBoomerang;
    [SerializeField] private Image turnPointer;
    [SerializeField] private Image pullsShuffle;
    [SerializeField] private Image previousCardOne;
    [SerializeField] private Image previousCardTwo;
    [SerializeField] private Image previousCardThree;
    [SerializeField] private Text pullsEnd;
    
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    public void stopShowingCard(Button b)
    {
        b.gameObject.SetActive(false);
    }

    public void showMyCard(Image i)
    {
        myCardOverlay.gameObject.GetComponent<Image>().sprite = i.sprite;
        myCardOverlay.gameObject.SetActive(true);
    }
    
    public void showACard(Image i)
    {
        defaultCardOverlay.gameObject.GetComponent<Image>().sprite = i.sprite;
        defaultCardOverlay.gameObject.SetActive(true);
        //TODO? Add a sleep then call stopShowingCard()
    }

    public void announceEffect(string s)
    {
        effectText.text = s;
    }

    public void updatePreviousCards(Image i)
    {
        previousCardOne.gameObject.GetComponent<Image>().sprite = previousCardTwo.gameObject.GetComponent<Image>().sprite;
        previousCardTwo.gameObject.GetComponent<Image>().sprite = previousCardThree.gameObject.GetComponent<Image>().sprite;
        previousCardThree.gameObject.GetComponent<Image>().sprite = i.sprite;
    }

    public void countdown()
    {
        int count = int.Parse(pullsEnd.gameObject.GetComponent<Text>().text);
        count--;
        pullsEnd.gameObject.GetComponent<Text>().text = count.ToString();
    }

    //Simulate drawing a boomerang from Seb.
    public void drawCardFromSeb()
    {
        showACard(tempBoomerang);
        announceEffect("Seb could draw one of his own cards!");
        turnPointer.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,110);
        pullsShuffle.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,160);
        updatePreviousCards(tempBoomerang);
        countdown();
    }
}
