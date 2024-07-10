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

    [Header("Bird")]
    [SerializeField]
    private AngryBird _angryBirdPrefab;
    [SerializeField]
    private const float ANGRY_BIRD_OFFSET = 0.275f;


    private AngryBird _spawnedAngryBird;

    private Vector3 _slingShotLinesPosition;

    private const float MAX_DISTANCE = 3.5f;

    private bool _clickedWithinArea = false;

    private Vector2 _direction;
    private Vector2 _directionNormalized;

    private void Awake()
    {
        _leftLineRenderer.enabled = false;
        _rightLineRenderer.enabled = false;
    }

    private void Start()
    {
        SpawnBird();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            _clickedWithinArea = _slingShotArea.IsClickedWithinSlingShotArea();
        }

        if (Mouse.current.leftButton.isPressed && _clickedWithinArea)
        {
            DrawSlingshot();
            DrawBird();
        } 
        
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            //SetLines(_idlePosition.position);
            _clickedWithinArea = false;
            _spawnedAngryBird.Launch(_direction, 5.0f);
        }
    }

    #region Sling Shot Methods
    private void DrawSlingshot()
    {

        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _slingShotLinesPosition = _centerPosition.position + Vector3.ClampMagnitude(touchPosition - _centerPosition.position, MAX_DISTANCE);
        
        SetLines(_slingShotLinesPosition);

        _direction = _centerPosition.position - _slingShotLinesPosition;
        _directionNormalized = _direction.normalized;
    }

    private void SetLines(Vector2 position)
    {
        _leftLineRenderer.SetPosition(0, position);
        _leftLineRenderer.SetPosition(1, _stripeStartLeft.position);
        _rightLineRenderer.SetPosition(0, position);
        _rightLineRenderer.SetPosition(1, _stripeStartRight.position);
    }
    #endregion

    #region Bird
    private void SpawnBird()
    {
        if (!_rightLineRenderer.enabled && !_leftLineRenderer.enabled)
        {
            _rightLineRenderer.enabled = true;
            _leftLineRenderer.enabled = true;
        }

        SetLines(_idlePosition.position);

        Vector2 direction = _centerPosition.position - _idlePosition.position;
        Vector2 spawnPosition = (Vector2)_idlePosition.position + direction * ANGRY_BIRD_OFFSET;

        _spawnedAngryBird = Instantiate(_angryBirdPrefab, spawnPosition, Quaternion.identity);
        _spawnedAngryBird.transform.right = direction;
    }

    private void DrawBird()
    {
        _spawnedAngryBird.transform.position = (Vector2)_slingShotLinesPosition + _directionNormalized * ANGRY_BIRD_OFFSET;
        _spawnedAngryBird.transform.right = _directionNormalized;
    }
    #endregion
}