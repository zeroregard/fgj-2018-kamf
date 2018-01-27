using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboExplode : MonoBehaviour
{
    void Start()
    {
        var meshes = GetComponentInChildren<MeshRenderer>();
    }

    private void ExplodePart(GameObject g)
    {
        g.transform.SetParent(null);
        if (g.GetComponent<Rigidbody>() == null)
        {
            var rb = g.AddComponent<Rigidbody>();
            g.AddComponent<MeshCollider>();
        }
    }


}
