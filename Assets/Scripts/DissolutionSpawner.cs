using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class DissolutionSpawner : MonoBehaviour
{
    public GameObject paperPrefab;    // ลาก Prefab กระดาษยุบพรรคมาใส่
    public GameObject warningUI;      // ลาก UI Image รูปศาลมาใส่ (ต้องปิดไว้ก่อนในเริ่มต้น)
    public float shootInterval = 5f;  // รอบการยิงทุกๆ กี่วินาที
    public float yRange = 3f;         // ระยะสุ่มความสูง

    public AudioClip warningSound; // ลากเสียงเตือนมาใส่

    private void Start()
    {
        // warningSound.GetComponent<AudioSource>().playOnAwake = false; // ปิดเสียงตอนเริ่มเกม

        // มั่นใจว่าตอนเริ่มเกมรูปศาลปิดอยู่
        if (warningUI != null) warningUI.SetActive(false);
        
        // เริ่มลูปการยิง
        StartCoroutine(ShootingRoutine());
    }

    IEnumerator ShootingRoutine()
    {
        while (true)
        {
            // 1. รอจนครบเวลาที่กำหนดไว้
            yield return new WaitForSeconds(shootInterval);

            // เล่นเสียงเตือน (ถ้ามี)
            // if (warningSound != null)
            // {
            //     AudioSource.PlayClipAtPoint(warningSound, transform.position);
            // }
            // 2. เปิดรูปศาลแจ้งเตือน
            if (warningUI != null) warningUI.SetActive(true);

            // 3. รอ 2 วิ ตามที่โจทย์สั่ง
            yield return new WaitForSeconds(2f);

            // 4. ปิดรูปศาล แล้วทำการยิง
            if (warningUI != null) warningUI.SetActive(false);
            SpawnPaper();
        }
    }

    void SpawnPaper()
    {
        float randomY = transform.position.y + Random.Range(-yRange, yRange);
        Vector3 spawnPos = new Vector3(transform.position.x, randomY, transform.position.z);
        
        // สร้างกระดาษออกมา (สคริปต์ที่ตัวมันเองจะสั่งพุ่งด้วย F=ma)
        Instantiate(paperPrefab, spawnPos, paperPrefab.transform.rotation);
    }
}