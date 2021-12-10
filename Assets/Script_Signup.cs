using System;
using System.Collections;
using System.Collections.Generic;
using CanardEcarlate.Utils;
using UnityEngine;
using UnityEngine.UI;

public class Script_Signup : MonoBehaviour
{
    public Button buttonSignup;

    public InputField inputPseudo, inputEmail, inputPassword, inputConfirm;

    protected LayerMask layerLogin, layerSignup;
    // Start is called before the first frame update
    void Start()
    {
        layerLogin = LayerMask.GetMask("Login");
        layerSignup = LayerMask.GetMask("Signup");
        Debug.Log("Start signup");
        buttonSignup.onClick.AddListener(Signup);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Signup()
    {
        var pseudo = inputPseudo.text;
        var email = inputEmail.text;
        var password = inputPassword.text;
        var confirm = inputConfirm.text;

        var response=GlobalVariable.webCommunicatorControler.AppelWebRegistration("https://localhost:5001/api/Authentication/Signup", pseudo, email, password, confirm);
        
        Debug.Log(response);
    }
}
