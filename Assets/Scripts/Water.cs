using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour, IBonus
{
    private Player _player;
    private AudioSource _audioSource;
    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _audioSource = GetComponent<AudioSource>();
    }
    public void Bonus()
    {
        _audioSource.Play();
        _player.TakeDamage();
    }
}
