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

        List<PathFormat> pathFormats  = new List<PathFormat>();
        for(int j = 0; j < Waves.Count; ++j)
        {
            for(int t = 0; t<Waves[j].Enemys.Count; ++t)
            {        
                PathFormat pathFormat = new PathFormat();
                pathFormat._parentWaveNum = j;        
                pathFormat._parentEnemyNum = t;
                pathFormat._path = new Serialization<Vector3>(Waves[j].Enemys[t]._path);
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
            } 
        }

        string toJsonDataEnemy = JsonUtility.ToJson(new Serialization<EnemyFormat>(enemyFormats), true);
        return toJsonDataEnemy;
    }
    static List<MapToolWaveManager.Wave> LoadEnemy(List<EnemyFormat> enemyFormats,List<PathFormat> pathFormats)
    {
        List<MapToolWaveManager.Wave> LoadWave = new List<MapToolWaveManager.Wave>();
        MapToolWaveManager.Wave tempWave = new MapToolWaveManager.Wave();
        Enemy tempEnemy = new Enemy();
        for (int EnemyNum = 0; EnemyNum < enemyFormats.Count; ++EnemyNum)
        {
            bool isHasParentWave = false;
            for (int WaveNum = 0; WaveNum < LoadWave.Count; ++WaveNum)
            {
                if (LoadWave[WaveNum].waveNum == enemyFormats[EnemyNum]._parentWaveNum)
                {
                    //만약 내 부모가 있다면 여기서 enemy 를 만들어서 add 만 해주고 return
                    isHasParentWave = true;
                    tempEnemy.HP = enemyFormats[EnemyNum]._HP;
                    tempEnemy._enemyType = (EnemyType) enemyFormats[EnemyNum]._enemyType;
                    tempEnemy._attackPattern = (AttackPattern) enemyFormats[EnemyNum]._attackPattern;
                    tempEnemy._moveSpeed = enemyFormats[EnemyNum]._moveSpeed;
                    tempEnemy._rotateSpeed = enemyFormats[EnemyNum]._rotateSpeed;
                    tempEnemy._bulletShootSpeed = enemyFormats[EnemyNum]._bulletShootSpeed;
                    tempEnemy._bulletMoveSpeed = enemyFormats[EnemyNum]._bulletMoveSpeed;

                    for (int t = 0; t < pathFormats.Count; ++t)
                    {

                        if (pathFormats[t]._parentWaveNum == WaveNum)
                        {
                            if (pathFormats[t]._parentEnemyNum == EnemyNum)
                            {
                                tempEnemy._path = new List<Vector3>();
                                tempEnemy._path.AddRange(pathFormats[t]._path.ToList());
                            }
                        }
                    }
                    LoadWave[WaveNum].Enemys.Add(tempEnemy);
                }
            }
            if(LoadWave.Count == 0 || isHasParentWave == false)
            {
                tempWave.waveNum = enemyFormats[EnemyNum]._parentWaveNum;
                tempWave.Enemys = new List<Enemy>();
                    
                tempEnemy.HP = enemyFormats[EnemyNum]._HP;
                tempEnemy._enemyType = (EnemyType) enemyFormats[EnemyNum]._enemyType;
                tempEnemy._attackPattern = (AttackPattern) enemyFormats[EnemyNum]._attackPattern;
                tempEnemy._moveSpeed = enemyFormats[EnemyNum]._moveSpeed;
                tempEnemy._rotateSpeed = enemyFormats[EnemyNum]._rotateSpeed;
                tempEnemy._bulletShootSpeed = enemyFormats[EnemyNum]._bulletShootSpeed;
                tempEnemy._bulletMoveSpeed = enemyFormats[EnemyNum]._bulletMoveSpeed;
                for (int t = 0; t < pathFormats.Count; ++t)
                {

                    if (pathFormats[t]._parentEnemyNum == EnemyNum)
                    {
                        tempEnemy._path = new List<Vector3>();
                        for (int pathNum = 0; pathNum < pathFormats[t]._path.ToList().Count; ++pathNum)
                        {
                            tempEnemy._path.Add(pathFormats[t]._path.ToList()[pathNum]);
                        }
                    }
                    
                }
                tempWave.Enemys.Add(tempEnemy);
                LoadWave.Add(tempWave);
            }
        }

        return LoadWave;
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

    public static List<MapToolWaveManager.Wave> Load()
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
        newWave.AddRange(LoadEnemy(enemyFormats,pathFormats));
        for (int i = 0; i < newWave[0].Enemys[0]._path.Count; ++i)
        {
            Debug.Log("0 번쨰 Path"+newWave[0].Enemys[0]._path[i]);
        }
        for (int i = 0; i < newWave[1].Enemys[0]._path.Count; ++i)
        {
            Debug.Log("0 번쨰 Path"+newWave[0].Enemys[1]._path[i]);
        }
        for (int i = 0; i < newWave[2].Enemys[0]._path.Count; ++i)
        {
            Debug.Log("0 번쨰 Path"+newWave[0].Enemys[2]._path[i]);
        }
        return newWave;
    }
}

