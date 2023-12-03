using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightUI : MonoBehaviour
{

    private int returnedCurrentTimerValue;  //Stores returned current timer value

    public float returnMaximunPlayerHealth;
    public float returnCurrentPlayerHealth;
    public float playerHealthBarLength;
    public float returnMaximunOpponentHealth;
    public float returnCurrentOpponentHealth;
    public float opponentHealthBarLength;

    private Vector2 healthBarSize;
    private Vector2 fightGUITimerSize; //Defines naming convention for the GUI timer size

    public Texture2D healthBarMinTexture;
    public Texture2D healthBarMaxTexture;

    public float fightGUIHeightPos;  //Defines fight GUI height position
    private float fightGUIOffSet;  //Defines naming convention for the GUI offset
    private GUIStyle fightGUISkin;  //Defines naming convention for the GUI style/skin

    // Start is called before the first frame update
    void Start()
    {
        fightGUIOffSet = Screen.width / 20;

        fightGUITimerSize = new Vector2( Screen.width / 7.5f, Screen.height / 7.5f);
        healthBarSize = new Vector2( Screen.width / 2.25f, Screen.height / 15f);
    }

    // Update is called once per frame
    void Update()
    {
        returnCurrentPlayerHealth = PlayerOneHealth.currentPlayerHealth;
        returnCurrentOpponentHealth = OpponentHealth.currentOpponentHealth;

        returnMaximunPlayerHealth = PlayerOneHealth.maximunPlayerHealth;
        returnMaximunOpponentHealth = OpponentHealth.maximumOpponentHealth;

        playerHealthBarLength = (returnCurrentPlayerHealth / returnMaximunPlayerHealth);
        opponentHealthBarLength = (returnCurrentOpponentHealth / returnMaximunOpponentHealth);
    }

    private void LateUpdate()
    {
        returnedCurrentTimerValue = FightManager.currentFightTimer;
    }

    private void OnGUI()
    {
        fightGUISkin = new GUIStyle(GUI.skin.GetStyle("label"));
        fightGUISkin.fontSize = Screen.width / 12;
        fightGUISkin.alignment = TextAnchor.MiddleCenter;
        fightGUIHeightPos = fightGUIOffSet / 50;

        if(returnedCurrentTimerValue >= 10)
        {
            GUI.Label(new Rect(
                Screen.width / 2 - (fightGUITimerSize.x / 2),
                fightGUIHeightPos,
                fightGUITimerSize.x, fightGUITimerSize.y), 
                returnedCurrentTimerValue.ToString(), fightGUISkin);
        }

        if(returnedCurrentTimerValue < 10)
        {
            GUI.Label(new Rect(
                Screen.width / 2 - (fightGUITimerSize.x / 2), 
                fightGUIHeightPos,
                fightGUITimerSize.x, fightGUITimerSize.y),
                "0" + returnedCurrentTimerValue.ToString(), fightGUISkin);
        }
        
        //Draw play health
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        GUI.DrawTexture(new Rect(
            fightGUIOffSet, fightGUIOffSet / 2, 
            healthBarSize.x, healthBarSize.y),
            healthBarMinTexture);

        GUI.DrawTexture(new Rect(
            fightGUIOffSet, fightGUIOffSet / 2, 
            healthBarSize.x * playerHealthBarLength, healthBarSize.y), 
            healthBarMaxTexture);

        GUI.EndGroup();

        //Draw opponent health
        GUI.BeginGroup(new Rect(0, 0, Screen.width, Screen.height));

        GUI.DrawTexture(new Rect(
            fightGUIOffSet, fightGUIOffSet / 2, 
            healthBarSize.x, healthBarSize.y),
            healthBarMinTexture);

        GUI.DrawTexture(new Rect(
            fightGUIOffSet, fightGUIOffSet / 2, 
            healthBarSize.x * opponentHealthBarLength, healthBarSize.y), 
            healthBarMaxTexture);
            
        GUI.EndGroup();
    }
}
