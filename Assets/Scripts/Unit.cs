using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Unit : MonoBehaviour
{
    private IObjectPool<Unit> _ManagedPool;
    public ETeam team = ETeam.None;
    SpriteRenderer spriteRenderer;

    public float _maxSpeed;
    public float _curSpeed;
    Vector3 _moveTargetPosition;

    public float _maxHp = 100f;
    public float _curHp;

    [SerializeField] Weapon _weapon;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init()
    {
        _moveTargetPosition = transform.position;
        _curHp = _maxHp;
        SetColor();
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            _moveTargetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        Move();
    }

    void Move()
    {
        if (_moveTargetPosition == null) return;

        transform.position = Vector2.MoveTowards(transform.position, _moveTargetPosition, _curSpeed * Time.deltaTime);
    }

    public void Hit(float Damage)
    {
        _curHp -= Damage;
        if (_curHp < 0)
            DestroyUnit();
    }


    public void SetManagedPool(IObjectPool<Unit> pool)
    {
        _ManagedPool = pool;
    }

    public void DestroyUnit()
    {
        if (_ManagedPool == null)
            Destroy(gameObject);
        else
            _ManagedPool.Release(this);
    }

    void SetColor()
    {
        spriteRenderer.color = Util.GetColor(team);
    }
}