using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public Text timerText;
    public Text gameOverText;
    public static float timeRemaining = 99;
    private int seconds;
    public static bool paused = true;
    public static float pauseRemaining = 3;
    public static int playAudio = 0;
    public AudioSource audioSource {
        get {
            return GetComponent<AudioSource>();
        }
    }
    public AudioClip readyFight;

    void Awake() {
        timeRemaining = 99;
        paused = true;
        pauseRemaining = 3;
        playAudio = 0;
    }

    // Start is called before the first frame update
    void Start() {
        gameObject.AddComponent<AudioSource>();
        MenuPausa.isPaused = true;
    }

    // Update is called once per frame
    void Update() {
        if (!paused) {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining < 0) {
                timeRemaining = 0;
            }

            seconds = (int) timeRemaining;

            timerText.text = string.Format("{0:00}", seconds);

            if (timeRemaining == 0) {
                playAudio = 0;
                pauseRemaining = 5;
                timeRemaining = 99;
                paused = true;
                MenuPausa.isPaused = true;
                
                if (PlayerOneHealth.Salud > OpponentHealth.Salud) {
                    if (PlayerOneHealth.roundsWon != 1) {
                        PlayerOneHealth.roundsWon++;
                        GameObject.FindGameObjectWithTag("R1P1").GetComponent<Text>().text = "V";
                    } else if (PlayerOneHealth.roundsWon == 1) {
                        PlayerOneHealth.roundsWon++;
                        GameObject.FindGameObjectWithTag("R2P1").GetComponent<Text>().text = "V";
                        MenuPausa.isGameOver = true;
                    }
                    
                    if (CharacterSelectManager._yujiroOpponent) {
                        YujiroPlayer2.FullKOTrigger();

                        if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroHanma) {
                            YujiroController.WinTrigger();
                        } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiHanma) {
                            BakiController.WinTrigger();
                        }
                    } else if (CharacterSelectManager._bakiOpponent) {
                        BakiPlayer2.FullKOTrigger();

                        if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroHanma) {
                            YujiroController.WinTrigger();
                        } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiHanma) {
                            BakiController.WinTrigger();
                        }
                    }
                } else {
                    if (OpponentHealth.roundsWon != 1) {
                        OpponentHealth.roundsWon++;
                        GameObject.FindGameObjectWithTag("R1O").GetComponent<Text>().text = "V";
                    } else if (OpponentHealth.roundsWon == 1) {
                        OpponentHealth.roundsWon++;
                        GameObject.FindGameObjectWithTag("R2O").GetComponent<Text>().text = "V";
                        MenuPausa.isGameOver = true;
                    }

                    if (CharacterSelectManager._yujiroHanma) {
                        YujiroController.FullKOTrigger();

                        if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroOpponent) {
                            YujiroPlayer2.WinTrigger();
                        } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiOpponent) {
                            BakiPlayer2.WinTrigger();
                        }
                    } else if (CharacterSelectManager._bakiHanma) {
                        BakiController.FullKOTrigger();

                        if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroOpponent) {
                            YujiroController.WinTrigger();
                        } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiOpponent) {
                            BakiController.WinTrigger();
                        }
                    }
                }
            }
        }

        if (paused && (PlayerOneHealth.roundsWon != 2 || OpponentHealth.roundsWon != 2)) {
            pauseRemaining -= Time.deltaTime;

            if (pauseRemaining < 0) {
                pauseRemaining = 0;
            }

            if (playAudio == 0) {
                audioSource.PlayOneShot(readyFight);
                playAudio++;
            }

            if (pauseRemaining == 0) {
                paused = false;
                MenuPausa.isPaused = false;
                PlayerOneHealth.Salud = 100;
                OpponentHealth.Salud = 100;

                if (CharacterSelectManager._yujiroHanma) {
                    YujiroController.IdleTrigger();

                    if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroOpponent) {
                        YujiroPlayer2.IdleTrigger();
                    } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiOpponent) {
                        BakiPlayer2.IdleTrigger();
                    }
                } else if (CharacterSelectManager._bakiHanma) {
                    BakiController.IdleTrigger();

                    if (CharacterSelectManager.players == 2 && CharacterSelectManager._yujiroOpponent) {
                        YujiroController.IdleTrigger();
                    } else if (CharacterSelectManager.players == 2 && CharacterSelectManager._bakiOpponent) {
                        BakiController.IdleTrigger();
                    }
                }
            }
        }
    }
}
