using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    Warrior _unit;

    public float _attackSpeed = 1f;
    public float _attackPower;
    public float _attackRange;

    public float _maxCoolTime;
    public float _curCoolTime;
    
    List<Warrior> enemiesInRange = new List<Warrior>();

    private void Awake()
    {
        _unit = GetComponentInParent<Warrior>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Unit"))
        {
            if (collision.TryGetComponent(out Warrior otherUnit) && 
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
            if (collision.TryGetComponent(out Warrior otherUnit) &&
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

    private Warrior FindClosestEnemy()
    {
        Warrior closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (Warrior enemy in enemiesInRange)
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
                Warrior closestEnemy = FindClosestEnemy();

                if (closestEnemy != null)
                {
                    Attack(closestEnemy);
                }
            }
        }
    }

    private void Attack(Warrior enemy)
    {
        _curCoolTime += _maxCoolTime;
        enemy.Hit(_attackPower);
    }

    private bool IsOtherTeam(ETeam myTeam, ETeam otherTeam)
    {
        return myTeam != otherTeam;
    }
}