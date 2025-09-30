using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Animator _player;
    [SerializeField] private GameObject _panel;
    
    public void LoadGame(int option)
    {
        Stats.Option = option;
        _player.SetTrigger("Yes");
        Stats.ResetAllStats();
        Invoke("FirstLevelLoad", 2f);
    }

    public void FirstLevelLoad()
    {
        _panel.SetActive(true);
    }
}
