using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]

public class CharacterSelect : CharacterSelectManager {

    private GameObject characterDemo;
    public int characterSelectState;
    public AudioClip cycleCharacterButtonPress;
    public Text titulo;

    private enum CharacterSelectModels {
        YujiroHanma = 0,
        BakiHanma = 1
    }

    // Start is called before the first frame update
    void Start() {
        CharacterSelectManager();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            Destroy(characterDemo);
            titulo.text = "Selecciona tu oponente";
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<OpponentSelect>().enabled = true;
            this.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.A)) {
            if (characterSelectState == 0) {
                return;
            }

            GetComponent<AudioSource>().PlayOneShot(cycleCharacterButtonPress);
            characterSelectState--;
            CharacterSelectManager();
        }

        if (Input.GetKeyDown(KeyCode.D)) {
            if (characterSelectState == 1) {
                return;
            }

            GetComponent<AudioSource>().PlayOneShot(cycleCharacterButtonPress);
            characterSelectState++;
            CharacterSelectManager();
        }
    }

    private void CharacterSelectManager() {
        switch (characterSelectState) {
            case 0:
                YujiroHanma();
                break;
            case 1:
                BakiHanma();
                break;
            default:
                break;
        }
    }

    private void YujiroHanma() {
        Destroy(characterDemo);
        characterDemo = Instantiate(Resources.Load("YujiroCharacterSelect")) as GameObject;
        characterDemo.transform.position = new Vector3(-0.5f, 0, -7);
        
        _yujiroHanma = true;
        _bakiHanma = false;
    }

    private void BakiHanma() {
        Destroy(characterDemo);
        characterDemo = Instantiate(Resources.Load("BakiCharacterSelect")) as GameObject;
        characterDemo.transform.position = new Vector3(-0.5f, 0, -7);
        
        _bakiHanma = true;
        _yujiroHanma = false;
    }
}
