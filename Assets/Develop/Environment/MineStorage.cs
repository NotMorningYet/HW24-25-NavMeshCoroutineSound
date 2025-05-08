using System.Collections.Generic;
using UnityEngine;

public class MineStorage : MonoBehaviour
{
    [SerializeField] private List<Transform> _mineTransforms = new List<Transform>();
    [SerializeField] private MineFactory _mineFactory;

    public List<Transform> MineTransforms => _mineTransforms;

    private void Awake()
    {
        foreach (Transform mineTransform in _mineTransforms)
            _mineFactory.Create(mineTransform);
    }
}
