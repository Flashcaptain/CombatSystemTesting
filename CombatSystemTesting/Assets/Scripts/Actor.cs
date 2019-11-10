using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public BlockEnum _blockEnum;
    public AttackManager _attack;
    public Animator _animator;
    public Actor _currentTarget = null;

    [SerializeField]
    protected float _health;

    [SerializeField]
    protected CombatBehavior _combatBehavior;

    [SerializeField]
    protected MovementBehavior _movementBehavior;

    public void TakeDamage(float _damage, BlockEnum blockDirection)
    {
        switch (_blockEnum)
        {
            case BlockEnum.None:
                _health -= _damage;
                break;
            case BlockEnum.Top:
                if (blockDirection == BlockEnum.Top)
                {
                    _damage = _damage - _combatBehavior._weapon._blockResistance;
                    if (_damage < 0)
                    {
                        _damage = 0;
                    }
                }
                _health -= _damage;
                break;
            case BlockEnum.Left:
                if (blockDirection == BlockEnum.Right)
                {
                    _damage = _damage - _combatBehavior._weapon._blockResistance;
                    if (_damage < 0)
                    {
                        _damage = 0;
                    }
                }
                _health -= _damage;
                break;
            case BlockEnum.Right:
                if (blockDirection == BlockEnum.Left)
                {
                    _damage = _damage - _combatBehavior._weapon._blockResistance;
                    if (_damage < 0)
                    {
                        _damage = 0;
                    }
                }
                _health -= _damage;
                break;
            case BlockEnum.Bottom:
                if (_combatBehavior._weapon._canBlocksArrows)
                {
                    _damage = _damage - _combatBehavior._weapon._blockResistance;
                    if (_damage < 0)
                    {
                        _damage = 0;
                    }
                }
                _health -= _damage;
                break;
        }
        if (_health <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {

    }
}
