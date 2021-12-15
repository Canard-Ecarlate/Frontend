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

public class Script_Login : MonoBehaviour
{
    public Button buttonLogin;

    public InputField inputPseudo, inputPassword;

    public Text textGoToSignup;
    
    public Canvas canvasLogin,canvasSignup;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start login");

        inputPseudo.text = DataSave.LoadDataString("name");
        
        canvasSignup.enabled=false;

        buttonLogin.onClick.AddListener(Login);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Login()
    {
        var pseudo = inputPseudo.text;
        var password = inputPassword.text;
        
        var response=GlobalVariable.webCommunicatorControler.AppelWebAuthentification("https://localhost:5001/api/Authentication/Login", pseudo, password);
        Debug.Log(response);
        GlobalVariable.CurrentUser = JsonConvert.DeserializeObject<User>(response);
        DataSave.SaveData("name", GlobalVariable.CurrentUser.name);
        DataSave.SaveData("token", GlobalVariable.CurrentUser.token);
    }

    public void GoToSignup()
    {
        canvasLogin.enabled = false;
        canvasSignup.enabled = true;
    }
}
