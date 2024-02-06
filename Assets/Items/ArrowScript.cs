using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using System;

public class ArrowScript : MonoBehaviour
{
    public Arrow arrow;
    int startHour = 0;
    int waitHour = 1;
    bool active = true;
    Animator thisAnimator;

    private void Start()
    {
        startHour = StateManager.hour;
        thisAnimator = GetComponent<Animator>();
        StateManager.onPlayAction += onHour;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, arrow.rotation));
    }

    void onHour(int hour)
    {
        if (!active)
            return;

        if (startHour + waitHour > hour)
            return;

        thisAnimator.Play("Fire");
    }

    public void setActive(bool on)
    {
        active = on;

        if (on)
            startHour = StateManager.hour;
    }

}
