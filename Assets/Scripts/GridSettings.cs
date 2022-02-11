using UnityEngine;
using System.Collections.Generic;

namespace Hexfall.Grid
{
    [CreateAssetMenu(menuName = "Hexfall/Grid/Settings")]

    public class GridSettings : ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private List<Color> _hexesColors = new List<Color>();

        public int Width { get => _width; }
        public int Height { get => _height; }
        public List<Color> HexesColors { get => _hexesColors; }
    }
}