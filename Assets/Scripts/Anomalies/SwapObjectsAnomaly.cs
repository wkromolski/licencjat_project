using UnityEngine;
using System.Collections.Generic;

public class SwapObjectsAnomaly : MonoBehaviour
{
    [Header("Light")]
    [SerializeField] private Transform objectA;  
    [SerializeField] private Transform objectB;

    [Header("Dark")]
    [SerializeField] private Transform objectC; 
    [SerializeField] private Transform objectD;
    
    private Transform t1;
    private Transform t2; 
    private Vector3   pos1, pos2; 
    private Quaternion rot1, rot2; 
    private bool hasSwapped = false;

    private void OnEnable()
    {
            SetupSwap(objectA, objectB);
            SetupSwap(objectC, objectD);
    }

    private void OnDisable()
    {
        if (hasSwapped && t1 != null && t2 != null)
        {
            t1.position = pos1;  t1.rotation = rot1;
            t2.position = pos2;  t2.rotation = rot2;
        }
        hasSwapped = false;
    }
    private void SetupSwap(Transform first, Transform second)
    {
        t1 = first;  t2 = second;
        pos1 = t1.position;  rot1 = t1.rotation;
        pos2 = t2.position;  rot2 = t2.rotation;
        
        Vector3 p = t1.position;           Quaternion r = t1.rotation;
        t1.position = t2.position;         t1.rotation = t2.rotation;
        t2.position = p;                   t2.rotation = r;

        hasSwapped = true;
    }
}
