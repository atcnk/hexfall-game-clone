using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

namespace Hexfall.Grid
{
    public class GridManager : MonoSingleton<GridManager>
    {
        [SerializeField] private GridSettings _gridSettings;
        [SerializeField] private ObjectPool _objectPool;

        private Dictionary<string, GameObject> _hexGrid = new Dictionary<string, GameObject>();
        private Dictionary<string, Vector3> _gridPositions = new Dictionary<string, Vector3>();
        private Dictionary<string, float> _distances = new Dictionary<string, float>();

        [SerializeField] private float _xPosOffset;
        [SerializeField] private float _yPosOffset;

        public Dictionary<string, GameObject> HexGrid { get => _hexGrid; }

        private void Start()
        {
            StartGridOperations();
        }
        private void StartGridOperations()
        {
            string hexKey;
            Vector3 hexPos;

            for (int y = 0; y < _gridSettings.Height; y++)
            {
                for (int x = 0; x < _gridSettings.Width; x++)
                {
                    hexKey = x.ToString() + y.ToString();
                    hexPos = GetHexPosition(x, y);

                    InitializeHexGrid(hexKey);
                    InitializeGridPositions(hexKey, hexPos);
                }
            }

            StartCoroutine(PlaceGrid());
        }

        public void FindClosestHexes(Vector3 clickPos)
        {
            string tempKey;
            GameObject[] minDistanceGO = new GameObject[3];

            foreach (KeyValuePair<string, GameObject> hex in _hexGrid)
            {
                _distances.Add(hex.Key, Vector3.Distance(hex.Value.transform.position, clickPos));
            }

            for (int i = 0; i < 3; i++)
            {
                 tempKey = _distances.FirstOrDefault(x => x.Value == _distances.Values.Min()).Key;
                 minDistanceGO[i] = _hexGrid[tempKey];
                _distances.Remove(tempKey);
            }

            SelectManager.Instance.SelectedHexes = minDistanceGO;
            SelectManager.Instance.StartSelectedOperations();

            _distances.Clear();
        }

        private void InitializeHexGrid(string key)
        {
            GameObject pooledGO = _objectPool.GetPooledObject();
            pooledGO.GetComponent<SpriteRenderer>().color = _gridSettings.HexesColors[Random.Range(0,5)];
            pooledGO.name = key;
            
            HexGrid.Add(key, pooledGO);
        }

        private void InitializeGridPositions(string key, Vector3 position)
        {
            _gridPositions.Add(key, position);
        }

        private IEnumerator PlaceGrid()
        {
            string hexKey;
            Vector3 hexPos;

            for (int y = 0; y < _gridSettings.Height; y++)
            {
                for (int x = 0; x < _gridSettings.Width; x++)
                {
                    hexKey = x.ToString() + y.ToString();
                    hexPos = _gridPositions[hexKey];

                    HexGrid[hexKey].SetActive(true);
                    HexGrid[hexKey].transform.DOMove(hexPos, 0.2f);

                    yield return new WaitForSeconds(0.025f);
                }
            }
        }

        private Vector3 GetHexPosition(int x, int y)
        {
            Vector3 hexPos = new Vector3();

            hexPos.x = x * _xPosOffset;
            hexPos.y = (x % 2 == 0 ? 0f : -(_yPosOffset / 2)) + (y * _yPosOffset);
            
            return hexPos;
        }
    }
}