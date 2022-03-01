using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField] private Image DefaultCardOverlay, MyCardOverlay;
    [SerializeField] private Image TurnPointer, PullsShuffle;
    [SerializeField] private Image PreviousCardOne, PreviousCardTwo, PreviousCardThree;
    [SerializeField] private Text PullsEnd, EffectText;
    [SerializeField] private Image TempBoomerang;
    
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait; 
    }

    public void StopShowingCard(Button b)
    {
        b.gameObject.SetActive(false);
    }

    public void ShowMyCard(Image i)
    {
        MyCardOverlay.sprite = i.sprite;
        MyCardOverlay.gameObject.SetActive(true);
    }
    
    public void ShowACard(Image i)
    {
        DefaultCardOverlay.sprite = i.sprite;
        DefaultCardOverlay.gameObject.SetActive(true);
        //TODO? Add a sleep then call stopShowingCard()
    }

    public void AnnounceEffect(string s)
    {
        EffectText.text = s;
    }

    public void UpdatePreviousCards(Image i)
    {
        PreviousCardOne.sprite = PreviousCardTwo.sprite;
        PreviousCardTwo.sprite = PreviousCardThree.sprite;
        PreviousCardThree.sprite = i.sprite;
    }

    public void Countdown()
    {
        int count = int.Parse(PullsEnd.text);
        count--;
        PullsEnd.text = count.ToString();
    }

    //Simulate drawing a boomerang from Seb.
    public void DrawCardFromSeb()
    {
        ShowACard(TempBoomerang);
        AnnounceEffect("Seb could draw one of his own cards!");
        TurnPointer.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,110);
        PullsShuffle.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0,0,160);
        UpdatePreviousCards(TempBoomerang);
        Countdown();
    }
}
