using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CrosshairManager : MonoBehaviour
{
    [HideInInspector]
    public BlockEnum _blockEnum = BlockEnum.Bottom;

    [Header("settings")]

    [Range(0, 10)]
    [SerializeField]
    private float _indicatorLerpSpeed;

    [SerializeField]
    private float _selectedIncreaseSize;

    [SerializeField]
    private GameObject _passive;
    [SerializeField]
    private GameObject _backGround;
    [SerializeField]
    private Image _indicator;
    [SerializeField]
    private Image _top;
    [SerializeField]
    private Image _left;
    [SerializeField]
    private Image _right;
    [SerializeField]
    private Image _bottom;

    private Image _image;
    public bool _isLocked;
    public bool _isActive;

    private void Start()
    {
        _indicator.color = Controls.Instance._blockColor;
        _image = _bottom;
        ToggleState(false);
    }

    public void ToggleState(bool state)
    {
        _passive.SetActive(!state);
        _backGround.SetActive(state);
        _isActive = state;

        if (state)
        {
            _image = _bottom;
            _image.transform.localScale *= _selectedIncreaseSize;
            _image.color = Controls.Instance._blockColor;
            _blockEnum = BlockEnum.Bottom;
            _indicator.transform.position = transform.position;
        }
        else
        {
            _image.transform.localScale = Vector3.one;
            _image.color = Color.white;
            _blockEnum = BlockEnum.None;
        }
    }

    public BlockEnum GetBlockDirection(float x, float y, Transform owner)
    {
        if (!_isActive)
        {
            return BlockEnum.None;
        }

        _indicator.transform.position = Vector3.Lerp(_indicator.transform.position, transform.position + new Vector3(x, y, 0) * 100f, _indicatorLerpSpeed * Time.deltaTime);
        
        transform.position = Player.Instance._mainCamera.WorldToScreenPoint(owner.position);

        if (Mathf.Abs(x) > Controls.Instance._crosshairDeathzone || Mathf.Abs(y) > Controls.Instance._crosshairDeathzone)
        {
            FindDirection(x, y);
        }

        return _blockEnum;
    }

    private void FindDirection(float x, float y)
    {
        if (_isLocked)
        {
            return;
        }

        float largest = 0;
        _image.transform.localScale = Vector3.one;
        _image.color = Color.white;

        if (y > 0 && y > largest) //top
        {
            _image = _top;
            largest = y;
            _blockEnum = BlockEnum.Top;
        }
        if (x > 0 && x > largest) //right
        {
            _image = _right;
            largest = x;
            _blockEnum = BlockEnum.Right;
        }
        if (x < 0 && Mathf.Abs(x) > largest) //left
        {
            _image = _left;
            largest = Mathf.Abs(x);
            _blockEnum = BlockEnum.Left;
        }
        if (y < 0 && Mathf.Abs(y) > largest) //bottom
        {
            _image = _bottom;
            largest = Mathf.Abs(y);
            _blockEnum = BlockEnum.Bottom;
        }

        _image.transform.localScale *= _selectedIncreaseSize;
        _image.color = Controls.Instance._blockColor;
    }
}
