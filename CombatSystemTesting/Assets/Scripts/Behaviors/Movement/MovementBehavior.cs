using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehavior : MonoBehaviour
{
    [SerializeField]
    protected Actor _actor;

    [SerializeField]
    protected float _deafaultSpeed;
    [SerializeField]
    protected float _sprintSpeed;
    [SerializeField]
    protected float _guardSpeed;
}
