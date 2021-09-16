using UnityEngine;
using System.Collections;

public class EnemyShip_02: MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Rocket;
    public GameObject Torpedo;
    public GameObject EF_Enemy_Gun_Light_01;

    public Transform ShotSpawn01;
    //public Transform ShotSpawn02;


    public Transform ShotSpawnRocket01;
    public Transform ShotSpawnTorpedo01;
    // public Transform ShotSpawnRocket02;


    public float fireRate;
    public float fireRateRocket;
    public float fireRateTorpedo;


    private float nextFire;
    private float nextFireRocket;
    private float nextFireTorpedo;



    void Update()
    {

        //                  --- Gun Fire
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(Bullet, ShotSpawn01.position, ShotSpawn01.rotation);
            Instantiate(EF_Enemy_Gun_Light_01, ShotSpawn01.position, ShotSpawn01.rotation);


        }

        //                  --- Rocket Fire
        if (Time.time > nextFireRocket)
        {
            nextFireRocket = Time.time + fireRateRocket;
            Instantiate(Rocket, ShotSpawnRocket01.position, ShotSpawnRocket01.rotation);

        }

        //                  --- Torpedo Fire
        if (Time.time > nextFireTorpedo)
        {
            nextFireTorpedo = Time.time + fireRateTorpedo;
            Instantiate(Torpedo, ShotSpawnTorpedo01.position, ShotSpawnTorpedo01.rotation);

        }

    }
}
