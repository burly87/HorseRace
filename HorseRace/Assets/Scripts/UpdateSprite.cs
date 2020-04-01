using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateSprite : MonoBehaviour
{
    public Sprite cardFace;
    public Sprite cardBack;
    private SpriteRenderer spriteRenderer;
    private GameLogic gameLogic;

    public bool faceUp = false;

    // Start is called before the first frame update
    void Start()
    {
        List<string> deck = GameLogic.GenerateDeck();
        gameLogic = FindObjectOfType<GameLogic>();


        int i = 0;
        foreach (string card in deck)
        {
            if (this.name == card)
            {
                cardFace = gameLogic.cardFaces[i];
                break;
            }
            i++;
        }
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (faceUp)
        {
            spriteRenderer.sprite = cardFace;
        }
        else
        {
            spriteRenderer.sprite = cardBack;
        }
    }
}
