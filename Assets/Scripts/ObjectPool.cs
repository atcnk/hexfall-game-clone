using UnityEngine;
using System.Collections.Generic;
using Hexfall.Grid;

namespace Hexfall
{
    public class ObjectPool : MonoBehaviour
    {
        private Queue<GameObject> pooledHexes;
        [SerializeField] private GameObject hexPrefab;
        [SerializeField] private GridSettings _gridSettings;

        private void Start()
        {
            int poolSize = _gridSettings.Width * _gridSettings.Height;
            pooledHexes = new Queue<GameObject>();

            for (int i = 0; i < poolSize; i++)
            {
                GameObject hexGO = Instantiate(hexPrefab);
                hexGO.SetActive(false);

                pooledHexes.Enqueue(hexGO);
            }
        }
    }
}