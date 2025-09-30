    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateBonus : MonoBehaviour
{
    private int _rotateSpeed = 4;
    private void Update()
    {
        transform.Rotate(0, _rotateSpeed, 0, Space.World);     
    }
}
