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
    [Range(0, 5)]
    private float _reactionTime;

    private BlockEnum _targetStance;

    private void Start()
    {
        AttackLoop();
    }

    private void AttackLoop()
    {
        if ((_actor._currentTarget._blockEnum != BlockEnum.None && Vector3.Distance(_actor._currentTarget.transform.position, _actor.transform.position) < _targetRange) || Vector3.Distance(_actor._currentTarget.transform.position, _actor.transform.position) < _weapon._range)
        {
                //switch to random block enum
                _actor._attack.MeleeAttack(_weapon, _crosshairManager);
        }
        Invoke("AttackLoop",1);
    }

    private void Update()
    {
        if ((_actor._currentTarget._blockEnum != BlockEnum.None && Vector3.Distance(_actor._currentTarget.transform.position, _actor.transform.position) < _targetRange) || Vector3.Distance(_actor._currentTarget.transform.position, _actor.transform.position) < _weapon._range)
        {
            if (!_crosshairManager._isActive)
            {
                _crosshairManager.ToggleState(true);
            }

            if (_targetStance != _actor._currentTarget._blockEnum)
            {
                _targetStance = _actor._currentTarget._blockEnum;
                StopAllCoroutines();

                StartCoroutine(SwitchBlockDirection(_targetStance, Random.Range(0,_reactionTime)));

                if ((_targetStance != BlockEnum.Bottom && PercentChance(_attackChance)) || (_targetStance == BlockEnum.Bottom && PercentChance(_bashChance)))
                {
                    _actor._attack.MeleeAttack(_weapon, _crosshairManager);
                }
            }
            _actor._blockEnum = _crosshairManager.GetBlockDirection(0, 0, this.transform);
            return;
        }
        if (_crosshairManager._isActive)
        {
            _crosshairManager.ToggleState(false);
        }
        _actor._blockEnum = BlockEnum.None;



    }

    private IEnumerator SwitchBlockDirection(BlockEnum blockEnum, float switchTime)
    {
        yield return new WaitForSeconds(switchTime);

        switch (blockEnum)
        {
            case BlockEnum.Top:
                _actor._blockEnum = _crosshairManager.GetBlockDirection(0, 1, this.transform);
                break;
            case BlockEnum.Left:
                _actor._blockEnum = _crosshairManager.GetBlockDirection(1, 0, this.transform);
                break;
            case BlockEnum.Right:
                _actor._blockEnum = _crosshairManager.GetBlockDirection(-1, 0, this.transform);
                break;
            case BlockEnum.Bottom:
                _actor._blockEnum = _crosshairManager.GetBlockDirection(0, -1, this.transform);
                break;
        }
    }

    private bool PercentChance(int Percent)
    {
        if (Random.Range(0,100) < Percent)
        {
            return true;
        }
        return false;
    }
}

