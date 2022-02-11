using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Hexfall {
    public class SelectManager : MonoSingleton<SelectManager>
    {
        [SerializeField] private GameObject[] _selectedHexes = new GameObject[3];
        [SerializeField] private Vector3 _rotateDirection;
        [SerializeField] private Vector3 _clockwiseDirection;
        [SerializeField] private Vector3 _counterClockwiseDirection;
        [SerializeField] private Vector3 _selectedCenter;
        [SerializeField] private bool _canRotate;
        [SerializeField] private bool _isClockwise;

        public GameObject[] SelectedHexes {
            get => _selectedHexes;
            set { 
                _selectedHexes = value;
                CalculateCenter();
            }
        }
        public bool IsClockwise { get => _isClockwise; set => _isClockwise = value; }
        public Vector3 SelectedCenter { get => _selectedCenter; private set => _selectedCenter = value; }
        public Vector3 RotateDirection {
            get => _rotateDirection;
            private set { 
                if (value.z == 360 || value.z == -360)
                {
                    _rotateDirection.z = 0;
                }
                else
                {
                    _rotateDirection = value;
                }
            }
        }

        public void StartSelectedOperations()
        {
            SetHexesParent(true);
        }

        private void CalculateCenter()
        {
            Vector3 totalPosition = Vector3.zero;

            for (int i = 0; i < 3; i++)
            {
                totalPosition +=_selectedHexes[i].transform.position;
            }

            SelectedCenter = totalPosition / 3;
        }

        private void SetHexesParent(bool toSelected)
        {
            this.transform.position = _selectedCenter;

            for (int i = 0; i < SelectedHexes.Length; i++)
            {
                SelectedHexes[i].transform.SetParent(toSelected ? this.transform : null);
            }

            if (toSelected)
            {
                StartCoroutine(RotateSelected());
            }
            else
            {
                _selectedHexes = null;
            }
        }

        private IEnumerator RotateSelected()
        {
            int counter = 0;

            RotateDirection = IsClockwise ? _clockwiseDirection : _counterClockwiseDirection;
            
            while (_canRotate && counter < 3)
            {
                this.transform.DORotate(RotateDirection, 0.3f);
                RotateDirection += (IsClockwise ? _clockwiseDirection : _counterClockwiseDirection);
                yield return new WaitForSeconds(0.3f);
                counter++;
            }

            SetHexesParent(false);
        } 
    }
}