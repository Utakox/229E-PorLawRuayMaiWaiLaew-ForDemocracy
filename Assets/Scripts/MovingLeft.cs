using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f; // ความเร็วในการเลื่อนไปทางซ้าย

    void Update()
    {
        // สั่งให้วัตถุเคลื่อนที่ไปทางซ้ายตามเวลาจริง
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ตรวจสอบว่าชนกับวัตถุที่มี Tag ว่า "EndZone" หรือไม่
        if (collision.CompareTag("EndZone"))
        {
            // ทำลายตัวเองเพื่อคืน Memory (หรือถ้าใช้ Object Pooling ก็ส่งกลับ Pool ตรงนี้)
            Destroy(gameObject);
            Debug.Log("Obstacle ถูกกำจัดโดยระบบ (EndZone)");
        }
    }
}