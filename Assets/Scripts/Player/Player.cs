using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public event UnityAction<int> HealthChanged;

    [SerializeField] private AudioClip _lose, _win;
    [SerializeField] private Animator _animator, _cameraAnimator;

    private UI _ui;
    private PlayerMovement _playerMove;
    private Collider _collider;
    private string _deadText, buttonText;
    private AudioSource _audioSource;
    //private InputSystem _controls; //debug

    private void Awake()
    {
        _ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UI>();
        _animator = GetComponent<Animator>();
        _playerMove = GetComponent<PlayerMovement>();
        _collider = GetComponent<Collider>();
        _audioSource = GetComponent<AudioSource>();
        //_controls = new InputSystem();//debug
    }
    /*private void OnEnable()//debug
    {
        _controls.Land.Victory.performed += ctx => DebugKeys();
        _controls.Enable();
    }*/
 
    private void Start()
    {
        HealthChanged?.Invoke(Stats.Life);
        StopMove();
        _animator.SetTrigger("Stand");
        Invoke("Run", 1.5f);
    }

    public void Run()
    {
        _animator.SetTrigger("Run");
        _playerMove.enabled = true;
    }
    public  void TakeDamage()
    {
        Stats.Life--;
        if (Stats.Life <= 0)
        {
            _deadText = "Повторим?";
            Invoke("Dead", 1f);
        }
        HealthChanged?.Invoke(Stats.Life);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IBonus>() != null || other.gameObject.GetComponent<Damage>() != null)
        {
            other.gameObject.GetComponent<IBonus>().Bonus();
        }
    }

    private void Dead()
    {
        _audioSource.PlayOneShot(_lose);
        _animator.SetTrigger("Die");
        StopMove();
        Invoke("Loose", 1.5f);
    }

    private void Loose()
    {
        _ui.UpdateLoose(_deadText);
    }

    public void TakeScore()
    {
        Stats.Score += 1 * Stats.Level;
        if (Stats.Level < 10) _playerMove.SetSpeed(0.2f);
        else _playerMove.ReturnSpeed();
        _ui.UpdateScore();
        if (Stats.Score >= 100 * Stats.Level)
        {
            if (Stats.Level < 10)
            {
                Stats.Level++;
                _audioSource.PlayOneShot(_win);
                _playerMove.ReturnSpeed();
                _animator.SetTrigger("Win");
                _ui.UpdateLevel();
            }
            else if (Stats.Level == 10 && Stats.Score == 2000)
            {
                Invoke("StopMove", 1f);
                _animator.SetTrigger("End");
                _cameraAnimator.SetTrigger("Victory");
                Invoke("Victory", 5f);
            }
        }
    }

    public void TakeScoreCoin(int score)
    {
        Stats.Score += score;
        _ui.UpdateScore();
    }

    public void TakeHeart()
    {
        Stats.Life++;
        HealthChanged?.Invoke(Stats.Life);
    }

    private void Victory()
    {
        _ui.Victory();
    }

    private void StopMove()
    {
        _playerMove.enabled = false;
    }
    //debug
    /*void DebugKeys()
    {
        Invoke("StopMove", 1f);
        _animator.SetTrigger("End");
        _cameraAnimator.SetTrigger("Victory");
        Invoke("Victory", 5f);
    }*/

    /*private void OnDisable()
    {
        _controls.Disable();
    }*/
}
