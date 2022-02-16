using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CanardEcarlate.Models;
using CanardEcarlate.Utils;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Script_Signup : MonoBehaviour
{
    [SerializeField] private Button buttonSignup;
    [SerializeField] private Toggle toggleTOS;
    [SerializeField] private InputField inputPseudo, inputEmail, inputPassword, inputConfirm;
    [SerializeField] private Canvas canvasLogin,canvasSignup;
    
    [SerializeField] private Canvas canvasToast;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }
    
    public void tryEnableButton()
    {
        buttonSignup.interactable = checkButton();
    }

    private bool checkButton()
    {
        if (!toggleTOS.isOn)
        {
            return false;
        }
        if (inputConfirm.text=="")
        {
            return false;
        }
        if (inputPassword.text=="")
        {
            return false;
        }
        if (inputEmail.text=="" || !inputEmail.text.Contains("@"))
        {
            return false;
        }
        if (inputPseudo.text=="")
        {
            return false;
        }

        return true;
    }
    
    public void Signup()
    {
        var pseudo = inputPseudo.text;
        var email = inputEmail.text;
        var password = inputPassword.text;
        var confirm = inputConfirm.text;

        var response=GlobalVariable.webCommunicatorControler.AppelWebRegistration("https://localhost:7223/api/Authentication/Signup", pseudo, email, password, confirm);
        
        try
        {
            GlobalVariable.CurrentUser.changeUser(JsonConvert.DeserializeObject<User>(response));
            DataSave.SaveData("name", GlobalVariable.CurrentUser.name);
            DataSave.SaveData("token", GlobalVariable.CurrentUser.token);
            GoToMain();
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

    private void GoToMain()
    {
        SceneManager.LoadScene("Scenes/Home");
    }
    
    public void GoToLogin()
    {
        canvasLogin.gameObject.SetActive(true);
        canvasSignup.gameObject.SetActive(false);
        canvasLogin.enabled = true;
        canvasSignup.enabled = false;
    }

    public void GoToTOS()
    {
        Application.OpenURL("https://canardecarlate.fr/termsofservice");
    }
}
