using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class EndgameScene : MonoBehaviour
{
    private const bool VICTORY = true;
    private const bool CIAT = false;
    [SerializeField] private Button ButtonReturnLobby, ButtonLeave;
    [SerializeField] private Image ImageCiat, ImageCe;
    [SerializeField] private Text TextVictoryDefeat;
    // Start is called before the first frame update
    void Start()
    {
        IRole winners = GlobalVariable.GameDto.Game.Winners;

        TextVictoryDefeat.text = winners.Name=="Blue" ? "Victoire CIAT !" : "Victoire Ecarlate !";
        TextVictoryDefeat.color = winners.Name=="Blue" ? Color.cyan : Color.red;

        ImageCiat.enabled = winners.Name=="Blue";    
        ImageCe.enabled = winners.Name=="Red";
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene("Scenes/BarScene");
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("Scenes/HomeScene");
    }
}
