using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public static class WaveSaveLoad
{
    public static string FilePath = "Wave.json";
    public static List<MapToolWaveManager.Wave> Waves;

    struct WaveFormat
    {
        public int Num;
        public int EnemyCount;
    }
    struct EnemyFormat
    {
        public int _parentWaveNum;
        //
        public int _HP;
        //Type
        public int _enemyType;
        public int _attackPattern;
        //Speed
        public float _moveSpeed;
        public float _rotateSpeed;
        //bullet
        public float _bulletShootSpeed;
        public float _bulletMoveSpeed;
        //path
        public int _pathCount;
    }
    struct PathFormat
    {
        public int _parentWaveNum;
        public int _parentEnemyNum;
        public Serialization<Vector3> _path;
    }
    static string saveVector(int WaveNum, int Enemynum , int vectorNum)
    {
        PathFormat pathFormat = new PathFormat();
        pathFormat._parentWaveNum = WaveNum;
        pathFormat._parentEnemyNum = Enemynum;
        pathFormat._path = new Serialization<Vector3>(Waves[WaveNum].Enemys[Enemynum]._path);
        
        string ToJsonDataVector = JsonUtility.ToJson(pathFormat,true);
        return ToJsonDataVector;
    }
    static string saveEnemy(int WaveNum, int Enemynum)
    {
        EnemyFormat enemyFormat = new EnemyFormat();
        enemyFormat._parentWaveNum = WaveNum;
        enemyFormat._HP = Waves[WaveNum].Enemys[Enemynum].HP;
        enemyFormat._enemyType = (int)Waves[WaveNum].Enemys[Enemynum]._enemyType;
        enemyFormat._attackPattern = (int)Waves[WaveNum].Enemys[Enemynum]._attackPattern;
        enemyFormat._moveSpeed = Waves[WaveNum].Enemys[Enemynum]._moveSpeed;
        enemyFormat._rotateSpeed = Waves[WaveNum].Enemys[Enemynum]._rotateSpeed;
        enemyFormat._bulletShootSpeed = Waves[WaveNum].Enemys[Enemynum]._bulletShootSpeed;
        enemyFormat._bulletMoveSpeed = Waves[WaveNum].Enemys[Enemynum]._bulletMoveSpeed;
        enemyFormat._pathCount = Waves[WaveNum].Enemys[Enemynum]._path.Count;

        string ToJasonDataVector = JsonUtility.ToJson(enemyFormat, true);
        return ToJasonDataVector;
    }

    static string saveWave(int waveNum)
    {
        WaveFormat waveFormat = new WaveFormat();
        waveFormat.Num = waveNum;
        waveFormat.EnemyCount = Waves[waveNum].Enemys.Count;
        string ToJasonDataVector = JsonUtility.ToJson(waveFormat, true);
        return ToJasonDataVector;
    }
    
    public static void SaveAll()
    {
        //save SaveWaveFirst
        string ToJsonDataWAVE  = ""; 
        string ToJsonDataENEMY  = ""; 
        string ToJsonDataPATH  = ""; 
        
        string filePathWAVE = Application.streamingAssetsPath+"WAVE" + FilePath;
        string filePathENEMY = Application.streamingAssetsPath+ "ENEMY" + FilePath ;
        string filePathPATH = Application.streamingAssetsPath + "PATH" + FilePath;

        for (int i = 0; i <Waves.Count; ++i)
        {
            ToJsonDataWAVE += saveWave(i);
            for (int j = 0; j <Waves[i].Enemys.Count; ++j)
            {
                ToJsonDataENEMY += saveEnemy(i, j);
                for (int t = 0; t < Waves[i].Enemys[j]._path.Count; ++t)
                {
                    ToJsonDataPATH += saveVector(i,j , t);
                }
            }    
        }
        
        Debug.Log(ToJsonDataWAVE);
        
        File.WriteAllText(filePathWAVE, ToJsonDataWAVE);
        File.WriteAllText(filePathENEMY, ToJsonDataENEMY);
        File.WriteAllText(filePathPATH, ToJsonDataPATH);

    }
    public static void LoadGameData() { 
        string filePath = Application.persistentDataPath + FilePath;
        if (File.Exists(filePath))
        {
            Debug.Log("불러오기 성공"); 
            string FromJsonData = File.ReadAllText(filePath); 
            WaveFormat waveFormat = JsonUtility.FromJson<WaveFormat>(FromJsonData);
            Debug.Log(FromJsonData);
        } 
        
        
        
        
        // 저장된 게임이 없다면
        // else
        // {
        //     print("새로운 파일 생성");
        //     _gameData = new GameData();
        // }
        //
        // // 저장된 게임이 있다면
        // //
        // WaveFormat waveFormat = new WaveFormat();
        // EnemyFormat enemyFormat = new EnemyFormat();
        // PathFormat pathFormat = new PathFormat();
        // for (int i = 0; i <Waves.Count; ++i)
        // {
        //     JsonToData += saveWave(i);
        //     for (int j = 0; j <Waves[i].Enemys.Count; ++j)
        //     {
        //         JsonToData += saveEnemy(i, j);
        //         for (int t = 0; t < Waves[i].Enemys[j]._path.Count; ++t)
        //         {
        //             JsonToData += saveVector(i,j , t);
        //         }
        //     }    
        // }
        
    }
    
}

