using UnityEngine;
using System.Collections.Generic;

public class OverlayPoolManager : MonoBehaviour
{
    public static OverlayPoolManager Instance;

    public GameObject frozenOverlayPrefab;
    public GameObject burnedOverlayPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public GameObject GetOverlayPrefab(DebuffType type)
    {
        switch (type)
        {
            case DebuffType.Frozen: return frozenOverlayPrefab;
            case DebuffType.Burned: return burnedOverlayPrefab;
            default: return null;
        }
    }
}