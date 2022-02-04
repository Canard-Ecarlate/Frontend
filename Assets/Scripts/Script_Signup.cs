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
    [SerializeField] public Button buttonSignup;
    [SerializeField] public Toggle toggleTOS;
    [SerializeField] public InputField inputPseudo, inputEmail, inputPassword, inputConfirm;
    [SerializeField] public Canvas canvasLogin,canvasSignup;
    // Start is called before the first frame update
    void Start()
    {
        //nothing to do here
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

        var response=GlobalVariable.webCommunicatorControler.AppelWebRegistration("https://localhost:5001/api/Authentication/Signup", pseudo, email, password, confirm);
        
        Debug.Log(response);
        
        GlobalVariable.CurrentUser = JsonConvert.DeserializeObject<User>(response);
        DataSave.SaveData("name", GlobalVariable.CurrentUser.name);
        DataSave.SaveData("token", GlobalVariable.CurrentUser.token);
        GoToMain();
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
