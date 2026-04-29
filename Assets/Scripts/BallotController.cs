using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private Vector2 externalForce = Vector2.zero;

    [Header("Game Over Settings")]
    public Canvas gameoverCanvas;

    private bool isGameOver = false;

    void Start()
    {
        gameoverCanvas.enabled = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            return; // หยุด Update ที่เหลือถ้า Game Over แล้ว
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Flap();
        }
    }

    void FixedUpdate()
    {
        if (isGameOver) return;

        // Air Resistance ปกติ
        Vector2 velocity = rb.velocity;
        Vector2 dragForce = -velocity * (airDensity * dragCoefficient);
        rb.AddForce(dragForce);

        // Air Resistance พิเศษ (เฉพาะในโซน)
        if (inAirResistanceZone)
        {
            Vector2 zoneDragForce = -zoneDragCoefficient * rb.velocity;
            rb.AddForce(zoneDragForce);
        }

        rb.AddForce(externalForce);
    }

    public void SetExternalForce(Vector2 force)
    {
        externalForce = force;
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
            GameOver();
        }
    }

    public void GameOver()
    {
        if (isGameOver) return; // กันเรียกซ้ำ

        Debug.Log("Game Over: โดนยุบพรรค!");
        isGameOver = true;
        gameoverCanvas.enabled = true;
        GameObject.Find("MusicBG").GetComponent<AudioSource>().Stop();
        Time.timeScale = 0f;
    }
}