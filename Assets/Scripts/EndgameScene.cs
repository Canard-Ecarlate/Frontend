using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        TextVictoryDefeat.text = VICTORY ? "Victory!" : "Defeat...";
        TextVictoryDefeat.color = CIAT ? Color.cyan : Color.red;

        ImageCiat.enabled = CIAT;    
        ImageCe.enabled = !CIAT;
    }

    // Update is called once per frame
    void Update()
    {
        //nothing to do here
    }

    public void GoToLobby()
    {
        //nothing to do here
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("Scenes/HomeScene");
    }
}
