using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField]
    protected Actor _actor;

    private CrosshairManager _crosshairManager;
    private Weapon _weapon;
    private bool _inAttack;
    private int _inRecovery;

    public void MeleeAttack(Weapon weapon, CrosshairManager crosshairManager)
    {
        _crosshairManager = crosshairManager;
        _weapon = weapon;

        if (CanAttack())
        {
            StartCoroutine(DoneAttacking(weapon));
            _actor._animator.Play("Attack");
        }
    }

    public void ParryAttack(Weapon weapon, CrosshairManager crosshairManager)
    {
        _crosshairManager = crosshairManager;
        _weapon = weapon;
        if (CanAttack())
        {
            StartCoroutine(Recover(weapon));
            _actor._animator.Play("Parry");
        }
    }

    public void TriggerAttack()
    {
        if (Vector3.Distance(_actor._currentTarget.transform.position, _actor.transform.position) < _weapon._range)
        {
            _actor._currentTarget.TakeDamage(_weapon._damage, _actor._blockEnum);
        }
    }

    private IEnumerator DoneAttacking(Weapon weapon)
    {
        _inAttack = true;
        _crosshairManager._isLocked = true;
        yield return new WaitForSeconds(weapon._attackSpeed);
        StartCoroutine(Recover(weapon));
        _crosshairManager._isLocked = false;
        _inAttack = false;
    }
    private IEnumerator Recover(Weapon weapon)
    {
        _inRecovery++;
        yield return new WaitForSeconds(weapon._recoverySpeed);
        _inRecovery--;
    }

    private bool CanAttack()
    {
        if (_inAttack || _inRecovery != 0 || _actor._blockEnum == BlockEnum.Bottom || _actor._blockEnum == BlockEnum.None)
        {
            return false;
        }
        return true;
    }
}
