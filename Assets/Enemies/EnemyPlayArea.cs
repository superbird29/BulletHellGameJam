using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlayArea : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        { 
            Destroy(collision.gameObject); 
        } 
    }

}
