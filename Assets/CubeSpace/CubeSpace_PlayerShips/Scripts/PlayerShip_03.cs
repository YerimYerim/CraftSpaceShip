using UnityEngine;
using System.Collections;

public class PlayerShip_03 : MonoBehaviour {
    public GameObject Bullet;
    public GameObject Rocket;
    public GameObject RocketBig;
    public GameObject Ef_Gun_Light_01;

    public Transform ShotSpawn01;
    public Transform ShotSpawn02;
    // public Transform ShotSpawn03;
    // public Transform ShotSpawn04;
    public Transform ShotSpawn05;

    public Transform ShotSpawnRocket01;
    public Transform ShotSpawnRocket02;

    public Transform ShotSpawnRocketBig01;
    public Transform ShotSpawnRocketBig02;

    public float fireRate;
    public float fireRateRocket;
    public float fireRateRocketBig;

    private float nextFire;
    private float nextFireRocket;
    private float nextFireRocketBig;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //                  --- Gun Fire
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(Bullet, ShotSpawn01.position, ShotSpawn01.rotation);
            Instantiate(Ef_Gun_Light_01, ShotSpawn01.position, ShotSpawn01.rotation);

            Instantiate(Bullet, ShotSpawn02.position, ShotSpawn02.rotation);
            Instantiate(Ef_Gun_Light_01, ShotSpawn02.position, ShotSpawn02.rotation);

            // Instantiate(Bullet, ShotSpawn03.position, ShotSpawn03.rotation);
            // Instantiate(Ef_Gun_Light_01, ShotSpawn03.position, ShotSpawn03.rotation);
            //
            // Instantiate(Bullet, ShotSpawn04.position, ShotSpawn04.rotation);
            // Instantiate(Ef_Gun_Light_01, ShotSpawn04.position, ShotSpawn04.rotation);

            Instantiate(Bullet, ShotSpawn05.position, ShotSpawn05.rotation);
            Instantiate(Ef_Gun_Light_01, ShotSpawn05.position, ShotSpawn05.rotation);
        }

        //                  --- Rocket Fire
        if (Time.time > nextFireRocket)
        {
            nextFireRocket = Time.time + fireRateRocket;
            Instantiate(Rocket, ShotSpawnRocket01.position, ShotSpawnRocket01.rotation);
            Instantiate(Rocket, ShotSpawnRocket02.position, ShotSpawnRocket02.rotation);
        }

        //                  --- RocketBig Fire
        if (Time.time > nextFireRocketBig)
        {
            nextFireRocketBig = Time.time + fireRateRocketBig;
            Instantiate(RocketBig, ShotSpawnRocketBig01.position, ShotSpawnRocketBig01.rotation);
            Instantiate(RocketBig, ShotSpawnRocketBig02.position, ShotSpawnRocketBig02.rotation);
        }
    }
}
