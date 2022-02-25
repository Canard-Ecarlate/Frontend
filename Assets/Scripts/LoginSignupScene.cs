using System;
using Models;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using Utils;
using Application = UnityEngine.Application;

public class LoginSignupScene : MonoBehaviour
{
    [SerializeField] private Button ButtonLogin, ButtonSignup;
    [SerializeField] private InputField InputPseudoLogin, InputPseudoSignup, InputPasswordLogin, InputPasswordSignup, InputEmail, inputConfirm;
    [SerializeField] private Toggle ToggleTos;
    [SerializeField] private Canvas CanvasLogin,CanvasSignup;
    
    [SerializeField] private Canvas CanvasToast;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        var token = DataSave.LoadDataString("token");
        if (token != "")
        {
            var response = GlobalVariable.WebCommunicatorControler.AppelWebCheckToken("https://localhost:7223/api/Authentication/CheckToken", token);
            if (response == "success")
            {
                //GoToMain();
            } 
        }

        GoToLogin();
        TryEnableButtons();

        InputPseudoLogin.text = DataSave.LoadDataString("name");
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }

    public void Login()
    {
        var pseudo = InputPseudoLogin.text;
        var password = InputPasswordLogin.text;
        
        var response=GlobalVariable.WebCommunicatorControler.AppelWebAuthentification("https://localhost:7223/api/Authentication/Login", pseudo, password);
        try
        {
            GlobalVariable.CurrentUser.ChangeUser(JsonConvert.DeserializeObject<User>(response));
            DataSave.SaveData("name", GlobalVariable.CurrentUser.Name);
            DataSave.SaveData("token", GlobalVariable.CurrentUser.Token);
            GoToMain();
        }
        catch (Newtonsoft.Json.JsonReaderException e)
        {
            Debug.Log(e.ToString());
            ShowToast.Toast(this, CanvasToast, "Erreur : Pseudo ou mot de passe incorrect");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e.ToString());
            ShowToast.Toast(this, CanvasToast, "Erreur : Connexion au serveur impossible");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            ShowToast.Toast(this, CanvasToast, "Erreur inconnue");
        }
    }
    
    public void Signup()
    {
        var pseudo = InputPseudoSignup.text;
        var email = InputEmail.text;
        var password = InputPasswordSignup.text;
        var confirm = inputConfirm.text;

        var response=GlobalVariable.WebCommunicatorControler.AppelWebRegistration("https://localhost:7223/api/Authentication/Signup", pseudo, email, password, confirm);
        
        try
        {
            GlobalVariable.CurrentUser.ChangeUser(JsonConvert.DeserializeObject<User>(response));
            DataSave.SaveData("name", GlobalVariable.CurrentUser.Name);
            DataSave.SaveData("token", GlobalVariable.CurrentUser.Token);
            GoToMain();
        }
        catch (Newtonsoft.Json.JsonReaderException e)
        {
            Debug.Log(e.ToString());
            ShowToast.Toast(this, CanvasToast, "Erreur : Informations incorrectes");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e.ToString());
            ShowToast.Toast(this, CanvasToast, "Erreur : Connexion au serveur impossible");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            ShowToast.Toast(this, CanvasToast, "Erreur inconnue");
        }
    }

    private void GoToMain()
    {
        SceneManager.LoadScene("Scenes/HomeScene");
    }

    public void GoToSignup()
    {
        CanvasLogin.gameObject.SetActive(false);
        CanvasSignup.gameObject.SetActive(true);
        CanvasLogin.enabled = false;
        CanvasSignup.enabled = true;
    }

    public void GoToLogin()
    {
        CanvasLogin.gameObject.SetActive(true);
        CanvasSignup.gameObject.SetActive(false);
        CanvasLogin.enabled = true;
        CanvasSignup.enabled = false;
    }

    public void GoToTos()
    {
        Application.OpenURL("https://canardecarlate.fr/termsofservice");
    }
    
    public void TryEnableButtons()
    {
        ButtonLogin.interactable = CheckButtonLogin();
        ButtonSignup.interactable = CheckButtonSignup();
    }

    private bool CheckButtonLogin()
    {
        if (InputPasswordLogin.text=="")
        {
            return false;
        }
        if (InputPseudoLogin.text=="")
        {
            return false;
        }

        return true;
    }
    
    private bool CheckButtonSignup()
    {
        if (!ToggleTos.isOn)
        {
            return false;
        }
        if (inputConfirm.text=="")
        {
            return false;
        }
        if (InputPasswordSignup.text=="")
        {
            return false;
        }
        if (InputEmail.text=="" || !InputEmail.text.Contains("@"))
        {
            return false;
        }
        if (InputPseudoSignup.text=="")
        {
            return false;
        }

        return true;
    }
}
