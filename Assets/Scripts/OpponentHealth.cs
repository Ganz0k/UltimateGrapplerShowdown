using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentHealth : MonoBehaviour
{

    public static int minimunOpponentHealth = 0;
    public static int maximumOpponentHealth = 100;
    public static int currentOpponentHealth;

    public bool isOpponentDefeated;

    // Start is called before the first frame update
    void Start()
    {
        currentOpponentHealth = maximumOpponentHealth;

        isOpponentDefeated = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if (isOpponentDefeated == true) return;

        if (currentOpponentHealth < minimunOpponentHealth) currentOpponentHealth = minimunOpponentHealth;

        if (currentOpponentHealth == minimunOpponentHealth)
        {
            isOpponentDefeated = true;

            SendMessage("SetOpponentDefeated", SendMessageOptions.DontRequireReceiver);
        }
    }
}
