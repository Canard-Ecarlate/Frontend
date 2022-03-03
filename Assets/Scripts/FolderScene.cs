using System;
using System.Threading.Tasks;
using Controllers;
using Models;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

public class FolderScene : MonoBehaviour
{
    [SerializeField] private Image CreateBg, PrivateBg;
    [SerializeField] private Text NbPlayers, RoomNameText;

    [SerializeField] private InputField RoomName, RoomCode;

    private string ContainerId;

    // Start is called before the first frame update
    void Start()
    {
        RoomNameText.horizontalOverflow = HorizontalWrapMode.Wrap;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    void Update()
    {
        if (DuckCityHub.IsConnectedCreate)
        {
            DuckCityHub.IsConnectedCreate = false;
            CreateRoomCallback();
        }
    }

    // Beginning of Transitions section
    public void ToCreate()
    {
        CreateBg.gameObject.SetActive(true);
        PrivateBg.gameObject.SetActive(false);
    }

    public void ToPrivate()
    {
        CreateBg.gameObject.SetActive(false);
        PrivateBg.gameObject.SetActive(true);
    }

    public void ToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }

    // Beginning of Create section
    public void LessPlayers()
    {
        int players = int.Parse(NbPlayers.text);
        if (players > 3)
        {
            players--;
            NbPlayers.text = players.ToString();
        }
    }

    public void MorePlayers()
    {
        int players = int.Parse(NbPlayers.text);
        if (players < 8)
        {
            players++;
            NbPlayers.text = players.ToString();
        }
    }

    public void CreateRoom()
    {
        GameContainer container = ApiRestController.FindContainerIdForCreateRoom(new RoomCreationApiDto(RoomName.text));
        try
        {
            ContainerId = container.Id;
            DuckCityHub.StartHub(container.Id,CreateRoomCallback);
        }
        catch (Exception e)
        {
            Debug.Log("Error when creating room : "+ e.Message);
            throw;
        }
    }
    
    private async Task CreateRoomCallback()
    {
        try
        {
            await DuckCityHub.CreateRoom(new RoomCreationDto
            {
                ContainerId = ContainerId,
                IsPrivate = true,
                NbPlayers = int.Parse(NbPlayers.text),
                RoomName = RoomName.text
            });
        }
        catch (Exception e)
        {
            Debug.Log("Error when creating room : "+ e.Message);
            throw;
        }
    }
    
    // Beginning of Private section
    public void JoinRoom()
    {
        GameContainer container = ApiRestController.FindContainerIdForJoinRoom(new UserAndRoomDto(RoomCode.text));
        if (container == null)
        {
            Debug.Log("Room not found");
            return;
        }
        DuckCityHub.StartHub(container.Id, JoinRoomCallback);
    }
    
    private async Task JoinRoomCallback()
    {
        try
        {
            await DuckCityHub.JoinRoom(RoomCode.text);
        }
        catch (Exception e)
        {
            Debug.Log("Error when joining room : "+ e.Message);
            throw;
        }
    }
}