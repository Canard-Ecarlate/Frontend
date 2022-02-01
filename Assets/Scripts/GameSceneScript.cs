using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneScript : MonoBehaviour
{
    public Button defaultCardOverlay;
    public Button myCardOverlay;
    
    public void hideCard(Button b)
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
    }
}
