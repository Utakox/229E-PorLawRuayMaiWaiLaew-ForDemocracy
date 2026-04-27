using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{
    [Header("Background Settings")]
    public List<Sprite> backgrounds;
    public float switchInterval = 40f;
    public float fadeDuration = 1.5f;

    [Header("References")]
    public Image bgCurrent;
    public Image bgNext;

    public int CurrentIndex { get; private set; } = 0;
    public System.Action<int> OnBackgroundChanged;

    private List<int> remainingPool = new List<int>();

    void Start()
    {
        if (backgrounds.Count == 0) return;

        CurrentIndex = 0;
        bgCurrent.sprite = backgrounds[CurrentIndex];
        bgCurrent.color = Color.white;
        bgNext.color = new Color(1, 1, 1, 0);

        RefillPool();
        StartCoroutine(SwitchLoop());

        // แจ้ง ZoneEffectManager ตั้งแต่เริ่มเกม
        OnBackgroundChanged?.Invoke(CurrentIndex);
    }

    void RefillPool()
    {
        remainingPool.Clear();
        for (int i = 0; i < backgrounds.Count; i++)
            remainingPool.Add(i);
        remainingPool.Remove(CurrentIndex);
    }

    IEnumerator SwitchLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            if (remainingPool.Count == 0) RefillPool();
            int randomPick = Random.Range(0, remainingPool.Count);
            int nextIndex = remainingPool[randomPick];
            remainingPool.RemoveAt(randomPick);

            yield return StartCoroutine(FadeToNext(nextIndex));
        }
    }

    IEnumerator FadeToNext(int nextIndex)
    {
        bgNext.sprite = backgrounds[nextIndex];
        bgNext.color = new Color(1, 1, 1, 0);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Clamp01(t / fadeDuration);
            bgNext.color = new Color(1, 1, 1, alpha);
            yield return null;
        }

        bgCurrent.sprite = backgrounds[nextIndex];
        bgCurrent.color = Color.white;
        bgNext.color = new Color(1, 1, 1, 0);

        CurrentIndex = nextIndex;
        OnBackgroundChanged?.Invoke(CurrentIndex);
    }
}