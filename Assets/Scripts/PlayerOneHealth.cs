using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOneHealth : MonoBehaviour
{

    public static int minimunPlayerHealth = 0;
    public static int maximunPlayerHealth = 100;
    public static int currentPlayerHealth;

    public bool isPlayerDefeated;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHealth = maximunPlayerHealth;

        isPlayerDefeated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDefeated == true) return;

        if (currentPlayerHealth < minimunPlayerHealth) currentPlayerHealth = minimunPlayerHealth;

        if (currentPlayerHealth == minimunPlayerHealth)
        {
            isPlayerDefeated = true;

            SendMessage("SetPlayerDefeated", SendMessageOptions.DontRequireReceiver);
        }
    }
}
