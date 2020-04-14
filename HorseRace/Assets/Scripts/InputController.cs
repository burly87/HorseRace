using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameController gameController;
    private UiController uiController;
    private DataManager dataManager;

    private bool _clickAble = true;
    public bool ClickAble { get => _clickAble; set => _clickAble = value; }

    // bet on horses
    int betCount = 0;           // sips a player bet on one horse., has to be resettet when different horse or player
    int playerNb;               // helper to choose the right player. Go through datamanager.players.count

    // Menu
    private bool menuOpen = false;



    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UiController>();
        dataManager = FindObjectOfType<DataManager>();

        // bet
        playerNb = dataManager.players.Count;
        Debug.Log("players participate:" + playerNb);
    }

    void Update()
    {
        GetMouseClick();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0) && _clickAble)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                // what has been hit? Deck/Card/EmptySlot...
                if (hit.collider.CompareTag("Deck"))
                {
                    DeckClicked();
                }
                else if (hit.collider.CompareTag("Horse"))
                {
                    BetOnHorse(hit.collider.gameObject);
                }
            }
        }
    }

    private void DeckClicked()
    {
        gameController.PlayDeckCard();
        // get back suite of card
            // move horse 
    }

    
    // for later to get sips on horses
    private void BetOnHorse(GameObject horse)
    {
        betCount++;
        if(horse.name[0] == 'C')
        {
            Debug.Log("Bet " + betCount + " sip on " + horse.name);
        }
        if(horse.name[0] == 'D')
        {
            Debug.Log("Bet " + betCount + " sip on " + horse.name);
        }
        if(horse.name[0] == 'H')
        {
            Debug.Log("Bet " + betCount + " sip on " + horse.name);
        }
        if(horse.name[0] == 'S')
        {
            Debug.Log("Bet " + betCount + " sip on " + horse.name);
        }

        NextPlayer(playerNb);
    }

    void NextPlayer(int playerNumber)
    {
        // reset sips bet 
        betCount = 0;
        //decrement global playerNb for next player
        playerNb--;

        // save data in player data 
        dataManager.UpdatePlayerStats(playerNumber,Suits.DIAMAND, betCount);
        // dataManager.players[playerNb].sips = betCount;
        
        // give next player chance to bet

    }

    private void OpenMenu()
    {
        menuOpen = !menuOpen;
        uiController.menu.SetActive(menuOpen);
    }

}
