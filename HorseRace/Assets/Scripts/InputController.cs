using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private GameController gameController;
    private UiController uiController;

    private bool _clickAble = true;
    public bool ClickAble { get => _clickAble; set => _clickAble = value; }

    // Menu
    private bool menuOpen = false;



    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        uiController = FindObjectOfType<UiController>();
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
                    HorseClicked(hit.collider.gameObject);
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
    private void HorseClicked(GameObject gameObject)
    {
        
    }

    private void OpenMenu()
    {
        menuOpen = !menuOpen;
        uiController.menu.SetActive(menuOpen);
    }

}
