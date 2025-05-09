using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    [SerializeField] private float speed = 10f;
    [SerializeField] private bool isWalking;
   
    [Header("Footstep Settings")]
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private float stepInterval = 0.5f;

    private float stepTimer = 0f;
    private bool wasWalkingLastFrame = false;
    
    [SerializeField] private float groundY = 1.344f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        isWalking = move != Vector3.zero;

        if (isWalking)
        {
            stepTimer += Time.deltaTime;

            if (!wasWalkingLastFrame)
            {
                audioManager.PlayFootstep();
                stepTimer = 0f;
            }
            else if (stepTimer >= stepInterval)
            {
                audioManager.PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }

        wasWalkingLastFrame = isWalking;

        if (Mathf.Abs(transform.position.y - groundY) > 0.001f)
        {
            controller.enabled = false;

            Vector3 pos = transform.position;
            pos.y = groundY;
            transform.position = pos;

            controller.enabled = true;
        }
    }

}