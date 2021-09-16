using UnityEngine;
using System.Collections;

public class RandomMover : MonoBehaviour {
    public Vector3 myPosition;
    public float timeMove;
    public float amplitude;
    // Use this for initialization
    void Start () {
        myPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        gameObject.transform.position = new Vector3 (
                 myPosition.x + amplitude * Mathf.Sin(Time.time* timeMove),
                 myPosition.y + amplitude * Mathf.Sin(Time.time * timeMove),
                 myPosition.z + amplitude * Mathf.Sin(Time.time * timeMove)
                );
    }
}
