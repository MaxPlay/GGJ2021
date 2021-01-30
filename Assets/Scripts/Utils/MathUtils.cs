using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtils
{
    public static Vector3 Vector3Lerp(Vector3 a, Vector3 b, float t)
    {
        return Vector3.right * Mathf.Lerp(a.x, b.x, t) + Vector3.up * Mathf.Lerp(a.y, b.y, t) + Vector3.forward * Mathf.Lerp(a.z, b.z, t);
    }
}
