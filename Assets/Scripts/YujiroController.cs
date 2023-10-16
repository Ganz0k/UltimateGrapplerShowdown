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
    public float inputTimeout = 1.5f;
    public KeyCode[] lightAxeKickF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.J
    };
    public KeyCode[] mediumAxeKickF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.K
    };
    public KeyCode[] heavyAxeKickF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.L
    };
    public KeyCode[] lightAxeKickB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.J
    };
    public KeyCode[] mediumAxeKickB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.K
    };
    public KeyCode[] heavyAxeKickB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.L
    };
    public KeyCode[] lightUppercutF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.U
    };
    public KeyCode[] mediumUppercutF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.I
    };
    public KeyCode[] heavyUppercutF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.O
    };
    public KeyCode[] lightUppercutB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.U
    };
    public KeyCode[] mediumUppercutB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.I
    };
    public KeyCode[] heavyUppercutB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.O
    };
    public KeyCode[] lightShaoriF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.A,
        KeyCode.U
    };
    public KeyCode[] mediumShaoriF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.A,
        KeyCode.I
    };
    public KeyCode[] heavyShaoriF = {
        KeyCode.D,
        KeyCode.S,
        KeyCode.A,
        KeyCode.O
    };
    public KeyCode[] lightShaoriB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.U
    };
    public KeyCode[] mediumShaoriB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.I
    };
    public KeyCode[] heavyShaoriB = {
        KeyCode.A,
        KeyCode.S,
        KeyCode.D,
        KeyCode.O
    };
    public KeyCode[] lightHugOfDeathF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.U
    };
    public KeyCode[] mediumHugOfDeathF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.I
    };
    public KeyCode[] heavyHugOfDeathF = {
        KeyCode.S,
        KeyCode.D,
        KeyCode.S,
        KeyCode.D,
        KeyCode.O
    };
    public KeyCode[] lightHugOfDeathB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.U
    };
    public KeyCode[] mediumHugOfDeathB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.I
    };
    public KeyCode[] heavyHugOfDeathB = {
        KeyCode.S,
        KeyCode.A,
        KeyCode.S,
        KeyCode.A,
        KeyCode.O
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

            if (Input.GetKeyDown(KeyCode.D) && isFacingRight) {
                inputBuffer[inputIndex] = KeyCode.D;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.A) && !isFacingRight) {
                inputBuffer[inputIndex] = KeyCode.A;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.A) && isFacingRight) {
                inputBuffer[inputIndex] = KeyCode.A;
                inputIndex++;
                lastInputTime = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.D) && !isFacingRight) {
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

        if (isFacingRight) {
            if (ContainsLightAxeKickF()) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsMediumAxeKickF()) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsHeavyAxeKickF()) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsLightUppercutF()) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsMediumUppercutF()) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsHeavyUppercutF()) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsLightShaoriF()) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsMediumShaoriF()) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsHeavyShaoriF()) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsLightHugOfDeathF()) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsMediumHugOfDeathF()) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsHeavyHugOfDeathF()) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }
        } else {
            if (ContainsLightAxeKickB()) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsMediumAxeKickB()) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsHeavyAxeKickB()) {
                TriggerOgreAxeKick();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsLightUppercutB()) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsMediumUppercutB()) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsHeavyUppercutB()) {
                TriggerOgreUppercut();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsLightShaoriB()) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsMediumShaoriB()) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsHeavyShaoriB()) {
                TriggerOgreShaori();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsLightHugOfDeathB()) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsMediumHugOfDeathB()) {
                TriggerHugOfDeath();
                inputBuffer = new KeyCode[InputBufferSize];
                inputIndex = 0;
            }

            if (ContainsHeavyHugOfDeathB()) {
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

    bool ContainsLightAxeKickF() {
        for (int i = 0; i <= inputBuffer.Length - lightAxeKickF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(lightAxeKickF.Length).ToArray();

            if (subsequence.SequenceEqual(lightAxeKickF)) {
                return true;
            }
        }

        return false;
    }
    
    bool ContainsMediumAxeKickF() {
        for (int i = 0; i <= inputBuffer.Length - mediumAxeKickF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(mediumAxeKickF.Length).ToArray();

            if (subsequence.SequenceEqual(mediumAxeKickF)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHeavyAxeKickF() {
        for (int i = 0; i <= inputBuffer.Length - heavyAxeKickF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(heavyAxeKickF.Length).ToArray();

            if (subsequence.SequenceEqual(heavyAxeKickF)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsLightAxeKickB() {
        for (int i = 0; i <= inputBuffer.Length - lightAxeKickB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(lightAxeKickB.Length).ToArray();

            if (subsequence.SequenceEqual(lightAxeKickB)) {
                return true;
            }
        }

        return false;
    }
    
    bool ContainsMediumAxeKickB() {
        for (int i = 0; i <= inputBuffer.Length - mediumAxeKickB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(mediumAxeKickB.Length).ToArray();

            if (subsequence.SequenceEqual(mediumAxeKickB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHeavyAxeKickB() {
        for (int i = 0; i <= inputBuffer.Length - heavyAxeKickB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(heavyAxeKickB.Length).ToArray();

            if (subsequence.SequenceEqual(heavyAxeKickB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsLightUppercutF() {
        for (int i = 0; i <= inputBuffer.Length - lightUppercutF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(lightUppercutF.Length).ToArray();

            if (subsequence.SequenceEqual(lightUppercutF)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsMediumUppercutF() {
        for (int i = 0; i <= inputBuffer.Length - mediumUppercutF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(mediumUppercutF.Length).ToArray();

            if (subsequence.SequenceEqual(mediumUppercutF)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHeavyUppercutF() {
        for (int i = 0; i <= inputBuffer.Length - heavyUppercutF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(heavyUppercutF.Length).ToArray();

            if (subsequence.SequenceEqual(heavyUppercutF)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsLightUppercutB() {
        for (int i = 0; i <= inputBuffer.Length - lightUppercutB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(lightUppercutB.Length).ToArray();

            if (subsequence.SequenceEqual(lightUppercutB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsMediumUppercutB() {
        for (int i = 0; i <= inputBuffer.Length - mediumUppercutB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(mediumUppercutB.Length).ToArray();

            if (subsequence.SequenceEqual(mediumUppercutB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHeavyUppercutB() {
        for (int i = 0; i <= inputBuffer.Length - heavyUppercutB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(heavyUppercutB.Length).ToArray();

            if (subsequence.SequenceEqual(heavyUppercutB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsLightShaoriF() {
        for (int i = 0; i <= inputBuffer.Length - lightShaoriF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(lightShaoriF.Length).ToArray();

            if (subsequence.SequenceEqual(lightShaoriF)) {
                return true;
            }
        }
        
        return false;
    }

    bool ContainsMediumShaoriF() {
        for (int i = 0; i <= inputBuffer.Length - mediumShaoriF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(mediumShaoriF.Length).ToArray();

            if (subsequence.SequenceEqual(mediumShaoriF)) {
                return true;
            } 
        }

        return false;
    }

    bool ContainsHeavyShaoriF() {
        for (int i = 0; i <= inputBuffer.Length - heavyShaoriF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(heavyShaoriF.Length).ToArray();

            if (subsequence.SequenceEqual(heavyShaoriF)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsLightShaoriB() {
        for (int i = 0; i <= inputBuffer.Length - lightShaoriB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(lightShaoriB.Length).ToArray();

            if (subsequence.SequenceEqual(lightShaoriB)) {
                return true;
            }  
        }

        return false;
    }

    bool ContainsMediumShaoriB() {
        for (int i = 0; i <= inputBuffer.Length - mediumShaoriB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(mediumShaoriB.Length).ToArray();

            if (subsequence.SequenceEqual(mediumShaoriB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHeavyShaoriB() {
        for (int i = 0; i <= inputBuffer.Length - heavyShaoriB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(heavyShaoriB.Length).ToArray();

            if (subsequence.SequenceEqual(heavyShaoriB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsLightHugOfDeathF() {
        for (int i = 0; i <= inputBuffer.Length - lightHugOfDeathF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(lightHugOfDeathF.Length).ToArray();

            if (subsequence.SequenceEqual(lightHugOfDeathF)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsMediumHugOfDeathF() {
        for (int i = 0; i <= inputBuffer.Length - mediumHugOfDeathF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(mediumHugOfDeathF.Length).ToArray();

            if (subsequence.SequenceEqual(mediumHugOfDeathF)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHeavyHugOfDeathF() {
        for (int i = 0; i <= inputBuffer.Length - heavyHugOfDeathF.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(heavyHugOfDeathF.Length).ToArray();

            if (subsequence.SequenceEqual(heavyHugOfDeathF)) {
                return true;
            } 
        }

        return false;
    }

    bool ContainsLightHugOfDeathB() {
        for (int i = 0; i <= inputBuffer.Length - lightHugOfDeathB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(lightHugOfDeathB.Length).ToArray();

            if (subsequence.SequenceEqual(lightHugOfDeathB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsMediumHugOfDeathB() {
        for (int i = 0; i <= inputBuffer.Length - mediumHugOfDeathB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(mediumHugOfDeathB.Length).ToArray();

            if (subsequence.SequenceEqual(mediumHugOfDeathB)) {
                return true;
            }
        }

        return false;
    }

    bool ContainsHeavyHugOfDeathB() {
        for (int i = 0; i <= inputBuffer.Length - heavyHugOfDeathB.Length; i++) {
            KeyCode[] subsequence = inputBuffer.Skip(i).Take(heavyHugOfDeathB.Length).ToArray();

            if (subsequence.SequenceEqual(heavyHugOfDeathB)) {
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
