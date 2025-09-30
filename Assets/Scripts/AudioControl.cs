using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioControl : MonoBehaviour
{
    [SerializeField] private Sprite _soundEnabledSprite, _soundDisabledSprite;
    [SerializeField] private Image _buttonImage;

    private bool _soundEnabled = true;

    public void SoundOff()
    {
        if (_soundEnabled)
        {
            AudioListener.volume = 0;
            _buttonImage.sprite = _soundDisabledSprite;
        }
        else {
            AudioListener.volume = 1;
            _buttonImage.sprite = _soundEnabledSprite;
        }
        _soundEnabled = !_soundEnabled;
    }
}
