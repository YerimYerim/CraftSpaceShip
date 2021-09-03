using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public enum EnemyType
{
    MINI,
    MIDDLE,
    BOSS
}

public enum AttackPattern
{
    ONE,
    THREE,
    EIGHT
}

// public enum MovePattern
// {
//     LEFTRIGHT,
//     ROUND,
//     FRONTBACK,
//     BACKFRONT
// }

public enum RotationPattern
{
    HALF,
    FULL    
}
public class Enemy : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private AttackPattern _attackPattern;
    [SerializeField] private int HP = 0;
    
    [Header("Move")]
    [SerializeField] private List<Vector3> _path;
    [SerializeField] private int _pathNum;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;
    
    [Header("BulletControll")]
    [SerializeField] private Bullets _bulletPattern;
    [SerializeField] private float _bulletShootSpeed;
    [SerializeField] private float _bulletMoveSpeed;
    [SerializeField] private static int bulletDamage = 1;


    public Enemy(EnemyType enemyType, AttackPattern attackPattern, List<Vector3> path, RotationPattern rotationPattern,
        int hp)
    {
        _bulletPattern = transform.parent.GetChild(1).GetComponent<Bullets>();
        _bulletPattern.init();
        _enemyType = enemyType;
        _attackPattern = attackPattern;
        _path.AddRange(path);
        HP = hp;

    } 
    void Awake()
    {
        _bulletPattern.init();
        StartCoroutine(ShootTimer(_bulletShootSpeed));
    }
    void Update()
    {
        MovePath();
        gameObject.transform.Rotate(0,_rotateSpeed,0);
        _bulletPattern.SetActiveFalse();
    }

    void MovePath()
    {
        transform.position = Vector3.MoveTowards(transform.position ,_path[_pathNum],  _moveSpeed );

        if (transform.position == _path[_pathNum])
        {
            ++_pathNum;
        }

        if (_pathNum == _path.Count)
        {
            _pathNum = 0;
        }
    }


    public IEnumerator ShootTimer(float BulletShootSpeed)
    {
        while (gameObject.activeSelf)
        {
            _bulletPattern.ShootBullet(_attackPattern,_bulletMoveSpeed , 0);
            yield return new WaitForSeconds(BulletShootSpeed);
        }
    
    }
}
