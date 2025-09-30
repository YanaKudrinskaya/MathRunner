using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Health _heartTemplate;
    private List<Health> _hearts = new List<Health>();

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }


    private void OnHealthChanged(int value)
    {
        if (_hearts.Count < value)
        {
            int createHealth = value - _hearts.Count;
            for (int i = 0; i < createHealth; i++)
            {
                CreateHeart();
            }
        }
        else if (_hearts.Count > value)
        {
            int deleteHealth = _hearts.Count - value;
            for (int i = 0; i < deleteHealth; i++)
            {
                DestroyHeart(_hearts[_hearts.Count - 1]);
            }
        }
    }
    private void DestroyHeart(Health heart)
    {
        _hearts.Remove(heart);
        heart.ToEmpty();
    }

    private void CreateHeart()
    {
        Health newHeart = Instantiate(_heartTemplate, transform);
        _hearts.Add(newHeart.GetComponent<Health>());
        newHeart.ToFill();
    }
}
