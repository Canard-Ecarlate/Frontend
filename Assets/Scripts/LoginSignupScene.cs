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
        InputConfirm;

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
            string response = GlobalVariable.WebCommunicatorControler.AppelWebCheckToken("https://localhost:7223/api/Authentication/CheckToken", token);
            Debug.Log("Auto connect : " + response);
            GlobalVariable.User.ChangeUser(JsonConvert.DeserializeObject<User>(response));
            GlobalVariable.User.Token = token;
            if (GlobalVariable.User.ContainerId == null)
            {
                GoToMain();
            }
            else
            {
                DuckCityHub.StartHub(GlobalVariable.User.ContainerId);
            }
        }

        GoToLogin();
        TryEnableButtons();

        InputPseudoLogin.text = DataSave.LoadDataString("name");
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
                DuckCityHub.StartHub(GlobalVariable.User.ContainerId);
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
        string pseudo = InputPseudoSignup.text;
        string email = InputEmail.text;
        string password = InputPasswordSignup.text;
        string confirm = InputConfirm.text;

        string response = GlobalVariable.WebCommunicatorControler.AppelWebRegistration(
            "https://localhost:7223/api/Authentication/Signup", pseudo, email, password, confirm);

        try
        {
            GlobalVariable.User.ChangeUser(JsonConvert.DeserializeObject<User>(response));
            DataSave.SaveData("name", GlobalVariable.User.Name);
            DataSave.SaveData("token", GlobalVariable.User.Token);
            GoToMain();
        }
        catch (JsonReaderException e)
        {
            Debug.Log(e.ToString());
            string except = response.Split(':')[0];
            switch (except)
            {
                case "DuckCity.Domain.Exceptions.UsernameAlreadyExistException":
                    ShowToast.Toast(this, CanvasToast,"Erreur : Ce pseudo est déjà utilisé");
                    break;
                case "DuckCity.Domain.Exceptions.MailAlreadyExistException":
                    ShowToast.Toast(this, CanvasToast,"Erreur : Cet email est déjà utilisé");
                    break;
                default:
                    ShowToast.Toast(this, CanvasToast, "Erreur : Informations incorrectes");
                    break;
            }
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

        if (InputConfirm.text == "")
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