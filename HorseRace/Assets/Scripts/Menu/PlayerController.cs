using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking.NetworkSystem;

public class PlayerController : MonoBehaviour
{
    DataManager dataManager;

    // choose by player and give to datamanager.players List
    public GameObject inputField;           // to get the PlayerName
    public Button btn_Add;                  // addbutton
    private int playerCount = 0;            // max of 8 

    //Playerlist
    public TextMeshProUGUI playerList;      // text field to change if player is added
    public Image colorImg;
    private List<Color> colors;
    private int colorNumber = 0;            // to walk through list of colors
    private int maxColor = 7;

    [Header("Player Colors")]
    public Image[] playerColorsPicked;


    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        playerList.text = "";

        colors = new List<Color>();
        AddColors();
        //set start color of colorpicker
        colorImg.color = colors[0];

        // disable all playerColorsPicked
        foreach (Image colorImg in playerColorsPicked)
        {
            colorImg.enabled = false;
        }
    }

    private void Update()
    {
        // handle interactable from AddBtns
        if (inputField.GetComponent<Text>().text == "")  btn_Add.interactable = false;
        else btn_Add.interactable = true;
        if (playerCount >= 8) btn_Add.interactable = false;
    }

    public void AddPlayer()
    {

        string tmpName = inputField.GetComponent<Text>().text;

        //add to datamanager.players to save data
      //  dataManager.AddPlayer(tmpName, colors[colorNumber]);

        //update Text from Playerlist
        playerList.text += "" + tmpName + "\n";

        //actiavet Colorpicked Image
        playerColorsPicked[playerCount].enabled = true;
        //set color of Coloricked Image
        playerColorsPicked[playerCount].color = colors[colorNumber];

        playerCount++;

        //remove color from colorList
        colors.RemoveAt(colorNumber);

        //update sprite
        maxColor--;
        if(colors.Count > 0) colorImg.color = colors[0];

    }

    public void SwitchColor(int dir)
    {
        if(dir == 0 && colorNumber < maxColor)
        {
            colorNumber++;
            colorImg.color = colors[colorNumber];
        }
        if (dir == 1 && colorNumber > 0)
        {
            colorNumber--;
            colorImg.color = colors[colorNumber];
        }
    }

    void AddColors()
    {
        colors.Clear();
        colors.Add(Color.red);
        colors.Add(Color.green);
        colors.Add(Color.blue);
        colors.Add(Color.cyan);
        colors.Add(Color.magenta);
        colors.Add(Color.yellow);
        colors.Add(new Color(255f / 255f, 157f / 255f, 0f));
        colors.Add(new Color(218f / 255f, 0f, 255f / 255f));
    }
}
