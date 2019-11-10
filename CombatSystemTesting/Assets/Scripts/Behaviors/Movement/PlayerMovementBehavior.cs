using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementBehavior : MovementBehavior
{
    private void FixedUpdate()
    {
        float x = Input.GetAxis(Controls.Instance._moveAxisX);
        float z = Input.GetAxis(Controls.Instance._moveAxisY);
        Movement(x, z);
    }

    private void Movement(float x, float z)
    {
        if (_actor._blockEnum == BlockEnum.None)
        {
            if (Input.GetKey(Controls.Instance._sprintKey))
            {
                _actor.transform.Translate(new Vector3(x,0,z) * _sprintSpeed * Time.deltaTime);
                return;
            }
            _actor.transform.Translate(new Vector3(x, 0, z) * _deafaultSpeed * Time.deltaTime);
            return;
        }
        _actor.transform.Translate(new Vector3(x, 0, z) * _guardSpeed * Time.deltaTime);
    }
}
