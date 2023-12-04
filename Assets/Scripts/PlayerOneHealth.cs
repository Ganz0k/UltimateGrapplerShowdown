using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneHealth : MonoBehaviour {
    
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
        GameObject.FindGameObjectWithTag("R1O").GetComponent<Text>().text = "";
        GameObject.FindGameObjectWithTag("R2O").GetComponent<Text>().text = "";
    }
    
    // Start is called before the first frame update
    void Start() {
        BarraSalud = GameObject.FindGameObjectWithTag("P1Health").GetComponent<Image>();
        BarraSuper = GameObject.FindGameObjectWithTag("P1Super").GetComponent<Image>();
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

        if (Salud == 0) {
            if (CharacterSelectManager._yujiroHanma) {
                YujiroController.FullKOTrigger();

                if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroOpponent) {
                    YujiroPlayer2.WinTrigger();
                } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiOpponent) {
                    BakiPlayer2.WinTrigger();
                } else if (CharacterSelectManager.players == 1) {
                    OpponentCharacter.WinTrigger();
                }
            } else if (CharacterSelectManager._bakiHanma) {
                BakiController.FullKOTrigger();

                if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroOpponent) {
                    YujiroController.WinTrigger();
                } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiOpponent) {
                    BakiController.WinTrigger();
                } else if (CharacterSelectManager.players == 1) {
                    OpponentCharacter.WinTrigger();
                }
            }

            if (OpponentHealth.roundsWon != 1) {
                OpponentHealth.roundsWon++;
                GameObject.FindGameObjectWithTag("R1O").GetComponent<Text>().text = "V";
                Timer.playAudio = 0;
                Timer.pauseRemaining = 5;
                Timer.timeRemaining = 99;
                Timer.paused = true;
                MenuPausa.isPaused = true;
            } else if (OpponentHealth.roundsWon == 1) {
                OpponentHealth.roundsWon++;
                GameObject.FindGameObjectWithTag("R2O").GetComponent<Text>().text = "V";
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
