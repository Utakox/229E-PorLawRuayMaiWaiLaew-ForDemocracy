using UnityEngine;
using System.Collections;

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
    public GameObject parliamentZone;   // ลาก ParliamentZone GameObject ใส่ตรงนี้

    private Coroutine activeCoroutine;

    void Start()
    {
        // ซ่อนโซนรัฐสภาตั้งแต่ต้น
        if (parliamentZone != null)
            parliamentZone.SetActive(false);

        bgManager.OnBackgroundChanged += HandleZoneChange;
    }

    void HandleZoneChange(int bgIndex)
    {
        StopAllEffects();

        switch (bgIndex)
        {
            case 0:
                Debug.Log("โซน: ปกติ");
                break;

            case 1:
                Debug.Log("โซน: รัฐประหาร!");
                activeCoroutine = StartCoroutine(GravitationalPullLoop());
                break;

            case 2:
                Debug.Log("โซน: รัฐสภา!");
                if (parliamentZone != null)
                    parliamentZone.SetActive(true);  // เปิด Buoyancy Zone
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

        // 🔥 กันแรงเกิน
        float maxForce = 25f;
        forceMagnitude = Mathf.Clamp(forceMagnitude, 0, maxForce);

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

        // ปิด Buoyancy Zone ทุกครั้งที่เปลี่ยนโซน
        if (parliamentZone != null)
            parliamentZone.SetActive(false);
           
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        player.SetExternalForce(Vector2.zero);
    }
}