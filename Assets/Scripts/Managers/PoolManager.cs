using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager
{
    public IObjectPool<Warrior> _UnitPool;

    private const string unitPrefabPath = "Assets/Prefabs/Warrior.prefab";
    private Warrior _UnitPrefab;

    public void Init()
    {
        _UnitPrefab = UnityEditor.AssetDatabase.LoadAssetAtPath<Warrior>(unitPrefabPath);
        _UnitPool = new ObjectPool<Warrior>(CreateUnit, OnGetUnit, OnReleaseUnit, OnDestroyUnit);
    }

    public void Clear()
    {
        _UnitPool = null;
    }

    private Warrior CreateUnit()
    {
        Warrior unit = MonoBehaviour.Instantiate(_UnitPrefab).GetComponent<Warrior>();
        unit.SetManagedPool(_UnitPool);
        return unit;
    }

    private void OnGetUnit(Warrior unit)
    {
        unit.gameObject.SetActive(true);
    }

    private void OnReleaseUnit(Warrior unit)
    {
        unit.gameObject.SetActive(false);
    }

    private void OnDestroyUnit(Warrior unit)
    {
        MonoBehaviour.Destroy(unit.gameObject);
    }
}