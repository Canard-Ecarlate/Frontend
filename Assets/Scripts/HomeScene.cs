using System.Collections;
using System.Collections.Generic;
using CanardEcarlate.Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

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
        
        inputFieldUsername.text = GlobalVariable.CurrentUser.name;
        setCash();
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }
    
    // Start of Transitions functions
    public void goToFolder()
    {
        SceneManager.LoadScene("FolderScene");
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
        Debug.Log("Profil");
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
        Debug.Log("Shop");
    }

    public void goToSkins()
    {
        Debug.Log("Skins");
    }
    // End of Transitions functions
    
    public void setCash()
    {
        inputFieldCash.text = "TODO";
    }
}
