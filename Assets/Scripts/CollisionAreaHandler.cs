using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionAreaHandler : MonoBehaviour
{
    [SerializeField]
    private LayerMask _slingShotArea;
    public bool IsClickedWithinSlingShotArea() => Physics2D.OverlapPoint(
                                    Camera.main.ScreenToWorldPoint(
                                        Mouse.current.position.ReadValue()), _slingShotArea);
}
