using UnityEngine;

public class GridCharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gridSize = 1f;
    
    private bool _isMoving;
    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private float _moveTime;
    
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
    }
    
    private void Update()
    {
        if (!_isMoving)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                TryMove(Vector2.up);
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
                TryMove(Vector2.down);
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                TryMove(Vector2.left);
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                TryMove(Vector2.right);
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
    
    private void TryMove(Vector2 direction)
    {
        Vector3 newPosition = transform.position + new Vector3(direction.x * gridSize, direction.y * gridSize, 0);
        
        _startPosition = transform.position;
        _targetPosition = newPosition;
        _isMoving = true;
        _moveTime = 0f;
    }
    
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.grey;
            Vector3 position = transform.position;
            float size = gridSize * 0.9f;
            Vector3 offset = new Vector3(gridSize / 2, gridSize / 2, 0);
            
            for (int x = -2; x <= 2; x++)
            {
                for (int y = -2; y <= 2; y++)
                {
                    Vector3 center = new Vector3(
                        Mathf.Round((position.x - offset.x) / gridSize) * gridSize + x * gridSize,
                        Mathf.Round((position.y - offset.y) / gridSize) * gridSize + y * gridSize,
                        position.z
                    ) + offset;
                    
                    Gizmos.DrawWireCube(center, new Vector3(size, size, 0.1f));
                }
            }
        }
    }
}