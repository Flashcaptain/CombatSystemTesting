using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [HideInInspector]
    public static Player Instance;

    public Camera _mainCamera;

    private void Awake()
    {
        Instance = this;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
