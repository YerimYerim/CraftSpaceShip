using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    private int _wave = 1;
    private int nowLiveEnemy;
   [SerializeField]private List<GameObject> _enemysList;
   private List<MapToolWaveManager.Wave> _waves;

    void Awake()
    {
        _waves = new List<MapToolWaveManager.Wave>();
        _waves = WaveSaveLoad.Load();
        
        for (int i = 0; i <_waves.Count; ++i)
        {
            for (int j = 0; j < _waves[i].Enemys.Count; ++j)
            {
                print(_waves[i].Enemys[j]._moveSpeed);
            }
        }
    }
    
    
    void FixedUpdate()
    {
        for (int i = 0; i < _waves[_wave].Enemys.Count; ++i)
        {
           
        }
    }
}
