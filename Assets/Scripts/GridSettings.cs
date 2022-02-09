using UnityEngine;

namespace Hexfall.Grid
{
    [CreateAssetMenu(menuName = "Hexfall/Grid/Settings")]

    public class GridSettings : ScriptableObject
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;

        public int Width { get => _width; }
        public int Height { get => _height; }   
    }
}