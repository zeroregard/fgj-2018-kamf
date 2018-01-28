using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    public float ConveyorSpeed;
    public MeshRenderer ConveyorMesh;

    private HashSet<Rigidbody> _currentColliders = new HashSet<Rigidbody>();
    // Use this for initialization
    void Start()
    {

    }

    void FixedUpdate()
    {
        ConveyorMesh.material.SetTextureOffset("_MainTex", new Vector2(0, ConveyorSpeed * Time.time));
        foreach (var v in _currentColliders)
        {
            if (v.velocity.magnitude < 1.5f)
            {
                v.AddForce(transform.TransformDirection(transform.forward) * ConveyorSpeed * 3.6f);
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        var rb = col.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            _currentColliders.Add(rb);
        }
    }

    void OnTriggerExit(Collider col)
    {
        var rb = col.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            _currentColliders.Remove(rb);
        }
    }
}
