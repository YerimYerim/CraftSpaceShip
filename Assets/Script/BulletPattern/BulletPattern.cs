using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum tempWall
{
    UP = 0 ,DOWN = 1,LEFT = 2,RIGHT = 3
}
public class BulletPattern : ObjectPoolManager
{

    public void ShootBullet(AttackPattern attackPattern , float speed , float time)
    {
        switch (attackPattern)
        {
            case AttackPattern.ONE:

                for (int i = 0; i < _objects.Count; ++i)
                {
                    if (_objects[i].activeSelf == true)
                    {
                        _objects[i].transform.position += Vector3.forward * Time.deltaTime * speed;
                    }
                }
                break;
            case AttackPattern.THREE:
                break;
            case AttackPattern.EIGHT:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(attackPattern), attackPattern, null);
        }
    }

    public IEnumerator ActiveBullet(float time)
    {
        while (true)
        {
            SetActive();
            print("cor");
            yield return new WaitForSeconds(time);
        }

    }
}
