using UnityEngine;
using System.Collections;

public class DissolutionSpawner : MonoBehaviour
{
    public GameObject paperPrefab;
    public GameObject warningUI;
    public float shootInterval = 5f;
    public float yRange = 3f;
    public AudioClip warningSound;

    private AudioSource audioSource; // เพิ่ม AudioSource ที่ตัว Spawner เอง

    private void Start()
    {
        if (warningUI != null) warningUI.SetActive(false);

        // เพิ่ม AudioSource บน GameObject นี้แทน
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = warningSound;

        StartCoroutine(ShootingRoutine());
    }

    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);

            // เล่นเสียงเตือน
            if (audioSource != null && warningSound != null)
            {
                audioSource.Play();
            }

            if (warningUI != null) warningUI.SetActive(true);

            yield return new WaitForSeconds(3f);

            // หยุดเสียงแล้วซ่อน UI
            audioSource.Stop();
            if (warningUI != null) warningUI.SetActive(false);
            SpawnPaper();
        }
    }

    void SpawnPaper()
    {
        float randomY = transform.position.y + Random.Range(-yRange, yRange);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, transform.position.z);
        Instantiate(paperPrefab, spawnPos, paperPrefab.transform.rotation);
    }
}