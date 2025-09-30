using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    [SerializeField] private float _distanceToPlayer = 0.1f;
    [SerializeField] private AudioClip _atack;
    private Transform _target;
    private AudioSource _audioSource;
    private Animator _animator;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _target = FindObjectOfType<Player>().transform;
        _animator = GetComponent<Animator>();
        int numBear = FindObjectsOfType<Bear>().Length;
        if (numBear > 1)
            Destroy(gameObject);
    }
    private void Update()
    {
        if (_target != null)
        {
            SetAngle(_target.position);
            if (Vector3.Distance(transform.position, _target.position) < _distanceToPlayer)
            {
                _animator.SetTrigger("Atack");
                _audioSource.PlayOneShot(_atack);
                _target = null;
            }
        }
        
    }
    protected void SetAngle(Vector3 target)
    {
        transform.LookAt(_target);
    }
}
