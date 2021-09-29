using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

//터렛 위치에 따라서 나와야함
//터렛 위치 5군데

public enum TurretPosition
{
    RIGHTFRONT,
    LEFTFRONT,
    CENTERFRONT,
    RIGHTBACK,
    LEFTBACK,
}

public class PlayerTurret : MonoBehaviour
{
    public List<Transform> Turrets;
    public List<Bullets> TurretsBullets;
    public List<AttackPattern> AttackPatterns;
    public List<int> TurretsLevel;
    public List<float> Speed;

    private static int TurretsCount = 5;

    private List<Coroutine> ShooterCoroutine;
    // Start is called before the first frame update
    void Awake()
    {
        TurretsBullets = new List<Bullets>();
        AttackPatterns = new List<AttackPattern>(TurretsCount);
        TurretsLevel = new List<int>(TurretsCount);
        Speed = new List<float>(TurretsCount);
        ShooterCoroutine = new List<Coroutine>(5);
        for (int i = 0; i < TurretsCount; ++i)
        {
            TurretsBullets.Add(transform.parent.GetChild(1+i).GetComponent<Bullets>());
            AttackPatterns.Add(AttackPattern.ONE);
            TurretsLevel.Add(1);
            Speed.Add(TurretsLevel[i] * 1.5f);
        }
        for (int i = 0; i < TurretsCount; ++i)
        {
            TurretsBullets[i].init();    
            AttackPatterns[i] = (AttackPattern)PlayerStatus.turretPosTypes[i];
            TurretsLevel[i] = PlayerStatus._turretinfos[(int)AttackPatterns[i]]._level;
            Speed[i] = PlayerStatus._turretinfos[(int)AttackPatterns[i]]._speed;
        }

        for(int i = 0; i<TurretsCount; ++i)
        {
            int t = i;
            ShooterCoroutine.Add(StartCoroutine(ShootTimer(0.1f, AttackPatterns[t], Speed[t], TurretsBullets[t])));
        }
    }

    public IEnumerator ShootTimer(float BulletShootSpeed ,AttackPattern attackPattern, float bulletSpeed, Bullets bullet )
    {
        while (gameObject.activeSelf)
        {
            bullet.ShootBullet(attackPattern,bulletSpeed , 0);
            yield return new WaitForSeconds(BulletShootSpeed);
        }
    
    }
}
