﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatBehavior : CombatBehavior
{
    [SerializeField]
    protected Transform _cameraHolder;

    [SerializeField]
    protected Transform _defaultCameraPosition;

    [SerializeField]
    protected Transform _combatCameraPosition;

    [SerializeField]
    protected float _cameraLerpSpeed;

    [SerializeField]
    protected float _cameraMaxAngle;

    [SerializeField]
    protected float _cameraMinAngle;

    float _cameraX;

    private void SwitchWeapon(Weapon weapon)
    {
        if (_actor._blockEnum == BlockEnum.None)
        {
            _weapon = weapon;
            _actor._animator.SetFloat("Speed", weapon._attackSpeed);
        }
    }

    private void Update()
    {
        switch (_weapon._weaponType)
        {
            case WeaponEnum.None:
                return;
            case WeaponEnum.Ranged:
                return;
            case WeaponEnum.Melee:
                MeleeManager();
                return;
        }
    }

    private void MeleeManager()
    {
        if (Input.GetKeyDown(Controls.Instance._blockStanceKey))
        {
            _crosshairManager.ToggleState(true);
            FindTarget();
        }
        if (Input.GetKeyUp(Controls.Instance._blockStanceKey))
        {
            _crosshairManager.ToggleState(false);
            _actor._blockEnum = BlockEnum.None;

        }

        float x = Input.GetAxis(Controls.Instance._lookAxisX);
        float y = Input.GetAxis(Controls.Instance._lookAxisY);

        if (_crosshairManager._isActive)
        {
            LookAtTarget();
            _actor._blockEnum = _crosshairManager.GetBlockDirection(x, y, this.transform);
            if (Input.GetKeyDown(Controls.Instance._attackKey))
            {
                _actor._attack.MeleeAttack(_weapon, _crosshairManager);
            }
        }
        else
        {
            LookAround(x, y);
        }
    }

        private void LookAround(float x, float y)
    {
        LerpCamera(_defaultCameraPosition, Controls.Instance._defaultCameraFOV);
        _cameraX += -y * Controls.Instance._mouseSensitivity * Time.deltaTime;
        _cameraX = Mathf.Clamp(_cameraX, _cameraMinAngle, _cameraMaxAngle);
        _cameraHolder.transform.eulerAngles = new Vector3(_cameraX, _cameraHolder.transform.eulerAngles.y, _cameraHolder.transform.eulerAngles.z);

        _actor.transform.eulerAngles += new Vector3(0, x, 0) * Controls.Instance._mouseSensitivity * Time.deltaTime;
    }

    private void LookAtTarget()
    {
        LerpCamera(_combatCameraPosition, Controls.Instance._combatCameraFOV);

        if (_actor._currentTarget == null)
        {
            return;
        }

        if (Vector3.Distance(_actor._currentTarget.transform.position, _actor.transform.position) > _targetRange)
        {
            _crosshairManager.ToggleState(false);
            _actor._currentTarget = null;
            return;
        }

        _combatCameraPosition.LookAt(_actor._currentTarget.transform);
        _actor.transform.LookAt(new Vector3(_actor._currentTarget.transform.position.x, _actor.transform.position.y, _actor._currentTarget.transform.position.z));
    }
    
    private void LerpCamera(Transform transform, float FOV)
    {
        Player.Instance._mainCamera.transform.position = Vector3.Lerp(Player.Instance._mainCamera.transform.position, transform.position, _cameraLerpSpeed * Time.deltaTime);
        Player.Instance._mainCamera.transform.rotation = Quaternion.Lerp(Player.Instance._mainCamera.transform.rotation, transform.rotation, _cameraLerpSpeed * Time.deltaTime);
        Player.Instance._mainCamera.fieldOfView = Mathf.Lerp(Player.Instance._mainCamera.fieldOfView, FOV, _cameraLerpSpeed * Time.deltaTime);
    }

    private void FindTarget()
    {
        RaycastHit hit;
        if (Physics.Raycast(Player.Instance._mainCamera.transform.position, Player.Instance._mainCamera.transform.forward, out hit, _targetRange))
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                _actor._currentTarget = enemy;
                return;
            }
        }

        List<Enemy> enemies = new List<Enemy>();
        enemies.AddRange(FindObjectsOfType<Enemy>());

        float closest = _targetRange;
        foreach (Enemy enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, _actor.transform.position);
            if (dist < closest)
            {
                _actor._currentTarget = enemy;
                closest = dist;
            }
        }
    }
}
