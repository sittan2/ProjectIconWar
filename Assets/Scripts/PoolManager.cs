using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Pool;

    public IObjectPool<Unit> _UnitPool;
    [SerializeField]
    private Unit _UnitPrefab;
    [SerializeField]
    public Color playerColor;
    [SerializeField]
    public Color enemyColor;
    [SerializeField]
    public Color noneColor;

    private void Awake()
    {
        if (Pool == null)
        {
            Pool = this;
            DontDestroyOnLoad(Pool);
        }
        else
        {
            Destroy(gameObject);
        }

        _UnitPool = new ObjectPool<Unit>(CreateUnit, OnGetUnit, OnReleaseUnit, OnDestroyUnit);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private Unit CreateUnit()
    {
        Unit unit = Instantiate(_UnitPrefab).GetComponent<Unit>();
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
        Destroy(unit.gameObject);
    }
}