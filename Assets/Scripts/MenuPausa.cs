using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

    public GameObject menuPausa;
    public static bool isPaused;
    
    // Start is called before the first frame update
    void Start() {
        menuPausa.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused) {
                ReanudarJuego();
            } else {
                PausarJuego();
            }
        }
    }

    public void PausarJuego() {
        menuPausa.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ReanudarJuego() {
        menuPausa.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void IrAMenuPrincipal() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }
}
