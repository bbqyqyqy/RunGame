using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HQYScripts : MonoBehaviour {
    private CollisionFlags flags;
    float forwardSpeed = 15F;
    float gravity = 8F;
    float jumpSpeed = 10F;
    int blood = 5;
    int horizontalSpeed = 8;

    Vector3 moveDir = Vector3.zero;
    CharacterController controller;

    // CharacterController controller;
    Animator anim;
    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        anim.SetInteger("condition", 0);
        moveDir = new Vector3(0, 0, 1);
        moveDir *= forwardSpeed;
    }

    // Update is called once per frame
    void Update() {

        if (anim.GetInteger("condition") == -1) {
            return;
        }
            
        if (anim.GetBool("jumping") == true) {
            moveDir.y -= gravity * Time.deltaTime;
            flags = controller.Move(moveDir * Time.deltaTime);
            if (flags == CollisionFlags.Below) {
                print("---Jump CollisionFlags");
                moveDir.y = 0;
                anim.SetInteger("condition", 0);
                anim.SetBool("jumping", false);
            }
            return;
        }
            
        moveDir.x = Input.GetAxis("Horizontal") * horizontalSpeed;
        
        if (Input.GetKey(KeyCode.W)) {
            print("---GetKeyDown: KeyCode.W");
            anim.SetInteger("condition", 1);
            moveDir = new Vector3(0, 0, 1);
            forwardSpeed = 25;
            moveDir *= forwardSpeed;
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            print("---GetKeyUp: KeyCode.W");
            anim.SetInteger("condition", 0);
            moveDir = new Vector3(0, 0, 1);
            forwardSpeed = 15;
            moveDir *= forwardSpeed;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("jumping")) {
            print("---GetKeyDown: KeyCode.Space");
            anim.SetBool("jumping", true);
            moveDir.y = jumpSpeed;
            anim.SetInteger("condition", 2);
        }
        if (Input.GetMouseButtonDown(0)) { // left mouse button
            print("---left mouse button");
            Attacking();
        }
        controller.Move(moveDir * Time.deltaTime);
    }

    void Attacking() {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine() {
        anim.SetBool("attacking", false);
        anim.SetInteger("condition", 3);
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("condition", 0);
        anim.SetBool("attacking", false);
    }
    
    void OnControllerColliderHit(ControllerColliderHit hit) {
        GameObject hitObject = hit.collider.gameObject;
        // If the player hit something which is not the road && the collision is in the player's face (hit.normal.y == 0.0)   
        // if (!hitObject.name.Equals("Road") && hit.normal.y != 1.0) {
        if (!hitObject.name.Equals("Plane") && hit.normal.y < 0.5) {
            anim.SetInteger("condition", -1);
            print("[OnControllerColliderHit] hitObject.name: " + hitObject.name);
            print("[OnControllerColliderHit] hit.normal.y: " + hit.normal.y);
            print("[OnControllerColliderHit] die!");
        }
    }
}
