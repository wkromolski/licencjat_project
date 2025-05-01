using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairAnomaly : MonoBehaviour
{
    [SerializeField] private Transform objectToMove;
    [SerializeField] private Vector3 anomalyPosition;
    [SerializeField] private Vector3 anomalyEulerAngles;

    private Vector3 _originalPosition;
    private Quaternion _originalRotation;
    private bool _hasApplied;

    private void Awake()
    {
        if (objectToMove != null)
        {
            _originalPosition = objectToMove.position;
            _originalRotation = objectToMove.rotation;
        }
    }

    private void OnEnable()
    {
        if (objectToMove == null) return;
        objectToMove.position = anomalyPosition;
        objectToMove.rotation = Quaternion.Euler(anomalyEulerAngles);
        _hasApplied = true;
    }
}
