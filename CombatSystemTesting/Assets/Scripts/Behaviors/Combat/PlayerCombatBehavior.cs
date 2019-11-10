using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatBehavior : CombatBehavior
{
    [SerializeField]
    protected CrosshairManager _crosshairManager;

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

    private Weapon _currentWeapon;
    private Enemy _currentTarget;
    float _cameraX;

    private void SwitchWeapon(Weapon weapon)
    {
        if (_actor._blockEnum == BlockEnum.None)
        {
            _currentWeapon = weapon;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(Controls.Instance._blockStanceKey))
        {
            _crosshairManager.ToggleState(true);
            FindTarget();
        }
        if (Input.GetKeyUp(Controls.Instance._blockStanceKey))
        {
            _crosshairManager.ToggleState(false);
        }

        float x = Input.GetAxis(Controls.Instance._lookAxisX);
        float y = Input.GetAxis(Controls.Instance._lookAxisY);

        if (_crosshairManager._isActive)
        {
            LookAtTarget();
            _actor._blockEnum = _crosshairManager.GetBlockDirection(x, y, this.transform);
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

        if (_currentTarget == null)
        {
            return;
        }

        if (Vector3.Distance(_currentTarget.transform.position, _actor.transform.position) > _targetRange)
        {
            _crosshairManager.ToggleState(false);
            _currentTarget = null;
            return;
        }

        _combatCameraPosition.LookAt(_currentTarget.transform);
        _actor.transform.LookAt(new Vector3(_currentTarget.transform.position.x, _actor.transform.position.y, _currentTarget.transform.position.z));
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
                _currentTarget = enemy;
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
                _currentTarget = enemy;
                closest = dist;
            }
        }
    }
}
