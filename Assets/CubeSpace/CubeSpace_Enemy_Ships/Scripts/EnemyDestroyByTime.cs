using UnityEngine;
using System.Collections;

public class EnemyDestroyByTime : MonoBehaviour {

    public float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
