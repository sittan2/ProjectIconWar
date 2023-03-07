using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class UnitBase : MonoBehaviour
{
    private IObjectPool<Unit> _Pool;
    public float _spawnTime = 4f;
    private float _restSpawnTime;

    public ETeam team = ETeam.None;

    void Start()
    {

    }

    void Update()
    {
        CheckSpawnTime();
    }

    void CheckSpawnTime()
    {
        if (_restSpawnTime > 0)
        {
            _restSpawnTime -= Time.deltaTime;
        }
        else
        {
            _restSpawnTime += _spawnTime;
            SpawnUnit();
        }
    }

    void SpawnUnit()
    {
        Unit unit = PoolManager.Pool._UnitPool.Get();
        unit.team = team;

        Vector2 spawnPosition = transform.position;
        spawnPosition.x += Random.Range(-1, 1);
        spawnPosition.y += Random.Range(-1, 1);

        unit.transform.position = spawnPosition;
    }
}