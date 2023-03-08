using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Unit : MonoBehaviour
{
    private IObjectPool<Unit> _ManagedPool;
    public ETeam team = ETeam.None;

    public float _maxSpeed;
    public float _curSpeed;
    Vector3 _moveTargetPosition;

    public float _maxHp = 100f;
    public float _curHp;
    public float _ap = 50f;             // Attack Power
    public float _dp = 10f;             // Defense Power

    // Start is called before the first frame update
    void Start()
    {
        _moveTargetPosition = transform.position;
        _curHp = _maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
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

    void Attack()
    {

    }


    public void SetManagedPool(IObjectPool<Unit> pool)
    {
        _ManagedPool = pool;
    }

    public void DestroyUnit()
    {
        _ManagedPool.Release(this);
    }
}
