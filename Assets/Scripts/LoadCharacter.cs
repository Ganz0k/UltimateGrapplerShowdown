using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour {

    private GameObject playerCharacter;
    private GameObject opponentCharacter;
    // Start is called before the first frame update
    void Start() {
        if (CharacterSelectManager._yujiroHanma) {
            playerCharacter = Instantiate(Resources.Load("Yujiro")) as GameObject;
        }
        
        if (CharacterSelectManager._bakiHanma) {
            playerCharacter = Instantiate(Resources.Load("Baki")) as GameObject;
        }

        if (CharacterSelectManager.players == 2) {
            if (CharacterSelectManager._yujiroOpponent) {
                opponentCharacter = Instantiate(Resources.Load("YujiroPlayer2")) as GameObject;
            }

            if (CharacterSelectManager._bakiOpponent) {
                opponentCharacter = Instantiate(Resources.Load("BakiPlayer2")) as GameObject;
            }
        } else {
            if (CharacterSelectManager.toWhichScene == "TrainingStage") {
                if (CharacterSelectManager._yujiroOpponent) {
                    opponentCharacter = Instantiate(Resources.Load("YujiroDummy")) as GameObject;
                }

                if (CharacterSelectManager._bakiOpponent) {
                    opponentCharacter = Instantiate(Resources.Load("BakiDummy")) as GameObject;
                }
            } else {
                if (CharacterSelectManager._yujiroOpponent) {
                    opponentCharacter = Instantiate(Resources.Load("YujiroAI")) as GameObject;
                }

                if (CharacterSelectManager._bakiOpponent) {
                    opponentCharacter = Instantiate(Resources.Load("BakiAI")) as GameObject;
                }
            }
        }

        camaraPelea.target1 = playerCharacter.transform;
        camaraPelea.target2 = opponentCharacter.transform;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
