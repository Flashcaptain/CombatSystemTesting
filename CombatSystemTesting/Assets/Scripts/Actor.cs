using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public BlockEnum _blockEnum;

    [SerializeField]
    protected CombatBehavior _combatBehavior;

    [SerializeField]
    protected MovementBehavior _movementBehavior;

    public void TakeDamage(float _damage , BlockEnum blockDirection)
    {

    }
}
