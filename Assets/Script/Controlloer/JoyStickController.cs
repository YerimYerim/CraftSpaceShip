using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoyStickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private RectTransform joystickBackGround;
    [SerializeField] private RectTransform joystick;
    
    [SerializeField] private bool isTouched;
    [SerializeField] private Vector2 FirstTouchPos;
    [SerializeField] private Vector2 CurTouchPos;
    [SerializeField] private Vector3 Velocity;
    
    [SerializeField] private float Distance;
    [SerializeField] private float MaxDistance;
    [SerializeField] private float DPS = 1f;
    void Awake()
    {
        
        playerTransform = GameObject.FindWithTag("PLAYER").gameObject.transform;
        joystickBackGround = GameObject.Find("JoyStickBackGround").gameObject.GetComponent<RectTransform>();
        joystick = GameObject.Find("JoyStickController").gameObject.GetComponent<RectTransform>();
        joystickBackGround.transform.position = Vector3.left * 200000;
        MaxDistance = joystickBackGround.rect.width / 2 - joystick.rect.width / 2;
    }

    void Update()
    {
        if (isTouched)
        {
            playerTransform.position += Velocity * Time.deltaTime * Math.Min(Distance , MaxDistance) * DPS * 0.1f;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isTouched = true;
        joystickBackGround.position = eventData.position;
        FirstTouchPos = eventData.position;
       
    }

    public void OnDrag(PointerEventData eventData)
    {
        CurTouchPos = eventData.position;
        Distance = Vector2.Distance(CurTouchPos, FirstTouchPos);
        
        Velocity = CurTouchPos - FirstTouchPos;
        Velocity.Normalize();
        
        if (Distance < MaxDistance)
        {
            joystick.localPosition = CurTouchPos - FirstTouchPos;
        }
        else
        {
            joystick.localPosition = Velocity * MaxDistance;
        }
        
        Velocity.Set(Velocity.x , 0 ,Velocity.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouched = false;
        joystickBackGround.transform.position = Vector3.left * 200000;
        joystick.transform.localPosition = Vector3.zero;
    }
}
