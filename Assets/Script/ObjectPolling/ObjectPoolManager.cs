using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> _objects;
    [SerializeField] private int AmountMax = 100;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parentTransform;
    
    // Start is called before the first frame update
    void Awake()
    {
        _objects = new List<GameObject>();

        for (int i = 0; i < AmountMax; ++i)
        {
            GameObject tempObject = Instantiate(prefab, parentTransform.transform);
            tempObject.SetActive(false);
            _objects.Add(tempObject);
        }

    }

    public GameObject getObject(int num)
    {
        if (num < _objects.Count)
        {
            return _objects[num];
        }
        else
        {
            return null;
        }
    }
}
