using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 52 cards
// 4 jack/horses
// 10 cards at side
// 38 cards left for cardStaple

public class GameLogic : MonoBehaviour
{
    // creating the cards and deck
    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "A", "K", "Q" };

    [Header("Sprites & Prefab of Deckcards")]
    public Sprite[] cardFaces;
    public GameObject cardPrefab;

    [Header("Track cards")]
    [SerializeField]
    public GameObject[] cardsOfRacetrackPos;                        // manage position of track cards
    public List<string> cardsOfRacetrack = new List<string>();      // List of cards on track to play with

    [Header("Horses")]
    public GameObject[] horses;
    public GameObject[] horsePos;

    [Header("Deck")]
    public List<string> deck;
    public List<string> discardPile = new List<string>();           // pile to save every drawn card to swap later with deckpile so we can start from begining. dont forget to shuffle again
    public Transform deckPilePos;                                   // deck 
    public Transform discardPilePos;                                // for the card drawn
    // public GameObject[] deckPrefabs;                             // for anreachable prefabs

    public void StartGame()
    {
        //Generate deck with all cards except Jacks
        deck = GenerateDeck();
        // shuffle that deck
        Shuffle(deck);
        // instantiate deckpile
        DealCards();
        // instantiate jacks
        StartCoroutine(DealHorses());
    }

    /// <summary>generate a complete new Deck based on suits and values in string array</summary>
    /// <returns></returns>
    public static List<string> GenerateDeck()
    {
        List<string> newDeck = new List<string>();
        foreach (string s in suits)
        {
            foreach (string v in values)
            {
                newDeck.Add(s + v);
            }
        }
        return newDeck;
    }

    // FisherYates shuffle algorithm
    void Shuffle<T>(List<T> list)
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

    /// <summary>handout the cards </summary>
    public void DealCards()
    {
        float zOffset = 0.03f;
        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(deckPilePos.position.x, deckPilePos.position.y, deckPilePos.position.z + zOffset), Quaternion.identity, deckPilePos);
            zOffset += 0.03f;
            newCard.name = card;
        }

        // place last 9 cards of deck on top 9 pos and remove from deck
        StartCoroutine(DeadlTrackCards());
    }

    /// <summary> Instantiate the Cards on the Track & remove them from deck </summary>
    public IEnumerator DeadlTrackCards()
    {
        discardPile.Clear();
        //add last 9 cards to track cards and remove them from deck
        for (int i = 0; i < 9; i++)
        {
            cardsOfRacetrack.Add(deck.Last<string>());
            deck.RemoveAt(deck.Count - 1);
        }

        int count = 0;
        foreach (string trackCard in cardsOfRacetrack)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject newCard = Instantiate(cardPrefab, new Vector3(cardsOfRacetrackPos[count].transform.position.x, cardsOfRacetrackPos[count].transform.position.y, cardsOfRacetrackPos[count].transform.position.z), Quaternion.identity, cardsOfRacetrackPos[count].transform);
            newCard.name = trackCard;
            count++;
        }
    }

    /// <summary> Instantiate Horses on their correct pos.</summary>
    public IEnumerator DealHorses()
    {
        int i = 0;

        if (horses.Equals(null))
        {
            // exception handling!

        }

        foreach (GameObject horse in horses)
        {
            yield return new WaitForSeconds(0.15f);
            GameObject newHorse = Instantiate(horses[i], new Vector3(horsePos[i].transform.position.x, horsePos[i].transform.position.y, horsePos[i].transform.position.z), Quaternion.identity, horsePos[i].transform);
            i++;
        }
    }

    /// <summary> Restack deck with the cards in the discardPile </summary>
    public void RestackDeck()
    {
        foreach (Transform child in discardPilePos)
        {
            if (child.CompareTag("Card"))
            {
                Destroy(child.gameObject);
            }
        }

        deck.Clear();

        foreach (string card in discardPile)
        {
            deck.Add(card);
        }
        discardPile.Clear();
        Shuffle(deck);
    }

    ///<summary> retrun string of last drawn card in discardPile</summary>
    public string GetSuitOfCard()
    {
        string suitOfCard = discardPile.Last<string>();
        return suitOfCard;
    }

    ///<summary>Restart Game by clearing lists, destroy Instantiated Cards and reset Positions</summary>
    public void RestartGame()
    {
        //Make sure that nothing is clickable or started till RestartGame is finished complettly

        //clear all lists
        deck.Clear();
        discardPile.Clear();
        cardsOfRacetrack.Clear();
        //destroy all instantiated cards
        foreach (Transform child in discardPilePos)
        {
            if (child.CompareTag("Card"))
            {
                Destroy(child.gameObject);
            }
        }
        foreach (Transform child in deckPilePos)
        {
            if (child.CompareTag("Card"))
            {
                Destroy(child.gameObject);
            }
        }
        foreach (GameObject child in cardsOfRacetrackPos)
        {
            foreach (Transform trans in child.transform)
            {
                if (trans.CompareTag("Card"))
                {
                    Destroy(trans.gameObject);
                }
            }
        }
        foreach (GameObject child in horsePos)
        {
            foreach (Transform trans in child.transform)
            {
                if (trans.CompareTag("Horse"))
                {
                    Destroy(trans.gameObject);
                }
            }
        }
        //reset Horse Position
        horsePos[0].transform.position = new Vector3(-19.0f, 5.5f, 0.0f);
        horsePos[1].transform.position = new Vector3(-19.0f, 1.0f, 0.0f);
        horsePos[2].transform.position = new Vector3(-19.0f, -3.5f, 0.0f);
        horsePos[3].transform.position = new Vector3(-19.0f, -8.0f, 0.0f);
        //start game
        StartGame();
    }

}


