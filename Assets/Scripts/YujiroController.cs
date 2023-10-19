using UnityEngine;
using System;
using System.Linq;

public class YujiroController : MonoBehaviour {
    
    private Animator animator;
    private const int InputBufferSize = 10;
    private KeyCode[] inputBuffer = new KeyCode[InputBufferSize];
    private int inputIndex = 0;
    private bool isFacingRight = true;
    private float inputTimeout = 1.5f;
    private KeyCode[] lightAxeKickF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.J
    };
    private KeyCode[] mediumAxeKickF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.K
    };
    private KeyCode[] heavyAxeKickF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.L
    };
    private KeyCode[] lightAxeKickB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.J
    };
    private KeyCode[] mediumAxeKickB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.K
    };
    private KeyCode[] heavyAxeKickB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.L
    };
    private KeyCode[] lightUppercutF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.U
    };
    private KeyCode[] mediumUppercutF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.I
    };
    private KeyCode[] heavyUppercutF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.O
    };
    private KeyCode[] lightUppercutB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.U
    };
    private KeyCode[] mediumUppercutB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.I
    };
    private KeyCode[] heavyUppercutB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.O
    };
    private KeyCode[] lightShaoriF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.A,
        KeyCode.U
    };
    private KeyCode[] mediumShaoriF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.A,
        KeyCode.I
    };
    private KeyCode[] heavyShaoriF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.A,
        KeyCode.O
    };
    private KeyCode[] lightShaoriB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.U
    };
    private KeyCode[] mediumShaoriB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.I
    };
    private KeyCode[] heavyShaoriB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.O
    };
    private KeyCode[] lightHugOfDeathF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.U
    };
    private KeyCode[] mediumHugOfDeathF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.I
    };
    private KeyCode[] heavyHugOfDeathF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.O
    };
    private KeyCode[] lightHugOfDeathB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.U
    };
    private KeyCode[] mediumHugOfDeathB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.I
    };
    private KeyCode[] heavyHugOfDeathB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.O
    };
    private float lastInputTime;

    public Transform opponent;
    public Transform personaje;
    public Collider headHurtbox;
    public Collider torsoHurtbox;
    public Collider legsHurtbox;
    public Collider leftElbowHitbox;
    public Collider rightElbowHitbox;
    public Collider leftFistHitbox;
    public Collider rightFistHitbox;
    public Collider leftKneeHitbox;
    public Collider rightKneeHitbox;
    public Collider leftFootHitbox;
    public Collider rightFootHitbox;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
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
        bool down = Input.GetKeyDown(KeyCode.S);
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
            if (isJumping) {
                inputBuffer[inputIndex] = KeyCode.W;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (isMovingBackwards && isFacingRight) {
                inputBuffer[inputIndex] = KeyCode.A;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (isMovingForwards && isFacingRight) {
                inputBuffer[inputIndex] = KeyCode.D;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (isMovingBackwards && !isFacingRight) {
                inputBuffer[inputIndex] = KeyCode.D;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (isMovingForwards && !isFacingRight) {
                inputBuffer[inputIndex] = KeyCode.A;
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

        if (isFacingRight) {
            if (ContainsInput(lightAxeKickF)) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumAxeKickF)) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyAxeKickF)) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightUppercutF)) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumUppercutF)) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyUppercutF)) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightShaoriF)) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumShaoriF)) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyShaoriF)) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightHugOfDeathF)) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumHugOfDeathF)) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyHugOfDeathF)) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }
        } else {
            if (ContainsInput(lightAxeKickB)) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumAxeKickB)) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyAxeKickB)) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightUppercutB)) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumUppercutB)) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyUppercutB)) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightShaoriB)) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumShaoriB)) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyShaoriB)) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(lightHugOfDeathB)) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(mediumHugOfDeathB)) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsInput(heavyHugOfDeathB)) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
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

    bool ContainsInput(KeyCode[] input) {
        for (int i = 0; i <= inputBuffer.Length - input.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(input.Length).ToArray();

            if (subsequence.SequenceEqual(input)) {
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
    
    private void OnTriggerEnter(Collider other) {
        
    }
}
