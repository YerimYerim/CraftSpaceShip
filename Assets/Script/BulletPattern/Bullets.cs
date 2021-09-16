using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : ObjectPoolManager
{
    [SerializeField]private Transform StartPoint;
    void Awake()
    {
        Application.targetFrameRate = 60;
        if (StartPoint == null)
        {
            StartPoint = transform.parent.GetChild(0);
        }
    }
    public void ShootBullet(AttackPattern attackPattern , float speed , float time)
    {
        if (StartPoint != null)
        {
          switch (attackPattern)
            {
                case AttackPattern.ONE:
                    StartCoroutine(ShootOneDirBullets(speed,initBullet()));  
                    break;
                case AttackPattern.THREE:
                {
                    float degree = -30;
                    for (int i = 0; i < 3; ++i)
                    {
                        StartCoroutine(ShootOneDirBullets(speed,initBullet(0,degree,0)));
                        degree += 30;
                    }
                } break;
                case AttackPattern.EIGHT:
                {
                    float degree = 0;
          
                    for (int i = 0; i < 8; ++i)
                    {
                        StartCoroutine(ShootOneDirBullets(speed,initBullet(0,degree,0)));
                        degree += 45;
                    }
                } break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(attackPattern), attackPattern, null);
            }
        }
       
    }

    private GameObject initBullet(float x, float y, float z)
    {
        GameObject temp = SetActive();
        if(temp != null)
        {
            temp.transform.position = StartPoint.position;
            temp.transform.rotation = StartPoint.rotation;
            temp.transform.eulerAngles += new Vector3(x,y,z);
            
        }
        return temp;
    }

    private GameObject initBullet()
    {
        GameObject temp = SetActive();
        temp.transform.position = StartPoint.position;
        temp.transform.rotation = StartPoint.rotation;
        return temp;
    }
    IEnumerator ShootOneDirBullets(float bulletSpeed, GameObject bullet)
    {
        while (bullet!=null && bullet.activeSelf)
        {
            bullet.transform.position +=  bullet.transform.forward * bulletSpeed * Time.deltaTime;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
