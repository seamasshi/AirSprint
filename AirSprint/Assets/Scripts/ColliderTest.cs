using UnityEngine;
using System.Collections;

public class ColliderTest : MonoBehaviour {


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Coll");
    }

   

}
