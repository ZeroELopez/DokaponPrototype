
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;
using System;
using UnityEngine.InputSystem;

public class Inputs : MonoBehaviour
{
    public static Inputs instance;
    public Controls controls;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);

        instance = this;
        controls = new Controls();
        controls.Player.Movement.performed += onDirection;
        controls.Enable();
    }

    void onDirection(InputAction.CallbackContext context)
    {
        Debug.Log("onDirection");
    }
}
