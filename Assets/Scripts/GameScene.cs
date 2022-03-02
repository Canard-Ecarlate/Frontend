using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using Utils;
using Random = System.Random;

public class GameScene : MonoBehaviour
{
    [SerializeField] private Image DefaultCardOverlay, MyCardOverlay;
    [SerializeField] private Image Antenna;
    [SerializeField] private Image EyeBase, Arrow;
    [SerializeField] private Image PreviousCardOne;
    [SerializeField] private Text PullsEnd, EffectText;
    [SerializeField] private Image Biberon;
    [SerializeField] private Button HideCards, ShowCards;

    [SerializeField] private Image GameRole;
    [SerializeField] private Image Card1,Card2,Card3,Card4,Card5;

    [SerializeField] private Image Player1,
        Player2,
        Player25,
        Player3,
        Player35,
        Player4,
        Player5,
        Player6,
        Player65,
        Player7,
        Player75;

    private readonly Dictionary<int, Image> PlayersPositions = new Dictionary<int, Image>();
    private readonly Dictionary<int, string> LaserPerPlayer = new Dictionary<int, string>();
    private readonly Dictionary<int, string> PlayersId = new Dictionary<int, string>();

    private readonly Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;

        int nbPlayers = GlobalVariable.RoomDto.RoomConfiguration.NbPlayers;
        InitPlayersPosition(nbPlayers);
        PullsEnd.text = (nbPlayers * 4).ToString();
        int i = 0;
        foreach (var player in GlobalVariable.Players)
        {
            PlayersId.Add(i,player.Id);

            Image playerimg = PlayersPositions[i];

            Text playerName = (Text) playerimg.transform.Find("playerName").gameObject.GetComponent(typeof(Text));

            playerName.text = player.Name;
            i++;
        }
        
        LoadSprites();
    }

    private void Update()
    {
        if (DuckCityHub.OnGamePush)
        {
            UpdateInterface();
            DuckCityHub.OnGamePush = false;
        }
    }

    public void ShowMe()
    {
        int me = 2;
        GameRole.sprite = Sprites["role_" + me];
        List<ICard> cards = GlobalVariable.GameDto.PlayerMe.CardsInHand.ToList();
        if (cards.Count>0)
        {
            SetCard(Card1,cards[0].Name);
            Card1.gameObject.SetActive(true);
        }
        else
        {
            Card1.gameObject.SetActive(false);
        }
        if (cards.Count>1)
        {
            SetCard(Card2,cards[1].Name);
            Card2.gameObject.SetActive(true);
        }
        else
        {
            Card2.gameObject.SetActive(false);
        }
        if (cards.Count>2)
        {
            SetCard(Card3,cards[2].Name);
            Card3.gameObject.SetActive(true);
        }
        else
        {
            Card3.gameObject.SetActive(false);
        }
        if (cards.Count>3)
        {
            SetCard(Card4,cards[3].Name);
            Card4.gameObject.SetActive(true);
        }
        else
        {
            Card4.gameObject.SetActive(false);
        }
        if (cards.Count>4)
        {
            SetCard(Card5,cards[4].Name);
            Card5.gameObject.SetActive(true);
        }
        else
        {
            Card5.gameObject.SetActive(false);
        }

        ShowCards.gameObject.SetActive(true);
        HideCards.gameObject.SetActive(false);
    }

    public void HideMe()
    {
        GameRole.sprite = Sprites["role_0"];
        
        List<ICard> cards = GlobalVariable.GameDto.PlayerMe.CardsInHand.ToList();
        if (cards.Count>0)
        {
            SetCard(Card1,"Default");
            Card1.gameObject.SetActive(true);
        }
        else
        {
            Card1.gameObject.SetActive(false);
        }
        if (cards.Count>1)
        {
            SetCard(Card2,"Default");
            Card2.gameObject.SetActive(true);
        }
        else
        {
            Card2.gameObject.SetActive(false);
        }
        if (cards.Count>2)
        {
            SetCard(Card3,"Default");
            Card3.gameObject.SetActive(true);
        }
        else
        {
            Card3.gameObject.SetActive(false);
        }
        if (cards.Count>3)
        {
            SetCard(Card4,"Default");
            Card4.gameObject.SetActive(true);
        }
        else
        {
            Card4.gameObject.SetActive(false);
        }
        if (cards.Count>4)
        {
            SetCard(Card5,"Default");
            Card5.gameObject.SetActive(true);
        }
        else
        {
            Card5.gameObject.SetActive(false);
        }
        
        ShowCards.gameObject.SetActive(false);
        HideCards.gameObject.SetActive(true);
    }

    private void SetCard(Image i, string key)
    {
        if (key == "Yellow") key += new Random().Next(1, 5).ToString();
        i.sprite = Sprites["card" + key];
    }
    
    public void StopShowingCard(Image i)
    {
        i.gameObject.SetActive(false);
    }

    public void ShowMyCard(Image i)
    {
        MyCardOverlay.sprite = i.sprite;
        MyCardOverlay.gameObject.SetActive(true);
    }
    
    public async void ShowACard(Image i)
    {
        DefaultCardOverlay.sprite = i.sprite;
        DefaultCardOverlay.gameObject.SetActive(true);
        await Task.Delay(3000).ConfigureAwait(false);
        StopShowingCard(DefaultCardOverlay);
    }

    public void AnnounceEffect(string s)
    {
        EffectText.text = s;
    }

    public void UpdatePreviousCards(Image i)
    {
        PreviousCardOne.sprite = i.sprite;
    }

    public void Countdown()
    {
        int count = int.Parse(PullsEnd.text);
        count--;
        PullsEnd.text = count.ToString();
    }

    public void DrawACard(Image i)
    {
        int playerPosition = PlayersPositions.FirstOrDefault(x => x.Value == i).Key;
        string id = PlayersId[playerPosition];
        DuckCityHub.DrawCard(id);
    }
    
    public void LoadSprites()
    {
        int nbPlayers = GlobalVariable.RoomDto.RoomConfiguration.NbPlayers;

        string[] rolePaths = new[] {GlobalVariable.SpritePathBase +"Ducks/Base_Duck/Canard_role.png",
            GlobalVariable.SpritePathBase +"Ducks/Base_Duck/Canard_good.png",
            GlobalVariable.SpritePathBase +"Ducks/Base_Duck/Canard_bad.png"};

        for (int i = 0; i < rolePaths.Length; i++)
        {
            AsyncOperationHandle<Sprite> roleHandle = Addressables.LoadAssetAsync<Sprite>(rolePaths[i]);
            var i1 = i;
            roleHandle.Completed+= obj =>
            {
                string key = "role_"+i1;
                LoadSprite(obj,key);
            };
        }
        
        string eyePath = GlobalVariable.SpritePathBase + "HUD/Bomb/" + nbPlayers + "_joueurs/Cadran_" + nbPlayers + "J_02.png";
        AsyncOperationHandle<Sprite> eyeHandle = Addressables.LoadAssetAsync<Sprite>(eyePath);
        eyeHandle.Completed+= obj =>
        {
            string key = "eye";
            LoadSprite(obj,key);
        };

        for (int i = 0; i < nbPlayers; i++)
        {
            string arrowPath = GlobalVariable.SpritePathBase + "HUD/Bomb/" + nbPlayers + "_joueurs/Fleches_0" + nbPlayers + "_0"+(i+1)+".png";
            
            AsyncOperationHandle<Sprite> arrowHandle = Addressables.LoadAssetAsync<Sprite>(eyePath);
            arrowHandle.Completed+= obj =>
            {
                string key = "arrow_"+(i+1);
                LoadSprite(obj,key);
            };
        }

        string biberon0Path=GlobalVariable.SpritePathBase + "HUD/Bomb/" + nbPlayers + "_joueurs/Biberon_0" + nbPlayers + "_00.png";
        AsyncOperationHandle<Sprite> biberon0Handle = Addressables.LoadAssetAsync<Sprite>(biberon0Path);
        biberon0Handle.Completed+= obj =>
        {
            string key = "biberon_0";
            LoadSprite(obj,key);
        };
        
        for (int i = 1; i <= nbPlayers; i++)
        {
            string antennaPath=GlobalVariable.SpritePathBase + "HUD/Bomb/" + nbPlayers + "_joueurs/Fleches_0" + nbPlayers + "_0"+i+".png";
            string biberonPath=GlobalVariable.SpritePathBase + "HUD/Bomb/" + nbPlayers + "_joueurs/Biberon_0" + nbPlayers + "_0"+i+".png";
            
            int i1 = i;
            
            AsyncOperationHandle<Sprite> antennaHandle = Addressables.LoadAssetAsync<Sprite>(antennaPath);
            antennaHandle.Completed+= obj =>
            {
                string key = "laser_"+i1;
                LoadSprite(obj,key);
            };
            AsyncOperationHandle<Sprite> biberonHandle = Addressables.LoadAssetAsync<Sprite>(biberonPath);
            biberonHandle.Completed+= obj =>
            {
                string key = "biberon_"+i1;
                LoadSprite(obj,key);
            };
        }

        foreach (var v in LaserPerPlayer.Values)
        {
            string laserPath=GlobalVariable.SpritePathBase + "HUD/Bomb/Lasers/graphisme_bombe_" + v + "_v1.png";
            AsyncOperationHandle<Sprite> laserHandle = Addressables.LoadAssetAsync<Sprite>(laserPath);
            laserHandle.Completed+=obj =>
            {
                LoadSprite(obj,v);
            };
        }

        Dictionary<string, string> dictCards = new Dictionary<string, string>();
        dictCards.Add("cardDefault",GlobalVariable.SpritePathBase + "Cards/Back.png");
        dictCards.Add("cardBomb",GlobalVariable.SpritePathBase + "Cards/DETONNATEUR.png");
        dictCards.Add("cardGreen",GlobalVariable.SpritePathBase + "Cards/LIQUIDE.png");
        dictCards.Add("cardYellow1",GlobalVariable.SpritePathBase + "Cards/NULL_01.png");
        dictCards.Add("cardYellow2",GlobalVariable.SpritePathBase + "Cards/NULL_02.png");
        dictCards.Add("cardYellow3",GlobalVariable.SpritePathBase + "Cards/NULL_03.png");
        dictCards.Add("cardYellow4",GlobalVariable.SpritePathBase + "Cards/NULL_04.png");

        foreach (var kv in dictCards)
        {
            AsyncOperationHandle<Sprite> cardHandle = Addressables.LoadAssetAsync<Sprite>(kv.Value);
            cardHandle.Completed += obj =>
            {
                LoadSprite(obj, kv.Key);
            };
        }

        InitInterface("players");
    }

    private void LoadSprite(AsyncOperationHandle<Sprite> handleToCheck, string key)
    {
        if(handleToCheck.Status == AsyncOperationStatus.Succeeded)
        {
            Sprites.Add(key,handleToCheck.Result);
            InitInterface(key);
        }
    }
    
    private void InitPlayersPosition(int nbPlayers)
    {
        LaserPerPlayer.Add(0,"laser_6");
        switch (nbPlayers)
        {
            case 3:
                PlayersPositions.Add(1,Player3);
                PlayersPositions.Add(2,Player6);
                LaserPerPlayer.Add(1,"laser_1");
                LaserPerPlayer.Add(2,"laser_9");
                break;
            case 4:
                PlayersPositions.Add(1,Player25);
                PlayersPositions.Add(2,Player3);
                PlayersPositions.Add(3,Player6);
                LaserPerPlayer.Add(1,"laser_4");
                LaserPerPlayer.Add(2,"laser_1");
                LaserPerPlayer.Add(3,"laser_9");
                break;
            case 5:
                PlayersPositions.Add(1,Player25);
                PlayersPositions.Add(2,Player3);
                PlayersPositions.Add(3,Player5);
                PlayersPositions.Add(4,Player75);
                LaserPerPlayer.Add(1,"laser_4");
                LaserPerPlayer.Add(2,"laser_1");
                LaserPerPlayer.Add(3,"laser_11");
                LaserPerPlayer.Add(4,"laser_8");
                break;
            case 6:
                PlayersPositions.Add(1,Player1);
                PlayersPositions.Add(2,Player35);
                PlayersPositions.Add(3,Player4);
                PlayersPositions.Add(4,Player65);
                PlayersPositions.Add(5,Player7);
                LaserPerPlayer.Add(1,"laser_5");
                LaserPerPlayer.Add(2,"laser_2");
                LaserPerPlayer.Add(3,"laser_14");
                LaserPerPlayer.Add(4,"laser_10");
                LaserPerPlayer.Add(5,"laser_7");
                break;
            case 7:
                PlayersPositions.Add(1,Player1);
                PlayersPositions.Add(2,Player35);
                PlayersPositions.Add(3,Player4);
                PlayersPositions.Add(4,Player5);
                PlayersPositions.Add(5,Player6);
                PlayersPositions.Add(6,Player7);
                LaserPerPlayer.Add(1,"laser_5");
                LaserPerPlayer.Add(2,"laser_2");
                LaserPerPlayer.Add(3,"laser_14");
                LaserPerPlayer.Add(4,"laser_11");
                LaserPerPlayer.Add(5,"laser_9");
                LaserPerPlayer.Add(6,"laser_7");
                break;
            case 8:
                PlayersPositions.Add(1,Player1);
                PlayersPositions.Add(2,Player2);
                PlayersPositions.Add(3,Player3);
                PlayersPositions.Add(4,Player4);
                PlayersPositions.Add(5,Player5);
                PlayersPositions.Add(6,Player6);
                PlayersPositions.Add(7,Player7);
                LaserPerPlayer.Add(1,"laser_5");
                LaserPerPlayer.Add(2,"laser_3");
                LaserPerPlayer.Add(3,"laser_1");
                LaserPerPlayer.Add(4,"laser_14");
                LaserPerPlayer.Add(5,"laser_11");
                LaserPerPlayer.Add(6,"laser_9");
                LaserPerPlayer.Add(7,"laser_7");
                break;
            default:
                //nothing to do
                break;
        }
    }
    
    public void InitInterface(string key)
    {
        switch (key)
        {
            case "role_0":
                GameRole.sprite = Sprites["role_0"];
                break;
            case "eye":
                EyeBase.sprite = Sprites["eye"];
                break;
            case "pointer_1":
                Arrow.sprite = Sprites["pointer_1"];
                break;
            case "biberon_0":
                Biberon.sprite = Sprites["biberon_0"];
                break;
            case "cardDefault":
                PreviousCardOne.sprite = Sprites["cardDefault"];
                Card1.sprite = Sprites["cardDefault"];
                Card2.sprite = Sprites["cardDefault"];
                Card3.sprite = Sprites["cardDefault"];
                Card4.sprite = Sprites["cardDefault"];
                Card5.sprite = Sprites["cardDefault"];
                break;
            case "players":
                foreach (var v in PlayersPositions.Values)
                {
                    v.gameObject.SetActive(true);
                }
                break;
            default:
                //nothing
                break;
        }
    }

    public void UpdateInterface()
    {
        Game game = GlobalVariable.GameDto.Game;
        int nbDrawForFinish =
            ((game.NbTotalRound - game.RoundNb) * game.NbCardsToDrawByRound) + (game.NbCardsToDrawByRound - game.NbDrawnDuringRound);
        PullsEnd.text = nbDrawForFinish.ToString();

        if (game.PreviousDrawnCard != null)
        {
            PreviousCardOne.sprite = Sprites[game.PreviousDrawnCard.Name];
        }
        else
        {
            PreviousCardOne.sprite = Sprites["cardDefault"];
        }

        int arrowInt = game.NbDrawnDuringRound + 1;
        Arrow.sprite = Sprites["arrow_" + arrowInt];

        Biberon.sprite = Sprites["biberon_" + game.NbGreenDrawn];

        int nextPlayerNumber = PlayersId.FirstOrDefault(x => x.Value == game.CurrentPlayerId).Key;
        Antenna.sprite = Sprites[LaserPerPlayer[nextPlayerNumber]];
        string nextPlayerName = GlobalVariable.Players.FirstOrDefault(x => x.Id == game.CurrentPlayerId).Name;
        AnnounceEffect("C'est à "+nextPlayerName+" de piocher !");

        foreach (OtherPlayerDto otherPlayer in GlobalVariable.GameDto.OtherPlayers)
        {
            int otherPlayerNumber = PlayersId.FirstOrDefault(x => x.Value == otherPlayer.PlayerId).Key;
            Image otherPlayerimg = PlayersPositions[otherPlayerNumber];

            Image otherPlayerCards = (Image) otherPlayerimg.transform.Find("playerCards").gameObject.GetComponent(typeof(Image));
            otherPlayerCards.gameObject.SetActive(otherPlayer.NbCardsInHand!=0);
            Image otherPlayerPosition = PlayersPositions[otherPlayerNumber];
            Button otherPlayerButton = (Button) otherPlayerPosition.gameObject.GetComponent(typeof(Button));
            otherPlayerButton.interactable = (otherPlayer.NbCardsInHand != 0);
        }
    }
}
