using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentCharacter : MonoBehaviour {

    public ParticleSystem hitEffect;
    public ParticleSystem blockEffect;
    private bool isFacingLeft = true;
    public int rutina;
    public float cronometro;
    public static  Animator animator;
    public Transform target;
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
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (!MenuPausa.isPaused) {
            bool previousFacing = isFacingLeft;

            if (Vector3.Distance(transform.position,target.transform.position) > 5) {
                cronometro += 1 * Time.deltaTime;
                
                if (cronometro >= 4) {
                    rutina = Random.Range(0, 1);
                    cronometro = 0;
                }

                switch (rutina) {
                    case 0:
                        animator.SetBool("Forward pressed", false);
                        break;
                    case 1:
                        animator.SetBool("Forward pressed", true);
                        break;
                }
            } else {
                if (Vector3.Distance(personaje.position, target.position) > 1) {
                    var lookPos = target.position - personaje.position;
                    lookPos.y = 0;
                    var rotation = Quaternion.LookRotation(lookPos);
                    transform.rotation = Quaternion.RotateTowards(personaje.rotation, rotation, 3);
                    animator.SetBool("Forward pressed", false);
                    transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                    animator.SetTrigger("MediumPunchTrigger");

                } else {
                    animator.SetBool("Forward pressed", false);
                }
            }

            isFacingLeft = IsFacingLeft();

            if (isFacingLeft != previousFacing) {
                FlipCharacter();
            }

            if (target.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
                golpeInt = 0;
            }
        }
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

    private bool IsFacingLeft() {
        float relativeXPosition = personaje.position.x - target.position.x;
        return relativeXPosition <= 0;
    }

    private void FlipCharacter() {
        personaje.Rotate(0, 180, 0);
    }

    private void OnTriggerEnter(Collider other) {
        Animator opponentAnimator = target.GetComponent<Animator>();
        AnimatorStateInfo currentOpponentState = opponentAnimator.GetCurrentAnimatorStateInfo(0);

        if (other.CompareTag("Hitbox")) {
            if (currentOpponentState.IsName("Cockroach dash") || currentOpponentState.IsName("Whip strike") ||
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
                currentOpponentState.IsName("Medium kick") || currentOpponentState.IsName("Heavy kick")) {
                if (golpeInt == 0) {
                    TriggerHurt();
                    hitEffect.Play();
                    audioSource.PlayOneShot(golpe);
                    PlayerOneHealth.BuildMeter();
                    golpeInt++;
                    
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
                    audioSource.PlayOneShot(golpe);
                    golpeInt++;
                    OpponentHealth.RecibirAtaque(50f);
                }
            }
        }
    }
}
