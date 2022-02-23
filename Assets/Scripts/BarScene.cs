using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

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
    public void ToFolder()
    {
        SceneManager.LoadScene("FolderScene");
    }
	public void ToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
