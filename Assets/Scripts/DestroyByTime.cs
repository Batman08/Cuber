using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float Time;

    private readonly string DestroyedCrateName = "Wooden_Crate_Cracked(Clone)";
    private Collider[] _colliders;
    private Rigidbody[] _rb;

    private void Awake()
    {
        Destroy(gameObject, Time);
        //Physics.IgnoreLayerCollision(9, 9);
        Physics.IgnoreLayerCollision(9, 10);
    }

    private void Update()
    {
        if (gameObject.name == DestroyedCrateName)
        {
            _colliders = GetComponentsInChildren<Collider>();
            _rb = GetComponentsInChildren<Rigidbody>();

            //foreach (Collider collider in _colliders)
            //{
            //    collider.enabled = false;
            //}

            //foreach (Rigidbody rigidbody in _rb)
            //{
            //    rigidbody.isKinematic = true;
            //    rigidbody.useGravity = false;
            //}
        }
    }
}
