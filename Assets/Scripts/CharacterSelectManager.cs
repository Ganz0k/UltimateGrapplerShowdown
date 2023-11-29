using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectManager : MonoBehaviour {

    public static bool _yujiroHanma;
    public static bool _bakiHanma;
    public static bool _yujiroOpponent;
    public static bool _bakiOpponent;
    public static string toWhichScene;

    void Awake() {
        _yujiroHanma = false;
        _bakiHanma = false;
        _bakiOpponent = false;
        _yujiroOpponent = false;
    }

    // Start is called before the first frame update
    void Start() {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update() {
        
    }
}
