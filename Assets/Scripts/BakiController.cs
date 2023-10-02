using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BakiController : MonoBehaviour {
    
    private Animator animator;
    
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        bool isMovingForwards = Input.GetKey(KeyCode.D);
        bool isMovingBackwards = Input.GetKey(KeyCode.A);

        animator.SetBool("D_key pressed", isMovingForwards);
        animator.SetBool("A_key pressed", isMovingBackwards);
    }
}
