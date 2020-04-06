using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameControll gameControll;

    // Start is called before the first frame update
    void Start()
    {
        gameControll = FindObjectOfType<GameControll>();
    }

    // Update is called once per frame
    void Update()
    {

        GetMouseClick();

        //Debug
        if (Input.GetKeyDown(KeyCode.Y))
        {
            gameControll.TurnFaceofTrackCard();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            gameControll.MoveHorse(0);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            gameControll.RestartGame();
        }
    }

    void GetMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
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
                    HorseClicked(hit.collider.gameObject);
                }
            }
        }
    }

    private void DeckClicked()
    {
        gameControll.PlayDeckCard();
        // get back suite of card
            // move horse 
    }

    private void HorseClicked(GameObject gameObject)
    {
        
    }

}
