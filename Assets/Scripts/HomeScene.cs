using Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class HomeScene : MonoBehaviour
{
    [SerializeField] private Button ButtonAnnouncement,
        ButtonCards,
        ButtonPlay,
        ButtonProfile,
        ButtonRules,
        ButtonSettings,
        ButtonShop,
        ButtonSkins;

    [SerializeField] private InputField InputFieldUsername, InputFieldCash;
    
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        
        InputFieldUsername.text = GlobalVariable.CurrentUser.Name;
        SetCash();
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }
    
    // Start of Transitions functions
    public void GoToFolder()
    {
        SceneManager.LoadScene("FolderScene");
    }

    public void GoToAnnouncements()
    {
        Debug.Log("Annonces");
    }
    
    public void GoToCards()
    {
        Debug.Log("Cartes");
    }
    
    public void GoToProfile()
    {
        Debug.Log("Profil");
    }

    public void GoToRules()
    {
        Debug.Log("Règles");
    }

    public void GoToSettings()
    {
        Debug.Log("Paramètres");
    }

    public void GoToShop()
    {
        Debug.Log("Shop");
    }

    public void GoToSkins()
    {
        Debug.Log("Skins");
    }
    // End of Transitions functions
    
    public void SetCash()
    {
        InputFieldCash.text = "TODO";
    }
}
