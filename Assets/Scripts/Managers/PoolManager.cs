using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager
{
    public struct Pool
    {
        public IObjectPool<Warrior> objectPool;
        public readonly string objectPath;
        public Warrior unit;
    }

    public IObjectPool<Warrior> _warriorPool;
    public IObjectPool<Warrior> _archerPool;
    public IObjectPool<Warrior> _shielderPool;
    public IObjectPool<Warrior> _knightPool;

    private const string warriorPath = "Assets/Prefabs/Unit/Warrior.prefab";
    private const string archerPath = "Assets/Prefabs/Unit/Archer.prefab";
    private const string shielderPath = "Assets/Prefabs/Unit/Shielder.prefab";
    private const string knightPath = "Assets/Prefabs/Unit/Knight.prefab";

    private Warrior _warrior;
    private Warrior _archer;
    private Warrior _shielder;
    private Warrior _knight;

    public void Init()
    {
        _warrior = UnityEditor.AssetDatabase.LoadAssetAtPath<Warrior>(warriorPath);
        _archer = UnityEditor.AssetDatabase.LoadAssetAtPath<Warrior>(archerPath);
        _shielder = UnityEditor.AssetDatabase.LoadAssetAtPath<Warrior>(shielderPath);
        _knight = UnityEditor.AssetDatabase.LoadAssetAtPath<Warrior>(knightPath);

        _warriorPool = new ObjectPool<Warrior>(() => CreateUnit(EUnitType.Warrior), OnGetUnit, OnReleaseUnit, OnDestroyUnit);
        _archerPool = new ObjectPool<Warrior>(() => CreateUnit(EUnitType.Archer), OnGetUnit, OnReleaseUnit, OnDestroyUnit);
        _shielderPool = new ObjectPool<Warrior>(() => CreateUnit(EUnitType.Shielder), OnGetUnit, OnReleaseUnit, OnDestroyUnit);
        _knightPool = new ObjectPool<Warrior>(() => CreateUnit(EUnitType.Knight), OnGetUnit, OnReleaseUnit, OnDestroyUnit);
    }

    public void Clear()
    {
        _warriorPool = null;
    }

    private Warrior CreateUnit(EUnitType unitType)
    {
        Warrior unit;

        switch (unitType)
        {
            case EUnitType.none:
            default:
                unit = MonoBehaviour.Instantiate(_warrior).GetComponent<Warrior>();
                break;

            case EUnitType.Warrior:
                unit = MonoBehaviour.Instantiate(_warrior).GetComponent<Warrior>();
                unit.SetManagedPool(_warriorPool);
                break;

            case EUnitType.Archer:
                unit = MonoBehaviour.Instantiate(_archer).GetComponent<Warrior>();
                unit.SetManagedPool(_archerPool);
                break;

            case EUnitType.Shielder:
                unit = MonoBehaviour.Instantiate(_shielder).GetComponent<Warrior>();
                unit.SetManagedPool(_shielderPool);
                break;

            case EUnitType.Knight:
                unit = MonoBehaviour.Instantiate(_knight).GetComponent<Warrior>();
                unit.SetManagedPool(_knightPool);
                break;
        }

        unit.transform.position = new Vector3(-1000f, -1000f);
        return unit;
    }

    private void OnGetUnit(Warrior unit)
    {
        unit.gameObject.SetActive(true);
    }

    private void OnReleaseUnit(Warrior unit)
    {
        unit.gameObject.SetActive(false);
        unit.transform.position = new Vector3(-1000f, -1000f);
    }

    private void OnDestroyUnit(Warrior unit)
    {
        MonoBehaviour.Destroy(unit.gameObject);
    }
}