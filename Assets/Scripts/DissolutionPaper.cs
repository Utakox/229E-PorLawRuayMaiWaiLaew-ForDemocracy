using UnityEngine;

public class DissolutionPaper : MonoBehaviour
{
    [Header("Physics Settings (F = ma)")]
    public float mass = 1.0f;             // มวล (m)
    public float targetAcceleration = 40f; // ความเร่งเชิงเส้น (a)

    [Header("Rotation Physics (τ = Iα)")]
    public float momentOfInertia = 0.5f;   // โมเมนต์ความเฉื่อย (I)
    public float angularAcceleration = 15f; // ความเร่งเชิงมุม (α)

    private BallotController gameOverController;

    void Start()
    {
        gameOverController = FindObjectOfType<BallotController>();

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.mass = mass;

            // ── F = ma ──
            float force = mass * targetAcceleration;
            rb.AddForce(Vector2.left * force, ForceMode2D.Impulse);

            // ── τ = Iα (Torque = moment of inertia × angular acceleration) ──
            float torque = momentOfInertia * angularAcceleration;
            rb.AddTorque(torque, ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Game Over: โดนยุบพรรค!");
            if (gameOverController != null)
                gameOverController.GameOver();
        }

        if (collision.CompareTag("EndZone"))
        {
            Destroy(gameObject);
        }
    }
}