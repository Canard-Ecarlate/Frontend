using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class FolderScene : MonoBehaviour
{
    [SerializeField] private Image createBg, privateBg;
    [SerializeField] private Text nbPlayers;

    [SerializeField] private InputField roomName;
    
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
    public void ToCreate()
    {
        createBg.gameObject.SetActive(true);
        privateBg.gameObject.SetActive(false);
    }
    public void ToPrivate()
    {
        createBg.gameObject.SetActive(false);
		privateBg.gameObject.SetActive(true);
    }

    public void ToHome()
    {
        SceneManager.LoadScene("HomeScene");
    }
    
    // Beginning of Create section
	public void lessPlayers()
	{
        int players = int.Parse(nbPlayers.text);
        if (players > 3)
        {
            players--;
            nbPlayers.text = players.ToString();
        }
	}
    public void morePlayers()
    {
        int players = int.Parse(nbPlayers.text);
        if (players < 8)
        {
            players++;
            nbPlayers.text = players.ToString();
        }
    }
	// End of Create section
}
