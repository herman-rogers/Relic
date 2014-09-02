using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor ( typeof ( Polygon ) )]
public class PolygonInspector : Editor {
    
    public override void OnInspectorGUI( ) {
        Polygon polygon = ( Polygon ) target;

        Vector2[ ] polyCorners = polygon.polygonCorners;
        int numberOfCorners = EditorGUILayout.IntField( "Number Of Corners", polyCorners.Length );

        for( int num = 0; num < polyCorners.Length; num++ ) {
            polyCorners[ num ] = EditorGUILayout.Vector2Field( "Corner " + num.ToString( ), polyCorners[ num ] );
        }

        CheckForResize( polygon, polyCorners, numberOfCorners );
    }

    private static void CheckForResize( Polygon polygon, Vector2[ ] polyCorners, int numberOfCorners ) {
        if( numberOfCorners > polyCorners.Length ) {
            Vector2[ ] newCorners = new Vector2[ numberOfCorners ];
            polyCorners.CopyTo( newCorners, 0 );
            polygon.polygonCorners = newCorners;
        } else if( numberOfCorners < polyCorners.Length ) {
            Vector2[ ] newCorners = new Vector2[ numberOfCorners ];
            for( int i = 0; i < numberOfCorners; i++ ) {
                newCorners[ i ] = polyCorners[ i ];
            }
            polygon.polygonCorners = newCorners;
        }
    }

}
