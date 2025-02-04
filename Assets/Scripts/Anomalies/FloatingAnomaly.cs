using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FloatingAnomaly : MonoBehaviour
{
    public GameObject[] floatingObjects;
    [SerializeField] private float _floatingSpeed = 2f;
    [SerializeField] private float _floatingStrength = 0.5f;
    void Start()
    {
        foreach (GameObject obj in floatingObjects)
        {
            if (obj != null)
            {
                AnimateFloatingObject(obj);
            }
        }
    }

    private void AnimateFloatingObject(GameObject obj)
    {
        float randomDelay = Random.Range(0f, _floatingSpeed);
        
        obj.transform
            .DOMoveY(obj.transform.position.y + Random.Range(0.1f, _floatingStrength), _floatingSpeed)
            .SetEase(Ease.InOutSine) 
            .SetLoops(-1, LoopType.Yoyo) 
            .SetDelay(randomDelay);
    }
}
