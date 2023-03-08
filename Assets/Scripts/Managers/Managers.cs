using System.Collections;
using UnityEditor;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers Instance;

    public static SettingData Setting => GetSettingData();
    private static string SettingDataPath = "Assets/Data/SettingData.asset";
    public static GameManager Game => Instance._game;
    public static PoolManager Pool => Instance._pool;

    private GameManager _game = new GameManager();
    private PoolManager _pool = new PoolManager();
    private SettingData _settingData;

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

            _game.Init();
            _pool.Init();
            GetSettingData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    static SettingData GetSettingData()
    {
        if (Instance == null)
            return AssetDatabase.LoadAssetAtPath<SettingData>(SettingDataPath);
        else
        {
            if (Instance._settingData == null)
            {
                Instance._settingData = AssetDatabase.LoadAssetAtPath<SettingData>(SettingDataPath);
            }
            return Instance._settingData;
        }
    }
}