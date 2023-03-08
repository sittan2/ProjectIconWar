using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager
{
    public IObjectPool<Unit> _UnitPool;
    [SerializeField]
    private Unit _UnitPrefab;

    public void Init()
    {
        _UnitPool = new ObjectPool<Unit>(CreateUnit, OnGetUnit, OnReleaseUnit, OnDestroyUnit);
    }

    private Unit CreateUnit()
    {
        Unit unit = MonoBehaviour.Instantiate(_UnitPrefab).GetComponent<Unit>();
        unit.SetManagedPool(_UnitPool);
        return unit;
    }

    private void OnGetUnit(Unit unit)
    {
        unit.gameObject.SetActive(true);
    }

    private void OnReleaseUnit(Unit unit)
    {
        unit.gameObject.SetActive(false);
    }

    private void OnDestroyUnit(Unit unit)
    {
        MonoBehaviour.Destroy(unit.gameObject);
    }
}