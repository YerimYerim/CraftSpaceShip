using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus instance;
    
    public static int _HP = 20;
    public static int _ComboCount = 1;
    public static int _ScoreCount = 0;
    public static bool isCoroutineStart = false;
    public static bool isGameEnd = false;
    
    private static Text _scoreTextUI;
    private static Text _comboTextUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _scoreTextUI = GameObject.Find("ScoreText").GetComponent<Text>();
        _comboTextUI = GameObject.Find("ComboText").GetComponent<Text>();
        ResetCombo();
        ResetScore();
    }
    
    public static void AddScore(EnemyType type)
    {
        _ScoreCount = _ComboCount * ((int) type + 1);
        SetScoreUI();
    }

    public static void AddCombo()
    {
        ++_ComboCount;
        SetComboUI();
    }

    public static void ResetCombo()
    {
        _ComboCount = 1;
        SetComboUI();
    }

    public static void HitPlayer()
    {
        --_HP;
        ResetCombo();
    }
    public static void ResetScore()
    {
        _ScoreCount = 0;
        SetScoreUI();
    }
    
    private static void SetScoreUI()
    {
        _scoreTextUI.text = _ScoreCount.ToString();
    }

    private static void SetComboUI()
    {
        _comboTextUI.text = _ComboCount.ToString();
    }
}
