using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    private void Start()
    {
        _animator.runtimeAnimatorController = _combatBehavior._weapon._attackAnimatorController;
        _currentTarget = Player.Instance;
    }
}
