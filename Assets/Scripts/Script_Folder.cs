using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Script_Folder : MonoBehaviour
{
    [SerializeField] public Image createBg, privateBg, publicBg;
    
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
    
    //Transitions
    public void ToCreate()
    {
        createBg.gameObject.SetActive(true);
        privateBg.gameObject.SetActive(false);
        publicBg.gameObject.SetActive(false);
    }
    public void ToPrivate()
    {
        createBg.gameObject.SetActive(false);
        privateBg.gameObject.SetActive(true);
        publicBg.gameObject.SetActive(false);
    }
    public void ToPublic()
    {
        createBg.gameObject.SetActive(false);
        privateBg.gameObject.SetActive(false);
        publicBg.gameObject.SetActive(true);
    }
}
