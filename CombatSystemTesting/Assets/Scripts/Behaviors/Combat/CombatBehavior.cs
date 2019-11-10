using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatBehavior : MonoBehaviour
{
    [SerializeField]
    protected Actor _actor;

    [SerializeField]
    public Weapon _weapon;

    [SerializeField]
    protected float _targetRange;
}
