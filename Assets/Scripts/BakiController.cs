using UnityEngine;
using System;
using System.Linq;

public class BakiController : MonoBehaviour {
    
    private Animator animator;
    private const int InputBufferSize = 10;
    private KeyCode[] inputBuffer = new KeyCode[InputBufferSize];
    private int inputIndex = 0;
    private bool isFacingLeft = true;
    private float inputTimeout = 0.5f;
    private KeyCode[] lightCockroachDashF = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.J
    };
    private KeyCode[] mediumCockroachDashF = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.K
    };
    private KeyCode[] heavyCockroachDashF = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.L
    };
    private KeyCode[] lightCockroachDashB = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.J
    };
    private KeyCode[] mediumCockroachDashB = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.K
    };
    private KeyCode[] heavyCockroachDashB = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.L
    };
    private KeyCode[] lightWhipStrikeF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.U
    };
    private KeyCode[] mediumWhipStrikeF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.I
    };
    private KeyCode[] heavyWhipStrikeF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.O
    };
    private KeyCode[] lightWhipStrikeB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.U
    };
    private KeyCode[] mediumWhipStrikeB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.I
    };
    private KeyCode[] heavyWhipStrikeB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.O
    };
    private KeyCode[] lightDemonBack = {
        KeyCode.S,
        KeyCode.S,
        KeyCode.U
    };
    private KeyCode[] mediumDemonBack = {
        KeyCode.S,
        KeyCode.S,
        KeyCode.I
    };
    private KeyCode[] heavyDemonBack = {
        KeyCode.S,
        KeyCode.S,
        KeyCode.O
    };
    private KeyCode[] lightTriceratopsF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.U
    };
    private KeyCode[] mediumTriceratopsF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.I
    };
    private KeyCode[] heavyTriceratopsF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.O
    };
    private KeyCode[] lightTriceratopsB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.U
    };
    private KeyCode[] mediumTriceratopsB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.I
    };
    private KeyCode[] heavyTriceratopsB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.O
    };
    private float lastInputTime;
    public Transform opponent;
    public Transform personaje;
    
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        bool isMovingForwards;
        bool isMovingBackwards;
        bool isJumping = Input.GetKey(KeyCode.W);
        bool down = Input.GetKeyDown(KeyCode.S);
        bool lightPunch = Input.GetKeyDown(KeyCode.U);
        bool mediumPunch = Input.GetKeyDown(KeyCode.I);
        bool heavyPunch = Input.GetKeyDown(KeyCode.O);
        bool lightKick = Input.GetKeyDown(KeyCode.J);
        bool mediumKick = Input.GetKeyDown(KeyCode.K);
        bool heavyKick = Input.GetKeyDown(KeyCode.L);
        bool previousFacing = isFacingLeft;

        if (inputIndex == InputBufferSize) {
            inputIndex = 0;
        }

        if (isFacingLeft) {
            isMovingForwards = Input.GetKey(KeyCode.A);
            isMovingBackwards = Input.GetKey(KeyCode.D);
        } else {
            isMovingForwards = Input.GetKey(KeyCode.D);
            isMovingBackwards = Input.GetKey(KeyCode.A);
        }

        animator.SetBool("Forward pressed", isMovingForwards);
        animator.SetBool("Back pressed", isMovingBackwards);
        animator.SetBool("Up pressed", isJumping);

        if (Time.time - lastInputTime > inputTimeout) {
            inputIndex = 0;
        }

        if (inputIndex < InputBufferSize) {
            if (isJumping) {
                inputBuffer[inputIndex] = KeyCode.W;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (isMovingBackwards && isFacingLeft) {
                inputBuffer[inputIndex] = KeyCode.D;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (isMovingForwards && isFacingLeft) {
                inputBuffer[inputIndex] = KeyCode.A;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (isMovingBackwards && !isFacingLeft) {
                inputBuffer[inputIndex] = KeyCode.A;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (isMovingForwards && !isFacingLeft) {
                inputBuffer[inputIndex] = KeyCode.D;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (down) {
                inputBuffer[inputIndex] = KeyCode.S;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (lightPunch) {
                inputBuffer[inputIndex] = KeyCode.U;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (mediumPunch) {
                inputBuffer[inputIndex] = KeyCode.I;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (heavyPunch) {
                inputBuffer[inputIndex] = KeyCode.O;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (lightKick) {
                inputBuffer[inputIndex] = KeyCode.J;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (mediumKick) {
                inputBuffer[inputIndex] = KeyCode.K;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (heavyKick) {
                inputBuffer[inputIndex] = KeyCode.L;
                inputIndex++;
                lastInputTime = Time.time;
            }
        }

        if (isFacingLeft) {
            if (ContainsInput(lightCockroachDashB)) {
                TriggerCockroachDash();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumCockroachDashB)) {
                TriggerCockroachDash();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyCockroachDashB)) {
                TriggerCockroachDash();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightWhipStrikeB)) {
                TriggerWhipStrike();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumWhipStrikeB)) {
                TriggerWhipStrike();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyWhipStrikeB)) {
                TriggerWhipStrike();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightTriceratopsB)) {
                TriggerTriceratopsFist();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumTriceratopsB)) {
                TriggerTriceratopsFist();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyTriceratopsB)) {
                TriggerTriceratopsFist();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }
        } else {
            if (ContainsInput(lightCockroachDashF)) {
                TriggerCockroachDash();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumCockroachDashF)) {
                TriggerCockroachDash();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyCockroachDashF)) {
                TriggerCockroachDash();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightWhipStrikeF)) {
                TriggerWhipStrike();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumWhipStrikeF)) {
                TriggerWhipStrike();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyWhipStrikeF)) {
                TriggerWhipStrike();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightTriceratopsF)) {
                TriggerTriceratopsFist();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumTriceratopsF)) {
                TriggerTriceratopsFist();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyTriceratopsF)) {
                TriggerTriceratopsFist();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }
        }
        
        if (ContainsInput(lightDemonBack)) {
            TriggerDemonBack();
            inputBuffer = new KeyCode[InputBufferSize];
            inputIndex = 0;
        }

        if (ContainsInput(mediumDemonBack)) {
            TriggerDemonBack();
            inputBuffer = new KeyCode[InputBufferSize];
            inputIndex = 0;
        }

        if (ContainsInput(heavyDemonBack)) {
            TriggerDemonBack();
            inputBuffer = new KeyCode[InputBufferSize];
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

        isFacingLeft = IsFacingLeft();

        if (isFacingLeft != previousFacing) {
            FlipCharacter();
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

    bool ContainsInput(KeyCode[] input) {
        for (int i = 0; i <= inputBuffer.Length - input.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(input.Length).ToArray();

            if (subsequence.SequenceEqual(input)) {
                return true;
            }
        }

        return false;
    }

    public bool IsFacingLeft() {
        float relativeXPosition = personaje.position.x - opponent.position.x;
        return relativeXPosition <= 0;
    }

    private void FlipCharacter() {
        personaje.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter(Collider other) {
        
    }
}
