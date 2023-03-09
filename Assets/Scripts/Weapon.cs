using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Unit _unit;

    public float _attackSpeed = 1f;
    public float _attackPower;
    public float _attackRange;

    public float _maxCoolTime;
    public float _curCoolTime;
    
    List<Unit> enemiesInRange = new List<Unit>();

    private void Awake()
    {
        _unit = GetComponentInParent<Unit>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            if (collision.TryGetComponent(out Unit otherUnit) && 
                IsOtherTeam(_unit.team, otherUnit.team))
            {
                enemiesInRange.Add(otherUnit);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            if (collision.TryGetComponent(out Unit otherUnit) &&
                enemiesInRange.Contains(otherUnit))
            {
                enemiesInRange.Remove(otherUnit);
            }
        }
    }

    private void Update()
    {
        CheckAttackable();
    }

    private Unit FindClosestEnemy()
    {
        Unit closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Unit enemy in enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void CheckAttackable()
    {
        if (_curCoolTime > 0)
        {
            _curCoolTime -= Time.deltaTime;
        }
        else
        {
            if (enemiesInRange.Count > 0)
            {
                Unit closestEnemy = FindClosestEnemy();

                if (closestEnemy != null)
                {
                    Attack(closestEnemy);
                }
            }
        }
    }

    private void Attack(Unit enemy)
    {
        _curCoolTime += _maxCoolTime;
        enemy.Hit(_attackPower);
    }

    private bool IsOtherTeam(ETeam myTeam, ETeam otherTeam)
    {
        return myTeam != otherTeam;
    }
}