using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class UnitBase : MonoBehaviour
{
    private IObjectPool<Unit> _Pool;
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
    }

    void SetColor()
    {
        switch (team)
        {
            case ETeam.None:
                spriteRenderer.color = Managers.Setting.noneColor;
                break;

            case ETeam.Player:
                spriteRenderer.color = Managers.Setting.playerColor;
                break;

            case ETeam.Enemy:
                spriteRenderer.color = Managers.Setting.enemyColor;
                break;

        }
    }
}