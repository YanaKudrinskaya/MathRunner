using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed, _jumpSpeed, _horizontalSpeed;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] AudioClip _jump;


    private InputSystem _controls;
    private Rigidbody _rb;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _direction = 0, _runSpeed, _firstPosition;

    private bool _canMove = true;
    private Vector3 _move = Vector3.zero;
    private int _line = 1, _targetLine = 1, _step = 4;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
        _firstPosition = transform.position.x;
        _runSpeed = _speed;
        _controls = new InputSystem();
    }

    private void OnEnable()
    {
        _controls.Land.Jump.performed += ctx => Jump();
        _controls.Land.Move.performed += ctx =>
        {
            _direction = ctx.ReadValue<float>();
        };
        _controls.Enable();
    }

    private void Update()
    {
        Vector3 forwardMovement = transform.forward * _runSpeed * Time.deltaTime;
        Vector3 pos = _rb.position;
        if(!_line.Equals(_targetLine))
        {
            if(_targetLine == 0 && pos.x < _firstPosition - _step)
            {
                _rb.MovePosition(new Vector3(_firstPosition - _step, pos.y, pos.z));
                StopHorizontalMove();
            }
            else if (_targetLine == 1 && pos.x != _firstPosition)
            {
                if((_line == 0 && pos.x > _firstPosition) || (_line == 2 && _rb.position.x < _firstPosition))
                {
                    _rb.MovePosition(new Vector3(_firstPosition, pos.y, pos.z));
                    StopHorizontalMove();
                }
            }
            else if(_targetLine == 2 && pos.x > _firstPosition + _step)
            {
                _rb.MovePosition(new Vector3(_firstPosition + _step, pos.y, pos.z));
                StopHorizontalMove();
            }
        }
        CheckInputs();
        Vector3 horizontalMovement = _move * _horizontalSpeed * Time.deltaTime;
        Vector3 newPosition = _rb.position + forwardMovement + horizontalMovement;
        _rb.MovePosition(newPosition);
    }

    private void StopHorizontalMove()
    {
        _line = _targetLine;
        _canMove = true;
        _move.x = 0;
    }
    private void Jump()
    {
        if (IsGrounded())
        {
            _animator.SetTrigger("Jump");
            _audioSource.PlayOneShot(_jump);
            _rb.AddForce(Vector3.up * _jumpSpeed, ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(transform.position, 0.2f, _groundMask);
    }
    public float GetSpeed()
    {
        return _runSpeed;
    }

    public void SetSpeed(float num)
    {
        _runSpeed += num;
    }

    public void ReturnSpeed()
    {
        Invoke("FirstSpeed", 1f); 
    }

    private void FirstSpeed()
    {
        _runSpeed = _speed;
    }

    private void CheckInputs()
    {
        if(_canMove)
        {
            if((_direction == -1 && _line > 0) || (_direction == 1 && _line < 2))
            {
                _targetLine += (int)_direction;
                _canMove = false;
                _move.x = _step * _direction;
                _direction = 0;
            }
        }
    }

    private void OnDisable()
    {
        _controls.Disable();
    }

}
