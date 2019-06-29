using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SafeArea : MonoBehaviour {
    
    void Start() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        gameObject.AddComponent<MeshCollider>();
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }

}
