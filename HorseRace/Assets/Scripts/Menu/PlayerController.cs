using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    DataManager dataManager;

    // choose by player and give to datamanager.players List
    public GameObject inputField;           // to get the PlayerName
    public Color colorPicker;

    //Playerlist
    public TextMeshProUGUI playerList;      // text field to change if player is added
    
    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
        playerList.text = "";
    }

    public void AddPlayer()
    {
        string tmpName = inputField.GetComponent<Text>().text;
        Color tmpColor = colorPicker;

        //add to datamanager.players to save data
        dataManager.AddPlayer(tmpName, tmpColor);

        //update Text from Playerlist
        playerList.text += "" + tmpName + "\t" + tmpColor +"\n";

    }


}
