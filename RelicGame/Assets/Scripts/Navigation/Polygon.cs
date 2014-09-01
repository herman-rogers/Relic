using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Polygon : MonoBehaviour {
    
    public Vector2[ ] polygonCorners;

    void Update( ) { 
        Vector2 previousCorner = Vector2.zero;
        int j = 1;
        for( int i = 0; i < polygonCorners.Length; i++, j++ ) {
            if( j >= polygonCorners.Length ) {
                j = 0;//TODO: Figure out better way of looping this.
            }
            DrawPoint( polygonCorners[ i ] );
            Debug.DrawLine( polygonCorners[ i ], polygonCorners[ j ], Color.red );
        }
    }

    void DrawPoint( Vector2 point ) {
        Vector2 pointDrawTool = new Vector3( 0.0f, 0.05f );
        Vector2 pointDrawTool2 = new Vector3( 0.05f, 0.0f );
        Debug.DrawLine( point - pointDrawTool2, point + pointDrawTool2, Color.yellow );
        Debug.DrawLine( point - pointDrawTool, point + pointDrawTool, Color.yellow );
    }

    /// <summary>
    /// Uses Point-In-Polygon Algorithm to determin if a point is within this polygon shape.
    /// The algorithm works by comparing intersections of the polygon on either side of the point.
    /// "...if there are an odd number of nodes on each side of the test point, then it is inside 
    /// the polygon; if there are an even number of nodes on each side of the test point, then it 
    /// is outside the polygon."
    /// http://alienryderflex.com/polygon/
    /// </summary>
    /// <param name="point"> The point to test against </param>
    /// <returns> Will return false if it isn't and true if it is. </returns>
    public bool PointInPoly( Vector2 point ) {
        bool oddNumberOfIntersection = false;
        int j = 1;
        for( int i = 0; i < polygonCorners.Length; i++, j++ ) {
            if( j >= polygonCorners.Length ) {
                j = 0;//TODO: Figure out better way of looping this.
            }
               if( polygonCorners[ i ].y < point.y && polygonCorners[ j ].y >= point.y
                   || polygonCorners[ j ].y < point.y && polygonCorners[ i ].y >= point.y ) {
                    if( polygonCorners[ i ].x + ( point.y - polygonCorners[ i ].y )
                        / ( polygonCorners[ j ].y - polygonCorners[ i ].y ) * ( polygonCorners[ j ].x - polygonCorners[ i ].x ) < point.x ) {
                            oddNumberOfIntersection = !oddNumberOfIntersection;
                    }
               }
        }
        return oddNumberOfIntersection;
    }

}
