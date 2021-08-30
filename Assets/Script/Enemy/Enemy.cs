using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private AttackPattern _attackPattern;
    [SerializeField] private RotationPattern _rotationPattern;
    [SerializeField] private int HP = 0;
    [SerializeField] private static int bulletDamage = 1;
    [SerializeField] private ObjectPoolManager _bullets;
    [SerializeField] private List<Vector3> _path;
    [SerializeField] private int _pathNum;
    [SerializeField] private float _speed;
    [SerializeField] private BulletPattern _bulletPattern;
    public Enemy(EnemyType enemyType, AttackPattern attackPattern, List<Vector3> path, RotationPattern rotationPattern,
        int hp)
    {
        _bulletPattern = transform.GetChild(0).GetComponent<BulletPattern>();
        _bulletPattern.init();
        _path = new List<Vector3>();
        _enemyType = enemyType;
        _attackPattern = attackPattern;
        _rotationPattern = rotationPattern;
        _path.AddRange(path);
        HP = hp;
    }

    void Awake()
    {
        _bulletPattern = transform.GetChild(0).GetComponent<BulletPattern>();
        _bulletPattern.init();
        StartCoroutine( _bulletPattern.ActiveBullet(0.5f));
    }
    void Update()
    {
        
        MovePath();
        _bulletPattern.SetActiveFalse();
        _bulletPattern.ShootBullet(_attackPattern, 5 , 1);
    }


    void MovePath()
    {
        transform.position = Vector3.MoveTowards(transform.position ,_path[_pathNum],  _speed );

        if (transform.position == _path[_pathNum])
        {
            ++_pathNum;
        }

        if (_pathNum == _path.Count)
        {
            _pathNum = 0;
        }
    }

    public void Attack()
    {
        
        switch (_attackPattern)
        {
            case AttackPattern.ONE:
                break;
            case AttackPattern.THREE:
                
                break;
            case AttackPattern.EIGHT:
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}
