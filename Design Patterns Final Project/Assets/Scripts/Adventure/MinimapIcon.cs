using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapIcon : MonoBehaviour
{
    [SerializeField] private GameObject _minimapIconPrefab;

    void Start()
    {
        MinimapManager.Instance.Register(gameObject, _minimapIconPrefab);
    }
}
