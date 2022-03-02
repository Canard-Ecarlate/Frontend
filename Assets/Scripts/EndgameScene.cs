using Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class EndgameScene : MonoBehaviour
{
    [SerializeField] private Image ImageCiat,
        ImageCe;

    [SerializeField] private Text TextVictoryDefeat;

    // Start is called before the first frame update
    void Start()
    {
        IRole winners = GlobalVariable.GameDto.Game.Winners;

        if (winners == null)
        {
            GoToBar();
        }

        TextVictoryDefeat.text =
            winners.Name == GlobalVariable.GameDto.PlayerMe.Role.Name ? "Victoire !" : "Défaite...";
        TextVictoryDefeat.color = winners.Name == "Blue" ? Color.cyan : Color.red;

        ImageCiat.enabled = winners.Name == "Blue";
        ImageCe.enabled = winners.Name == "Red";
    }

    public void GoToBar()
    {
        SceneManager.LoadScene("Scenes/BarScene");
    }

    public async void GoToHome()
    {
        await DuckCityHub.LeaveRoom();
        SceneManager.LoadScene("Scenes/HomeScene");
    }
}