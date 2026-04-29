using UnityEngine;
using System.Collections.Generic;

public class MasterObstacleSpawner : MonoBehaviour
{
    [Header("Pool of Obstacles")]
    // ลาก Prefab อุปสรรคทั้ง 5 แบบมาใส่ในลิสต์นี้ที่หน้า Inspector
    public List<GameObject> obstaclePrefabs; 

    [Header("Spawn Settings")]
    public float spawnRate = 2.5f;     // สร้างทุกๆ กี่วินาที
    public float heightOffset = 3f;    // ระยะสุ่มความสูง (Y) เพื่อให้ไม่เกิดที่เดิมซ้ำๆ
    
    private float timer = 0;

    void Start()
    {
        // เริ่มเกมมาให้ส่งอุปสรรคออกมาทักทายอันแรกเลย
        SpawnRandomObstacle();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnRandomObstacle();
            timer = 0;
        }
    }

    void SpawnRandomObstacle()
    {
        if (obstaclePrefabs.Count > 0)
        {
            // 1. สุ่มเลือกอุปสรรคจาก List
            int randomIndex = Random.Range(0, obstaclePrefabs.Count);
            GameObject selectedPrefab = obstaclePrefabs[randomIndex];

            // 2. คำนวณตำแหน่งเกิด (X คือตำแหน่ง Spawner, Y คือสุ่ม)
            float randomY = transform.position.y + Random.Range(-heightOffset, heightOffset);
            Vector3 spawnPos = new Vector3(transform.position.x, randomY, transform.position.z);

            // 3.สร้างอุปสรรคออกมาในฉาก (แก้ไขตรงนี้!)
            // ใช้ selectedPrefab.transform.rotation เพื่อให้มันหันหน้าตามที่ตั้งไว้ใน Prefab ครับ
            Instantiate(selectedPrefab, spawnPos, selectedPrefab.transform.rotation);
        }
    }
}