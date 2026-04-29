using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ZoneEffectManager : MonoBehaviour
{
    [Header("References")]
    public BackgroundManager bgManager;
    public BallotController player;

    [Header("Zone 2 — รัฐประหาร (Gravitational Pull)")]
    public float G = 12f;
    public float blackMassMass = 10f;
    public Transform blackMassPoint;
    public float minDistance = 2.5f;

    [Header("Zone 3 — รัฐสภา (Buoyancy — Physics 2D Built-in)")]
    public GameObject parliamentZone;

    private Coroutine activeCoroutine;
    private Coroutine typewriterCoroutine;

    public Text zoneText;
    ConstantForce2D cf;
    Rigidbody2D rb;

    void Start()
    {
        cf = player.GetComponent<ConstantForce2D>();
        cf.enabled = false;
        rb = player.GetComponent<Rigidbody2D>();

        if (parliamentZone != null)
            parliamentZone.SetActive(false);

        if (zoneText != null)
            zoneText.text = "";

        bgManager.OnBackgroundChanged += HandleZoneChange;
    }

    // ── Typewriter Effect ──
    IEnumerator TypewriterEffect(string message, float charDelay = 0.05f, float holdTime = 2f)
    {
        zoneText.text = "";
        foreach (char c in message)
        {
            zoneText.text += c;
            yield return new WaitForSecondsRealtime(charDelay); // ใช้ Realtime กัน timeScale = 0 กวน
        }

        yield return new WaitForSecondsRealtime(holdTime);

        // ค่อยๆ ลบออก
        while (zoneText.text.Length > 0)
        {
            zoneText.text = zoneText.text.Substring(0, zoneText.text.Length - 1);
            yield return new WaitForSecondsRealtime(0.03f);
        }
    }

    void ShowZoneText(string message)
    {
        if (typewriterCoroutine != null)
            StopCoroutine(typewriterCoroutine);

        typewriterCoroutine = StartCoroutine(TypewriterEffect(message));
    }

    void HandleZoneChange(int bgIndex)
    {
        StopAllEffects();

        switch (bgIndex)
        {
            case 0:
                Debug.Log("โซน: ปกติ");
                ShowZoneText("Welcome to Democracy Zone");
                break;

            case 1:
                Debug.Log("โซน: รัฐประหาร!");
                ShowZoneText("Entering Coup Zone | Gravitational Pull Activated");
                activeCoroutine = StartCoroutine(GravitationalPullLoop());
                break;

            case 2:
                Debug.Log("โซน: สตง!");
                ShowZoneText("Entering Stable Zone | Constant Force Activated!");
                if (parliamentZone != null)
                    parliamentZone.SetActive(true);
                if (cf != null)
                    cf.enabled = true;
                rb.gravityScale = 0.7f;
                rb.linearDamping = 1.5f;
                break;
        }
    }

    IEnumerator GravitationalPullLoop()
    {
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        float playerMass = playerRb.mass;

        while (true)
        {
            if (blackMassPoint == null) yield break;

            Vector2 direction = (Vector2)(blackMassPoint.position - player.transform.position);
            float r = Mathf.Max(direction.magnitude, minDistance);
            float forceMagnitude = G * (playerMass * blackMassMass) / (r * r);
            forceMagnitude = Mathf.Clamp(forceMagnitude, 0, 25f);

            Vector2 gravForce = direction.normalized * forceMagnitude;
            playerRb.AddForce(gravForce);

            yield return new WaitForFixedUpdate();
        }
    }

    void StopAllEffects()
    {
        if (activeCoroutine != null)
        {
            StopCoroutine(activeCoroutine);
            activeCoroutine = null;
        }

        if (parliamentZone != null)
            parliamentZone.SetActive(false);

        if (cf != null)
            cf.enabled = false;

        rb.gravityScale = 1f;
        rb.linearDamping = 0f;

        player.SetExternalForce(Vector2.zero);
    }
}