using UnityEngine;
using System.Collections;

public class EnemyShip_01 : MonoBehaviour
{
    public GameObject Bullet;
    //public GameObject Rocket;
    //public GameObject RocketBig;
    public GameObject EF_Enemy_Gun_Light_01;

    public Transform ShotSpawn01;
    public Transform ShotSpawn02;


    //public Transform ShotSpawnRocket01;
    //public Transform ShotSpawnRocket02;


    public float fireRate;
   // public float fireRateRocket;


    private float nextFire;
    //private float nextFireRocket;



    void Update()
    {

        //                  --- Gun Fire
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(Bullet, ShotSpawn01.position, ShotSpawn01.rotation);
            Instantiate(EF_Enemy_Gun_Light_01, ShotSpawn01.position, ShotSpawn01.rotation);

            Instantiate(Bullet, ShotSpawn02.position, ShotSpawn02.rotation);
            Instantiate(EF_Enemy_Gun_Light_01, ShotSpawn02.position, ShotSpawn02.rotation);

        }

    }
}
