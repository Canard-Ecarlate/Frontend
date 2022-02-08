using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{
    [SerializeField] private Button buttonAnnouncement,
        buttonCards,
        buttonPlay,
        buttonProfile,
        buttonRules,
        buttonSettings,
        buttonShop,
        buttonSkins;

    [SerializeField] private InputField inputFieldUsername, inputFieldCash;
    
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        
        inputFieldUsername.text = "MONEY MONEY MONEY";
        inputFieldCash.text = "ALWAYS SUNNY";
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }

    public void goToAnnouncements()
    {
        Debug.Log("Annonces");
    }
    
    public void goToCards()
    {
        Debug.Log("Cartes");
    }
    
    public void goToProfile()
    {
        Debug.Log("Viens me voir de profil, si t'es un homme !");
    }

    public void goToRoomList()
    {
        Debug.Log("Liste des salles");
    }

    public void goToRules()
    {
        Debug.Log("Règles");
    }

    public void goToSettings()
    {
        Debug.Log("Paramètres");
    }

    public void goToShop()
    {
        Debug.Log("IN A RICH MEN'S WORLD!");
    }

    public void goToSkins()
    {
        Debug.Log("Skins");
    }
}
