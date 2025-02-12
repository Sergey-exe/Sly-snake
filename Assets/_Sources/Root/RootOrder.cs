using System.Collections.Generic;
using _Sources.Root;
using UnityEngine;

public class RootOrder : MonoBehaviour
{
    [SerializeField] private List<Root> _roots;

    private void Awake()
    {
        foreach (var root in _roots)
        {
            root.Init();
            root.enabled = true;
        }
    }
}
