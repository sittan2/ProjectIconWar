using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class UnitBase : MonoBehaviour
{
    public bool _isCaptured = false;            // 점령 되었는가? -> 병력이 생산 되는가.
    public float _maxDegreeToCapture = 100f;
    public float _curDegreeToCapture = 0;
    public List<Unit> _caputureUnits = new List<Unit>();

    public float _spawnTime = 4f;
    private float _restSpawnTime;

    private SpriteRenderer spriteRenderer;
    public ETeam team = ETeam.None;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
        _isCaptured = !(team == ETeam.None);
    }

    private void Start()
    {
        _maxDegreeToCapture = _curDegreeToCapture / 2;
    }

    void Update()
    {
        CheckSpawnTime();
        CalculateCaptureDegree();
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

    void CalculateCaptureDegree()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2f);
        int playerCount = 0, enemyCount = 0;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Unit") &&
                colliders[i].TryGetComponent(out Unit unit))
            {
                if (unit.team == ETeam.Player)
                    playerCount += 1;
                else if (unit.team == ETeam.Enemy)
                    enemyCount += 1;
            }
        }

        if (playerCount > 0 && enemyCount == 0)
        {
            team = ETeam.Player;
            SetColor();
        }
        else if (enemyCount > 0 && playerCount == 0)
        {
            team = ETeam.Enemy;
            SetColor();
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