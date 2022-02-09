using UnityEngine;
using System.Collections.Generic;

namespace Hexfall.Grid
{
    public class GridManager : MonoSingleton<GridManager>
    {
        [SerializeField] private GridSettings _gridSettings;
        [SerializeField] private ObjectPool _objectPool;

        private Dictionary<Vector2, GameObject> _hexGrid = new Dictionary<Vector2, GameObject>();
        private Dictionary<Vector2, Vector3> _hexPositions = new Dictionary<Vector2, Vector3>();

        [SerializeField] private float _xPosOffset;
        [SerializeField] private float _yPosOffset;

        private void Start()
        {
            StartGridOperations();
        }
        private void StartGridOperations()
        {
            for (int y = 0; y < _gridSettings.Height; y++)
            {
                for (int x = 0; x < _gridSettings.Width; x++)
                {
                    Vector2 hexKey = GenerateHexKey(x, y);
                    Vector3 hexPos = GenerateHexPosition(x, y);

                    InitializeHexGrid(hexKey);
                    InitializeHexPositions(hexKey, hexPos);
                    PositionGrid(hexKey, hexPos); 
                }
            }
        }

        private void InitializeHexGrid(Vector2 key)
        {
            GameObject pooledGO = _objectPool.GetPooledObject();
            _hexGrid.Add(key, pooledGO);
        }

        private void InitializeHexPositions(Vector2 key, Vector3 position)
        {
            _hexPositions.Add(key, position);
        }

        private void PositionGrid(Vector2 key, Vector3 position)
        {
            _hexGrid[key].transform.position = position;
            _hexGrid[key].SetActive(true);
        }

        private Vector3 GenerateHexPosition(int x, int y)
        {
            Vector3 hexPos = new Vector3();

            hexPos.x = x * _xPosOffset;
            hexPos.y = (x % 2 == 0 ? 0f : -(_yPosOffset / 2)) + (y * _yPosOffset);

            return hexPos;
        }

        private Vector2 GenerateHexKey(int x, int y)
        {
            Vector2 hexKey = new Vector2();

            hexKey.x = x;
            hexKey.y = y;

            return hexKey;
        }
    }
}