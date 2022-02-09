using UnityEngine;

namespace Hexfall.Grid
{
    public class GridManager : MonoSingleton<GridManager>
    {
        [SerializeField] private GridSettings _gridSettings;
        private void Start()
        {
            
        }
    }
}