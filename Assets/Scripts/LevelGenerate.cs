using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public GameObject rampPrefab;
    public GameObject snowflakePrefab;
    public int numObstacles = 10;
    public int numSnowflakes = 5;
    public Vector2 levelSize = new Vector2(50, 100);

    private EdgeCollider2D groundCollider; // Thêm biến này
    private List<Vector3> spawnedPositions = new List<Vector3>(); // Lưu các vị trí đã spawn
    public float minDistanceBetweenObjects = 2.0f; // Khoảng cách tối thiểu giữa các vật thể

    void Start()
    {
        groundCollider = FindObjectOfType<EdgeCollider2D>(); // Tìm Collider của địa hình
        GenerateObstacles();
        GenerateSnowflakes();
    }
    bool IsPositionValid(Vector3 newPos)
    {
        foreach (Vector3 pos in spawnedPositions)
        {
            if (Vector3.Distance(pos, newPos) < minDistanceBetweenObjects)
            {
                return false; // Nếu khoảng cách nhỏ hơn minDistance, không hợp lệ
            }
        }
        return true; // Hợp lệ nếu không có vật nào quá gần
    }

    float GetGroundHeight(float xPos)
    {
        if (groundCollider == null || groundCollider.pointCount == 0)
            return -10f; // Trả về giá trị mặc định nếu không có collider

        Vector2[] points = groundCollider.points; // Lấy danh sách các điểm của EdgeCollider2D

        float closestY = -10f;
        float minDistance = Mathf.Infinity;

        foreach (Vector2 point in points)
        {
            float distance = Mathf.Abs(point.x - xPos); // Tìm điểm có x gần nhất với xPos
            if (distance < minDistance)
            {
                minDistance = distance;
                closestY = point.y; // Lấy giá trị y tương ứng
            }
        }

        return closestY;
    }

    void GenerateObstacles()
    {
        for (int i = 0; i < numObstacles; i++)
        {
            int attempts = 0; // Số lần thử tìm vị trí hợp lệ
            Vector3 position;

            do
            {
                float xPos = Random.Range(-levelSize.x / 2, levelSize.x / 2);
                float yPos = GetGroundHeight(xPos) + 0.5f;
                position = new Vector3(xPos, yPos, 0);
                attempts++;
            }
            while (!IsPositionValid(position) && attempts < 10); // Thử tối đa 10 lần

            // Nếu tìm được vị trí hợp lệ, spawn và lưu vào danh sách
            GameObject obstacle = Instantiate(rampPrefab, position, Quaternion.identity);
            obstacle.transform.SetParent(transform);
            spawnedPositions.Add(position);
        }
    }


    void GenerateSnowflakes()
    {
        for (int i = 0; i < numSnowflakes; i++)
        {
            int attempts = 0;
            Vector3 position;

            do
            {
                float xPos = Random.Range(-levelSize.x / 2, levelSize.x / 2);
                float yPos = GetGroundHeight(xPos) + 2.5f;
                position = new Vector3(xPos, yPos, 0);
                attempts++;
            }
            while (!IsPositionValid(position) && attempts < 10); // Thử tối đa 10 lần

            GameObject snowflake = Instantiate(snowflakePrefab, position, Quaternion.identity);
            snowflake.transform.SetParent(transform);
            spawnedPositions.Add(position);
        }
    }

}
