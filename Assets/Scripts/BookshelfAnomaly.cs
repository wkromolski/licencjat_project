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
            Rigidbody rb = book.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = book.gameObject.AddComponent<Rigidbody>();
            }
            
            rb.isKinematic = true;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            
            Vector3 launchDirection = new Vector3(0f, 0.1f, Random.Range(-0.6f, -0.2f)).normalized * launchDistance;
            
            var jumpTween = book.DOJump(book.position + launchDirection, jumpPower, jumpCount, launchTime)
                .SetEase(Ease.OutQuad);
    
            jumpTween.OnUpdate(() => {
                if (jumpTween.ElapsedPercentage() >= 0.2f && rb.isKinematic)
                {
                    rb.isKinematic = false;
                }
            });
            
            book.DORotate(new Vector3(
                Random.Range(-rotationAmount, rotationAmount),
                Random.Range(-rotationAmount, rotationAmount),
                Random.Range(-rotationAmount, rotationAmount)
            ), launchTime, RotateMode.FastBeyond360);
        }
    }
}
