using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BookshelfAnomaly : MonoBehaviour
{
    [SerializeField] private Transform[] books; 
    [SerializeField] private float launchDistance = 2f; 
    [SerializeField] private float launchTime = 0.5f; 
    [SerializeField] private float rotationAmount = 180f; 
    [SerializeField] private float jumpPower = 1f; 
    [SerializeField] private int jumpCount = 2;
    [SerializeField] private Rigidbody rb;

    private bool triggered = false; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !triggered)
        {
            triggered = true;
            LaunchBooks();
            rb.isKinematic = false;
        }
    }

    private void LaunchBooks()
    {
        foreach (Transform book in books)
        {
            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f), 
                Random.Range(0.5f, 0.5f), 
                Random.Range(-1f, 1f) 
            ).normalized * launchDistance;

            book.DOJump(book.position + randomDirection, jumpPower, jumpCount, launchTime)
                .SetEase(Ease.OutQuad);

            book.DORotate(new Vector3(
                Random.Range(-rotationAmount, rotationAmount),
                Random.Range(-rotationAmount, rotationAmount),
                Random.Range(-rotationAmount, rotationAmount)
            ), launchTime, RotateMode.FastBeyond360);
        }
    }
}
