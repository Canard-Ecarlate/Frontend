using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneScript : MonoBehaviour
{
    [SerializeField] private Button defaultCardOverlay;
    [SerializeField] private Button myCardOverlay;
    [SerializeField] private Text effectText;
    
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

    public void announceEffect(string s)
    {
        effectText.text = s;
    }
}
