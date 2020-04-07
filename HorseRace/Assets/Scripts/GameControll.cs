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

        //instantiate last drawn card on discardPile
        string card = gameLogic.deck.Last<string>();
        GameObject drawnCard = Instantiate(gameLogic.cardPrefab, new Vector3(gameLogic.discardPilePos.transform.position.x, gameLogic.discardPilePos.transform.position.y, gameLogic.discardPilePos.transform.position.z - zOffset), Quaternion.identity, gameLogic.discardPilePos.transform);
        drawnCard.name = card;
        drawnCard.GetComponent<UpdateSprite>().faceUp = true;
        zOffset += 0.03f;
        cardsDrawn++;

        //remove from deck
        gameLogic.deck.RemoveAt(gameLogic.deck.Count - 1);

        // get suit of that card & move horse depending on suit by getting first char of stringArray[0]
        string drawnCardSuit = gameLogic.GetSuitOfCard();
        ChooseHorseToMove(drawnCardSuit[0], 3.5f);

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
            string drawnCardSuit = gameLogic.cardsOfRacetrack[faceUpCount];
            ChooseHorseToMove(drawnCardSuit[0], -3.5f);
            
            faceUpCount++;
        }
    }

    ///<summar>Move horse with number int</summary>
    public void MoveHorse(int horse, float xDir)
    {
        gameLogic.horsePos[horse].transform.position += new Vector3(xDir, 0.0f, 0.0f);
    }

    ///<summar>Decide on char of StringArray which horse to move and in which direction. Negativ -3.5f means back</summary>
    void ChooseHorseToMove(char drawnCardSuit, float xDir)
    {
        switch (drawnCardSuit)
        {
            case 'C': MoveHorse(0, xDir); break;
            case 'D': MoveHorse(1, xDir); break;
            case 'H': MoveHorse(2, xDir); break;
            case 'S': MoveHorse(3, xDir); break;
        }
    }

    ///<summar>Restart game</summary>
    public void RestartGame()
    {
        zOffset = 0.0f;
        cardsDrawn = 0;
        faceUpCount = 0;
        gameLogic.RestartGame();
    }
}
