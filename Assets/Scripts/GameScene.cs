using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Random = System.Random;

public class GameScene : MonoBehaviour
{
    [SerializeField] private Image DefaultCardOverlay;

    [SerializeField] private Image Antenna;

    [SerializeField] private Image EyeBase,
        Arrow;

    [SerializeField] private Image PreviousCardOne;

    [SerializeField] private Text PullsEnd,
        EffectText;

    [SerializeField] private Image Biberon;

    [SerializeField] private Button HideCards,
        ShowCards;

    [SerializeField] private Image GameRole;

    [SerializeField] private Image Card1,
        Card2,
        Card3,
        Card4,
        Card5;

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

    private bool IsInit;
    private readonly Random Random = new Random();
    private bool IsMyCardsAndRoleShown;
    private bool DisplayShowMe;
    private bool DisplayHideMe;

    [SerializeField] private Canvas CanvasSprites;

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    private void Update()
    {
        if (DuckCityHub.OnGamePushInGame)
        {
            DuckCityHub.OnGamePushInGame = false;
            if (GlobalVariable.GameDto.Game.IsGameEnded)
            {
                goToEndgame();
            }
            if (!IsInit)
            {
                IsInit = true;
                InitInterface();
            }
            else
            {
                UpdateInterface();
            }
        }

        if (DisplayShowMe)
        {
            DisplayShowMe = false;
            ShowMe();
        }

        if (DisplayHideMe)
        {
            DisplayHideMe = false;
            HideMe();
        }
    }

    public void ShowMeOnClick()
    {
        DisplayShowMe = true;
    } 

    public void HideMeOnClick()
    {
        DisplayHideMe = true;
    } 

    private void InitInterface()
    {
        GameDto gameDto = GlobalVariable.GameDto;
        Game game = gameDto.Game;
        int nbPlayers = gameDto.OtherPlayers.Count() + 1;
        InitPlayersPosition(nbPlayers);
        
        // Players
        foreach (Image playerImage in PlayersPositions.Values)
        {
            playerImage.gameObject.SetActive(true);
        }
        
        // Affectation des id de joueurs à une position 
        PlayersId.Add(0, GlobalVariable.User.Id);
        int i = 1;
        foreach (OtherPlayerDto player in gameDto.OtherPlayers)
        {
            PlayersId.Add(i, player.PlayerId);
            Image playerImg = PlayersPositions[i];
            Text playerName = (Text) playerImg.transform.Find("playerName").gameObject.GetComponent(typeof(Text));
            playerName.text = player.PlayerName;
            i++;
        }
        
        LoadSpritesDegueulasse(nbPlayers);
        
        int nbDrawForFinish = ((game.NbTotalRound - game.RoundNb) * game.NbCardsToDrawByRound) + (game.NbCardsToDrawByRound - game.NbDrawnDuringRound);
        PullsEnd.text = nbDrawForFinish.ToString();

        DisplayInfoText();
        DisplayCardsInOtherPlayersHands();
    }

    public void ShowMe()
    {
        IsMyCardsAndRoleShown = true;
        IRole role = GlobalVariable.GameDto.PlayerMe.Role;
        int me = role.Name == "Blue" ? 1 : 2;
        GameRole.sprite = Sprites["role_" + me];
        List<ICard> cards = GlobalVariable.GameDto.PlayerMe.CardsInHand.ToList();
        if (cards.Count > 0)
        {
            SetCard(Card1, cards[0].Name);
            Card1.gameObject.SetActive(true);
        }
        else
        {
            Card1.gameObject.SetActive(false);
        }

        if (cards.Count > 1)
        {
            SetCard(Card2, cards[1].Name);
            Card2.gameObject.SetActive(true);
        }
        else
        {
            Card2.gameObject.SetActive(false);
        }

        if (cards.Count > 2)
        {
            SetCard(Card3, cards[2].Name);
            Card3.gameObject.SetActive(true);
        }
        else
        {
            Card3.gameObject.SetActive(false);
        }

        if (cards.Count > 3)
        {
            SetCard(Card4, cards[3].Name);
            Card4.gameObject.SetActive(true);
        }
        else
        {
            Card4.gameObject.SetActive(false);
        }

        if (cards.Count > 4)
        {
            SetCard(Card5, cards[4].Name);
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
        IsMyCardsAndRoleShown = false;
        GameRole.sprite = Sprites["role_0"];

        List<ICard> cards = GlobalVariable.GameDto.PlayerMe.CardsInHand.ToList();
        if (cards.Count > 0)
        {
            SetCard(Card1, "Default");
            Card1.gameObject.SetActive(true);
        }
        else
        {
            Card1.gameObject.SetActive(false);
        }

        if (cards.Count > 1)
        {
            SetCard(Card2, "Default");
            Card2.gameObject.SetActive(true);
        }
        else
        {
            Card2.gameObject.SetActive(false);
        }

        if (cards.Count > 2)
        {
            SetCard(Card3, "Default");
            Card3.gameObject.SetActive(true);
        }
        else
        {
            Card3.gameObject.SetActive(false);
        }

        if (cards.Count > 3)
        {
            SetCard(Card4, "Default");
            Card4.gameObject.SetActive(true);
        }
        else
        {
            Card4.gameObject.SetActive(false);
        }

        if (cards.Count > 4)
        {
            SetCard(Card5, "Default");
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
        if (key == "Yellow")
        {
            key += Random.Next(1, 5).ToString();
        }

        i.sprite = Sprites["card" + key];
    }

    public void StopShowingCard()
    {
        DefaultCardOverlay.gameObject.SetActive(false);
    }

    public void ShowACard(Image i)
    {
        DefaultCardOverlay.sprite = i.sprite;
        DefaultCardOverlay.gameObject.SetActive(true);
    }

    private void AnnounceEffect(string s)
    {
        EffectText.text = s;
    }

    public async void DrawACard(Image i)
    {
        int playerPosition = PlayersPositions.FirstOrDefault(x => x.Value == i).Key;
        string id = PlayersId[playerPosition];
        await DuckCityHub.DrawCard(id);
    }

    private void goToEndgame()
    {
        SceneManager.LoadScene("Scenes/EndgameScene");
    }

    private void InitPlayersPosition(int nbPlayers)
    {
        LaserPerPlayer.Add(0, "laser_6");
        switch (nbPlayers)
        {
            default: // case 3
                PlayersPositions.Add(1, Player3);
                PlayersPositions.Add(2, Player6);
                LaserPerPlayer.Add(1, "laser_1");
                LaserPerPlayer.Add(2, "laser_9");
                break;
            case 4:
                PlayersPositions.Add(1, Player25);
                PlayersPositions.Add(2, Player3);
                PlayersPositions.Add(3, Player6);
                LaserPerPlayer.Add(1, "laser_4");
                LaserPerPlayer.Add(2, "laser_1");
                LaserPerPlayer.Add(3, "laser_9");
                break;
            case 5:
                PlayersPositions.Add(1, Player25);
                PlayersPositions.Add(2, Player3);
                PlayersPositions.Add(3, Player5);
                PlayersPositions.Add(4, Player75);
                LaserPerPlayer.Add(1, "laser_4");
                LaserPerPlayer.Add(2, "laser_1");
                LaserPerPlayer.Add(3, "laser_11");
                LaserPerPlayer.Add(4, "laser_8");
                break;
            case 6:
                PlayersPositions.Add(1, Player1);
                PlayersPositions.Add(2, Player35);
                PlayersPositions.Add(3, Player4);
                PlayersPositions.Add(4, Player65);
                PlayersPositions.Add(5, Player7);
                LaserPerPlayer.Add(1, "laser_5");
                LaserPerPlayer.Add(2, "laser_2");
                LaserPerPlayer.Add(3, "laser_14");
                LaserPerPlayer.Add(4, "laser_10");
                LaserPerPlayer.Add(5, "laser_7");
                break;
            case 7:
                PlayersPositions.Add(1, Player1);
                PlayersPositions.Add(2, Player35);
                PlayersPositions.Add(3, Player4);
                PlayersPositions.Add(4, Player5);
                PlayersPositions.Add(5, Player6);
                PlayersPositions.Add(6, Player7);
                LaserPerPlayer.Add(1, "laser_5");
                LaserPerPlayer.Add(2, "laser_2");
                LaserPerPlayer.Add(3, "laser_14");
                LaserPerPlayer.Add(4, "laser_11");
                LaserPerPlayer.Add(5, "laser_9");
                LaserPerPlayer.Add(6, "laser_7");
                break;
            case 8:
                PlayersPositions.Add(1, Player1);
                PlayersPositions.Add(2, Player2);
                PlayersPositions.Add(3, Player3);
                PlayersPositions.Add(4, Player4);
                PlayersPositions.Add(5, Player5);
                PlayersPositions.Add(6, Player6);
                PlayersPositions.Add(7, Player7);
                LaserPerPlayer.Add(1, "laser_5");
                LaserPerPlayer.Add(2, "laser_3");
                LaserPerPlayer.Add(3, "laser_1");
                LaserPerPlayer.Add(4, "laser_14");
                LaserPerPlayer.Add(5, "laser_11");
                LaserPerPlayer.Add(6, "laser_9");
                LaserPerPlayer.Add(7, "laser_7");
                break;
        }
    }

    private void LoadSprites(int nbPlayers)
    {
        Game game = GlobalVariable.GameDto.Game;
        
        // Roles
        string[] rolePaths =
        {
            GlobalVariable.SpritePathBase + "Ducks/Base_Duck/Canard_role.png",
            GlobalVariable.SpritePathBase + "Ducks/Base_Duck/Canard_good.png",
            GlobalVariable.SpritePathBase + "Ducks/Base_Duck/Canard_bad.png"
        };
        for (int i = 0; i < rolePaths.Length; i++)
        {
            AsyncOperationHandle<Sprite> roleHandle = Addressables.LoadAssetAsync<Sprite>(rolePaths[i]);
            int i1 = i;
            roleHandle.Completed += obj =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    Sprites.Add("role_" + i1, obj.Result);
                }
            };
        }

        // Eye
        string eyePath = GlobalVariable.SpritePathBase + "HUD/Bomb/" + nbPlayers + "_joueurs/Cadran_" + nbPlayers +
                         "J_02.png";
        AsyncOperationHandle<Sprite> eyeHandle = Addressables.LoadAssetAsync<Sprite>(eyePath);
        eyeHandle.Completed += obj =>
        {
            if (obj.Status == AsyncOperationStatus.Succeeded)
            {
                Sprites.Add("eye", obj.Result);
                EyeBase.sprite = obj.Result;
            }
        };

        // Bottle
        for (int i = 0; i <= nbPlayers; i++)
        {
            string biberonPath = GlobalVariable.SpritePathBase + "HUD/Bomb/" + nbPlayers + "_joueurs/Biberon_0" +
                                 nbPlayers + "_0" + i + ".png";
            int i1 = i;
            AsyncOperationHandle<Sprite> biberonHandle = Addressables.LoadAssetAsync<Sprite>(biberonPath);
            biberonHandle.Completed += obj =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    Sprites.Add("biberon_" + i1, obj.Result);
                    if (i1 == game.NbGreenDrawn)
                    {
                        Biberon.sprite = obj.Result;
                    }
                }
            };
        }

        // Arrow
        for (int i = 1; i <= nbPlayers; i++)
        {
            string arrowPath = GlobalVariable.SpritePathBase + "HUD/Bomb/" + nbPlayers + "_joueurs/Fleches_0" +
                               nbPlayers + "_0" + i + ".png";
            int i1 = i;

            AsyncOperationHandle<Sprite> arrowHandle = Addressables.LoadAssetAsync<Sprite>(arrowPath);
            arrowHandle.Completed += obj =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    Sprites.Add("arrow_" + i1, obj.Result);
                    if (i1 == game.NbDrawnDuringRound + 1)
                    {
                        Arrow.sprite = obj.Result;
                    }
                }
            };
        }

        // Laser
        foreach (string laserKey in LaserPerPlayer.Values)
        {
            string laserPath = GlobalVariable.SpritePathBase + "HUD/Bomb/Lasers/graphisme_bombe_" + laserKey +
                               "_v1.png";
            AsyncOperationHandle<Sprite> laserHandle = Addressables.LoadAssetAsync<Sprite>(laserPath);
            laserHandle.Completed += obj =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    Sprites.Add(laserKey, obj.Result);
                    int nextPlayerNumber = PlayersId.FirstOrDefault(x => x.Value == game.CurrentPlayerId).Key;
                    if (laserKey == LaserPerPlayer[nextPlayerNumber])
                    {
                        Antenna.sprite = obj.Result;
                    }
                }
            };
        }

        // Cards
        Dictionary<string, string> dictCards = new Dictionary<string, string>
        {
            {"cardDefault", GlobalVariable.SpritePathBase + "Cards/Back.png"},
            {"cardBomb", GlobalVariable.SpritePathBase + "Cards/DETONNATEUR.png"},
            {"cardGreen", GlobalVariable.SpritePathBase + "Cards/LIQUIDE.png"},
            {"cardYellow1", GlobalVariable.SpritePathBase + "Cards/NULL_01.png"},
            {"cardYellow2", GlobalVariable.SpritePathBase + "Cards/NULL_02.png"},
            {"cardYellow3", GlobalVariable.SpritePathBase + "Cards/NULL_03.png"},
            {"cardYellow4", GlobalVariable.SpritePathBase + "Cards/NULL_04.png"}
        };

        foreach (KeyValuePair<string, string> kv in dictCards)
        {
            AsyncOperationHandle<Sprite> cardHandle = Addressables.LoadAssetAsync<Sprite>(kv.Value);
            cardHandle.Completed += obj =>
            {
                if (obj.Status == AsyncOperationStatus.Succeeded)
                {
                    Sprites.Add(kv.Key, obj.Result);
                    
                    // Previous card
                    if (game.PreviousDrawnCard != null && kv.Key.Contains(game.PreviousDrawnCard.Name))
                    {
                        PreviousCardOne.sprite = obj.Result;
                    }
                }
            };
        }
    }
    
    private void LoadSpritesDegueulasse(int nbPlayers)
    {
        Game game = GlobalVariable.GameDto.Game;
        
        // Roles
        for (int i = 0; i < 3; i++)
        {
            Image img = (Image) CanvasSprites.transform.Find("role_"+i).gameObject.GetComponent(typeof(Image));
            Sprites.Add("role_" + i, img.sprite);
        }

        // Eye
        Canvas canvasPlayers = (Canvas) CanvasSprites.transform.Find("Canvas_" + nbPlayers).gameObject
            .GetComponent(typeof(Canvas));
        Image imgEye = (Image) canvasPlayers.transform.Find("eye").gameObject.GetComponent(typeof(Image));
        Sprites.Add("eye", imgEye.sprite);

        // Bottle
        for (int i = 0; i <= nbPlayers; i++)
        {
            Image imgBib = (Image) canvasPlayers.transform.Find("biberon_"+i).gameObject.GetComponent(typeof(Image));
            Sprites.Add("biberon_"+i,imgBib.sprite);
        }

        // Arrow
        for (int i = 1; i <= nbPlayers; i++)
        {
            Image imgArr = (Image) canvasPlayers.transform.Find("arrow_"+i).gameObject.GetComponent(typeof(Image));
            Sprites.Add("arrow_"+i,imgArr.sprite);
        }

        // Laser
        Canvas canvasLasers = (Canvas) CanvasSprites.transform.Find("Canvas_Lasers").gameObject
            .GetComponent(typeof(Canvas));
        foreach (string laserKey in LaserPerPlayer.Values)
        {
            Image imgLas = (Image) canvasLasers.transform.Find(laserKey).gameObject.GetComponent(typeof(Image));
            Sprites.Add(laserKey,imgLas.sprite);
        }

        // Cards
        Dictionary<string, string> dictCards = new Dictionary<string, string>
        {
            {"cardDefault", GlobalVariable.SpritePathBase + "Cards/Back.png"},
            {"cardBomb", GlobalVariable.SpritePathBase + "Cards/DETONNATEUR.png"},
            {"cardGreen", GlobalVariable.SpritePathBase + "Cards/LIQUIDE.png"},
            {"cardYellow1", GlobalVariable.SpritePathBase + "Cards/NULL_01.png"},
            {"cardYellow2", GlobalVariable.SpritePathBase + "Cards/NULL_02.png"},
            {"cardYellow3", GlobalVariable.SpritePathBase + "Cards/NULL_03.png"},
            {"cardYellow4", GlobalVariable.SpritePathBase + "Cards/NULL_04.png"}
        };

        foreach (string key in dictCards.Keys)
        {
            Image imgCard = (Image) CanvasSprites.transform.Find(key).gameObject.GetComponent(typeof(Image));
            Sprites.Add(key,imgCard.sprite);
        }
    }
    

    private void UpdateInterface()
    {
        Game game = GlobalVariable.GameDto.Game;
        if (IsMyCardsAndRoleShown)
        {
            ShowMe();
        }
        else
        {
            HideMe();
        }
        
        int nbDrawForFinish = ((game.NbTotalRound - game.RoundNb) * game.NbCardsToDrawByRound) +
                              (game.NbCardsToDrawByRound - game.NbDrawnDuringRound);
        PullsEnd.text = nbDrawForFinish.ToString();

        SetCard(PreviousCardOne, game.PreviousDrawnCard.Name); 

        int arrowInt = game.NbDrawnDuringRound + 1;
        Arrow.sprite = Sprites["arrow_" + arrowInt];

        Biberon.sprite = Sprites["biberon_" + game.NbGreenDrawn];

        DisplayAntenna();
        DisplayInfoText();
        DisplayCardsInOtherPlayersHands();
    }

    private void DisplayAntenna()
    {
        int nextPlayerNumber =
            PlayersId.FirstOrDefault(x => x.Value == GlobalVariable.GameDto.Game.CurrentPlayerId).Key;
        Antenna.sprite = Sprites[LaserPerPlayer[nextPlayerNumber]];
    }

    private void DisplayInfoText()
    {
        string nextPlayerName = GlobalVariable.GameDto.Game.CurrentPlayerName;
        AnnounceEffect("C'est à " + nextPlayerName + " de piocher !");
    }

    private void DisplayCardsInOtherPlayersHands()
    {
        foreach (OtherPlayerDto otherPlayer in GlobalVariable.GameDto.OtherPlayers)
        {
            int otherPlayerNumber = PlayersId.FirstOrDefault(x => x.Value == otherPlayer.PlayerId).Key;
            Image otherPlayerImg = PlayersPositions[otherPlayerNumber];
            Image otherPlayerCards = (Image) otherPlayerImg.transform.Find("playerCards").gameObject.GetComponent(typeof(Image));
            otherPlayerCards.gameObject.SetActive(otherPlayer.NbCardsInHand != 0);
            Image otherPlayerPosition = PlayersPositions[otherPlayerNumber];
            Button otherPlayerButton = (Button) otherPlayerPosition.gameObject.GetComponent(typeof(Button));
            otherPlayerButton.interactable = (otherPlayer.NbCardsInHand != 0);
        }
    }

    public async void QuitMidGame()
    {
        await DuckCityHub.QuitMidGame();
        SceneManager.LoadScene("Scenes/HomeScene");
    }
}