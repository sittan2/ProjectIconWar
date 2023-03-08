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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            if (TryGetComponent(out Unit otherUnit) && 
                IsOtherTeam(_unit.team, otherUnit.team))
            {
                enemiesInRange.Add(otherUnit);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            if (TryGetComponent(out Unit otherUnit) &&
                enemiesInRange.Contains(otherUnit))
            {
                enemiesInRange.Remove(otherUnit);
            }
        }
    }

    private void Update()
    {
        if (_attackSpeed)
        if (enemiesInRange.Count > 0)
        {
            GameObject closestEnemy = FindClosestEnemy();

            if (closestEnemy != null)
            {
                Attack(closestEnemy);
            }
        }
    }

    private GameObject FindClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject enemy in enemiesInRange)
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

    private void Attack(GameObject enemy)
    {
        // 가장 가까운 적 공격 처리
    }

    private bool IsOtherTeam(ETeam myTeam, ETeam otherTeam)
    {
        return myTeam == otherTeam;
    }
}