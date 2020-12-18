using System;
using UnityEngine;

public class PositionCorrector : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Transform _targetTransform;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        var pos = _transform.position;
        if (_targetTransform.position.x > 0)
        {
            pos.x = _offset.x;
        }
        else
        {
            pos.x = -_offset.x;
        }
        
        if (_targetTransform.position.z > 0)
        {
            pos.z = _offset.z;
        }
        else
        {
            pos.z = -_offset.z;
        }

        var correctPos = Vector3.Lerp(transform.position, pos, .05f);
        _transform.position = correctPos;
    }
}
