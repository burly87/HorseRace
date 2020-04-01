using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public static string[] values = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
    
    public Sprite[] cardFaces;
    public GameObject cardPrefab;
    public List<string> deck;

    // positioning the cards
    private GameObject[] cardsOfRacetrackPos;
    private List<string>[] cardsOfRacetrack;

    private string currentCardPos;

    public void Start()
    {
        deck = GenerateDeck();
        Shuffle(deck);
        //test the cards in the deck:
        foreach (string card in deck)
        {
            print(card);
        }
        DealCards();
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
        float yOffset = 0.0f;
        float zOffset = 0.03f;

        foreach (string card in deck)
        {
            GameObject newCard = Instantiate(cardPrefab, new Vector3(this.transform.position.x, this.transform.position.y - yOffset, this.transform.position.z - zOffset), Quaternion.identity);
            newCard.name = card;
            //newCard.GetComponent<UpdateSprite>().faceUp = true;
            yOffset += 0.4f;
            zOffset += 0.03f;
        }
    }
}


