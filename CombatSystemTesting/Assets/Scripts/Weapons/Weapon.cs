using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public WeaponEnum _weaponType;
    public AnimatorController _attackAnimatorController;

    public float _attackSpeed;
    public float _recoveryTime;

    [Header("Melee")]
    public float _damage;
    public float _range;
    public bool _canBlocksArrows;
    public float _blockResistance;

    [Header("Ranged")]
    public Projectile _projectile;
}
