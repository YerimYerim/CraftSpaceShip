using UnityEngine;
using System.Collections;

public class EnemyShip_03: MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Rocket;
    public GameObject Torpedo;
    public GameObject TorpedoBig;
    public GameObject EF_Enemy_Gun_Light_01;

    public Transform ShotSpawn01;
    public Transform ShotSpawn02;


    public Transform ShotSpawnRocket01;
    public Transform ShotSpawnRocket02;
    public Transform ShotSpawnTorpedo01;
    public Transform ShotSpawnTorpedo02;
    public Transform ShotSpawnTorpedoBig01;
    public Transform ShotSpawnTorpedoBig02;



    public float fireRate;
    public float fireRateRocket;
    public float fireRateTorpedo;
    public float fireRateTorpedoBig;


    private float nextFire;
    private float nextFireRocket;
    private float nextFireTorpedo;
    private float nextFireTorpedoBig;



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

        //                  --- Rocket Fire
        if (Time.time > nextFireRocket)
        {
            nextFireRocket = Time.time + fireRateRocket;
            Instantiate(Rocket, ShotSpawnRocket01.position, ShotSpawnRocket01.rotation);
            Instantiate(Rocket, ShotSpawnRocket02.position, ShotSpawnRocket02.rotation);

        }

        //                  --- Torpedo Fire
        if (Time.time > nextFireTorpedo)
        {
            nextFireTorpedo = Time.time + fireRateTorpedo;
            Instantiate(Torpedo, ShotSpawnTorpedo01.position, ShotSpawnTorpedo01.rotation);
            Instantiate(Torpedo, ShotSpawnTorpedo02.position, ShotSpawnTorpedo02.rotation);

        }


        //                  --- Torpedo BIG Fire
        if (Time.time > nextFireTorpedoBig)
        {
            nextFireTorpedoBig = Time.time + fireRateTorpedoBig;
            Instantiate(TorpedoBig, ShotSpawnTorpedoBig01.position, ShotSpawnTorpedoBig01.rotation);
            Instantiate(TorpedoBig, ShotSpawnTorpedoBig02.position, ShotSpawnTorpedoBig02.rotation);

        }

    }
}
