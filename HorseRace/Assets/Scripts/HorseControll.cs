using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseControll : MonoBehaviour
{
    // move exactly 1 step 
    float step = 3.5f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("test"))
        {
            Move();
        }
    }

    public void Move()
    {
        this.transform.position += new Vector3(step, 0,0);
    }
}
