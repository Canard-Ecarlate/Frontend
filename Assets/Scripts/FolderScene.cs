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
    
    // Start is called before the first frame update
    void Start()
    {
        RoomNameText.horizontalOverflow = HorizontalWrapMode.Wrap;
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        // does nothing for now
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
        // Appel pour gameContainer
        DuckCityHub.Start();
        bool created = DuckCityHub.CreateRoom(new RoomCreationDto
        {
            ContainerId = "containerId",
            IsPrivate = true,
            NbPlayers = int.Parse(NbPlayers.text),
            RoomName = RoomName.text
        });
        if (created)
        {
            SceneManager.LoadScene("BarScene");
        }
        else
        {
            Debug.Log("Error when creating room");
        }
    }

    public void JoinRoom()
    {
        // Appel pour gameContainer
        DuckCityHub.Start();
        bool joined = DuckCityHub.JoinRoom(RoomCode.text);
        if (joined)
        {
            SceneManager.LoadScene("BarScene");
        }
        else
        {
            Debug.Log("Error when joining room");
        }
    }
}