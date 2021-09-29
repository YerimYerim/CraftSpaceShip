using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class XMLReader 
{

    public static void LoadXMLTurretTable(string filepath, out List <Turretinfo> turret)
    {
        //데이터를 형성할 문서 생성 및 파일읽기

        XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
        turret = new List<Turretinfo>();
        //루트 설정
        XmlElement nodes = doc["root"];

        //루트에서 요소 꺼내기
        foreach (XmlElement node in nodes.ChildNodes)
        {
            Turretinfo turretinfo = new Turretinfo();

            turretinfo._attackPattern = GetStringtoBulletType(node.GetAttribute("_attackPattern"));
            turretinfo._name = node.GetAttribute("_name");
            turretinfo._info = node.GetAttribute("_info");
            
            turret.Add(turretinfo);
        }
    }

    public static XmlDocument SaveXMLWave(List<MapToolWaveManager.Wave> waves)
    {
        XmlDocument doc = new XmlDocument();
        return doc;
    }
    
    public static AttackPattern GetStringtoBulletType(string attackPattern)
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
