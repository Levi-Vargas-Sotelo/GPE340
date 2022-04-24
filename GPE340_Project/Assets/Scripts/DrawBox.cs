using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBox : MonoBehaviour
{
    public Vector3 scale;
    public Color gizmoColor;

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS (transform.position, transform.rotation, Vector3.one);
        Gizmos.color = gizmoColor;
        Gizmos.DrawCube (Vector3.up * scale.y / 2f, scale);
        Gizmos.color = gizmoColor;
        Gizmos.DrawRay (Vector3.zero, Vector3.forward * 0.4f);
    }
}
