using UnityEngine;

namespace Items.Weapons
{
    public class GridWeaponSpawner : MonoBehaviour
    {
        [SerializeField] private WeaponTemplate weaponToSpawn;
        [SerializeField] private int numberToSpawn = 3;
        [SerializeField] private float gridSize = 1f;
        [SerializeField] private Vector2Int spawnBounds = new Vector2Int(5, 5); // Area to spawn within

        private void Start()
        {
            SpawnWeapons();
        }

        public void SpawnWeapons()
        {
            for (int i = 0; i < numberToSpawn; i++)
            {
                Vector3 spawnPosition = GetRandomGridPosition();
                SpawnWeaponAtPosition(spawnPosition);
            }
        }

        private Vector3 GetRandomGridPosition()
        {
            // Calculate the bounds relative to the spawner's position
            Vector3 offset = new Vector3(gridSize / 2, gridSize / 2, 0);
            Vector3 basePosition = transform.position - (new Vector3(spawnBounds.x, spawnBounds.y, 0) * gridSize / 2);

            // Get random grid coordinates within bounds
            int randomX = Random.Range(0, spawnBounds.x);
            int randomY = Random.Range(0, spawnBounds.y);

            // Calculate world position
            Vector3 position = basePosition + new Vector3(
                randomX * gridSize,
                randomY * gridSize,
                0
            );

            // Add offset to center on grid
            return position + offset;
        }

        private void SpawnWeaponAtPosition(Vector3 position)
        {
            GameObject weaponInstance = Instantiate(weaponToSpawn.weaponPrefab, position, Quaternion.identity);
            
            if (weaponInstance.TryGetComponent<Weapon>(out var weaponComponent))
            {
                weaponComponent.Initialize(weaponToSpawn, WeaponRarity.Common, null);
            }
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
            {
                Vector3 offset = new Vector3(gridSize / 2, gridSize / 2, 0);
                Vector3 position = transform.position;
                position -= offset; // Remove offset for visualization

                // Draw spawn area bounds
                Gizmos.color = Color.yellow;
                Vector3 size = new Vector3(spawnBounds.x * gridSize, spawnBounds.y * gridSize, 0.1f);
                Gizmos.DrawWireCube(position + (size / 2), size);

                // Draw grid within bounds
                Gizmos.color = Color.grey;
                float boundsMinX = position.x;
                float boundsMinY = position.y;

                for (int x = 0; x < spawnBounds.x; x++)
                {
                    for (int y = 0; y < spawnBounds.y; y++)
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
}