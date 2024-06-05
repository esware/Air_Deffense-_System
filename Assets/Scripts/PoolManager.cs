using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject targetPrefab;
    public int TargetCount;

    private List<GameObject> targetList = new List<GameObject>();

    private static PoolManager _instance;

    public static PoolManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PoolManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("PoolManager");
                    _instance = obj.AddComponent<PoolManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject); // PoolManager'ı sahne değişikliklerinde yok etmemek için
            CreateTargets();
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void CreateTargets()
    {
        for (int i = 0; i < TargetCount; i++)
        {
            var clone = Instantiate(targetPrefab);
            clone.gameObject.SetActive(false);
            targetList.Add(clone);
        }
    }

    public GameObject GetTarget()
    {
        foreach (var target in targetList)
        {
            if (!target.activeInHierarchy)
            {
                target.SetActive(true);
                return target;
            }
        }
        return null;
    }

    public void ReturnPool(GameObject target)
    {
        target.SetActive(false);
        if (!targetList.Contains(target))
        {
            targetList.Add(target);
        }
    }
}
