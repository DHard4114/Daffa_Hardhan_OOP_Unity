using System.Collections;
using UnityEngine;

public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkingCount = 7; // Banyaknya kedipan
    [SerializeField] private float blinkInterval = 0.1f; // Interval antar kedipan
    [SerializeField] private Material blinkMaterial; // Material untuk efek blink
    private SpriteRenderer spriteRenderer; // Untuk merubah tampilan sprite
    private Material originalMaterial; // Material asli sebelum berkedip
    public bool isInvincible = false; // Menandakan apakah objek kebal

    private void Awake()
    {
        // Mendapatkan referensi SpriteRenderer dan material asli
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    // Enumerator untuk efek blinking
    private IEnumerator BlinkingEffect()
    {
        int blinkCount = 0;
        while (blinkCount < blinkingCount)
        {
            // Ganti dengan material blinking
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);

            // Kembali ke material asli
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);

            blinkCount++;
        }
    }

    // Fungsi untuk memulai blinking jika Entity tidak sedang invincible
    public void StartBlinking()
    {
        if (!isInvincible) // Pastikan Entity tidak sedang invincible
        {
            StartCoroutine(BlinkingEffect());
        }
    }

    // Fungsi untuk men-set objek menjadi invincible
    public void SetInvincible(bool state)
    {
        isInvincible = state;
    }
}
