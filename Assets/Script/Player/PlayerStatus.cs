using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
public struct Turretinfo
{
    //xml 로부터 읽어오는 db
    public AttackPattern _attackPattern;
    public string _name;
    public string _info;

    //유저가 가지고있는 정보
    public int _level;
    public int _speed;
    public int _damage;
    //이름에 따라 가져오는 부분
    public Sprite _sprite;
}
public class PlayerStatus : MonoBehaviour
{
    
    public static int _HP = 20;
    public static int _ComboCount = 1;
    public static int _ScoreCount = 0;
    public static int _BestScore = 0;
    public static int _MaxCombo = 1;
    
    public static bool isCoroutineStart = false;
    public static bool isGameEnd = false;

    
    private static Text _scoreTextUI;
    private static Text _comboTextUI;
    public static List<Turretinfo> _turretinfos = new List<Turretinfo>();
    public static List<int> turretPosTypes = new List<int>();
    
    void Awake()
    {        
        DontDestroyOnLoad(gameObject);
    }

    public static void GetScorePannel()
    {
        _scoreTextUI = GameObject.Find("ScoreText").GetComponent<Text>();
        _comboTextUI = GameObject.Find("ComboText").GetComponent<Text>();
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
