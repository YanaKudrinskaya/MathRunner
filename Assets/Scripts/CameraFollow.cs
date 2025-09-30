using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform _player;
    private Vector3 _offset;

    private void Awake()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    private void Start()
    {
        _offset = transform.position - _player.position;
    }

    private void Update()
    {
        Vector3 targetPosition = _player.position + _offset;
        targetPosition.x = 15;
        transform.position = targetPosition;
    }
}
