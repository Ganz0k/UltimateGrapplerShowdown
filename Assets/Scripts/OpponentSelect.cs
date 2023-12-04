using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpponentSelect : CharacterSelectManager {
    
    private GameObject characterDemo;
    public int characterSelectState;
    public AudioClip cycleCharacterButtonPress;

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
            SceneManager.LoadScene(toWhichScene);
        }


        if (players == 1) {
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
        } else {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                if (characterSelectState == 0) {
                    return;
                }

                GetComponent<AudioSource>().PlayOneShot(cycleCharacterButtonPress);
                characterSelectState--;
                CharacterSelectManager();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow)) {
                if (characterSelectState == 1) {
                    return;
                }

                GetComponent<AudioSource>().PlayOneShot(cycleCharacterButtonPress);
                characterSelectState++;
                CharacterSelectManager();
            }
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
        
        _yujiroOpponent = true;
        _bakiOpponent = false;
    }

    private void BakiHanma() {
        Destroy(characterDemo);
        characterDemo = Instantiate(Resources.Load("BakiCharacterSelect")) as GameObject;
        characterDemo.transform.position = new Vector3(-0.5f, 0, -7);
        
        _bakiOpponent = true;
        _yujiroOpponent = false;
    }
}
