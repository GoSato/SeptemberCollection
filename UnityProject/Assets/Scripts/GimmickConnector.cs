using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickConnector : MonoBehaviour
{
    [SerializeField]
    private Transform _gimmicA;
    [SerializeField]
    private Transform _gimmicB;
    [SerializeField]
    private float _rotationToPositionCoefficient;
    [SerializeField]
    private float _positionToPositionCoefficient;

    private float _beforeAngleGimmicA;
    private float _currentAngleGimmicA;

    private float _beforePosGimmicA;
    private float _currentPosGimmicA;

    private float _beforePosGimmicB;
    private float _currentPosGimmicB;

    private void Start()
    {
        _beforeAngleGimmicA = _gimmicA.eulerAngles.z;
        _beforePosGimmicA = _gimmicA.localPosition.y;
        _beforePosGimmicB = _gimmicB.localPosition.y;
    }

    private void Update()
    {
        UpdatePositionToRotation();
        UpdateRotationToPosition();
    }

    /// <summary>
    /// GimmicB : position → GimmicA : rotation
    /// </summary>
    private void UpdatePositionToRotation()
    {
        _currentPosGimmicB = _gimmicB.localPosition.y;

        var newRot = _gimmicA.eulerAngles;

        if(_currentPosGimmicB - _beforePosGimmicB == 0)
        {
            return;
        }

        newRot.x += (_currentPosGimmicB - _beforePosGimmicB) / _rotationToPositionCoefficient;

        _beforePosGimmicB = _currentPosGimmicB;

        _gimmicA.eulerAngles = newRot;
        _beforeAngleGimmicA = _gimmicA.eulerAngles.x;
    }

    /// <summary>
    /// GimmicA : rotation → GimmicB : position
    /// </summary>
    private void UpdateRotationToPosition()
    {
        _currentAngleGimmicA = _gimmicA.eulerAngles.x;

        var newPos = _gimmicB.transform.localPosition;

        if(_currentAngleGimmicA - _beforeAngleGimmicA == 0)
        {
            return;
        }

        newPos.y += Mathf.DeltaAngle(_beforeAngleGimmicA, _currentAngleGimmicA) * _rotationToPositionCoefficient;

        _beforeAngleGimmicA = _currentAngleGimmicA;

        _gimmicB.transform.localPosition = newPos;
        _beforePosGimmicB = _gimmicB.transform.localPosition.y;
    }

    /// <summary>
    /// GimmicA : position → GimmicB : position
    /// </summary>
    private void UpdatePositionToPosition()
    {
        _currentPosGimmicA = _gimmicA.localPosition.y;

        var newPos = _gimmicB.transform.localPosition;

        if(_currentPosGimmicA - _beforePosGimmicA == 0)
        {
            return;
        }

        newPos.y += _currentPosGimmicA - _beforePosGimmicA;

        _beforePosGimmicA = _currentPosGimmicA;

        _gimmicB.transform.localPosition = newPos;
        _beforePosGimmicB = _gimmicB.transform.localPosition.y;
    }
}
