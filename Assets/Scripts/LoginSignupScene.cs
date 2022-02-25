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

    [SerializeField] private InputField InputPseudoLogin,
        InputPseudoSignup,
        InputPasswordLogin,
        InputPasswordSignup,
        InputEmail,
        inputConfirm;

    [SerializeField] private Toggle ToggleTos;
    [SerializeField] private Canvas CanvasLogin, CanvasSignup;

    [SerializeField] private Canvas CanvasToast;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        string token = DataSave.LoadDataString("token");
        if (token != "")
        {
            string containerId = GlobalVariable.WebCommunicatorControler.AppelWebCheckToken("https://localhost:7223/api/Authentication/CheckToken", token);
            Debug.Log(containerId);
            if (containerId == "")
            {
                GoToMain();
            }
            else
            {
                SceneManager.LoadScene("GameScene");
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
        string pseudo = InputPseudoLogin.text;
        string password = InputPasswordLogin.text;

        string response =
            GlobalVariable.WebCommunicatorControler.AppelWebAuthentification(
                "https://localhost:7223/api/Authentication/Login", pseudo, password);
        try
        {
            GlobalVariable.User.ChangeUser(JsonConvert.DeserializeObject<User>(response));
            DataSave.SaveData("token", GlobalVariable.User.Token);
            if (GlobalVariable.User.ContainerId == null)
            {
                GoToMain();
            }
            else
            {
                DuckCityHub.StartHub();
                SceneManager.LoadScene("BarScene");
            }
        }
        catch (JsonReaderException e)
        {
            Debug.Log(e.ToString());
            ShowToast.Toast(this, CanvasToast, "Erreur : Pseudo ou mot de passe incorrect");
        }
        catch (NullReferenceException e)
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

        var response = GlobalVariable.WebCommunicatorControler.AppelWebRegistration(
            "https://localhost:7223/api/Authentication/Signup", pseudo, email, password, confirm);

        try
        {
            GlobalVariable.User.ChangeUser(JsonConvert.DeserializeObject<User>(response));
            DataSave.SaveData("name", GlobalVariable.User.Name);
            DataSave.SaveData("token", GlobalVariable.User.Token);
            GoToMain();
        }
        catch (Newtonsoft.Json.JsonReaderException e)
        {
            Debug.Log(e.ToString());
            var except = response.Split(':')[0];
            if (except == "DuckCity.Domain.Exceptions.UsernameAlreadyExistException")
            {
                ShowToast.Toast(this, CanvasToast,"Erreur : Ce pseudo est déjà utilisé");
            }
            else if(except == "DuckCity.Domain.Exceptions.MailAlreadyExistException")
            {
                ShowToast.Toast(this, CanvasToast,"Erreur : Cet email est déjà utilisé");
            }
            else
            {
                ShowToast.Toast(this, CanvasToast, "Erreur : Informations incorrectes");
            }
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
        if (InputPasswordLogin.text == "")
        {
            return false;
        }

        if (InputPseudoLogin.text == "")
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

        if (inputConfirm.text == "")
        {
            return false;
        }

        if (InputPasswordSignup.text == "")
        {
            return false;
        }

        if (InputEmail.text == "" || !InputEmail.text.Contains("@"))
        {
            return false;
        }

        if (InputPseudoSignup.text == "")
        {
            return false;
        }

        return true;
    }
}