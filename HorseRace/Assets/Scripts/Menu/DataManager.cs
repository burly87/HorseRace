using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public struct Player{
    public string name;
    public Color color;
    public int sipsBet_C, sipsBet_D, sipsBet_H, sipsBet_S;
    public int sipstoGive;
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
        player.sipsBet_C = 0;
        player.sipsBet_D = 0;
        player.sipsBet_H = 0;
        player.sipsBet_S = 0;
        player.sipstoGive = 0;

        // add player to list
        players.Add(player);
    }

    public void UpdatePlayerStats(int index, int sipsUp_C, int sipsUp_D, int sipsUp_H, int sipsUp_S)
    {
        Player p = players[index];
        p.sipsBet_C = sipsUp_C;
        p.sipsBet_D = sipsUp_D;
        p.sipsBet_H = sipsUp_H;
        p.sipsBet_S = sipsUp_S;

        players[index] = p;
       // Debug.Log("Playernindex: " + index + "Suit: "+ players[index].suit + "Sips: " + players[index].sips);
    }

}
