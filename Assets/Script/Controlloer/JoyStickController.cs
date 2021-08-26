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
    [SerializeField] private RectTransform joystickController;
    
    [SerializeField] private bool isTouched;
    
    [SerializeField] private Vector2 FirstTouchPos;
    [SerializeField] private Vector2 CurTouchPos;
    [SerializeField] private Vector3 Force;
    [SerializeField] private float Distance;
    [SerializeField] private float MaxSpeed;
    [SerializeField] private float DPS = 1f;
    void Awake()
    {
        playerTransform = GameObject.FindWithTag("PLAYER").gameObject.transform;
        joystickBackGround = GameObject.Find("JoyStickBackGround").gameObject.GetComponent<RectTransform>();
        joystickController = GameObject.Find("JoyStickController").gameObject.GetComponent<RectTransform>();
        joystickBackGround.transform.position = Vector3.left * 200000;
        MaxSpeed = (joystickBackGround.rect.width / 2 - joystickController.rect.width / 2) / 10;
    }

    void Update()
    {
        if (isTouched)
        {
            playerTransform.position = playerTransform.position + Force * Time.deltaTime * Math.Min(Distance/10 , MaxSpeed) * DPS;
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

        if (Vector2.Distance(CurTouchPos, FirstTouchPos) < (joystickBackGround.rect.width /2 - joystickController.rect.width/2))
        {
            joystickController.position = CurTouchPos;
            Force.Set((CurTouchPos -  FirstTouchPos).x , 0 ,(CurTouchPos -  FirstTouchPos).y );
            Force.Normalize();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isTouched = false;
        joystickBackGround.transform.position = Vector3.left * 200000;
        joystickController.transform.localPosition = Vector3.zero;
    }
}
