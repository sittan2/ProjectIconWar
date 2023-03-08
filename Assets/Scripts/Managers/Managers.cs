using System.Collections;
using UnityEditor;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;

    public static SettingData Setting => Instance.GetSettingData();
    public static PoolManager Pool => Instance._pool;

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

            _pool.Init();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    SettingData GetSettingData()
    {
        return AssetDatabase.LoadAssetAtPath<SettingData>("Assets/Data/SettingData.asset");
    }

}