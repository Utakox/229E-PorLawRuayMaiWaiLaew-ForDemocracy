using UnityEngine;

public class BallotController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float flapForce = 12f;
    public float airDensity = 1.2f;
    public float dragCoefficient = 0.5f;

    private Rigidbody2D rb;

    // ── Zone Air Resistance ──
    private bool inAirResistanceZone = false;
    private float zoneDragCoefficient = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Flap();
        }
    }

    void FixedUpdate()
    {
        // Air Resistance ปกติ (เกิดตลอดเวลา)
        Vector2 velocity = rb.velocity;
        Vector2 dragForce = -velocity * (airDensity * dragCoefficient);
        rb.AddForce(dragForce);

        // Air Resistance พิเศษ (เกิดเฉพาะในโซน)
        if (inAirResistanceZone)
        {
            Vector2 zoneDragForce = -zoneDragCoefficient * rb.velocity; // F = -bv
            rb.AddForce(zoneDragForce);
        }
    }

    void Flap()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * flapForce, ForceMode2D.Impulse);
    }

    // ── เรียกจาก AirResistanceZone ──
    public void EnterZone(float b)
    {
        inAirResistanceZone = true;
        zoneDragCoefficient = b;
    }

    public void ExitZone()
    {
        inAirResistanceZone = false;
        zoneDragCoefficient = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || 
            collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("บัตรเขย่ง! หรือโดนรัฐประหาร!");
            Time.timeScale = 0;
        }
    }
}