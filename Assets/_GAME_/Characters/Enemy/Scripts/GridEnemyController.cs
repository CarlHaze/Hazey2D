using UnityEngine;

public class GridEnemyController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float gridSize = 1f;
    [SerializeField] private float moveDelay = 1f;
    [SerializeField] private Vector2Int movementBounds = new Vector2Int(5, 5);

    private bool _isMoving;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private float _moveTime;
    private float _nextMoveTime;
    private Vector3 _originPosition;

    private void Start()
    {
        // Snap initial position to grid with offset to center on tiles
        Vector3 offset = new Vector3(gridSize / 2, gridSize / 2, 0);
        transform.position = new Vector3(
            Mathf.Round(transform.position.x / gridSize) * gridSize,
            Mathf.Round(transform.position.y / gridSize) * gridSize,
            transform.position.z
        ) + offset;

        _targetPosition = transform.position;
        _startPosition = transform.position;
        _originPosition = transform.position;
        _nextMoveTime = Time.time + moveDelay;
    }

    private void Update()
    {
        if (!_isMoving)
        {
            if (Time.time >= _nextMoveTime)
            {
                TryRandomMove();
                _nextMoveTime = Time.time + moveDelay;
            }
        }
        else
        {
            _moveTime += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(_startPosition, _targetPosition, _moveTime);

            if (_moveTime >= 1f)
            {
                transform.position = _targetPosition;
                _isMoving = false;
                _moveTime = 0f;
            }
        }
    }

    private void TryRandomMove()
    {
        Vector2[] directions = { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
        Vector2 randomDirection = directions[Random.Range(0, directions.Length)];
        
        Vector3 newPosition = transform.position + new Vector3(randomDirection.x * gridSize, randomDirection.y * gridSize, 0);

        if (IsWithinBounds(newPosition))
        {
            _startPosition = transform.position;
            _targetPosition = newPosition;
            _isMoving = true;
            _moveTime = 0f;
        }
    }

    private bool IsWithinBounds(Vector3 position)
    {
        Vector3 offset = new Vector3(gridSize / 2, gridSize / 2, 0);
        position -= offset; // Remove offset for bounds checking
        _originPosition -= offset;

        float maxX = _originPosition.x + (movementBounds.x * gridSize / 2);
        float minX = _originPosition.x - (movementBounds.x * gridSize / 2);
        float maxY = _originPosition.y + (movementBounds.y * gridSize / 2);
        float minY = _originPosition.y - (movementBounds.y * gridSize / 2);

        _originPosition += offset; // Restore offset
        return position.x >= minX && position.x <= maxX && 
               position.y >= minY && position.y <= maxY;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Vector3 offset = new Vector3(gridSize / 2, gridSize / 2, 0);
            Vector3 position = transform.position;
            position -= offset; // Remove offset for visualization

            // Draw movement area bounds
            Gizmos.color = Color.red;
            Vector3 size = new Vector3(movementBounds.x * gridSize, movementBounds.y * gridSize, 0.1f);
            Gizmos.DrawWireCube(position + (size / 2), size);

            // Draw grid within bounds
            Gizmos.color = Color.grey;
            float boundsMinX = position.x;
            float boundsMinY = position.y;

            for (int x = 0; x < movementBounds.x; x++)
            {
                for (int y = 0; y < movementBounds.y; y++)
                {
                    Vector3 cellCenter = new Vector3(
                        boundsMinX + (x * gridSize),
                        boundsMinY + (y * gridSize),
                        position.z
                    ) + offset;
                    Gizmos.DrawWireCube(cellCenter, new Vector3(gridSize * 0.9f, gridSize * 0.9f, 0.1f));
                }
            }
        }
    }
}