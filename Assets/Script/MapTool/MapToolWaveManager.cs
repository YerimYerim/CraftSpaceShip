using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

[Serializable]
public class Serialization<T>
{
    [SerializeField] List<T> _list;

    public List<T> ToList()
    {
        return _list;
    }

    public Serialization(List<T> target)
    {
        this._list = target;
    }
};
[Serializable]
public class MapToolWaveManager : MonoBehaviour
{
    [Serializable]
    public class Wave
    {
        public int waveNum;
        public List<Enemy> Enemys;
    }

    [Header("Prefab")] 
    [SerializeField] private GameObject EnemyModelPrefab;
    [SerializeField] private GameObject EnemyListPrefab;
    [SerializeField] private GameObject WaveListPrefab;
    [SerializeField] private GameObject PointPrefab;
    [Header("WaveNum Count")] [SerializeField]
    private int nowWaveNum = -1;

    [SerializeField] private int nowMaxWaveNum = -1;

    [Header("Wave add Button")] [SerializeField]
    private Button addWaveButton;
    [Header("EnemySelect Button")] [SerializeField]
    private Button waveSelectButton;
    [Header("Enemytype setting")] [SerializeField]
    private Button miniSelectButton;

    [SerializeField] private Button middleSelectButton;
    [SerializeField] private Button bigSelectButton;

    [Header("Bullet Pattern")] [SerializeField]
    private Button onePatternSelectButton;

    [SerializeField] private Button threePatternSelectButton;
    [SerializeField] private Button eightPatternSelectButton;

    [Header("Movement Setting")] [SerializeField]
    private Button addPointButton;

    [SerializeField] private Transform PointParent;
    
    [SerializeField] private InputField xInput;
    [SerializeField] private InputField yInput;
    [SerializeField] private InputField moveSpeedInput;
    [SerializeField] private bool isChangeVector;
    [SerializeField] private int nowSelectVector = -1;
    [SerializeField] private Vector3 tempVector3 = new Vector3(0, 0, 0);

    [Header("Movement Setting")] 
    [SerializeField] private InputField rotationSpeedInput;

    [Header("Hp Setting")] [SerializeField]
    private InputField HpInput;

    [Header("Enemy add and Delete buttons")] [SerializeField]
    private Button addEnemyButton;
    

    [Header("Wave and Object Buttons List")] [SerializeField]
    private List<GameObject> WaveButton;

    [SerializeField] private Transform waveListParent;
    [SerializeField] private List<Wave> Waves;


    [Header("Enemy List")] [SerializeField]
    private List<GameObject> enemyListObject;

    [SerializeField] private Transform enemyListParent;
    [SerializeField] private int nowEnemyNum = -1;

    private LineRenderer _lineRenderer;


    [Header("Save&Load")] 
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    void Awake()
    {
        rotationSpeedInput = GameObject.Find("RotationSpeedInput").GetComponent<InputField>();
        rotationSpeedInput.onValueChanged.AddListener(delegate(string str) { OnValueChangeRotateSpeed(str);  });
        HpInput = GameObject.Find("HpInput").GetComponent<InputField>();
        HpInput.onValueChanged.AddListener(delegate(string str) { OnValueChangeHP(str);  });
        
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = 0;
        WaveButton = new List<GameObject>();
        Waves = new List<Wave>();
        enemyListObject = new List<GameObject>();
        if (addWaveButton == null)
        {
            addWaveButton = GameObject.Find("AddWaveButton").GetComponent<Button>();
            waveListParent = GameObject.Find("WavesView").transform.GetChild(0).GetChild(0);
            nowWaveNum = 0;
            nowMaxWaveNum = 0;
            addWaveButton.onClick.AddListener(OnClickAddWaveButton);
        }

        if (addEnemyButton == null)
        {
            addEnemyButton = GameObject.Find("EnemySaveButton").GetComponent<Button>();
            enemyListParent = GameObject.Find("ObjectsList").transform.GetChild(0).GetChild(0);
            addEnemyButton.onClick.AddListener(OnClickEnemyAddButton);
        }

        if (miniSelectButton == null || middleSelectButton == null || bigSelectButton == null)
        {
            miniSelectButton = GameObject.Find("MiniSelectButton").GetComponent<Button>();
            middleSelectButton = GameObject.Find("MiddleSelectButton").GetComponent<Button>();
            bigSelectButton = GameObject.Find("BigSelectButton").GetComponent<Button>();


            miniSelectButton.onClick.AddListener(delegate { OnClickSizeButton(EnemyType.MINI); });
            middleSelectButton.onClick.AddListener(delegate { OnClickSizeButton(EnemyType.MIDDLE); });
            bigSelectButton.onClick.AddListener(delegate { OnClickSizeButton(EnemyType.BOSS); });

        }

        if (onePatternSelectButton == null && threePatternSelectButton == null && eightPatternSelectButton == null)
        {
            onePatternSelectButton = GameObject.Find("OneBulletPatternButton").GetComponent<Button>();
            threePatternSelectButton = GameObject.Find("ThreeBulletPatternButton").GetComponent<Button>();
            eightPatternSelectButton = GameObject.Find("EightBulletPatternButton").GetComponent<Button>();

            onePatternSelectButton.onClick.AddListener(delegate { OnClickAttackPatternButton(AttackPattern.ONE); });
            threePatternSelectButton.onClick.AddListener(delegate { OnClickAttackPatternButton(AttackPattern.THREE); });
            eightPatternSelectButton.onClick.AddListener(delegate { OnClickAttackPatternButton(AttackPattern.EIGHT); });

        }

        if (addPointButton == null || xInput == null || yInput == null)
        {
            addPointButton = GameObject.Find("MovePointAddButton").GetComponent<Button>();
            addPointButton.onClick.AddListener(delegate { OnClickAddPointButton(); });
            xInput = GameObject.Find("inputX").GetComponent<InputField>();
            yInput = GameObject.Find("inputY").GetComponent<InputField>();
            PointParent = GameObject.Find("Points").transform;

        }

        if (moveSpeedInput == null)
        {
            moveSpeedInput = GameObject.Find("MoveSpeedInput").GetComponent<InputField>();
            moveSpeedInput.onValueChanged.AddListener(delegate(string speed) { OnValueChangeMoveSpeed( speed); });
        }

        if (saveButton == null || loadButton == null)
        {
            saveButton = GameObject.Find("SaveButton").GetComponent<Button>();
            saveButton.onClick.AddListener(Save);
            loadButton = GameObject.Find("LoadButton").GetComponent<Button>();
            loadButton.onClick.AddListener(Load);
        }
        
        
    }
    
    private void OnValueChangeMoveSpeed(string speed)
    {
        Waves[nowWaveNum].Enemys[nowEnemyNum]._moveSpeed = float.Parse(speed);
    }

    private void OnValueChangeRotateSpeed(string speed)
    {
        Waves[nowWaveNum].Enemys[nowEnemyNum]._rotateSpeed = float.Parse(speed);
    }

    private void OnValueChangeHP(string hp)
    {
        Waves[nowWaveNum].Enemys[nowEnemyNum].HP = int.Parse(hp);
    }
    void Update()
    {
        if ( Input.GetMouseButtonDown(0) && nowEnemyNum != -1)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (ray, out hit))
            { 
                if (hit.transform.gameObject.CompareTag("GROUND"))
                {
                    tempVector3 = hit.point;
                    xInput.text = tempVector3.x.ToString();
                    yInput.text = tempVector3.z.ToString();    
                    
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Waves[nowWaveNum].Enemys[nowEnemyNum]._path[nowSelectVector] = new Vector3( float.Parse(xInput.text), -0.8f, float.Parse(yInput.text));
            print("enter");
            drawPointandLine(nowSelectVector, Waves[nowWaveNum].Enemys[nowEnemyNum]._path[nowSelectVector]);
            Destroy(PointParent.GetChild(nowSelectVector).gameObject);
        }
    }

    private void OnClickAddPointButton()
    {
        if (nowEnemyNum != -1)
        {
            tempVector3.x = float.Parse(xInput.text);
            tempVector3.y = -0.8f;
            tempVector3.z = float.Parse(yInput.text);
            int tempNum = Waves[nowWaveNum].Enemys[nowEnemyNum]._path.Count;
            
            Waves[nowWaveNum].Enemys[nowEnemyNum]._path.Add(new Vector3(tempVector3.x, -0.8f, tempVector3.z));
            drawPointandLine(tempNum , tempVector3);
        }
    }
    

    private void drawPointandLine(int Num , Vector3 point )
    {
        GameObject Point  = Instantiate(PointPrefab , PointParent);
        Point.transform.position = Camera.main.WorldToScreenPoint(point);
        Point.transform.GetChild(0).GetComponent<Text>().text = Num.ToString();
        int tempNum = Num;
        
        Point.GetComponent<Button>().onClick.AddListener(delegate { OnClickSelectPointButton(tempNum);});
        if (Num >= _lineRenderer.positionCount)
        {
            _lineRenderer.positionCount = Num + 1;
        }
        _lineRenderer.SetPosition(Num, point);
    }

    private void OnClickSelectPointButton(int myNum)
    {
        nowSelectVector = myNum;
        //print(nowSelectVector);
        xInput.text = Waves[nowWaveNum].Enemys[nowEnemyNum]._path[nowSelectVector].x.ToString();    
        yInput.text = Waves[nowWaveNum].Enemys[nowEnemyNum]._path[nowSelectVector].z.ToString();

        
    }
  private void OnClickAttackPatternButton(AttackPattern attackPattern)
    {
        if (nowEnemyNum != -1)
        {
            Waves[nowWaveNum].Enemys[nowEnemyNum]._attackPattern =  attackPattern;
            enemyListObject[nowEnemyNum].transform.GetChild(2).GetComponent<Text>().text =
                Waves[nowWaveNum].Enemys[nowEnemyNum].GetBulletTypeString();
        }
        else
        {
            print("enemy가 선택 되지 않았습니다.");
        }
    }
    private void OnClickSizeButton(EnemyType type)
    {
        if (nowEnemyNum != -1)
        {
             Waves[nowWaveNum].Enemys[nowEnemyNum]._enemyType = type;
             enemyListObject[nowEnemyNum].transform.GetChild(1).GetComponent<Text>().text =
             Waves[nowWaveNum].Enemys[nowEnemyNum].GetEnemyTypeString();
        }
        else
        {
            print("enemy가 선택 되지 않았습니다.");
        }

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

        nowEnemyNum = 0;
        print(nowWaveNum);
        nowWaveNum = nowMaxWaveNum;
        ++nowMaxWaveNum;
    }
    void OnClickEnemyAddButton()
    {
        if (nowWaveNum != -1)
        { 
            GameObject TempEnemyPrefab = Instantiate(EnemyListPrefab,enemyListParent);
            Enemy tempEnemy = new Enemy();
            tempEnemy._path = new List<Vector3>();
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
        prefab.transform.GetChild(0).GetComponent<Text>().text = tempEnemyNum.ToString();
        prefab.transform.GetChild(1).GetComponent<Text>().text = tempEnemy.GetEnemyTypeString();
        prefab.transform.GetChild(2).GetComponent<Text>().text = tempEnemy.GetBulletTypeString();
        enemyListObject.Add(prefab);
    }
    void OnClickWaveButton(int Mynum)
    {
        nowWaveNum = Mynum;

        for (int i = 0; i < enemyListParent.childCount; ++i)
        {
            enemyListObject.Clear();
            Destroy(enemyListParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < Waves[nowWaveNum].Enemys.Count; ++i)
        {
            SetEnemyObjectPrefab(Instantiate(EnemyListPrefab, enemyListParent), Waves[nowWaveNum].Enemys[i], i);
        }
        nowEnemyNum = -1;

    }

    void OnClickSelectEnemyButton(int Mynum)
    {
        nowEnemyNum = Mynum;
        for (int i = 0; i < PointParent.childCount; ++i)
        {
            Destroy(PointParent.GetChild(i).gameObject);
        }

        _lineRenderer.positionCount = 0;
        int pathCount =  Waves[nowWaveNum].Enemys[nowEnemyNum]._path.Count;
        for (int i = 0; i < pathCount; ++i)
        {
            drawPointandLine(i,Waves[nowWaveNum].Enemys[nowEnemyNum]._path[i] );
        }
    }

    void OnClickSaveButton()
    {
        Save();

    }
    void Save()
    {
        WaveSaveLoad.Waves = new List<Wave>();
        WaveSaveLoad.Waves = Waves;
        WaveSaveLoad.SaveAll();
    }
    

    void Load()
    {
        List<Wave> waves = new List<Wave>(WaveSaveLoad.Load());

        for (int waveNum = 0; waveNum < waves.Count; ++waveNum)
        {
            OnClickAddWaveButton();
            // for (int enemyNum = 0; enemyNum < waves[waveNum].Enemys.Count; ++enemyNum)
            // {
            //     OnClickEnemyAddButton();
            //     for (int pathNum = 0; pathNum < waves[waveNum].Enemys[enemyNum]._path.Count; ++pathNum)
            //     {
            //         print("path NUM"+pathNum);
            //         if (nowEnemyNum != -1)
            //         {
            //             tempVector3.x = Waves[waveNum].Enemys[enemyNum]._path[pathNum].x;
            //             tempVector3.y = -0.8f;
            //             tempVector3.z = Waves[waveNum].Enemys[enemyNum]._path[pathNum].z;
            //             
            //             Waves[waveNum].Enemys[enemyNum]._path.Add(new Vector3(tempVector3.x, -0.8f, tempVector3.z));
            //             drawPointandLine(pathNum , tempVector3);
            //         }
            //     }
            //     
            // }
        }
    }
}
    