using Utils;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScene : MonoBehaviour
{
    [SerializeField] private InputField InputFieldUsername, InputFieldCash;
    
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        
        InputFieldUsername.text = GlobalVariable.User.Name;
        SetCash();
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

    public void Disconnect()
    {
        DataSave.SaveData("token", "");
        SceneManager.LoadScene("LoginSignupScene");
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
