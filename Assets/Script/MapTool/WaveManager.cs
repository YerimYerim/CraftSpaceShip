using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    struct Wave
    {
        public int waveNum;
        public List<Enemy> Enemys;
    }

    [Header("Prefab")] 
    [SerializeField] private GameObject EnemyModelPrefab;
    [SerializeField] private GameObject EnemyListPrefab;
    [SerializeField] private GameObject WaveListPrefab;

    [Header("WaveNum Count")] 
    [SerializeField] private int nowWaveNum = -1;
    [SerializeField] private int nowMaxWaveNum = -1;

    [Header("Wave add Button")] [SerializeField]
    private Button addWaveButton;

    [SerializeField] private Button deleteWaveButton;

    [Header("EnemySelect Button")] 
    [SerializeField] private Button waveSelectButton;
    [SerializeField] private Button bossSelectButton;

    [Header("Enemytype setting")] [SerializeField]
    private Button miniSelectButton;

    [SerializeField] private Button middleSelectButton;
    [SerializeField] private Button bigSelectButton;

    [Header("Bullet Pattern")] [SerializeField]
    private Button onePatternSelectButton;

    [SerializeField] private Button threePatternSelectButton;
    [SerializeField] private Button eightPatternSelectButton;

    [Header("Movement Setting")] 
    [SerializeField] private Button addPointButton;

    [SerializeField] private Button deletePointButton;
    [SerializeField] private Text xInput;
    [SerializeField] private Text yInput;
    [SerializeField] private Text moveSpeedInput;

    [Header("Movement Setting")] [SerializeField]
    private Button leftTurnButton;

    [SerializeField] private Button rightTurnButton;
    [SerializeField] private Text rotationSpeedInput;

    [Header("Hp Setting")] [SerializeField]
    private Text HpInput;

    [Header("Enemy add and Delete buttons")] 
    [SerializeField] private Button addEnemyButton;

    [SerializeField] private Button deleteEnemyButton;
    [SerializeField] private Button resetEnemyButton;


    [Header("Wave and Object Buttons List")] 
    [SerializeField] private List<GameObject> WaveButton;
    [SerializeField] private Transform  waveListParent;
    [SerializeField] private List<Wave> Waves;
    

    [Header("Enemy List")] 
    [SerializeField] private List<GameObject> enemyListObject;
    [SerializeField] private Transform enemyListParent;
    [SerializeField]private int nowEnemyNum = -1;

    void Awake()
    {
        WaveButton = new List<GameObject>();
        Waves = new List<Wave>();
        enemyListObject = new List<GameObject>();
        if (addWaveButton == null)
        {
            addWaveButton = GameObject.Find("AddWaveButton").GetComponent<Button>();
            waveListParent = GameObject.Find("WavesView").transform.GetChild(0).GetChild(0);
            addWaveButton.onClick.AddListener(OnClickAddWaveButton);
        }
        
        if (addEnemyButton == null )
        {
            addEnemyButton = GameObject.Find("EnemySaveButton").GetComponent<Button>();
            enemyListParent = GameObject.Find("ObjectsList").transform.GetChild(0).GetChild(0);
            addEnemyButton.onClick.AddListener(OnClickEnemyAddButton);
        }

        if (miniSelectButton == null)
        {
            miniSelectButton = GameObject.Find("MiniSelectButton").GetComponent<Button>();
            miniSelectButton.onClick.AddListener(OnClickMiniButton);
        }        
        if (middleSelectButton == null)
        {
            middleSelectButton = GameObject.Find("MiddleSelectButton").GetComponent<Button>();

        }
        if (bigSelectButton == null)
        {
            bigSelectButton = GameObject.Find("BigSelectButton").GetComponent<Button>();
        }
    }

    private void OnClickMiniButton()
    {
        
    }

    void OnClickAddWaveButton()
    {
        GameObject TempWavePrefab = Instantiate(WaveListPrefab, waveListParent);
        Wave TempWave = new Wave {waveNum = nowMaxWaveNum, Enemys = new List<Enemy>()};
        TempWavePrefab.transform.GetChild(1).GetComponent<Text>().text = nowMaxWaveNum.ToString();
        
        int tempMyNum = nowMaxWaveNum;
        TempWavePrefab.GetComponent<Button>().onClick.AddListener(delegate { OnClickWaveButton(tempMyNum); });
        
        Waves.Add(TempWave);
        WaveButton.Add(TempWavePrefab);
        
        nowWaveNum = nowMaxWaveNum;
        ++nowMaxWaveNum;
    }
    void OnClickEnemyAddButton()
    {
        if (nowWaveNum != -1)
        { 
            GameObject TempEnemyPrefab = Instantiate(EnemyListPrefab,enemyListParent);
            Enemy tempEnemy = new Enemy();
            nowEnemyNum = Waves[nowWaveNum].Enemys.Count;

            int tempNum = nowEnemyNum;
            SetEnemyObjectPrefab(TempEnemyPrefab , tempEnemy , tempNum);
            Waves[nowWaveNum].Enemys.Add(tempEnemy);
        }
    }
    private void SetEnemyObjectPrefab(GameObject prefab , Enemy tempEnemy , int Enemynum)
    {
        int tempEnemyNum = Enemynum;
        prefab.GetComponent<Button>().onClick.AddListener(delegate { OnClickSelectEnemyButton(tempEnemyNum); });
        prefab.transform.GetChild(0).GetComponent<Text>().text = Waves[nowWaveNum].Enemys.Count.ToString();
        prefab.transform.GetChild(1).GetComponent<Text>().text = tempEnemy.GetEnemyTypeString();
        prefab.transform.GetChild(2).GetComponent<Text>().text = tempEnemy.GetBulletTypeString();
        enemyListObject.Add(prefab);
    }
    void OnClickWaveButton(int Mynum)
    {
        nowWaveNum = Mynum;

        for (int i = 0; i < enemyListParent.childCount; ++i)
        {
            Destroy(enemyListParent.GetChild(i).gameObject);
        }
    }
    void OnClickSelectEnemyButton(int Mynum)
    {
        nowEnemyNum = Mynum;
        print(nowEnemyNum);
    }
}
