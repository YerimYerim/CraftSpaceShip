using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public GameObject HitEffect;
    public List<GameObject> HpImages;
    private Coroutine _effectCoroutine;

    // Start is called before the first frame update
    void Awake()
    {
        HitEffect = GameObject.Find("HitEffect");
        HpImages  = new List<GameObject>();
        
        var tempHP = GameObject.Find("PlayerHP").transform;
        
        for (int i = 0; i < tempHP.childCount; ++i)
        {
            HpImages.Add(tempHP.GetChild(i).gameObject);
        }
        
        HitEffect.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Enemy_Bullet(Clone)") || other.gameObject.CompareTag("ENEMY") )
        {
            PlayerStatus.HitPlayer();
            HpImages[PlayerStatus._HP-1].SetActive(false);
            HitEffect.SetActive(true);
  
            
            if (PlayerStatus.isCoroutineStart == false)
            {
                PlayerStatus.isCoroutineStart = true;
                _effectCoroutine = StartCoroutine( "effectCorutine" );
            }
            
            if (other.gameObject.name.Equals("Enemy_Bullet(Clone)"))
            {
                other.gameObject.SetActive(false);
                print("sd");
            }

            if (PlayerStatus._HP < 0)
            {
                PlayerStatus.isGameEnd = true;
            }
        }
    }

    IEnumerator effectCorutine()
    {
        yield return new WaitForSeconds(0.5f);
        HitEffect.SetActive(false);
        PlayerStatus.isCoroutineStart = false;
    }

}
