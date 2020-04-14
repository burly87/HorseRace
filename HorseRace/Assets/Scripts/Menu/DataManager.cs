using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Suits {CLUB, DIAMAND, HEART, SPADES}

public struct Player{
    public string name;
    public Suits suit;
    public Color color;
    public int sips {get=> sips; set => sips = value;}
    public int result;
    }

public class DataManager : MonoBehaviour
{
    private static DataManager _instance;

    public static DataManager Instance
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

    // -------

    public List<Player> players;

    void Start()
    {
        players = new List<Player>();
    }

    public void AddPlayer(string name, Color color)
    {
        Player player = new Player();
        // set all values 
        player.name = name;
        player.color = color;

        // add player to list
        players.Add(player);

        // foreach (Player p in players)
        // {
        //     Debug.Log(p.name);
        //     Debug.Log(p.suit);
        //     Debug.Log(p.color);
        //     Debug.Log(p.sips);
        //     Debug.Log(p.result);
        // }

    }

    public void UpdatePlayerStats(int playerNb, Suits suitsUpdate,int sipsUpdate)
    {
        Player p = players[playerNb];
        p.sips = sipsUpdate;
        p.suit = suitsUpdate;
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            foreach (Player p in players)
            {
                Debug.Log("Player : " + p.name  + "\n" + p.suit+ "\n" + p.sips  );
            }
            // Debug.Log("Player 0: " + players[0].name  + "\n" + players[0].color+ "\n" + players[0].suit  );
            // Debug.Log("Player 1: " + players[1].name  + "\n" + players[1].color+ "\n" + players[1].suit  );
            // Debug.Log("Player 2: " + players[2].name  + "\n" + players[2].color+ "\n" + players[2].suit  );


        }
    }
    
}
