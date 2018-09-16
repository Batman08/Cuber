using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject DestroyedVersion;

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(DestroyedVersion, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
