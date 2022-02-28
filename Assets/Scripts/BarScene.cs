using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

public class BarScene : MonoBehaviour
{
    [SerializeField] private Text RoomName;
    [SerializeField] private Image Player1, 
        Player2, 
        Player3, 
        Player4, 
        Player5, 
        Player6, 
        Player7, 
        Player8;
    [SerializeField] private Button PlayButton,
        ReadyToggle;
    private int NbPlayers;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        RoomName.text = GlobalVariable.Room.Name;
        if (GlobalVariable.Room.HostId == GlobalVariable.User.Id)
        {
            ReadyToggle.gameObject.SetActive(false);
            PlayButton.gameObject.SetActive(true);
        }
        if (NbPlayers == GlobalVariable.Players.Count) return;
        NbPlayers = GlobalVariable.Players.Count;
        SetPlayerDucks(NbPlayers);
    }

    private void SetPlayerDucks(int nbPlayers)
    {
        switch (nbPlayers)
        {
            case 1:
                Player1.gameObject.SetActive(true);
                Player2.gameObject.SetActive(false);
                Player3.gameObject.SetActive(false);
                Player4.gameObject.SetActive(false);
                Player5.gameObject.SetActive(false);
                Player6.gameObject.SetActive(false);
                Player7.gameObject.SetActive(false);
                Player8.gameObject.SetActive(false);
                break;
            case 2:
                Player1.gameObject.SetActive(true);
                Player2.gameObject.SetActive(true);
                Player3.gameObject.SetActive(false);
                Player4.gameObject.SetActive(false);
                Player5.gameObject.SetActive(false);
                Player6.gameObject.SetActive(false);
                Player7.gameObject.SetActive(false);
                Player8.gameObject.SetActive(false);
                break;
            case 3:
                Player1.gameObject.SetActive(true);
                Player2.gameObject.SetActive(true);
                Player3.gameObject.SetActive(true);
                Player4.gameObject.SetActive(false);
                Player5.gameObject.SetActive(false);
                Player6.gameObject.SetActive(false);
                Player7.gameObject.SetActive(false);
                Player8.gameObject.SetActive(false);
                break;
            case 4:
                Player1.gameObject.SetActive(true);
                Player2.gameObject.SetActive(true);
                Player3.gameObject.SetActive(true);
                Player4.gameObject.SetActive(true);
                Player5.gameObject.SetActive(false);
                Player6.gameObject.SetActive(false);
                Player7.gameObject.SetActive(false);
                Player8.gameObject.SetActive(false);
                break;
            case 5:
                Player1.gameObject.SetActive(true);
                Player2.gameObject.SetActive(true);
                Player3.gameObject.SetActive(true);
                Player4.gameObject.SetActive(true);
                Player5.gameObject.SetActive(true);
                Player6.gameObject.SetActive(false);
                Player7.gameObject.SetActive(false);
                Player8.gameObject.SetActive(false);
                break;
            case 6:
                Player1.gameObject.SetActive(true);
                Player2.gameObject.SetActive(true);
                Player3.gameObject.SetActive(true);
                Player4.gameObject.SetActive(true);
                Player5.gameObject.SetActive(true);
                Player6.gameObject.SetActive(true);
                Player7.gameObject.SetActive(false);
                Player8.gameObject.SetActive(false);
                break;
            case 7:
                Player1.gameObject.SetActive(true);
                Player2.gameObject.SetActive(true);
                Player3.gameObject.SetActive(true);
                Player4.gameObject.SetActive(true);
                Player5.gameObject.SetActive(true);
                Player6.gameObject.SetActive(true);
                Player7.gameObject.SetActive(true);
                Player8.gameObject.SetActive(false);
                break;
            case 8:
                Player1.gameObject.SetActive(true);
                Player2.gameObject.SetActive(true);
                Player3.gameObject.SetActive(true);
                Player4.gameObject.SetActive(true);
                Player5.gameObject.SetActive(true);
                Player6.gameObject.SetActive(true);
                Player7.gameObject.SetActive(true);
                Player8.gameObject.SetActive(true);
                break;
        }
    }

    // Beginning of Transitions section
    public async void ToFolder()
    {
        try
        {
            await DuckCityHub.LeaveRoom();
            SceneManager.LoadScene("FolderScene");
        }
        catch (Exception e)
        {
            Debug.Log("Error when leaving room : "+ e.Message);
            throw;
        }
    }
	public async void ToGame()
    {
        try
        {
            await DuckCityHub.StartGame();
            SceneManager.LoadScene("GameScene");
        }
        catch (Exception e)
        {
            Debug.Log("Error when leaving room : "+ e.Message);
            throw;
        }
    }
}
