using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlingshotHandler : MonoBehaviour
{
    [Header("Line Renderers")]
    [SerializeField]
    private LineRenderer _leftLineRenderer;
    [SerializeField]
    private LineRenderer _rightLineRenderer;

    [Header("Slingshot positions")]
    [SerializeField]
    private Transform _stripeStartLeft;
    [SerializeField]
    private Transform _stripeStartRight;
    [SerializeField]
    private Transform _idlePosition;
    [SerializeField]
    private Transform _centerPosition;

    [Header("Method callers")]
    [SerializeField]
    private CollisionAreaHandler _slingShotArea;

    private Vector3 _slingShotLinesPosition;

    private const float MAX_DISTANCE = 3.5f;

    private bool _clickedWithinArea = false;

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _clickedWithinArea = _slingShotArea.IsWithinArea();
            Debug.Log(_clickedWithinArea);
        }

        if (Mouse.current.leftButton.isPressed && _clickedWithinArea)
        {
            DrawSlingshot();
        } 
        
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            SetLines(_idlePosition.position);
            _clickedWithinArea = false;
        }
    }

    private void DrawSlingshot()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _slingShotLinesPosition = _centerPosition.position + Vector3.ClampMagnitude(touchPosition - _centerPosition.position, MAX_DISTANCE);
        SetLines(_slingShotLinesPosition);
    }

    private void SetLines(Vector2 position)
    {
        Debug.Log(_stripeStartLeft.position);
        Debug.Log(_stripeStartRight.position);
        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _stripeStartLeft.position);

        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _stripeStartRight.position);
    }
}