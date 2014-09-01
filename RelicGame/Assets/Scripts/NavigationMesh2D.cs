using UnityEngine;
using System.Collections;

public class NavigationMesh2D : MonoBehaviour {
    

    public Vector3[ ] walkableArea;

    public bool CanMoveTo( Vector3 worldCoordinates ) {
        if( worldCoordinates.x < walkableArea[ 0 ].x ) {//0 is left most.
            return false;
        }
        if( worldCoordinates.x > walkableArea[ 1 ].x ) {//1 is right most.
            return false;
        }
        if( worldCoordinates.y < walkableArea[ 2 ].y ) {//2 is top.
            return false;
        }
        if( worldCoordinates.y > walkableArea[ 3 ].y ) {//3 is down.
            return false;
        }
        return true;
    }

    void Update(  ) {
        Debug.DrawLine( Vector3.zero, walkableArea[ 0 ], Color.red );
        Debug.DrawLine( Vector3.zero, walkableArea[ 1 ], Color.red );
        Debug.DrawLine( Vector3.zero, walkableArea[ 2 ], Color.green );
        Debug.DrawLine( Vector3.zero, walkableArea[ 3 ], Color.green );
    }

}
