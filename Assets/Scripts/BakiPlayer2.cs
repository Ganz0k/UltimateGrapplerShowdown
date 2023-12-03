using UnityEngine;
using System;
using System.Linq;

public class BakiPlayer2 : MonoBehaviour {
    
    public ParticleSystem hitEffect;
    public ParticleSystem blockEffect;
    public static Animator animator;
    private const int InputBufferSize = 10;
    private KeyCode[] inputBuffer = new KeyCode[InputBufferSize];
    private int inputIndex = 0;
    private bool isFacingLeft = true;
    private float inputTimeout = 0.5f;
    private KeyCode[] lightCockroachDashF = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad1
    };
    private KeyCode[] mediumCockroachDashF = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad2
    };
    private KeyCode[] heavyCockroachDashF = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad3
    };
    private KeyCode[] lightCockroachDashB = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad1
    };
    private KeyCode[] mediumCockroachDashB = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad2
    };
    private KeyCode[] heavyCockroachDashB = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad3
    };
    private KeyCode[] lightWhipStrikeF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumWhipStrikeF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyWhipStrikeF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightWhipStrikeB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumWhipStrikeB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyWhipStrikeB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightDemonBack = {
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumDemonBack = {
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyDemonBack = {
        KeyCode.DownArrow,
        KeyCode.DownArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightTriceratopsF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumTriceratopsF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyTriceratopsF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightTriceratopsB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumTriceratopsB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyTriceratopsB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad6
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
        opponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (!MenuPausa.isPaused) {
            bool isMovingForwards;
            bool isMovingBackwards;
            bool isJumping = Input.GetKey(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);
            bool lightPunch = Input.GetKeyDown(KeyCode.Keypad4);
            bool mediumPunch = Input.GetKeyDown(KeyCode.Keypad5);
            bool heavyPunch = Input.GetKeyDown(KeyCode.Keypad6);
            bool lightKick = Input.GetKeyDown(KeyCode.Keypad1);
            bool mediumKick = Input.GetKeyDown(KeyCode.Keypad2);
            bool heavyKick = Input.GetKeyDown(KeyCode.Keypad3);
            bool previousFacing = isFacingLeft;

            if (inputIndex == InputBufferSize) {
                inputIndex = 0;
            }

            if (isFacingLeft) {
                isMovingForwards = Input.GetKey(KeyCode.LeftArrow);
                isMovingBackwards = Input.GetKey(KeyCode.RightArrow);
            } else {
                isMovingForwards = Input.GetKey(KeyCode.RightArrow);
                isMovingBackwards = Input.GetKey(KeyCode.LeftArrow);
            }

            animator.SetBool("Forward pressed", isMovingForwards);
            animator.SetBool("Back pressed", isMovingBackwards);
            animator.SetBool("Up pressed", isJumping);

            if (Time.time - lastInputTime > inputTimeout) {
                inputIndex = 0;
            }

            if (inputIndex < InputBufferSize) {
                if (Input.GetKeyDown(KeyCode.UpArrow)) {
                    inputBuffer[inputIndex] = KeyCode.UpArrow;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    inputBuffer[inputIndex] = KeyCode.RightArrow;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    inputBuffer[inputIndex] = KeyCode.LeftArrow;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (down) {
                    inputBuffer[inputIndex] = KeyCode.DownArrow;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (lightPunch) {
                    inputBuffer[inputIndex] = KeyCode.Keypad4;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (mediumPunch) {
                    inputBuffer[inputIndex] = KeyCode.Keypad5;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (heavyPunch) {
                    inputBuffer[inputIndex] = KeyCode.Keypad6;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (lightKick) {
                    inputBuffer[inputIndex] = KeyCode.Keypad1;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (mediumKick) {
                    inputBuffer[inputIndex] = KeyCode.Keypad2;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (heavyKick) {
                    inputBuffer[inputIndex] = KeyCode.Keypad3;
                    inputIndex++;
                    lastInputTime = Time.time;
                }
            }

            if (isFacingLeft) {
                if (ContainsInput(lightTriceratopsB) && OpponentHealth.superMeter >= 100f) {
                    OpponentHealth.superMeter = 0;
                    TriggerTriceratopsFist();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

                if (ContainsInput(mediumTriceratopsB) && OpponentHealth.superMeter >= 100f) {
                    OpponentHealth.superMeter = 0;
                    TriggerTriceratopsFist();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

                if (ContainsInput(heavyTriceratopsB) && OpponentHealth.superMeter >= 100f) {
                    OpponentHealth.superMeter = 0;
                    TriggerTriceratopsFist();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

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
            } else {
                if (ContainsInput(lightTriceratopsF) && OpponentHealth.superMeter >= 100f) {
                    OpponentHealth.superMeter = 0;
                    TriggerTriceratopsFist();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

                if (ContainsInput(mediumTriceratopsF) && OpponentHealth.superMeter >= 100f) {
                    OpponentHealth.superMeter = 0;
                    TriggerTriceratopsFist();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

                if (ContainsInput(heavyTriceratopsF) && OpponentHealth.superMeter >= 100f) {
                    OpponentHealth.superMeter = 0;
                    TriggerTriceratopsFist();
                    inputBuffer = new KeyCode[InputBufferSize];
                    inputIndex = 0;
                }

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

            if (opponent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                golpeInt = 0;
            }
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

    public bool IsFacingLeft() {
        float relativeXPosition = personaje.position.x - opponent.position.x;
        return relativeXPosition <= 0;
    }

    private void FlipCharacter() {
        personaje.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter(Collider other) {
        Animator opponentAnimator = opponent.GetComponent<Animator>();
        AnimatorStateInfo currentOpponentState = opponentAnimator.GetCurrentAnimatorStateInfo(0);

        if (other.CompareTag("Hitbox")) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk backwards") && OpponentHealth.Salud != 0) {
                TriggerBlock();
                blockEffect.Play();

                if (golpeInt == 0) {
                    audioSource.PlayOneShot(bloqueo);
                    golpeInt++;
                    OpponentHealth.RecibirAtaque(2.5f);
                }
            } else if (currentOpponentState.IsName("Cockroach dash") || currentOpponentState.IsName("Whip strike") ||
                currentOpponentState.IsName("Ogre axe kick") || currentOpponentState.IsName("Ogre uppercut") || currentOpponentState.IsName("Ogre shaori followup") && OpponentHealth.Salud != 0) {
                if (golpeInt == 0) {
                    TriggerKnockdown();
                    hitEffect.Play();
                    audioSource.PlayOneShot(golpe);
                    golpeInt++;
                    OpponentHealth.RecibirAtaque(25f);
                    PlayerOneHealth.BuildMeter();
                }
            } else if (currentOpponentState.IsName("Light punch") || currentOpponentState.IsName("Medium punch") ||
                currentOpponentState.IsName("Heavy punch") || currentOpponentState.IsName("Light kick") ||
                currentOpponentState.IsName("Medium kick") || currentOpponentState.IsName("Heavy kick") && OpponentHealth.Salud != 0) {
                if (golpeInt == 0) {
                        TriggerHurt();
                        hitEffect.Play();
                        audioSource.PlayOneShot(golpe);
                        golpeInt++;
                        PlayerOneHealth.BuildMeter();
                        
                        if (currentOpponentState.IsName("Light punch") || currentOpponentState.IsName("Light kick")) {
                            OpponentHealth.RecibirAtaque(5f);
                        } else if (currentOpponentState.IsName("Medium punch") || currentOpponentState.IsName("Medium kick")) {
                            OpponentHealth.RecibirAtaque(10f);
                        } else if (currentOpponentState.IsName("Heavy punch") || currentOpponentState.IsName("Heavy kick")) {
                            OpponentHealth.RecibirAtaque(15f);
                        }
                    }
            } else if (currentOpponentState.IsName("Triceratops fist followup") || currentOpponentState.IsName("Hug of death followup") && OpponentHealth.Salud != 0) {
                if (golpeInt == 0) {
                    TriggerHurt();
                    hitEffect.Play();
                    TriggerKnockdown();
                    OpponentHealth.RecibirAtaque(50f);
                    audioSource.PlayOneShot(golpe);
                    golpeInt++;
                }
            }
        }

        if (other.CompareTag("Hurtbox") && animator.GetCurrentAnimatorStateInfo(0).IsName("Triceratops fist") && !currentOpponentState.IsName("Block")) {
            TriggerTriceratopsFistFollowup();
        }
    }
}
