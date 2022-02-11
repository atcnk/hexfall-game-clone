using UnityEngine;
using Hexfall.Grid;

namespace Hexfall.MouseInput
{
    public class MouseManager : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

                if (rayHit.collider == null)
                {
                    return;
                }

                GridManager.Instance.FindClosestHexes(rayHit.point);

                // Destroy(GridManager.Instance.HexGrid[rayHit.collider.gameObject.name]);

                /* MouseManager destroy etmeyecek, GridManager'a objeyi gonderecek. 
                 *  Oradan destroy edilecek.
                 */
            }
        }
    }
}