using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Warrior : Unit
{
    [SerializeField] EUnitType unitType = EUnitType.none;

    public static float SpwanTime = 2f;
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
        Look();
    }

    void Move()
    {
        if (_moveTargetPosition == null) return;
        if (team != ETeam.Player) return;

        transform.position = Vector2.MoveTowards(transform.position, _moveTargetPosition, _curSpeed * Time.deltaTime);
    }

    void Look()
    {
        if (team != ETeam.Player) return;

        Vector3 targetPos = _moveTargetPosition;
        targetPos.z = transform.position.z;
        Quaternion targetRot = Quaternion.Euler(targetPos - transform.position);

        transform.rotation = targetRot;
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
        _ManagedPool.Release(this);
    }

    protected override void SetColor()
    {
        spriteRenderer.color = Util.GetColor(team);
    }
}