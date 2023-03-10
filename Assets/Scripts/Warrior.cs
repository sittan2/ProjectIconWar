using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Warrior : Unit
{
    private IObjectPool<Warrior> _ManagedPool;
    SpriteRenderer spriteRenderer;

    public float _maxSpeed;
    public float _curSpeed;
    Vector3 _moveTargetPosition;

    public float _maxHp = 100f;
    public float _curHp;

    [SerializeField] Weapon _weapon;

    public override void Init()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
        SetSight();

        _moveTargetPosition = transform.position;
        _curHp = _maxHp;
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
        if (team != ETeam.Player) return;

        transform.position = Vector2.MoveTowards(transform.position, _moveTargetPosition, _curSpeed * Time.deltaTime);
    }

    public void Hit(float Damage)
    {
        _curHp -= Damage;
        if (_curHp < 0)
            DestroyUnit();
    }


    public void SetManagedPool(IObjectPool<Warrior> pool)
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

    protected override void SetColor()
    {
        spriteRenderer.color = Util.GetColor(team);
    }
}