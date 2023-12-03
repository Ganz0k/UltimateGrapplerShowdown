using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuPausa : MonoBehaviour {

    public GameObject menuPausa;
    public GameObject menuAyuda;
    public GameObject fightUI;
    public GameObject gameOverUI;
    public Text gameOverText;
    public static bool isPaused;
    public static bool isGameOver;
    public AudioSource audioSource {
        get {
            return GetComponent<AudioSource>();
        }
    }
    public AudioClip win;
    public AudioClip gameOver;
    private int playAudio = 0;

    void Awake() {

    }
    
    // Start is called before the first frame update
    void Start() {
        gameObject.AddComponent<AudioSource>();
        menuPausa.SetActive(false);
        menuAyuda.SetActive(false);
        gameOverUI.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (!isGameOver) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                if (isPaused) {
                    ReanudarJuego();
                } else {
                    PausarJuego();
                }
            }
        } else {
            GameOver();
        }
    }

    public void PausarJuego() {
        menuPausa.SetActive(true);
        fightUI.SetActive(false);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void GameOver() {
        Time.timeScale = 0f;
        gameOverUI.SetActive(true);

        if (PlayerOneHealth.roundsWon == 2) {
            gameOverText.text = "P1 GANA";

            if (playAudio == 0) {
                audioSource.PlayOneShot(win);
                playAudio++;
            }
        } else {
            if (CharacterSelectManager.players == 2) {
                gameOverText.text = "P2 GANA";
                
                if (playAudio == 0) {
                    audioSource.PlayOneShot(win);
                    playAudio++;
                }
            } else {
                gameOverText.text = "PERDISTE";

                if (playAudio == 0) {
                    audioSource.PlayOneShot(gameOver);
                    playAudio++;
                }
            }
        }
    }

    public void ReanudarJuego() {
        menuPausa.SetActive(false);
        menuAyuda.SetActive(false);
        fightUI.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void IrAMenuPrincipal() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
        isGameOver = false;
    }

    public void IrAMenuAyuda() {
        menuPausa.SetActive(false);
        menuAyuda.SetActive(true);
    }

    public void IrACharacterSelect() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("CharacterSelect");
        isGameOver = false;
    }

    public void RegresarAPausa() {
        menuPausa.SetActive(true);
        menuAyuda.SetActive(false);
    }
}
