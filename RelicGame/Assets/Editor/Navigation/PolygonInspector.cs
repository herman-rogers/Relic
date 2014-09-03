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
        polygon.polygonCorners = polyCorners;
        CheckForResize( polygon, polyCorners, numberOfCorners );
    }

    public void OnSceneGUI( ) {
        Polygon polygon = ( Polygon )target;
        Vector2[ ] polygonCorners = polygon.polygonCorners;
        int j = 1;
        for( int i = 0; i < polygonCorners.Length; i++, j++ ) {
            if( j >= polygonCorners.Length ) {
                j = 0;//TODO: Figure out better way of looping this.
            }
            DrawPoint( new Vector3( polygonCorners[ i ].x, polygonCorners[ i ].y ) );
            Handles.color = Color.red;
            Handles.DrawLine( new Vector3( polygonCorners[ i ].x, polygonCorners[ i ].y ) ,
                new Vector3( polygonCorners[ j ].x, polygonCorners[ j ].y ) );
        }
        SceneView.RepaintAll( );
    }

    void DrawPoint( Vector3 point ) {
        Handles.color = Color.yellow;
        Vector3 pointDrawTool = new Vector3( 0.0f, 0.05f, 0.0f );
        Vector3 pointDrawTool2 = new Vector3( 0.05f, 0.0f, 0.0f );
        Handles.DrawLine( point - pointDrawTool2, point + pointDrawTool2 );
        Handles.DrawLine( point - pointDrawTool, point + pointDrawTool );
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
