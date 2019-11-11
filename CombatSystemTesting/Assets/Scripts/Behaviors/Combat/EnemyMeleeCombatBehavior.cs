using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeCombatBehavior : CombatBehavior
{
    [SerializeField]
    private float _arrowBlockRange;

    [SerializeField]
    [Range(1,100)]
    private int _blockChance;

    [SerializeField]
    [Range(1, 100)]
    private int _attackChance;

    [SerializeField]
    [Range(1, 100)]
    private int _bashChance;

    [SerializeField]
    [Range(0, 10)]
    private float _reactionTime;

    private void Update()
    {
        if ((_actor._currentTarget._blockEnum != BlockEnum.None && Vector3.Distance(_actor._currentTarget.transform.position, _actor.transform.position) < _targetRange) || Vector3.Distance(_actor._currentTarget.transform.position, _actor.transform.position) < _weapon._range)
        {
            if (!_crosshairManager._isActive)
            {
                _crosshairManager.ToggleState(true);
            }
            _actor._blockEnum = _crosshairManager.GetBlockDirection(0, 0, this.transform);
            if (false)//when attacks
            {
                _actor._attack.MeleeAttack(_weapon, _crosshairManager);
            }
            return;
        }
        if (_crosshairManager._isActive)
        {
            _crosshairManager.ToggleState(false);
        }
        _actor._blockEnum = BlockEnum.None;



    }
}

