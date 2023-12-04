using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpponentHealth : MonoBehaviour {
    
    public static float Salud = 100;
    public static float SaludMaxima = 100;
    public static float superMeter = 0;
    public static float superMaximo = 100;
    public static int roundsWon = 0;

    [Header("Interfaz")]
    public Image BarraSalud;
    public Image BarraSuper;

    void Awake() {
        Salud = 100;
        SaludMaxima = 100;
        superMeter = 0;
        superMaximo = 100;
        roundsWon = 0;
        GameObject.FindGameObjectWithTag("R1P1").GetComponent<Text>().text = "";
        GameObject.FindGameObjectWithTag("R2P1").GetComponent<Text>().text = "";
    }

    // Start is called before the first frame update
    void Start() {
        BarraSalud = GameObject.FindGameObjectWithTag("OHealth").GetComponent<Image>();
        BarraSuper = GameObject.FindGameObjectWithTag("OSuper").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update() {
        ActualizarBarras();
    }

    void ActualizarBarras() {
        BarraSalud.fillAmount = Salud / SaludMaxima;
        BarraSuper.fillAmount = superMeter / superMaximo;
    }

    public static void RecibirAtaque(float ataque) {
        Salud -= ataque;
        
        if (Salud - ataque <= 0) {
            Salud = 0;
        }

        Debug.Log(Salud);

        if (Salud == 0) {
            if (CharacterSelectManager._yujiroOpponent) {
                if (CharacterSelectManager.players == 2) {
                    YujiroPlayer2.FullKOTrigger();
                } else {
                    OpponentCharacter.FullKOTrigger();
                }
                
                if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroHanma) {
                    YujiroController.WinTrigger();
                } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiHanma) {
                    BakiController.WinTrigger();
                }
            } else if (CharacterSelectManager._bakiOpponent) {
                if (CharacterSelectManager.players == 2) {
                    BakiPlayer2.FullKOTrigger();
                } else {
                    OpponentCharacter.FullKOTrigger();
                }

                if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroHanma) {
                    YujiroController.WinTrigger();
                } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiHanma) {
                    BakiController.WinTrigger();
                }
            }

            if (PlayerOneHealth.roundsWon != 1) {
                PlayerOneHealth.roundsWon++;
                GameObject.FindGameObjectWithTag("R1P1").GetComponent<Text>().text = "V";
                Timer.playAudio = 0;
                Timer.pauseRemaining = 5;
                Timer.timeRemaining = 99;
                Timer.paused = true;
                MenuPausa.isPaused = true;
            } else if (PlayerOneHealth.roundsWon == 1) {
                PlayerOneHealth.roundsWon++;
                GameObject.FindGameObjectWithTag("R2P1").GetComponent<Text>().text = "V";
                MenuPausa.isGameOver = true;
            }
        }
    }

    public static void BuildMeter() {
        if (superMeter + 10f >= 100) {
            superMeter = 100;
        }

        superMeter += 10f;
    }
}
