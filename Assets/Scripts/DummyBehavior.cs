using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyBehavior : MonoBehaviour {

    public ParticleSystem hitEffect;
    public static Animator animator;
    public Transform opponent;
    public Transform personaje;
    public AudioSource audioSource {
        get {
            return GetComponent<AudioSource>();
        }
    }
    public AudioClip golpe;
    private int golpeInt = 0;

    // Start is called before the first frame update
    void Start() {
        gameObject.AddComponent<AudioSource>();
        animator = gameObject.GetComponent<Animator>();
        opponent = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        if (opponent.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            golpeInt = 0;
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

    private void OnTriggerEnter(Collider other) {
        Animator opponentAnimator = opponent.GetComponent<Animator>();
        AnimatorStateInfo currentOpponentState = opponentAnimator.GetCurrentAnimatorStateInfo(0);

        if (other.CompareTag("Hitbox")) {
            if (currentOpponentState.IsName("Cockroach dash") || currentOpponentState.IsName("Whip strike") ||
                currentOpponentState.IsName("Ogre axe kick") || currentOpponentState.IsName("Ogre uppercut") || currentOpponentState.IsName("Ogre shaori followup")) {
                if (golpeInt == 0) {
                    TriggerKnockdown();
                    hitEffect.Play();
                    audioSource.PlayOneShot(golpe);
                    golpeInt++;
                    PlayerOneHealth.BuildMeter();
                }
            } else if (currentOpponentState.IsName("Light punch") || currentOpponentState.IsName("Medium punch") ||
                currentOpponentState.IsName("Heavy punch") || currentOpponentState.IsName("Light kick") ||
                currentOpponentState.IsName("Medium kick") || currentOpponentState.IsName("Heavy kick")) {
                if (golpeInt == 0) {
                    TriggerHurt();
                    hitEffect.Play();
                    audioSource.PlayOneShot(golpe);
                    golpeInt++;
                    PlayerOneHealth.BuildMeter();
                }
            } else if (currentOpponentState.IsName("Triceratops fist followup") || currentOpponentState.IsName("Hug of death followup")) {
                if (golpeInt == 0) {
                    TriggerHurt();
                    hitEffect.Play();
                    TriggerKnockdown();
                    audioSource.PlayOneShot(golpe);
                    golpeInt++;
                }
            }
        }
    }
}
