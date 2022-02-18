using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using CanardEcarlate.Models;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using CanardEcarlate.Utils;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using UnityEngine.WSA;
using Application = UnityEngine.Application;

public class LoginSignupScene : MonoBehaviour
{
    [SerializeField] private Button buttonLogin, buttonSignup;
    [SerializeField] private InputField inputPseudoLogin, inputPseudoSignup, inputPasswordLogin, inputPasswordSignup, inputEmail, inputConfirm;
    [SerializeField] private Toggle toggleTos;
    [SerializeField] private Canvas canvasLogin,canvasSignup;
    
    [SerializeField] private Canvas canvasToast;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        var token = DataSave.LoadDataString("token");
        if (token != "")
        {
            var response = GlobalVariable.webCommunicatorControler.AppelWebCheckToken("https://localhost:7223/api/Authentication/CheckToken", token);
            Debug.Log(response.ToString());
            if (response == "success")
            {
                goToMain();
            } 
        }

        goToLogin();
        tryEnableButtons();

        inputPseudoLogin.text = DataSave.LoadDataString("name");
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }

    public void login()
    {
        var pseudo = inputPseudoLogin.text;
        var password = inputPasswordLogin.text;
        
        var response=GlobalVariable.webCommunicatorControler.AppelWebAuthentification("https://localhost:7223/api/Authentication/Login", pseudo, password);
        try
        {
            GlobalVariable.CurrentUser.changeUser(JsonConvert.DeserializeObject<User>(response));
            DataSave.SaveData("name", GlobalVariable.CurrentUser.name);
            DataSave.SaveData("token", GlobalVariable.CurrentUser.token);
            goToMain();
        }
        catch (Newtonsoft.Json.JsonReaderException e)
        {
            Debug.Log(e.ToString());
            ShowToast.toast(this, canvasToast, "Erreur : Pseudo ou mot de passe incorrect");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e.ToString());
            ShowToast.toast(this, canvasToast, "Erreur : Connexion au serveur impossible");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            ShowToast.toast(this, canvasToast, "Erreur inconnue");
        }
    }
    
    public void signup()
    {
        var pseudo = inputPseudoSignup.text;
        var email = inputEmail.text;
        var password = inputPasswordSignup.text;
        var confirm = inputConfirm.text;

        var response=GlobalVariable.webCommunicatorControler.AppelWebRegistration("https://localhost:7223/api/Authentication/Signup", pseudo, email, password, confirm);
        
        try
        {
            GlobalVariable.CurrentUser.changeUser(JsonConvert.DeserializeObject<User>(response));
            DataSave.SaveData("name", GlobalVariable.CurrentUser.name);
            DataSave.SaveData("token", GlobalVariable.CurrentUser.token);
            goToMain();
        }
        catch (Newtonsoft.Json.JsonReaderException e)
        {
            Debug.Log(e.ToString());
            ShowToast.toast(this, canvasToast, "Erreur : Informations incorrectes");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e.ToString());
            ShowToast.toast(this, canvasToast, "Erreur : Connexion au serveur impossible");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            ShowToast.toast(this, canvasToast, "Erreur inconnue");
        }
    }

    private void goToMain()
    {
        SceneManager.LoadScene("Scenes/HomeScene");
    }

    public void goToSignup()
    {
        canvasLogin.gameObject.SetActive(false);
        canvasSignup.gameObject.SetActive(true);
        canvasLogin.enabled = false;
        canvasSignup.enabled = true;
    }

    public void goToLogin()
    {
        canvasLogin.gameObject.SetActive(true);
        canvasSignup.gameObject.SetActive(false);
        canvasLogin.enabled = true;
        canvasSignup.enabled = false;
    }

    public void goToTos()
    {
        Application.OpenURL("https://canardecarlate.fr/termsofservice");
    }
    
    public void tryEnableButtons()
    {
        buttonLogin.interactable = checkButtonLogin();
        buttonSignup.interactable = checkButtonSignup();
    }

    private bool checkButtonLogin()
    {
        if (inputPasswordLogin.text=="")
        {
            return false;
        }
        if (inputPseudoLogin.text=="")
        {
            return false;
        }

        return true;
    }
    
    private bool checkButtonSignup()
    {
        if (!toggleTos.isOn)
        {
            return false;
        }
        if (inputConfirm.text=="")
        {
            return false;
        }
        if (inputPasswordSignup.text=="")
        {
            return false;
        }
        if (inputEmail.text=="" || !inputEmail.text.Contains("@"))
        {
            return false;
        }
        if (inputPseudoSignup.text=="")
        {
            return false;
        }

        return true;
    }
}
