using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Rotates Object in selected Direction.
public class RotatingObject : MonoBehaviour
{
    public Vector3 rotation;

    void Update()
    {
        transform.Rotate(rotation);
    }
}
