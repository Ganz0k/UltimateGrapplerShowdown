using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{

    private int fightTimer = 99;  //Define how many seconds in a round
    public static int currentFightTimer;  //Defines the current timer value

    // Start is called before the first frame update
    void Start()
    {
        currentFightTimer = fightTimer;  //Set current timer to equial fiht timer on start up
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
