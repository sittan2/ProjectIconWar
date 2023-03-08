using System.Collections;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;

    public static PoolManager Pool => Instance._pool;

    private PoolManager _pool = new PoolManager();

    [Header("Setting")]
    [SerializeField] public Color playerColor;
    [SerializeField] public Color enemyColor;
    [SerializeField] public Color noneColor;

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        if (Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            Instance = go.GetComponent<Managers>();

            _pool.Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

}