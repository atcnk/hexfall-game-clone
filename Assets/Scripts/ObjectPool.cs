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
        [SerializeField] private int _sizeOffset;

        private void Start()
        {
            InitializePool();
        }

        public GameObject GetPooledObject()
        {
            GameObject hexGO = pooledHexes.Dequeue();

            pooledHexes.Enqueue(hexGO);

            return hexGO;
        }

        private void InitializePool()
        {
            int poolSize = (_gridSettings.Width * _gridSettings.Height) + _sizeOffset;
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