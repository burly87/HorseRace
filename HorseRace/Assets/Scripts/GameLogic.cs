﻿using System.Collections;
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
    [HideInInspector]
    public List<string> deck;
    public Transform deckPile;                          // deck 
    public Transform discardPile;                       // for the card drawn
    // public GameObject[] deckPrefabs;                 // for anreachable prefabs

    public void Start()
    {
        //Generate deck with all cards except Jacks
        deck = GenerateDeck();
        // shuffle that deck
        Shuffle(deck);
        // instantiate deckpile
        DealCards();
        // instantiate jacks
        StartCoroutine( SetHorses());
        
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
            GameObject newCard = Instantiate(cardPrefab, new Vector3(deckPile.position.x, deckPile.position.y, deckPile.position.z + zOffset), Quaternion.identity, deckPile);
            zOffset += 0.03f;
            newCard.name = card;
        }

        // place last 9 cards of deck on top 9 pos and remove from deck
        StartCoroutine( GetTrackCards());
    }

    /// <summary>
    /// Instantiate Horses on their correct pos.
    /// </summary>
    IEnumerator SetHorses()
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

    /// <summary>
    /// Get the track cards to play with
    /// </summary>
    IEnumerator GetTrackCards()
    {
        cardsOfRacetrack = new List<string>();

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

        //foreach (string c in cardsOfRacetrack)
        //{
        //    print(c);
        //}
    }
}


