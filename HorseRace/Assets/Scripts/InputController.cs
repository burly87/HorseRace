using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputController : MonoBehaviour
{
    private GameController gameController;
    private UiController uiController;
    private DataManager dataManager;

    private bool _clickAble = true;
    public bool ClickAble { get => _clickAble; set => _clickAble = value; }

    // bet on horses
    int betCount_C = 0;             // sips a player bet on one horse., has to be resettet when different horse or player
    int betCount_D = 0;
    int betCount_H = 0;
    int betCount_S = 0;
    int playerIndex;                   // helper to choose the right player. Go through datamanager.players.count

    int betCounter = 0;

    public Text txt_betC;
    public Text txt_betD;
    public Text txt_betH;
    public Text txt_betS;

    public TextMeshProUGUI txt_betTitle;

    // Menu
    private bool menuOpen = false;

    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UiController>();
        dataManager = FindObjectOfType<DataManager>();

        // bet
        playerIndex = dataManager.players.Count-1;
        betCounter = dataManager.players.Count;
        Debug.Log("players participate (+1):" + playerIndex);

        txt_betTitle.text = dataManager.players[playerIndex].name + " it is your time to bet";

    }

    void Update()
    {
        GetMouseClick();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            NextPlayer(playerIndex);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (Player p in dataManager.players)
            {
                Debug.Log("Player : " + p.name + "\n" + "\n Sips C: " + p.sipsBet_C + "\n Sips D: " + p.sipsBet_D + "\n Sips H: " + p.sipsBet_H + "\n Sips S: " + p.sipsBet_S);
            }
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
                else if (hit.collider.CompareTag("BetHorse"))
                {
                    BetOnHorse(hit.collider.gameObject);
                }
            }
        }
    }

    private void DeckClicked()
    {
        gameController.PlayDeckCard();
    }

    
    // for later to get sips on horses
    public void BetOnHorse(GameObject horse)
    {

        if(horse.name[0] == 'C')
        {
            betCount_C++;
            txt_betC.text = betCount_C + " sips";
        }
        if(horse.name[0] == 'D')
        {
            betCount_D++;
            txt_betD.text = betCount_D + " sips";
        }
        if(horse.name[0] == 'H')
        {
            betCount_H++;
            txt_betH.text = betCount_H + " sips";
        }
        if(horse.name[0] == 'S')
        {
            betCount_S++;
            txt_betS.text = betCount_S + " sips";
        }
    }

    // help to assign Button in Editor
    public void DoneBet()
    {
        NextPlayer(playerIndex);
    }

    void NextPlayer(int playerNumber)
    {
        

        // save data in player data 
        dataManager.UpdatePlayerStats(playerNumber, betCount_C, betCount_D, betCount_H, betCount_S);

        // reset sips bet 
        betCount_C = 0;
        betCount_D = 0;
        betCount_H = 0;
        betCount_S = 0;

        txt_betC.text = betCount_C + " sips";
        txt_betD.text = betCount_D + " sips";
        txt_betH.text = betCount_H + " sips";
        txt_betS.text = betCount_S + " sips";

        //decrement global playerNb for next player
        if (playerNumber > 0)
        {
            playerIndex--;
        }

        // change title
        txt_betTitle.text = dataManager.players[playerIndex].name + " it is your time to bet";

        betCounter--;

        // if everyone has set bet close menu
        if (betCounter == 0)
        {
            uiController.CloseBetMenu();
            return;
        }
    }

    private void OpenMenu()
    {
        menuOpen = !menuOpen;
        uiController.menu.SetActive(menuOpen);
    }

}
