using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour, IBonus
{
    private Player _player;
    private AudioSource _audioSource;
    private Renderer _coin;
    public void Bonus()
    {
        _audioSource.Play();
        _player.TakeScoreCoin(1);
        _coin.enabled = false;
        Destroy(gameObject, 1f);
    }

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _audioSource = GetComponent<AudioSource>();
        _coin = GetComponentInChildren<Renderer>();
    }

}
