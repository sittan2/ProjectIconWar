using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager
{
    public IObjectPool<Unit> _UnitPool;

    private const string unitPrefabPath = "Assets/Prefabs/Unit.prefab";
    private Unit _UnitPrefab;

    public void Init()
    {
        _UnitPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<Unit>(unitPrefabPath);
        _UnitPool = new ObjectPool<Unit>(CreateUnit, OnGetUnit, OnReleaseUnit, OnDestroyUnit);
    }

    public void Clear()
    {
        _UnitPool = null;
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