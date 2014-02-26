using UnityEngine;
using System.Collections;

public class CollisionUtils : MonoBehaviour {

	public static bool IsCollidingRight(Transform transform, float distance, int layerMask)
	{
		Debug.DrawRay(transform.position, 
		              transform.position + new Vector3(distance, 0.0f, 0.0f), Color.green);
		Vector2 transPos = new Vector2(transform.position.x, transform.position.y);
		return Physics2D.Linecast(transPos, transPos + new Vector2(distance, 0.0f), layerMask);
	}

	public static bool IsCollidingDown(Transform transform, float distance, int layerMask)
	{
		//		Debug.DrawRay(transform.position, 
		//		              transform.position + new Vector3(0.0f, -distance, 0.0f));
		Vector2 transPos = new Vector2(transform.position.x, transform.position.y);
		return Physics2D.Linecast(transPos, transPos + new Vector2(0.0f, -distance), layerMask);
	}
}
