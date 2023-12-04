using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    /**
     * Método utilizado para cargar el siguiente nivel o excena
     */
    public void CargarEscena(string NombreNivel) {
        SceneManager.LoadScene(NombreNivel); 
    }
    
    /**
     * Método utilizado para salir/terminar la ejecución 
     */
	public void Exit() {
        Application.Quit();
    }

    public void SinglePlayer() {
        SceneManager.LoadScene("CharacterSelect");
        CharacterSelectManager.toWhichScene = "UndergroundArena";
        CharacterSelectManager.players = 1;
    }

    public void Multiplayer() {
        SceneManager.LoadScene("CharacterSelect");
        CharacterSelectManager.toWhichScene = "UndergroundArena";
        CharacterSelectManager.players = 2;
    }

    public void Training() {
        SceneManager.LoadScene("CharacterSelect");
        CharacterSelectManager.toWhichScene = "TrainingStage";
        CharacterSelectManager.players = 1;
    }
}
