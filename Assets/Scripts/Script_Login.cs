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

public class Script_Login : MonoBehaviour
{
    [SerializeField] private Button buttonLogin;
    [SerializeField] private InputField inputPseudo, inputPassword;
    [SerializeField] private Canvas canvasLogin,canvasSignup;
    
    [SerializeField] private Canvas toastCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        
        canvasSignup.enabled=false;

        inputPseudo.text = DataSave.LoadDataString("name");
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }

    public void Login()
    {
        var pseudo = inputPseudo.text;
        var password = inputPassword.text;
        
        var response=GlobalVariable.webCommunicatorControler.AppelWebAuthentification("https://localhost:7223/api/Authentication/Login", pseudo, password);
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
            ShowToast.toast(this, toastCanvas, "Erreur : Pseudo ou mot de passe incorrect");
        }
        catch (System.NullReferenceException e)
        {
            Debug.Log(e.ToString());
            ShowToast.toast(this, toastCanvas, "Erreur : Connexion au serveur impossible");
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            ShowToast.toast(this, toastCanvas, "Erreur");
        }
    }

    private void GoToMain()
    {
        SceneManager.LoadScene("Scenes/Home");
    }

    public void GoToSignup()
    {
        canvasLogin.gameObject.SetActive(false);
        canvasSignup.gameObject.SetActive(true);
        canvasLogin.enabled = false;
        canvasSignup.enabled = true;
    }
}
