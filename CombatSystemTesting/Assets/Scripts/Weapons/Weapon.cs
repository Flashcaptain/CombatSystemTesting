using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponEnum _weaponType;

    public float _attackSpeed;
    public float _recoverySpeed;

    [Header("Melee")]
    public float _damage;
    public float _range;
    public bool _canBlocksArrows;
    public float _blockResistance;

    [Header("Ranged")]
    public Projectile _projectile;
}
