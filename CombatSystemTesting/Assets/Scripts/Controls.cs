using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public static Controls Instance;

    public string _lookAxisX = "Mouse X";
    public string _lookAxisY = "Mouse Y";

    public string _moveAxisX = "Horizontal";
    public string _moveAxisY = "Vertical";

    public Color _blockColor;
    public Color _attackColor;
    public Color _bashColor;

    [Range(50, 120)]
    public float _defaultCameraFOV;
    [Range(50, 120)]
    public float _combatCameraFOV;

    public KeyCode _blockStanceKey;
    public KeyCode _sprintKey;
    public KeyCode _attackKey;

    [Range(0, 1)]
    public float _crosshairDeathzone;

    [Range(1,200)]
    public float _mouseSensitivity;

    private void Awake()
    {
        Instance = this;
    }
}
