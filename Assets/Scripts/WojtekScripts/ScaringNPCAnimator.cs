using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaringNPCAnimator : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private PlayerLooking playerCamera;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("isTurning", playerCamera.IsLooking());
    }
}
