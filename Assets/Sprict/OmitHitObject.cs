using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmitHitObject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Object"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce((other.gameObject.transform.position - transform.position) * 5, ForceMode.Impulse);
        }
    }
}
