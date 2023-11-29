using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCharacter : MonoBehaviour {

    private GameObject playerCharacter;
    private GameObject opponentCharacter;
    // Start is called before the first frame update
    void Start() {
        if (CharacterSelectManager._yujiroHanma == true) {
            playerCharacter = Instantiate(Resources.Load("Yujiro")) as GameObject;
        }
        
        if (CharacterSelectManager._bakiHanma) {
            playerCharacter = Instantiate(Resources.Load("Baki")) as GameObject;
        }

        if (CharacterSelectManager._yujiroOpponent) {
            opponentCharacter = Instantiate(Resources.Load("YujiroOpponent")) as GameObject;
        }

        if (CharacterSelectManager._bakiOpponent) {
            opponentCharacter = Instantiate(Resources.Load("BakiOpponent")) as GameObject;
        }

        camaraPelea.target1 = playerCharacter.transform;
        camaraPelea.target2 = opponentCharacter.transform;
    }

    // Update is called once per frame
    void Update() {
        
    }
}
