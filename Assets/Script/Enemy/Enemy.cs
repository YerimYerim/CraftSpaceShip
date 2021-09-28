using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
[Serializable]
public enum EnemyType
{
    MINI,
    MIDDLE,
    BOSS
}
[Serializable]
public enum AttackPattern
{
    ONE,
    THREE,
    EIGHT,
}

[Serializable]
public enum RotationPattern
{
    HALF,
    FULL    
}
[Serializable]
public class Enemy : MonoBehaviour
{
    [Header("Type")]
    [SerializeField] public EnemyType _enemyType;
    [SerializeField] public AttackPattern _attackPattern;
    [SerializeField] public int HP = 0;
    
    [Header("Move")]
    [SerializeField] public List<Vector3> _path;
    [SerializeField] private int _pathNum;
    [SerializeField] public float _moveSpeed;
    [SerializeField] public float _rotateSpeed;
    
    [Header("BulletControll")]
    [SerializeField] public Bullets _bulletPattern;
    [SerializeField] public float _bulletShootSpeed;
    [SerializeField] public float _bulletMoveSpeed;
    [SerializeField] private static int bulletDamage = 1;

    [SerializeField] public bool isStart = false;
    [SerializeField] private Coroutine ShooterCoroutine;

    [SerializeField] private GameObject DeadEffect;
    [SerializeField] private Animator _animator;
    [SerializeField] private Material _material;
    void Awake()
    {
        _bulletPattern.init();
        DeadEffect = gameObject.transform.parent.GetChild(0).gameObject;
        _animator = GetComponent<Animator>();
        _material = gameObject.transform.GetChild(0).GetComponent<Material>();
    }
    void Update()
    {

        if (isStart)
        {
            if (ShooterCoroutine == null)
            {
                ShooterCoroutine = StartCoroutine(ShootTimer(_bulletShootSpeed));
            }
            else
            {
                MovePath();
                gameObject.transform.Rotate(0, _rotateSpeed, 0);
                _bulletPattern.SetActiveFalse();
            }
        }
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

    public void SetProperty(
    EnemyType enemyType, AttackPattern attackPattern, int hp, 
    List<Vector3> path, int pathNum, float moveSpeed, 
    float rotateSpeed, Bullets bulletPattern, 
    float bulletShootSpeed,    float bulletMoveSpeed)
    {
        _enemyType = enemyType;
        _attackPattern = attackPattern;
        HP = hp;
        _path.AddRange(path);
        _pathNum = pathNum;
        _moveSpeed = moveSpeed;
        _rotateSpeed = rotateSpeed;
        _bulletPattern = bulletPattern;
        _bulletShootSpeed = bulletShootSpeed;
        _bulletMoveSpeed = bulletMoveSpeed;
    }


    public string GetEnemyTypeString()
    {
        switch (_enemyType)
        {
            case EnemyType.MINI:
                return "MINI";
                break;
            case EnemyType.MIDDLE:
                return "MIDDLE";
                break;
            case EnemyType.BOSS:
                return "BIG";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public string GetBulletTypeString()
    {
        switch (_attackPattern)
        {
            case AttackPattern.ONE:
                return "ONE";
                break;
            case AttackPattern.THREE:
                return "THREE";
                break;
            case AttackPattern.EIGHT:
                return "EIGHT";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnTriggerEnter(Collider bullet)
    {

        if (bullet.name.Equals("Bullet_01(Clone)"))
        {
            bullet.gameObject.SetActive(false);
            --HP;
            if (HP < 0)
            {
                DeadEffect.transform.position = transform.position;
                DeadEffect.SetActive(true);
                gameObject.SetActive(false);
                PlayerStatus.AddScore(_enemyType);
                PlayerStatus.AddCombo();
                
            }
            print("호에엥" + bullet.name);
            _animator.SetBool("isHit", true);

        }
    }
}
