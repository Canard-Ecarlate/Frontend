using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Utils;

public class BarScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    // Update is called once per frame
    void Update()
    {
        // does nothing for now        
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
