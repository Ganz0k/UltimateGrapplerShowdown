using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour {

    public GameObject menuPausa;
    public GameObject menuAyuda;
    public static bool isPaused;
    
    // Start is called before the first frame update
    void Start() {
        menuPausa.SetActive(false);
        menuAyuda.SetActive(false);
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
        menuAyuda.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void IrAMenuPrincipal() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        isPaused = false;
    }

    public void IrAMenuAyuda() {
        menuPausa.SetActive(false);
        menuAyuda.SetActive(true);
    }

    public void RegresarAPausa() {
        menuPausa.SetActive(true);
        menuAyuda.SetActive(false);
    }
}
