using UnityEngine;

public class MovingYObstacle : MonoBehaviour
{
    public float speed = 2f;
    public float moveDistance = 2f;
    private float startY; // เก็บเฉพาะค่า Y เริ่มต้น

    void Start()
    {
        startY = transform.position.y;
    }

    void Update()
    {
        // คำนวณค่า Y ใหม่จากฟังก์ชัน Sin
        float newY = startY + Mathf.Sin(Time.time * speed) * moveDistance;
        
        // อัปเดตตำแหน่งโดยที่ค่า X ปล่อยให้สคริปต์ ObstacleMover เป็นตัวจัดการ
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}