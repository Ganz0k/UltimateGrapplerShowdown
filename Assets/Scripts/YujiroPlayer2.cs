using UnityEngine;
using System;
using System.Linq;

public class YujiroPlayer2 : MonoBehaviour {
    
    public ParticleSystem hitEffect;
    public ParticleSystem blockEffect;
    public Animator animator;
    private const int InputBufferSize = 10;
    private KeyCode[] inputBuffer = new KeyCode[InputBufferSize];
    private int inputIndex = 0;
    private bool isFacingLeft = true;
    private float inputTimeout = 0.5f;
    private KeyCode[] lightAxeKickF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad1
    };
    private KeyCode[] mediumAxeKickF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad2
    };
    private KeyCode[] heavyAxeKickF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad3
    };
    private KeyCode[] lightAxeKickB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad1
    };
    private KeyCode[] mediumAxeKickB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad2
    };
    private KeyCode[] heavyAxeKickB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad3
    };
    private KeyCode[] lightUppercutF = {
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumUppercutF = {
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyUppercutF = {
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightUppercutB = {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumUppercutB = {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyUppercutB = {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightShaoriF = {
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumShaoriF = {
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyShaoriF = {
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightShaoriB = {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumShaoriB = {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyShaoriB = {
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightHugOfDeathF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumHugOfDeathF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyHugOfDeathF = {
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.DownArrow,
        KeyCode.RightArrow,
        KeyCode.Keypad6
    };
    private KeyCode[] lightHugOfDeathB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad4
    };
    private KeyCode[] mediumHugOfDeathB = {
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.Keypad5
    };
    private KeyCode[] heavyHugOfDeathB = {
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
        opponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (!MenuPausa.isPaused) {
            bool isMovingForwards;
            bool isMovingBackwards;
            bool isJumping = Input.GetKey(KeyCode.UpArrow);
            bool lightPunch = Input.GetKeyDown(KeyCode.Keypad4);
            bool mediumPunch = Input.GetKeyDown(KeyCode.Keypad5);
            bool heavyPunch = Input.GetKeyDown(KeyCode.Keypad6);
            bool lightKick = Input.GetKeyDown(KeyCode.Keypad1);
            bool mediumKick = Input.GetKeyDown(KeyCode.Keypad2);
            bool heavyKick = Input.GetKeyDown(KeyCode.Keypad3);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);
            bool previousFacing = isFacingLeft;

            if (inputIndex == InputBufferSize) {
                inputIndex = 0;
            }

            if (isFacingLeft) {
                isMovingForwards = Input.GetKey(KeyCode.RightArrow);
                isMovingBackwards = Input.GetKey(KeyCode.LeftArrow);
            } else {
                isMovingForwards = Input.GetKey(KeyCode.LeftArrow);
                isMovingBackwards = Input.GetKey(KeyCode.RightArrow);
            }

            animator.SetBool("Forward pressed", isMovingForwards);
            animator.SetBool("Back pressed", isMovingBackwards);
            animator.SetBool("Up pressed", isJumping);

            if (Time.time - lastInputTime > inputTimeout) {
                inputIndex = 0;
            }

            if (inputIndex < InputBufferSize) {
                if (isJumping) {
                    inputBuffer[inputIndex] = KeyCode.UpArrow;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                    inputBuffer[inputIndex] = KeyCode.LeftArrow;
                    inputIndex++;
                    lastInputTime = Time.time;
                }

                if (Input.GetKeyDown(KeyCode.RightArrow)) {
                    inputBuffer[inputIndex] = KeyCode.RightArrow;
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

            if (!isFacingLeft) {
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

            isFacingLeft = IsFacingLeft();

            if (isFacingLeft != previousFacing) {
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

    private bool IsFacingLeft() {
        float relativeXPosition = personaje.position.x - opponent.position.x;
        return relativeXPosition <= 0;
    }

    private void FlipCharacter() {
        personaje.Rotate(0, 180, 0);
    }
    
    void OnTriggerEnter(Collider other) {
        Animator opponentAnimator = opponent.GetComponent<Animator>();
        AnimatorStateInfo currentOpponentState = opponentAnimator.GetCurrentAnimatorStateInfo(0);

        if (other.CompareTag("Hitbox")) {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk back")) {
                TriggerBlock();
                blockEffect.Play();

                if (golpeInt == 0) {
                    audioSource.PlayOneShot(bloqueo);
                    golpeInt++;
                }
            } else if (currentOpponentState.IsName("Cockroach dash") || currentOpponentState.IsName("Whip strike") ||
                currentOpponentState.IsName("Ogre axe kick") || currentOpponentState.IsName("Ogre uppercut") || currentOpponentState.IsName("Ogre shaori followup")) {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ogre shaori stance")) {
                    TriggerOgreShaoriFollowup();
                } else {
                    if (golpeInt == 0) {
                        TriggerKnockdown();
                        hitEffect.Play();
                        audioSource.PlayOneShot(golpe);
                        golpeInt++;
                    }
                }
            } else if (currentOpponentState.IsName("Light punch") || currentOpponentState.IsName("Medium punch") ||
                currentOpponentState.IsName("Heavy punch") || currentOpponentState.IsName("Light kick") ||
                currentOpponentState.IsName("Medium kick") || currentOpponentState.IsName("Heavy kick")) {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ogre shaori stance")) {
                    TriggerOgreShaoriFollowup();
                } else {
                    if (golpeInt == 0) {
                        TriggerHurt();
                        hitEffect.Play();
                        audioSource.PlayOneShot(golpe);
                        golpeInt++;
                    }
                }
            } else if (currentOpponentState.IsName("Triceratops fist followup") || currentOpponentState.IsName("Hug of death followup")) {
                TriggerHurt();
                hitEffect.Play();

                if (golpeInt == 0) {
                    audioSource.PlayOneShot(golpe);
                    golpeInt++;
                }
                TriggerKnockdown();
            }
        }

        if (other.CompareTag("Hurtbox") && animator.GetCurrentAnimatorStateInfo(0).IsName("Hug of death activation") && !currentOpponentState.IsName("Block")) {
            TriggerHugOfDeathFollowup();
        }
    }
}
