using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;


public static class Stats
{
    public static int Level { get; set; } = 1;
    public static int Life { get; set; } = 3;
    public static int Score { get; set; } = 0;
    public static int Option { get; set; } = 0;
    public static int PreviousNumber { get; set; } = 0;
    public static int Attempts { get; set; } = 3;

    public static void ResetAllStats()
    {
        Level = 1;
        Score = 0;
        Life = 3;
        Attempts = 3;
    }
}
