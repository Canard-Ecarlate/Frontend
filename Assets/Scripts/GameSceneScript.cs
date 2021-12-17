using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneScript : MonoBehaviour
{
    public Button defaultCardOverlay;
    public Button myCardOverlay;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideCard(Button b)
    {
        b.gameObject.SetActive(false);
    }

    public void showMyCard()
    {
        myCardOverlay.gameObject.SetActive(true);
    }
}
