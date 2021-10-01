using System.Collections;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]private int _waveNum = 0;
    private int DeadEnemy = 1;
    private int cycleoffset = 1;
    [SerializeField]private List<GameObject> _enemysList;
    [SerializeField] private List<GameObject> _enemyPrefabs;
    private List<MapToolWaveManager.Wave> _waves;
    private Coroutine _coroutine;
    private Vector3 _startPosition;
    int nowEnemyCount = 0;
    int nowWaveCount = 0;
    [SerializeField] private Coroutine _enemyTimer;
    void Awake()
    {
        _waves = new List<MapToolWaveManager.Wave>();
        
        _waves = WaveSaveLoad.Load();
        
        _enemysList = new List<GameObject>();
        _startPosition = gameObject.transform.position;
        _waveNum = _waves.Count;
        for (int WaveCount = 0; WaveCount < _waveNum; ++WaveCount)
        {
            for (int EnemyCount = 0; EnemyCount < _waves[WaveCount].Enemys.Count; ++EnemyCount)
            {
                initEnemy(WaveCount, EnemyCount);
            }
            
        }


        _enemyTimer = StartCoroutine(EnemyTimer());
    }

    void initEnemy(int waveCount, int enemyCount)
    {
        _enemysList.Add(Instantiate(_enemyPrefabs[(int) _waves[waveCount].Enemys[enemyCount]._enemyType], transform));
        _enemysList[_enemysList.Count - 1].transform.position = _startPosition;
        _enemysList[_enemysList.Count - 1].transform.GetChild(2).GetComponent<Enemy>()._path = _waves[waveCount].Enemys[enemyCount]._path;
        _enemysList[_enemysList.Count - 1].transform.GetChild(2).GetComponent<Enemy>()._attackPattern = _waves[waveCount].Enemys[enemyCount]._attackPattern;
        _enemysList[_enemysList.Count - 1].transform.GetChild(2).GetComponent<Enemy>()._enemyType = _waves[waveCount].Enemys[enemyCount]._enemyType;
        _enemysList[_enemysList.Count - 1].transform.GetChild(2).GetComponent<Enemy>()._moveSpeed = _waves[waveCount].Enemys[enemyCount]._moveSpeed;
        _enemysList[_enemysList.Count - 1].transform.GetChild(2).GetComponent<Enemy>()._rotateSpeed = _waves[waveCount].Enemys[enemyCount]._rotateSpeed;
        _enemysList[_enemysList.Count - 1].transform.GetChild(2).GetComponent<Enemy>()._pathNum = _waves[waveCount].Enemys[enemyCount]._path.Count - 1;
        _enemysList[_enemysList.Count - 1].transform.GetChild(2).GetComponent<Enemy>().HP = _waves[waveCount].Enemys[enemyCount].HP;
        _enemysList[_enemysList.Count - 1].SetActive(false);
    }
    void resetEnemy(int waveCount, int enemyCount)
    {
        _enemysList[enemyCount ].transform.position = _startPosition;
        _enemysList[enemyCount ].transform.GetChild(2).GetComponent<Enemy>().HP = _waves[waveCount].Enemys[enemyCount].HP * cycleoffset * 2;
        _enemysList[enemyCount ].gameObject.SetActive(false);
        _enemysList[enemyCount].transform.GetChild(2).GetComponent<Enemy>().isStart = false;
        _enemysList[enemyCount ].transform.GetChild(0).gameObject.SetActive(false);
        _enemysList[enemyCount ].transform.GetChild(2).gameObject.SetActive(true);

    }
    void Update()
    {
        
        if (PlayerStatus.isGameEnd == false)
        {
            if (nowWaveCount == _waveNum)
            {
                PlayerStatus.isGameEnd = true;
            }
        }
        else if(PlayerStatus.isGameEnd  == true)
        {
            _enemyTimer = null;
        }

    }
    

    private void StartNextLevel()
    {
        for (int i = 0; i < _waveNum; ++i)
        {
            for (int j = 0; j < _waves[i].Enemys.Count; ++j)
            {
                resetEnemy(i, j);
            }
        }
    }

    private IEnumerator EnemyTimer()
    {
        while (nowEnemyCount <= _enemysList.Count -1)
        {
            for (int i = 0; i < _waves[nowWaveCount].Enemys.Count; ++i)
            {
                _enemysList[i + nowEnemyCount].transform.gameObject.SetActive(true);
                _enemysList[i + nowEnemyCount].transform.GetChild(2).gameObject.SetActive(true);
                _enemysList[i + nowEnemyCount].transform.GetChild(2).GetComponent<Enemy>().isStart = true;
                ++nowEnemyCount;
            }
            nowWaveCount += 1;
            yield return new WaitForSeconds(10);    
        }
        
    }

}
