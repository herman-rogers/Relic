using UnityEngine;
using System.Collections;

public class NavigationMesh2D : MonoBehaviour {

    public Polygon[ ] polygons;

    public bool CanMoveTo( Vector3 worldCoordinates ) {
        foreach( Polygon poly in polygons ) {
            if( poly.PointInPoly( new Vector2( worldCoordinates.x, worldCoordinates.y ) ) ) {
                return true;
            }
        }
        return false;
    }

}
