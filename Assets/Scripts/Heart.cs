using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour, IBonus
{
    private Player _player;
    private AudioSource _audioSource;
    private Renderer _heart;

    public void Bonus()
    {
        _audioSource.Play();
        _player.TakeHeart();
        _heart.enabled = false;
        Destroy(gameObject, 1f);
    }

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _audioSource = GetComponent<AudioSource>();
        _heart = GetComponentInChildren<Renderer>();
        int numHeart = FindObjectsOfType<Heart>().Length;
        if (numHeart > 1)
            Destroy(gameObject);
    }
}

