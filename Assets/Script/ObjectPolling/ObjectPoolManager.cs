using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  ObjectPoolManager : MonoBehaviour
{
    [SerializeField] protected List<GameObject> _objects;
    [SerializeField] protected int AmountMax = 100;
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected Transform parentTransform;
    [SerializeField] protected int ActiveNum = 0;
    public virtual void init()
    {
        _objects = new List<GameObject>();    

        for (int i = 0; i < AmountMax; ++i)
        {
            GameObject tempObject = Instantiate(prefab, parentTransform.transform);
            tempObject.SetActive(false); 
            _objects.Add(tempObject);
        }
        
    }

    protected GameObject getObject(int num)
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

    void Update()
    {
        print(ActiveNum);
    }
    public void SetActiveFalse()
    {
        for (int i = 0; i < _objects.Count; ++i)
        {
            if(Vector3.Distance(_objects[i].transform.position,Vector3.zero) > 20f && _objects[i].activeSelf == true)
            {
                _objects[i].SetActive(false);
                _objects[i].transform.position = Vector3.zero;
                --ActiveNum;
            }
        }
    }

    protected void SetActive()
    {
        print("im active");
        if (ActiveNum < AmountMax)
        {
            _objects[ActiveNum].SetActive(true);
            ++ActiveNum;
        }
        else
        {
            if (_objects[0].activeSelf == false)
            {
                ActiveNum = 0;
                _objects[ActiveNum].SetActive(true);
            }
        }

    }

    
}
