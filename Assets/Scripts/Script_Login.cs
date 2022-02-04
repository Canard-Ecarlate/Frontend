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

public class Script_Login : MonoBehaviour
{
    [SerializeField] private Button buttonLogin;
    [SerializeField] private InputField inputPseudo, inputPassword;
    [SerializeField] private Canvas canvasLogin,canvasSignup;
    // Start is called before the first frame update
    void Start()
    {
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
        
        var response=GlobalVariable.webCommunicatorControler.AppelWebAuthentification("https://localhost:5001/api/Authentication/Login", pseudo, password);
        Debug.Log(response);
        GlobalVariable.CurrentUser.changeUser(JsonConvert.DeserializeObject<User>(response));
        DataSave.SaveData("name", GlobalVariable.CurrentUser.name);
        DataSave.SaveData("token", GlobalVariable.CurrentUser.token);
        GoToMain();
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
