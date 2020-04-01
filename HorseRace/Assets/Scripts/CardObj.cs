using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 51)]
public class CardObj : ScriptableObject
{
    [SerializeField]    private CardColor color;
    [SerializeField]    private string name;
    private int count = 0;

    public CardColor Color => color;
    public string Name => name;
    public int Count { get ; set; }
}