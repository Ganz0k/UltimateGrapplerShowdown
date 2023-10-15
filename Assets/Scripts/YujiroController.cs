using UnityEngine;
using System;
using System.Linq;

public class YujiroController : MonoBehaviour {
    
    private Animator animator;
    private Transform personaje;
    private const int InputBufferSize = 10;
    private KeyCode[] inputBuffer = new KeyCode[InputBufferSize];
    private int inputIndex = 0;
    private bool isFacingRight = true;

    public Transform opponent;
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
    public KeyCode[] quarterCircleForwardLeft = {
        KeyCode.S,
        KeyCode.S,
        KeyCode.D,
        KeyCode.D
    };
    public KeyCode[] quarterCircleForwardRight = {
        KeyCode.S,
        KeyCode.S,
        KeyCode.A,
        KeyCode.A
    };
    public KeyCode[] dragonPunchLeft = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.S,
        KeyCode.D
    };
    public KeyCode[] dragonPunchRight = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.S,
        KeyCode.A
    };
    public KeyCode[] halfCircleBackLeft = {
        KeyCode.D,
        KeyCode.D,
        KeyCode.S,
        KeyCode.S,
        KeyCode.S,
        KeyCode.A,
        KeyCode.A
    };
    public KeyCode[] halfCircleBackRight = {
        KeyCode.A,
        KeyCode.A,
        KeyCode.S,
        KeyCode.S,
        KeyCode.S,
        KeyCode.D,
        KeyCode.D
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
        personaje = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        bool isMovingForwards;
        bool isMovingBackwards;
        bool isJumping = Input.GetKey(KeyCode.W);
        bool lightPunch = Input.GetKeyDown(KeyCode.U);
        bool mediumPunch = Input.GetKeyDown(KeyCode.I);
        bool heavyPunch = Input.GetKeyDown(KeyCode.O);
        bool lightKick = Input.GetKeyDown(KeyCode.J);
        bool mediumKick = Input.GetKeyDown(KeyCode.K);
        bool heavyKick = Input.GetKeyDown(KeyCode.L);
        bool previousFacing = isFacingRight;

        if (inputIndex == InputBufferSize) {
            inputIndex = 0;
        }

        if (isFacingRight) {
            isMovingForwards = Input.GetKey(KeyCode.D);
            isMovingBackwards = Input.GetKey(KeyCode.A);
        } else {
            isMovingForwards = Input.GetKey(KeyCode.A);
            isMovingBackwards = Input.GetKey(KeyCode.D);
        }

        animator.SetBool("Forward pressed", isMovingForwards);
        animator.SetBool("Back pressed", isMovingBackwards);
        animator.SetBool("Up pressed", isJumping);

        if (Time.time - lastInputTime > inputTimeout) {
            inputIndex = 0;
        }

        if (inputIndex < InputBufferSize) {
            if (Input.GetKeyDown(quarterCircleForwardLeft[inputIndex])) {
                inputBuffer[inputIndex] = quarterCircleForwardLeft[inputIndex];
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(quarterCircleForwardRight[inputIndex])) {
                inputBuffer[inputIndex] = quarterCircleForwardRight[inputIndex];
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(dragonPunchLeft[inputIndex])) {
                inputBuffer[inputIndex] = dragonPunchLeft[inputIndex];
                inputIndex++;
                lastInputTime = Time.time;
            }
            
            if (Input.GetKeyDown(dragonPunchRight[inputIndex])) {
                inputBuffer[inputIndex] = dragonPunchRight[inputIndex];
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(halfCircleBackLeft[inputIndex])) {
                inputBuffer[inputIndex] = halfCircleBackLeft[inputIndex];
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(halfCircleBackRight[inputIndex])) {
                inputBuffer[inputIndex] = halfCircleBackRight[inputIndex];
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(doubleQuarterCircleForwardLeft[inputIndex])) {
                inputBuffer[inputIndex] = doubleQuarterCircleForwardLeft[inputIndex];
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(doubleQuarterCircleForwardRight[inputIndex])) {
                inputBuffer[inputIndex] = doubleQuarterCircleForwardRight[inputIndex];
                inputIndex++;
                lastInputTime = Time.time;
            }
        }

        if (isFacingRight) {
            if (ContainsQuarterCircleForwardLeft()) {
                if (Array.Exists(kickKeys, key => Input.GetKeyDown(key))) {
                    TriggerOgreAxeKick();
                    inputBuffer = new KeyCode[InputBufferSize];
                }

                inputIndex = 0;
            }

            if (ContainsDragonPunchLeft()) {
                if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                    TriggerOgreUppercut();
                    inputBuffer = new KeyCode[InputBufferSize];
                }

                inputIndex = 0;
            }

            if (ContainsHalfCircleBackLeft()) {
                if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                    TriggerOgreShaori();
                    inputBuffer = new KeyCode[InputBufferSize];
                }

                inputIndex = 0;
            }

            if (ContainsDoubleQuarterCircleForwardLeft()) {
                if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                    TriggerHugOfDeath();
                    inputBuffer = new KeyCode[InputBufferSize];
                }

                inputIndex = 0;
            }
        } else {
            if (ContainsQuarterCircleForwardRight()) {
                if (Array.Exists(kickKeys, key => Input.GetKeyDown(key))) {
                    TriggerOgreAxeKick();
                    inputBuffer = new KeyCode[InputBufferSize];
                }

                inputIndex = 0;
            }

            if (ContainsDragonPunchRight()) {
                if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                    TriggerOgreUppercut();
                    inputBuffer = new KeyCode[InputBufferSize];
                }

                inputIndex = 0;
            }

            if (ContainsHalfCircleBackRight()) {
                if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                    TriggerOgreShaori();
                    inputBuffer = new KeyCode[InputBufferSize];
                }

                inputIndex = 0;
            }

            if (ContainsDoubleQuarterCircleForwardRight()) {
                if (Array.Exists(punchKeys, key => Input.GetKeyDown(key))) {
                    TriggerHugOfDeath();
                    inputBuffer = new KeyCode[InputBufferSize];
                }

                inputIndex = 0;
            }
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

        isFacingRight = IsFacingRight();

        if (isFacingRight != previousFacing) {
            FlipCharacter();
        }
    }

    void TriggerOgreAxeKick() {
        animator.SetTrigger("OgreAxeKickTrigger");
    }

    void TriggerOgreUppercut() {
        animator.SetTrigger("OgreUppercutTrigger");
    }

    void TriggerOgreShaori() {
        animator.SetTrigger("OgreShaoriTrigger");
    }

    void TriggerHugOfDeath() {
        animator.SetTrigger("HugOfDeathTrigger");
    }

    void TriggerHugOfDeathFollowup() {
        animator.SetTrigger("HugOfDeathFollowupTrigger");
    }

    bool ContainsQuarterCircleForwardLeft() {
        for (int i = 0; i <= inputBuffer.Length - quarterCircleForwardLeft.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(quarterCircleForwardLeft.Length).ToArray();

            if (subsequence.SequenceEqual(quarterCircleForwardLeft)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsQuarterCircleForwardRight() {
        for (int i = 0; i <= inputBuffer.Length - quarterCircleForwardRight.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(quarterCircleForwardRight.Length).ToArray();

            if (subsequence.SequenceEqual(quarterCircleForwardRight)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsDragonPunchLeft() {
        for (int i = 0; i <= inputBuffer.Length - dragonPunchLeft.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(dragonPunchLeft.Length).ToArray();

            if (subsequence.SequenceEqual(dragonPunchLeft)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsDragonPunchRight() {
        for (int i = 0; i <= inputBuffer.Length - dragonPunchRight.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(dragonPunchRight.Length).ToArray();

            if (subsequence.SequenceEqual(dragonPunchRight)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHalfCircleBackLeft() {
        for (int i = 0; i <= inputBuffer.Length - halfCircleBackLeft.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(halfCircleBackLeft.Length).ToArray();

            if (subsequence.SequenceEqual(halfCircleBackLeft)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHalfCircleBackRight() {
        for (int i = 0; i <= inputBuffer.Length - halfCircleBackRight.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(halfCircleBackRight.Length).ToArray();

            if (subsequence.SequenceEqual(halfCircleBackRight)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsDoubleQuarterCircleForwardLeft() {
        for (int i = 0; i <= inputBuffer.Length - doubleQuarterCircleForwardLeft.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(doubleQuarterCircleForwardLeft.Length).ToArray();

            if (subsequence.SequenceEqual(doubleQuarterCircleForwardLeft)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsDoubleQuarterCircleForwardRight() {
        for (int i = 0; i <= inputBuffer.Length - doubleQuarterCircleForwardRight.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(doubleQuarterCircleForwardRight.Length).ToArray();

            if (subsequence.SequenceEqual(doubleQuarterCircleForwardRight)) {
                return true;
            }
        }

        return false;
    }

    private bool IsFacingRight() {
        float relativeXPosition = personaje.position.x - opponent.position.x;
        return relativeXPosition >= 0;
    }

    private void FlipCharacter() {
        personaje.Rotate(0, 180, 0);
    }
}
