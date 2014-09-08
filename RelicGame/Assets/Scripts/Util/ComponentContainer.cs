using UnityEngine;

public class ComponentContainer< T > where T : MonoBehaviour {

    private T _stashedObject;

    public T stashedObject {
        get {            
            return _stashedObject ?? ( FindObject( ) );
        }
    }

    private T FindObject( ) {
        return GameObject.FindObjectOfType< T >( );
    }
}
