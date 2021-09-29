using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


[Serializable]
public static class WaveSaveLoad
{
    public static string FilePath = "Wave.json";
    public static List<MapToolWaveManager.Wave> Waves;

    [Serializable]
    struct WaveFormat
    {
        [SerializeField]public int Num;
        [SerializeField]public int EnemyCount;
    }
    
    [Serializable]
    public class EnemyFormat
    {
        [SerializeField]public int _parentWaveNum;
        //
        [SerializeField]public int _HP;
        //Type
        [SerializeField]public int _enemyType;
        [SerializeField]public int _attackPattern;
        //Speed
        [SerializeField]public float _moveSpeed;
        [SerializeField]public float _rotateSpeed;
        //bullet
        [SerializeField]public float _bulletShootSpeed;
        [SerializeField]public float _bulletMoveSpeed;
        //path
        [SerializeField]public int _pathCount;
    }
    
    [Serializable]
    public class PathFormat
    {
        [SerializeField]public int _parentWaveNum;
        [SerializeField]public int _parentEnemyNum;
        [SerializeField]public Serialization<Vector3> _path;
    }
    static string saveVector()
    {
        PathFormat pathFormat = new PathFormat();
        List<PathFormat> pathFormats  = new List<PathFormat>();
        for(int j = 0; j < Waves.Count; ++j)
        {
            for(int t = 0; t<Waves[j].Enemys.Count; ++t)
            {
                pathFormat._parentWaveNum = j;        
                pathFormat._parentEnemyNum = t;
                for (int i = 0; i < Waves[j].Enemys[t]._path.Count; ++i)
                {
                    pathFormat._path = new Serialization<Vector3>(Waves[j].Enemys[t]._path);
                }
                pathFormats.Add(pathFormat);
            }
            
        }
        string ToJsonDataVector = JsonUtility.ToJson(new Serialization<PathFormat>(pathFormats),true);
        return ToJsonDataVector;
    }

    static string saveEnemy()
    {
        List<EnemyFormat> enemyFormats = new List<EnemyFormat>();
        for (int j = 0; j < Waves.Count; ++j)
        {
            for (int i = 0; i < Waves[j].Enemys.Count; ++i)
            {
                EnemyFormat enemyFormat = new EnemyFormat();
                enemyFormat._parentWaveNum = j;
                enemyFormat._HP = Waves[j].Enemys[i].HP;
                enemyFormat._enemyType = (int) Waves[j].Enemys[i]._enemyType;
                enemyFormat._attackPattern = (int) Waves[j].Enemys[i]._attackPattern;
                enemyFormat._moveSpeed = Waves[j].Enemys[i]._moveSpeed;
                enemyFormat._rotateSpeed = Waves[j].Enemys[i]._rotateSpeed;
                enemyFormat._bulletShootSpeed = Waves[j].Enemys[i]._bulletShootSpeed;
                enemyFormat._bulletMoveSpeed = Waves[j].Enemys[i]._bulletMoveSpeed;
                enemyFormat._pathCount = Waves[j].Enemys[i]._path.Count;
                enemyFormats.Add(enemyFormat);
                Debug.Log(enemyFormat._pathCount);
            } 
        }

        string toJsonDataEnemy = JsonUtility.ToJson(new Serialization<EnemyFormat>(enemyFormats), true);
        return toJsonDataEnemy;
    }
    static void LoadEnemy(List<EnemyFormat> enemyFormats,List<PathFormat> pathFormats)
    {
        for (int i = 0; i < enemyFormats.Count; ++i)
        {
            int j = enemyFormats[i]._parentWaveNum ;
            Waves[j].Enemys[i].HP = enemyFormats[i]._HP;
            Waves[j].Enemys[i]._enemyType = (EnemyType)enemyFormats[i]._enemyType;
            Waves[j].Enemys[i]._attackPattern = (AttackPattern)enemyFormats[i]._attackPattern;
            Waves[j].Enemys[i]._moveSpeed = enemyFormats[i]._moveSpeed;
            Waves[j].Enemys[i]._rotateSpeed = enemyFormats[i]._rotateSpeed;
            Waves[j].Enemys[i]._bulletShootSpeed = enemyFormats[i]._bulletShootSpeed;
            Waves[j].Enemys[i]._bulletMoveSpeed = enemyFormats[i]._bulletMoveSpeed;
            for (int t = 0; t < pathFormats.Count; ++t)
            {
                if (pathFormats[t]._parentWaveNum == j)
                {
                    if (pathFormats[t]._parentEnemyNum == i)
                    {
                        Waves[j].Enemys[i]._path.AddRange(pathFormats[t]._path.ToList());
                    }
                }
            }
        }
    }

    static void LoadWave(List<WaveFormat> waveFormats)
    {

    }
    static string saveWave()
    {
        WaveFormat waveFormat = new WaveFormat();
        List<WaveFormat> waveFormats = new List<WaveFormat>();
        for (int i = 0; i < Waves.Count; ++i)
        {
            waveFormat.Num = i;
            waveFormat.EnemyCount = Waves[i].Enemys.Count;
            waveFormats.Add(waveFormat);
        }

        string ToJasonDataWave = JsonUtility.ToJson(new Serialization<WaveFormat>(waveFormats), true);
        return ToJasonDataWave;
    }
    
    public static void SaveAll()
    {
        //save SaveWaveFirst
        string ToJsonDataWAVE  = ""; 
        string ToJsonDataENEMY  = ""; 
        string ToJsonDataPATH  = ""; 
        
        string filePathWAVE = Application.streamingAssetsPath+"WAVE" + FilePath;
        string filePathENEMY = Application.streamingAssetsPath+ "ENEMY" + FilePath;
        string filePathPATH = Application.streamingAssetsPath + "PATH" + FilePath;
        
        ToJsonDataWAVE = saveWave();
        ToJsonDataENEMY = saveEnemy();
        ToJsonDataPATH = saveVector();
        
        File.Delete(filePathWAVE);
        File.Delete(filePathENEMY);
        File.Delete(filePathPATH);
        
        File.WriteAllText(filePathWAVE, ToJsonDataWAVE);
        File.WriteAllText(filePathENEMY, ToJsonDataENEMY);
        File.WriteAllText(filePathPATH, ToJsonDataPATH);

    }
    // public static void LoadGameData() { 
    //     string filePath = Application.persistentDataPath + FilePath;
    //     if (File.Exists(filePath))
    //     {
    //         Debug.Log("불러오기 성공"); 
    //         string FromJsonData = File.ReadAllText(filePath); 
    //         WaveFormat waveFormat = JsonUtility.FromJson<WaveFormat>(FromJsonData);
    //         Debug.Log(FromJsonData);
    //     } 
    //     // 저장된 게임이 없다면
    //     // else
    //     // {
    //     //     print("새로운 파일 생성");
    //     //     _gameData = new GameData();
    //     // }
    //     //
    //     // // 저장된 게임이 있다면
    //     // //
    //     // WaveFormat waveFormat = new WaveFormat();
    //     // EnemyFormat enemyFormat = new EnemyFormat();
    //     // PathFormat pathFormat = new PathFormat();
    //     // for (int i = 0; i <Waves.Count; ++i)
    //     // {
    //     //     JsonToData += saveWave(i);
    //     //     for (int j = 0; j <Waves[i].Enemys.Count; ++j)
    //     //     {
    //     //         JsonToData += saveEnemy(i, j);
    //     //         for (int t = 0; t < Waves[i].Enemys[j]._path.Count; ++t)
    //     //         {
    //     //             JsonToData += saveVector(i,j , t);
    //     //         }
    //     //     }    
    //     // }
    //     
    // }

    public static void Load()
    {
        string ToJsonDataWAVE  = ""; 
        string ToJsonDataENEMY  = ""; 
        string ToJsonDataPATH  = ""; 
        
        string filePathWAVE = Application.streamingAssetsPath+"WAVE" + FilePath;
        string filePathENEMY = Application.streamingAssetsPath+ "ENEMY" + FilePath;
        string filePathPATH = Application.streamingAssetsPath + "PATH" + FilePath;

        string WaveText = File.ReadAllText(filePathWAVE);
        List<WaveFormat> waveFormats =JsonUtility.FromJson<Serialization<WaveFormat>>(WaveText).ToList();
        
        string EnemyText = File.ReadAllText(filePathENEMY);
        List<EnemyFormat> enemyFormats = JsonUtility.FromJson<Serialization<EnemyFormat>>(EnemyText).ToList();
        
        string PathText = File.ReadAllText(filePathPATH);
        List<PathFormat> pathFormats = JsonUtility.FromJson<Serialization<PathFormat>>(PathText).ToList();
        
        
        List<MapToolWaveManager.Wave> newWave = new List<MapToolWaveManager.Wave>();
        
        
        //Debug.Log(Enemies[0]);
    }
}

