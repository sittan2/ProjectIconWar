using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class UnitBase : MonoBehaviour
{
    public float _spawnTime = 4f;
    private float _restSpawnTime;


    private SpriteRenderer spriteRenderer;
    public ETeam team = ETeam.None;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
    }

    private void Start()
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
        Unit unit = Managers.Pool._UnitPool.Get();
        unit.team = team;

        Vector2 spawnPosition = transform.position;
        spawnPosition.x += Random.Range(0, 2);
        spawnPosition.y += Random.Range(0, 2);
        unit.transform.position = spawnPosition;

        unit.Init();
    }

    void SetColor()
    {
        spriteRenderer.color = Util.GetColor(team);
    }

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().color = Util.GetColor(team);
    }
}