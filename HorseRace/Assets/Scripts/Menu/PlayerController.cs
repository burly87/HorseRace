using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    DataManager dataManager;

    // choose by player and give to datamanager.players List
    public GameObject inputField;           // to get the PlayerName
    public Color colorPicker;

    void Start()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    public void AddPlayer()
    {
        string tmpName = inputField.GetComponent<Text>().text;
        Color tmpColor = colorPicker;

        dataManager.AddPlayer(tmpName, tmpColor);
    }

}
