using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldCtrl : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{
        
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
        
    //}
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            return;
        }
        Debug.Log("弾が盾に当たった");
        Destroy(other.gameObject);
    }
}
