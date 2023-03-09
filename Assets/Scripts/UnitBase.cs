using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class UnitBase : MonoBehaviour
{
    public float _maxDegreeToCapture = 100f;
    public float _curDegreeToCapture = 0;
    public float _captureRange = 1f;
    public List<Unit> _caputureUnits = new List<Unit>();

    public float _spawnTime = 4f;
    private float _restSpawnTime;
    public float _spawnBound = 1f;

    private SpriteRenderer spriteRenderer;
    public ETeam team = ETeam.None;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetColor();
    }

    private void Start()
    {
        SpawnUnit();
        SpawnUnit();
        SpawnUnit();
        SpawnUnit();
        SpawnUnit();

        _maxDegreeToCapture = _curDegreeToCapture / 2;
    }

    void Update()
    {
        CheckSpawnTime();
        CalculateCaptureDegree();
    }

    void CheckSpawnTime()
    {
        if (team == ETeam.None) return;
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
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _captureRange);
        ETeam unitTeam = ETeam.None;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Unit") &&
                colliders[i].TryGetComponent(out Unit unit))
            {
                if (unitTeam == ETeam.None)
                {
                    unitTeam = unit.team;
                    continue;
                }

                if (unitTeam != unit.team)
                {
                    return;
                }
            }
        }

        if (unitTeam == ETeam.None) return;
        if (team == unitTeam) return;
        team = unitTeam;
        SetColor();
    }

    void SpawnUnit()
    {
        Unit unit = Managers.Pool._UnitPool.Get();
        unit.team = team;

        Vector2 spawnPosition = transform.position;
        spawnPosition.x += Random.Range(-_spawnBound, _spawnBound);
        spawnPosition.y += Random.Range(-_spawnBound, _spawnBound);
        unit.transform.position = spawnPosition;

        unit.Init();
    }

    void SetColor()
    {
        spriteRenderer.color = Util.GetTeamColor(team).BackColor;
    }

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().color = Util.GetTeamColor(team).BackColor;
    }
}