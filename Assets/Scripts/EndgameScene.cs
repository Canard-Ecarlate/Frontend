using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndgameScene : MonoBehaviour
{
    private const bool VICTORY = true;
    private const bool CIAT = false;
    [SerializeField] private Button buttonReturnLobby, buttonLeave;
    [SerializeField] private Image imageCIAT, imageCE;
    [SerializeField] private Text textVictoryDefeat;
    // Start is called before the first frame update
    void Start()
    {
        textVictoryDefeat.text = VICTORY ? "Victory!" : "Defeat...";
        textVictoryDefeat.color = CIAT ? Color.cyan : Color.red;

        imageCIAT.enabled = CIAT;    
        imageCE.enabled = !CIAT;
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }

    public void goToLobby()
    {
        //nothing to do here
    }

    public void goToHome()
    {
        SceneManager.LoadScene("Scenes/HomeScene");
    }
}
