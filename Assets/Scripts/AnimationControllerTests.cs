using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControllerTests : MonoBehaviour {
    
    private Animator animator;
    private Transform personaje;
    private Quaternion rotation;

    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        personaje = GetComponent<Transform>();
        rotation = personaje.rotation;
    }

    // Update is called once per frame
    void Update() {
        Vector3 newPosition = personaje.position;
        newPosition.y = 0.034f;
        personaje.position = newPosition;

        bool isMovingForwards = Input.GetKey(KeyCode.D);
        bool isMovingBackwards = Input.GetKey(KeyCode.A);
        bool isJumping = Input.GetKey(KeyCode.W);
        bool lightPunch = Input.GetKeyDown(KeyCode.U);
        bool mediumPunch = Input.GetKeyDown(KeyCode.I);
        bool heavyPunch = Input.GetKeyDown(KeyCode.O);
        bool lightKick = Input.GetKeyDown(KeyCode.J);
        bool mediumKick = Input.GetKeyDown(KeyCode.K);
        bool heavyKick = Input.GetKeyDown(KeyCode.L);
        bool ogreAxeKick = Input.GetKeyDown(KeyCode.Z);
        bool ogreUppercut = Input.GetKeyDown(KeyCode.X);
        bool ogreShaori = Input.GetKeyDown(KeyCode.C);
        bool ogreShaoriFollowup = Input.GetKeyDown(KeyCode.V);
        bool hugOfDeath = Input.GetKeyDown(KeyCode.B);
        bool hugOfDeathFollowup = Input.GetKeyDown(KeyCode.N);
        bool hurt = Input.GetKeyDown(KeyCode.M);
        bool knockdown = Input.GetKeyDown(KeyCode.F);
        bool fullKO = Input.GetKeyDown(KeyCode.G);
        bool win = Input.GetKeyDown(KeyCode.H);
        bool block = Input.GetKeyDown(KeyCode.T);

        animator.SetBool("Forward pressed", isMovingForwards);
        animator.SetBool("Back pressed", isMovingBackwards);
        animator.SetBool("Up pressed", isJumping);

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

        if (ogreAxeKick) {
            animator.SetTrigger("OgreAxeKickTrigger");
        }

        if (ogreUppercut) {
            animator.SetTrigger("OgreUppercutTrigger");
        }

        if (ogreShaori) {
            animator.SetTrigger("OgreShaoriTrigger");
        }

        if (ogreShaoriFollowup) {
            animator.SetTrigger("OgreShaoriFollowupTrigger");
        }

        if (hugOfDeath) {
            animator.SetTrigger("HugOfDeathTrigger");
        }

        if (hugOfDeathFollowup) {
            animator.SetTrigger("HugOfDeathFollowupTrigger");
        }

        if (hurt) {
            animator.SetTrigger("HurtTrigger");
        }

        if (knockdown) {
            animator.SetTrigger("KnockdownTrigger");
        }

        if (fullKO) {
            animator.SetTrigger("FullKOTrigger");
        }

        if (win) {
            animator.SetTrigger("WinTrigger");
        }

        if (block) {
            animator.SetTrigger("BlockTrigger");
        }
    }
}
