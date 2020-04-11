using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FinishLine : MonoBehaviour
{
    public static event Action<FinishLine> OnEnterFinishLine = delegate { };

    private string _horseName = "";
    public string HorseName => _horseName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Set horsename to the wining HorseCollider
        if (other.tag == "Horse")
        {
            _horseName = other.name;
        }

        // Trigger itself
        OnEnterFinishLine(this);
    }
}
