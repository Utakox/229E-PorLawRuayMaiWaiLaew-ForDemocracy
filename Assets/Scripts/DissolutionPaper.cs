using UnityEngine;

public class DissolutionPaper : MonoBehaviour
{
    [Header("Physics Settings (F = ma)")]
    public float mass = 1.0f;            // มวล (m)
    public float targetAcceleration = 40f; // ความเร่ง (a)

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.mass = mass;

            // คำนวณ F = ma ตามเกณฑ์ส่งงานอาจารย์ (หัวข้อ C)
            float calculatedForce = mass * targetAcceleration;

            // ยิงพุ่งไปทางซ้ายทันที
            rb.AddForce(Vector2.left * calculatedForce, ForceMode2D.Impulse);
        }
    }

    // ฟังก์ชันเช็คการชน
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ถ้าชนวัตถุที่มี Tag ว่า "Player"
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Game Over: โดนยุบพรรค!");
            
            // เรียกฟังก์ชันจบเกมจาก GameManager ของคุณ (ตัวอย่างเช่น)
            // FindObjectOfType<GameManager>().GameOver();
            
            // หรือถ้าจะทดสอบแบบง่ายที่สุดคือหยุดเวลาเกม
            Time.timeScale = 0f; 
        }
        
        // ถ้าชน EndZone ให้ทำลายตัวเอง (ป้องกันการกินสเปคเครื่อง)
        if (collision.CompareTag("EndZone"))
        {
            Destroy(gameObject);
        }
    }
}