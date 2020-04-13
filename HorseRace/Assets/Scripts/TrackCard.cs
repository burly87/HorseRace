using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TrackCard : MonoBehaviour
{
    public static event Action<TrackCard> OnTriggerEnterTrackcard = delegate { };

    [SerializeField]
    private int _counter = 0;
    public int Counter 
    { 
        get => _counter;
        set => _counter = value;
    }

    private bool clubPassed = false;
    private bool diamandPassed = false;
    private bool heartPassed = false;
    private bool spadesPassed = false;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Horse")
        {
            if(!clubPassed && other.name[0] =='C')
            {
                clubPassed = true;
                _counter++;
            }
            if (!diamandPassed && other.name[0] == 'D')
            {
                diamandPassed = true;
                _counter++;
            }
            if (!heartPassed && other.name[0] == 'H')
            {
                heartPassed = true;
                _counter++;
            }
            if (!spadesPassed && other.name[0] == 'S')
            {
                spadesPassed = true;
                _counter++;
            }
        }
            
        OnTriggerEnterTrackcard(this);

    }
}
