using UnityEngine;
using System.Collections;

public class PlayerShip_01 : MonoBehaviour {
    public GameObject Bullet;
    public GameObject Rocket;
    public GameObject RocketBig;
    public GameObject Ef_Gun_Light_01;

    public Transform ShotSpawn01;
    public Transform ShotSpawn02;


    public Transform ShotSpawnRocket01;
    public Transform ShotSpawnRocket02;


    public float fireRate;
    public float fireRateRocket;


    private float nextFire;
    private float nextFireRocket;


	// Update is called once per frame
	void Update ()
    {

        //                  --- Gun Fire
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(Bullet, ShotSpawn01.position, ShotSpawn01.rotation);
            Instantiate(Ef_Gun_Light_01, ShotSpawn01.position, ShotSpawn01.rotation);

            Instantiate(Bullet, ShotSpawn02.position, ShotSpawn02.rotation);
            Instantiate(Ef_Gun_Light_01, ShotSpawn02.position, ShotSpawn02.rotation);

        }

        //                  --- Rocket Fire
        if (Time.time > nextFireRocket)
        {
            nextFireRocket = Time.time + fireRateRocket;
            Instantiate(Rocket, ShotSpawnRocket01.position, ShotSpawnRocket01.rotation);
            Instantiate(Rocket, ShotSpawnRocket02.position, ShotSpawnRocket02.rotation);
        }
    }
}
