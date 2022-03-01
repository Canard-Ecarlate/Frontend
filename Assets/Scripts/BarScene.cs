using System;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

public class BarScene : MonoBehaviour
{
    [SerializeField] private Text RoomName,
        RoomCode;
    [SerializeField] private Image Player1, 
        Player2, 
        Player3, 
        Player4, 
        Player5, 
        Player6, 
        Player7, 
        Player8;

    [SerializeField] private Button PlayButton,
        ReadyButton,
        NotReadyButton;

    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Update()
    {
        if (DuckCityHub.OnRoomPush)
        {
            // Set texts
            RoomCode.text = GlobalVariable.Room.Code; 
            RoomName.text = GlobalVariable.Room.Name;
            DuckCityHub.OnRoomPush = false; 
        }

        if (DuckCityHub.OnPlayersPush)
        {
            PlayerInWaitingRoomDto me = GlobalVariable.Players.Single(p => p.Id == GlobalVariable.User.Id);
            NotReadyButton.gameObject.SetActive(!me.Ready);
            ReadyButton.gameObject.SetActive(me.Ready);

            int nbReady = GlobalVariable.Players.Count(p => p.Ready); 
        
            // Display Play button if everybody is ready
            PlayButton.gameObject.SetActive(nbReady == GlobalVariable.Room.RoomConfiguration.NbPlayers);
        
            // Display ducks
            SetPlayerDucks(GlobalVariable.Players.Count);
            DuckCityHub.OnPlayersPush = false;
        }
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

    public async void PlayerReady()
    {
        try
        {
            await DuckCityHub.PlayerReady();
            ReadyButton.gameObject.SetActive(!ReadyButton.IsActive());
            NotReadyButton.gameObject.SetActive(!NotReadyButton.IsActive());
        }
        catch (Exception e)
        {
            Debug.Log("Error when player ready : "+ e.Message);
            throw;
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
        }
        catch (Exception e)
        {
            Debug.Log("Error when leaving room : "+ e.Message);
            throw;
        }
    }
}
