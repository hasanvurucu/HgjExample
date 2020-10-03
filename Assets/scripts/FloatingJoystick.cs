﻿using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    Vector2 joystickCenter = Vector2.zero;
    public GameObject tut_finger;

    void Start()
    {
        background.gameObject.SetActive(false);
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if(tut_finger.activeSelf==true){tut_finger.SetActive(false);}
        Vector2 direction = eventData.position - joystickCenter;
        inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
        ClampJoystick();
        handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(tut_finger.activeSelf==true){tut_finger.SetActive(false);}
        background.gameObject.SetActive(true);
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
        joystickCenter = eventData.position;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        inputVector = Vector2.zero;
    }
}