using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameControll : MonoBehaviour
{
    private static GameControll _instance;

    public static GameControll Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // --- Classes to controll ---
    private GameLogic gameLogic;
    private InputController inputController;

    // --- Play card from deck relevants ---
    float zOffset = 0f;                     // offset to set cards on top of each other, could also be done with layers in unity
    int cardsDrawn = 0;                     // hardcoded count for drawn cards to manage reshuffle deckpile
    int faceUpCount = 0;                    // hardcoded count for trackCards to face up


    // -------- Methodes ---------

    void Start()
    {
        //init all controllers
        gameLogic = FindObjectOfType<GameLogic>();
        inputController = FindObjectOfType<InputController>();

        //Start Game
        gameLogic.StartGame();
    }

    ///<summary>Make the deck clickable, draw card, put it on discardPile and remove from deck</summary>
    public void PlayDeckCard()
    {
        // move from deck to discardPile
        gameLogic.discardPile.Add(gameLogic.deck.Last<string>());

        //instantiate last drawn card
        string card = gameLogic.deck.Last<string>();
        GameObject drawnCard = Instantiate(gameLogic.cardPrefab, new Vector3(gameLogic.discardPilePos.transform.position.x, gameLogic.discardPilePos.transform.position.y, gameLogic.discardPilePos.transform.position.z - zOffset), Quaternion.identity, gameLogic.discardPilePos.transform);
        drawnCard.name = card;
        drawnCard.GetComponent<UpdateSprite>().faceUp = true;
        zOffset += 0.03f;

        cardsDrawn++;
        //remove from deck
        gameLogic.deck.RemoveAt(gameLogic.deck.Count - 1);


        // if deck is empty copy all discardPile cards to deck and clear discardPile
        if (cardsDrawn >= 39)
        {
            cardsDrawn = 0;
            gameLogic.RestackDeck();
        }
    }

    /// <summary>Flip card to Face</summary>
    public void TurnFaceofTrackCard()
    {
        // UpdateSprite to correct card sprite
        if (faceUpCount <= 8)
        {
            gameLogic.cardsOfRacetrackPos[faceUpCount].GetComponentInChildren<UpdateSprite>().faceUp = true;
            faceUpCount++;
        }
        // get back CardSuit to know which horse has to move back

    }

    ///<summar>Move horse with number int</summary>
    public void MoveHorse(int horse)
    {
        gameLogic.horsePos[horse].transform.position += new Vector3(3.5f, 0.0f, 0.0f);
    }

    public void RestartGame()
    {
        zOffset = 0.0f;
        cardsDrawn = 0;
        faceUpCount = 0;
        gameLogic.RestartGame();
    }
}
