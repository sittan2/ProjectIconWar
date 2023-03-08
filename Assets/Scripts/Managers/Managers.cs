using System.Collections;
using UnityEditor;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;

    public static SettingData Setting => Instance.GetSettingData();
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

    SettingData GetSettingData()
    {
        if (_settingData == null)
        {
            _settingData = AssetDatabase.LoadAssetAtPath<SettingData>(SettingDataPath);
        }
        return _settingData;
    }
}