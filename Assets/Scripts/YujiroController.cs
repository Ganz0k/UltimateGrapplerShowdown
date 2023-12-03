using UnityEngine;
using System;
using System.Linq;

public class YujiroController : MonoBehaviour {
    
    public ParticleSystem hitEffect;
    public ParticleSystem blockEffect;
    public static Animator animator;
    private const int InputBufferSize = 10;
    private KeyCode[] inputBuffer = new KeyCode[InputBufferSize];
    private int inputIndex = 0;
    private bool isFacingRight = true;
    private float inputTimeout = 0.5f;
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
    public AudioSource audioSource {
        get {
            return GetComponent<AudioSource>();
        }
    }
    public AudioClip golpe;
    public AudioClip bloqueo;
    private int golpeInt = 0;

    // Start is called before the first frame update
    void Start() {
        gameObject.AddComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
        opponent = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (!MenuPausa.isPaused) {
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

                if (Input.GetKeyDown(KeyCode.A)) {
                    inputBuffer[inputIndex] = KeyCode.A;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (Input.GetKeyDown(KeyCode.D)) {
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
                if (ContainsInput(lightHugOfDeathF) && PlayerOneHealth.superMeter >= 100f) {
                    PlayerOneHealth.superMeter = 0;
                    TriggerHugOfDeath();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

                if (ContainsInput(mediumHugOfDeathF) && PlayerOneHealth.superMeter >= 100f) {
                    PlayerOneHealth.superMeter = 0;
                    TriggerHugOfDeath();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

                if (ContainsInput(heavyHugOfDeathF) && PlayerOneHealth.superMeter >= 100f) {
                    PlayerOneHealth.superMeter = 0;
                    TriggerHugOfDeath();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

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
            } else {
                if (ContainsInput(lightHugOfDeathB) && PlayerOneHealth.superMeter >= 100f) {
                    PlayerOneHealth.superMeter = 0;
                    TriggerHugOfDeath();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

                if (ContainsInput(mediumHugOfDeathB) && PlayerOneHealth.superMeter >= 100f) {
                    PlayerOneHealth.superMeter = 0;
                    TriggerHugOfDeath();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

                if (ContainsInput(heavyHugOfDeathB) && PlayerOneHealth.superMeter >= 100f) {
                    PlayerOneHealth.superMeter = 0;
                    TriggerHugOfDeath();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

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

            if (opponent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                golpeInt = 0;
            }
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

    void TriggerOgreShaoriFollowup() {
        animator.SetTrigger("OgreShaoriFollowupTrigger");
    }

    void TriggerHugOfDeath() {
        animator.SetTrigger("HugOfDeathTrigger");
    }

    void TriggerHugOfDeathFollowup() {
        animator.SetTrigger("HugOfDeathFollowupTrigger");
    }

    void TriggerHurt() {
        animator.SetTrigger("HurtTrigger");
    }

    void TriggerKnockdown() {
        animator.SetTrigger("KnockdownTrigger");
    }

    public static void FullKOTrigger() {
        animator.SetTrigger("FullKOTrigger");
    }

    public static void WinTrigger() {
        animator.SetTrigger("WinTrigger");
    }

    public static void IdleTrigger() {
        animator.SetTrigger("IdleTrigger");
    }

    void TriggerBlock() {
        animator.SetTrigger("BlockTrigger");
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
    
    void OnTriggerEnter(Collider other) {
        Animator opponentAnimator = opponent.GetComponent<Animator>();
        AnimatorStateInfo currentOpponentState = opponentAnimator.GetCurrentAnimatorStateInfo(0);

        if (other.CompareTag("Hitbox")) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk back") && PlayerOneHealth.Salud != 0) {
                TriggerBlock();
                blockEffect.Play();

                if (golpeInt == 0) {
                    audioSource.PlayOneShot(bloqueo);
                    golpeInt++;
                    PlayerOneHealth.RecibirAtaque(2.5f);
                    OpponentHealth.BuildMeter();
                }
            } else if (currentOpponentState.IsName("Cockroach dash") || currentOpponentState.IsName("Whip strike") ||
                currentOpponentState.IsName("Ogre axe kick") || currentOpponentState.IsName("Ogre uppercut") || currentOpponentState.IsName("Ogre shaori followup") && PlayerOneHealth.Salud != 0) {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ogre shaori stance")) {
                    TriggerOgreShaoriFollowup();
                } else {
                    if (golpeInt == 0) {
                        TriggerKnockdown();
                        hitEffect.Play();
                        audioSource.PlayOneShot(golpe);
                        golpeInt++;
                        PlayerOneHealth.RecibirAtaque(25f);
                        OpponentHealth.BuildMeter();
                    }
                }
            } else if (currentOpponentState.IsName("Light punch") || currentOpponentState.IsName("Medium punch") ||
                currentOpponentState.IsName("Heavy punch") || currentOpponentState.IsName("Light kick") ||
                currentOpponentState.IsName("Medium kick") || currentOpponentState.IsName("Heavy kick") && PlayerOneHealth.Salud != 0) {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ogre shaori stance")) {
                    TriggerOgreShaoriFollowup();
                } else {
                    if (golpeInt == 0) {
                        TriggerHurt();
                        hitEffect.Play();
                        audioSource.PlayOneShot(golpe);
                        golpeInt++;
                        OpponentHealth.BuildMeter();
                        
                        if (currentOpponentState.IsName("Light punch") || currentOpponentState.IsName("Light kick")) {
                            PlayerOneHealth.RecibirAtaque(5f);
                        } else if (currentOpponentState.IsName("Medium punch") || currentOpponentState.IsName("Medium kick")) {
                            PlayerOneHealth.RecibirAtaque(10f);
                        } else if (currentOpponentState.IsName("Heavy punch") || currentOpponentState.IsName("Heavy kick")) {
                            PlayerOneHealth.RecibirAtaque(15f);
                        }
                    }
                }
            } else if (currentOpponentState.IsName("Triceratops fist followup") || currentOpponentState.IsName("Hug of death followup") && PlayerOneHealth.Salud != 0) {
                if (golpeInt == 0) {
                    TriggerHurt();
                    hitEffect.Play();
                    TriggerKnockdown();
                    audioSource.PlayOneShot(golpe);
                    golpeInt++;
                    PlayerOneHealth.RecibirAtaque(50f);
                }
            }
        }
        
        if (other.CompareTag("Hurtbox") && animator.GetCurrentAnimatorStateInfo(0).IsName("Hug of death activation") && !currentOpponentState.IsName("Block")) {
            TriggerHugOfDeathFollowup();
        }
    }
}
