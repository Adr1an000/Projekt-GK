//author: Dawid Musialik

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class W_PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public CharacterController controller;
    public float gravity;
    public Transform groundCheck;
    public float distance = 0.4f;
    public LayerMask groundMask;
    public float jumpHeight = 3f;
    public bool allowMovement = true;

    bool isGrounded = false;
    bool secondJump = false;
    Vector3 velocity;
    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var healthPickup = hit.gameObject.GetComponentInParent<HealthPickup>();
        if (healthPickup)
        {
            healthPickup.PickUp(gameObject);
        }
        var ammoPickup = hit.gameObject.GetComponentInParent<AmmoPickup>();
        if (ammoPickup)
        {
            ammoPickup.PickUp(gameObject);
        }
    }
    void Update()
    {
        if (!allowMovement) return;

        isGrounded = Physics.CheckSphere(groundCheck.position, distance, groundMask);
        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
            if (Input.GetButtonDown("Jump"))
            {
                secondJump = true;
                velocity.y = Mathf.Sqrt(jumpHeight * (-2) * gravity);
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && secondJump)
            {
                secondJump = false;
                velocity.y = Mathf.Sqrt(jumpHeight * (-2) * gravity);
            }
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        velocity.y += gravity * Time.deltaTime;
        Vector3 move = transform.right * x + transform.forward * z;
        move.Normalize();
        controller.Move(move * speed * Time.deltaTime + velocity * Time.deltaTime);
    }
}
