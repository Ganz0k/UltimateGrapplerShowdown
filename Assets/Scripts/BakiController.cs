using UnityEngine;
using System;
using System.Linq;

public class BakiController : MonoBehaviour {
    
    private Animator animator;
    private Transform personaje;
    private const int InputBufferSize = 10;
    private KeyCode[] inputBuffer = new KeyCode[InputBufferSize];
    private int inputIndex = 0;

    public float inputTimeout = 0.5f;
    public KeyCode[] punchKeys = {
        KeyCode.U,
        KeyCode.I,
        KeyCode.O
    };
    public KeyCode[] kickKeys = {
        KeyCode.J,
        KeyCode.K,
        KeyCode.L
    };
    public keyCode[] quarterCircleBackLeft = { //Same as quarterCircleForwardRight
        KeyCode.S,
        KeyCode.S,
        KeyCode.A,
        KeyCode.A
    };
    public KeyCode[] quarterCircleBackRight = { //Same as quarterCircleForwardLeft
        KeyCode.S,
        KeyCode.S,
        KeyCode.D,
        KeyCode.D
    };
    public KeyCode[] downDown = {
        KeyCode.S,
        KeyCode.S
    };
    public KeyCode[] doubleQuarterCircleForwardLeft = {
        KeyCode.S,
        KeyCode.S,
        KeyCode.D,
        KeyCode.D,
        KeyCode.S,
        KeyCode.S,
        KeyCode.D,
        KeyCode.D
    };
    public KeyCode[] doubleQuarterCircleForwardRight = {
        KeyCode.S,
        KeyCode.S,
        KeyCode.A,
        KeyCode.A,
        KeyCode.S,
        KeyCode.S,
        KeyCode.A,
        KeyCode.A
    };

    private float lastInputTime;
    
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        bool isMovingForwards = Input.GetKey(KeyCode.D);
        bool isMovingBackwards = Input.GetKey(KeyCode.A);
        bool isJumping = Input.GetKey(KeyCode.W);
        bool lightPunch = Input.GetKeyDown(KeyCode.U);
        bool mediumPunch = Input.GetKeyDown(KeyCode.I);
        bool heavyPunch = Input.GetKeyDown(KeyCode.O);
        bool lightKick = Input.GetKeyDown(KeyCode.J);
        bool mediumKick = Input.GetKeyDown(KeyCode.K);
        bool heavyKick = Input.GetKeyDown(KeyCode.L);

        animator.SetBool("Forward pressed", isMovingForwards);
        animator.SetBool("Back pressed", isMovingBackwards);
        animator.SetBool("Up pressed", isJumping);

        

        if (Time.time - lastInputTime > inputTimeout) {
            inputIndex = 0;
        }

        if (inputIndex < InputBufferSize) {
            if (Input.GetKeyDown(quarterCircleBackLeft[inputIndex])) {
                inputBuffer[inputIndex] = quarterCircleBackLeft[inputIndex];
                inputIndex ++;
                lastInputTime = Time.time;
            }
            
            if (Input.GetKeyDown(quarterCircleBackRight[inputIndex])) {
                inputBuffer[inputIndex] = quarterCircleBackLeft[inputIndex];
                inputIndex ++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(downDown[inputIndex])) {
                inputBuffer[inputIndex] = downDown[inputIndex];
                inputIndex ++;
            }

            if (Input.GetKeyDown(doubleQuarterCircleForwardLeft[inputIndex])) {
                inputBuffer[inputIndex] = doubleQuarterCircleForwardLeft[inputIndex];
                inputIndex ++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(doubleQuarterCircleForwardRight[inputIndex])) {
                inputBuffer[inputIndex] = doubleQuarterCircleForwardRight[inputIndex];
                inputIndex ++;
                lastInputTime = Time.time;
            }
        }

        if (ContainsQuarterCircleBackLeft()) {
            if (Array.Exists(kickKeys, key => Input.GetKeyDown(key))) {
                TriggerCockroachDash();
            }

            inputIndex = 0;
        }

        if (ContainsQuarterCircleBackLeft()) {
            if (Array.Exists(kickKeys, key => Input.GetKeyDown(key))) {
                TriggerCockroachDash();
            }

            inputIndex = 0;
        }

        if (ContainsQuarterCircleBackLeft()) {
            if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                TriggerWhipStrike();
            }

            inputIndex = 0;
        }

        if (ContainsQuarterCircleBackRight()) {
            if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                TriggerWhipStrike();
            }

            inputIndex = 0;
        }
        
        if (ContainsDownDown()) {
            if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                TriggerDemonBack();
            }

            inputIndex = 0;
        }

        if (ContainsDoubleQuarterCircleForwardLeft()) {
            if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                TriggerTriceratopsFist();
            }

            inputIndex = 0;
        }

        if (ContainsDoubleQuarterCircleForwardRight()) {
            if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                TriggerTriceratopsFist();
            }

            inputIndex = 0;
        }

        if (lightPunch) {
            animator.SetTrigger("LightPunchTrigger");
        }

        if (mediumPunch) {
            animator.SetTrigger("MediumPunchTrigger");
        }

        if (heavyPunch) {
            animator.SetTrigger("HeavyPunchTrigger");
        }

        if (lightKick) {
            animator.SetTrigger("LightKickTrigger");
        }

        if (mediumKick) {
            animator.SetTrigger("MediumKickTrigger");
        }

        if (heavyKick) {
            animator.SetTrigger("HeavyKickTrigger");
        }
    }

    void TriggerCockroachDash() {
        animator.SetTrigger("CockroachDashTrigger");
    }

    void TriggerWhipStrike() {
        animator.SetTrigger("WhipStrikeTrigger");
    }

    void TriggerDemonBack() {
        animator.SetTrigger("DemonBackTrigger");
    }

    void TriggerTriceratopsFist() {
        animator.SetTrigger("TriceratopsFistTrigger");
    }

    void TriggerTriceratopsFistFollowup() {
        animator.SetTrigger("TriceratopsFistFollowupTrigger");
    }

    bool ContainsQuarterCircleBackLeft() {
        for (int i = 0; i <= inputBuffer.Length - quarterCircleBackLeft.Length: i ++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(quarterCircleBackLeft.Length).ToArray();

            if (subsequence.SequenceEqual(quarterCircleBackLeft)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsQuarterCircleBackRight() {
        for (int i = 0; i <= inputBuffer.Length - quarterCircleBackRight.Length: i ++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(quarterCircleBackRight.Length).ToArray();

            if (subsequence.SequenceEqual(quarterCircleBackRight)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsDownDown() {
        for (int i = 0; i <= inputBuffer.Length - downDown.Length: i ++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(downDown.Length).ToArray();

            if (subsequence.SequenceEqual(downDown)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsDoubleQuarterCircleForwardLeft() {
        for (int i = 0; i <= inputBuffer.Length - doubleQuarterCircleForwardLeft.Length; i ++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(doubleQuarterCircleForwardLeft.Length).ToArray();

            if (subsequence.SequenceEqual(doubleQuarterCircleForwardLeft)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsDoubleQuarterCircleForwardRight() {
        for (int i = 0; i <= inputBuffer.Length - doubleQuarterCircleForwardRight.Length; i ++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(doubleQuarterCircleForwardRight.Length).ToArray();

            if (subsequence.SequenceEqual(doubleQuarterCircleForwardRight)) {
                return true;
            }
        }

        return false;
    }
}
