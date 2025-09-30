using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour, IBonus
{
    private Player _player;
    private AudioSource _audioSource;

    private bool _isActived = true;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void Bonus()
    {
        if (_isActived)
        {
            _audioSource.Play();
            _player.TakeDamage();
            _isActived = false;
        }
    }
}
