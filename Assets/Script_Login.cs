using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using CanardEcarlate.Utils;

public class Script_Login : MonoBehaviour
{
    public Button buttonLogin;

    public InputField inputPseudo, inputPassword;

    protected LayerMask layerLogin, layerSignup;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start login");
        
        layerLogin = LayerMask.GetMask("Login");
        layerSignup = LayerMask.GetMask("Signup");
        
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
    }
}
