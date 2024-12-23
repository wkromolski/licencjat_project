using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    [SerializeField] private float speed = 10f;
    [SerializeField] private bool isWalking;
   // [SerializeField] private AudioSource walking;


    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        isWalking = move != Vector3.zero;


       // if (isWalking == false)
      //  {
      //      walking.Play();
      //  }
    }

}
