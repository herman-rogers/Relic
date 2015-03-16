using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Navigation{

    public class NavigationMesh2D : MonoBehaviour {
    
        public bool CanMoveTo( Vector3 worldCoordinates ){
            return this.GetComponentsInChildren< Polygon >( )
                .Any( poly => poly.PointInPoly( new Vector2( worldCoordinates.x, worldCoordinates.y ) ) );
        }

    }

}
