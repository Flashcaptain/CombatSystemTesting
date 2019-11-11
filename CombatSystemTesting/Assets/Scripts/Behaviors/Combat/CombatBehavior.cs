using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehavior : MonoBehaviour
{
    [SerializeField]
    public Weapon _weapon;

    [SerializeField]
    protected Actor _actor;

    [SerializeField]
    protected float _targetRange;

    [SerializeField]
    protected CrosshairManager _crosshairManager;
}
