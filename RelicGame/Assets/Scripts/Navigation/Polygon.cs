using UnityEngine;

namespace Assets.Scripts.Navigation{

    public class Polygon : MonoBehaviour {
    
        public Vector2[ ] polygonCorners;
    
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
            for( int i = 0; i < this.polygonCorners.Length; i++, j++ ) {
                if( j >= this.polygonCorners.Length ) {
                    j = 0;//TODO: Figure out better way of looping this.
                }
                if( this.polygonCorners[ i ].y < point.y && this.polygonCorners[ j ].y >= point.y
                    || this.polygonCorners[ j ].y < point.y && this.polygonCorners[ i ].y >= point.y ) {
                    if( this.polygonCorners[ i ].x + ( point.y - this.polygonCorners[ i ].y )
                        / ( this.polygonCorners[ j ].y - this.polygonCorners[ i ].y ) * ( this.polygonCorners[ j ].x - this.polygonCorners[ i ].x ) < point.x ) {
                        oddNumberOfIntersection = !oddNumberOfIntersection;
                    }
                }
            }
            return oddNumberOfIntersection;
        }

    }

}
