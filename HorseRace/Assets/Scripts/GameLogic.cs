using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum CardColor { HEART, SPADE, CLOVER, DIAMOND };
public enum Values { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King }; // delete Jack later

// 52 cards
// 4 jack/horses
// 10 cards at side
// 38 cards left for cardStaple

public class GameLogic : MonoBehaviour
{
    // creating the cards and deck
    public static string[] suits = new string[] { "C", "D", "H", "S" };
    public static string[] values = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "A", "K","Q"};
    
    [Header("Sprites & Prefab of Deckcards")]
    public Sprite[] cardFaces;
    public GameObject cardPrefab;

    [Header("Track cards")]
    [SerializeField]
    private GameObject[] cardsOfRacetrackPos;           // manage position of track cards
    private List<string> cardsOfRacetrack;              // List of cards on track to play with

    [Header("Horses")]
    public GameObject[] horses;
    public GameObject[] horsePos;

    [Header("Deck")]
    //[HideInInspector]
    public List<string> deck;
    public Transform deckPile;
    public Transform discardPile;
    // public GameObject[] deckPrefabs;                // for anreachable prefabs
    private string currentCardPos;

    public void Start()
    {
        //Generate deck with all cards except Jacks
        deck = GenerateDeck();
        // shuffle that deck
        Shuffle(deck);
        // instantiate deckpile
        DealCards();
        // instantiate jacks
        SetHorses();
        
        // place rest on pile
    }

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

    /// <summary>
    /// handout the cards 
    /// </summary>
    void DealCards()
    {
        float zOffset = 0.03f;
        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(deckPile.position.x, deckPile.position.y, deckPile.position.z + zOffset), Quaternion.identity);
            zOffset += 0.03f;
            newCard.name = card;
        }

        // place last 9 cards of deck on top 9 pos and remove from deck
        GetTrackCards();
    }

    /// <summary>
    /// Instantiate Horses on their correct pos.
    /// </summary>
    void SetHorses()
    {
        int i = 0;

        if (horses.Equals(null))
        {
            // exception handling!
            return;
        }

        foreach (GameObject horse in horses)
        {
            GameObject newHorse = Instantiate(horses[i], new Vector3(horsePos[i].transform.position.x, horsePos[i].transform.position.y, horsePos[i].transform.position.z), Quaternion.identity);
            i++;
        }
    }

    /// <summary>
    /// Get the track cards to play with
    /// </summary>
    void GetTrackCards()
    {
        cardsOfRacetrack = new List<string>();

        for (int i = 1; i < 10; i++)
        {
            string tmp = deck[deck.Count-i];
            cardsOfRacetrack.Add(tmp);
        }

        //remove last 9 cards starting at index 39
        deck.RemoveRange(38, 9);


        int count = 0;
        foreach (string trackCard in cardsOfRacetrack)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(cardsOfRacetrackPos[count].transform.position.x, cardsOfRacetrackPos[count].transform.position.y, cardsOfRacetrackPos[count].transform.position.z), Quaternion.identity);
            newCard.name = trackCard;
            count++;
        }



        //print("last 9 elements of list are: ");
        //for (int i = 39; i < 48; i++)
        //{
        //    string test = deck[i];
        //    print(test);
        //}

        foreach (string c in cardsOfRacetrack)
        {
            print(c);
        }
    }
}


