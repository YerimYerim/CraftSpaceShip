using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEngine;

public static class XMLReader 
{

    public static void LoadXMLTurretTable(string filepath, out List <Turretinfo> turret)
    {
        //데이터를 형성할 문서 생성 및 파일읽기
        #if UNITY_EDITOR

        XmlDocument doc = new XmlDocument();
        doc.Load( Application.streamingAssetsPath + filepath);
        turret = new List<Turretinfo>();
        
 
        //루트 설정
        XmlElement nodes = doc["root"];

        //루트에서 요소 꺼내기
        foreach (XmlElement node in nodes.ChildNodes)
        {
            Turretinfo turretinfo = new Turretinfo
            {
                _attackPattern = GetStringtoBulletType(node.GetAttribute("_attackPattern")),
                _name = node.GetAttribute("_name"),
                _info = node.GetAttribute("_info")
            };


            //가져온 데이터를 리스트에 입력
            turret.Add(turretinfo);
        }
        #elif UNITY_ANDROID 
        turret = new List<Turretinfo>();
        Turretinfo turretinfo = new Turretinfo();
        turretinfo._attackPattern = AttackPattern.ONE;
        turretinfo._name          = "머신건포탑";
        turretinfo._info          = "한 방향으로 빠르게 발사하여 약한 데미지를 준다";
       
        turret.Add(turretinfo);
        turretinfo._attackPattern = AttackPattern.ONE;
        turretinfo._name          = "샷건포탑";
        turretinfo._info          = "세갈래 방향으로 준수한 속도로 발사하여 준수한 데미지를 준다";
        turret.Add(turretinfo);

        turretinfo._attackPattern = AttackPattern.ONE;
        turretinfo._name          = "다방향포탑";
        turretinfo._info          = "여러 방향으로 느리게 발사하여 큰 데미지를 준다.";
        turret.Add(turretinfo);

        #endif
    }

    public static XmlDocument SaveXMLWave(List<MapToolWaveManager.Wave> waves)
    {
        XmlDocument doc = new XmlDocument();
        return doc;
    }
    
    private static AttackPattern GetStringtoBulletType(string attackPattern)
    {
        switch (attackPattern)
        {
            case "ONE":
                return AttackPattern.ONE ;
                break;
            case "THREE":
                return AttackPattern.THREE;
                break;
            case "EIGHT":
                return AttackPattern.EIGHT;
                break;
            default: 
                return AttackPattern.ONE ;
        }
    }
    
}
