using UnityEngine;

public class CombatManager : MonoBehaviour
{
    [Header("Enemy Spawners")]
    public EnemySpawner[] enemySpawners; // Array untuk menyimpan semua EnemySpawner yang bertanggung jawab memunculkan musuh

    [Header("Wave Settings")]
    public float timer = 0f; // Timer untuk menghitung waktu sebelum memulai wave berikutnya
    [SerializeField] private float waveInterval = 5f; // Interval waktu antar wave dalam detik
    public int waveNumber = 1; // Nomor wave saat ini
    public int totalEnemies = 0; // Total jumlah musuh dalam wave saat ini

    private bool isWaveActive = false; // Flag untuk mengecek apakah wave sedang aktif

    void Start()
    {
        // Memulai wave pertama saat permainan dimulai
        StartNextWave();
    }

    void Update()
    {
        // Jika wave tidak aktif, jalankan timer untuk memulai wave berikutnya
        if (!isWaveActive)
        {
            timer += Time.deltaTime; // Menambahkan waktu yang berlalu ke timer
            if (timer >= waveInterval) // Jika timer sudah mencapai waveInterval
            {
                StartNextWave(); // Mulai wave berikutnya
                timer = 0f; // Reset timer
            }
        }
    }

    // Fungsi untuk memulai wave baru
    public void StartNextWave()
    {
        waveNumber++; // Tingkatkan nomor wave
        totalEnemies = 0; // Reset jumlah total musuh
        isWaveActive = true; // Tandai bahwa wave sedang aktif

        // Reset semua spawner dan mulai proses spawn musuh
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.ResetSpawner(); // Atur ulang pengaturan spawner
            spawner.isSpawning = true; // Aktifkan proses spawn musuh
        }
    }

    // Fungsi untuk memeriksa apakah wave telah selesai
    public void CheckWaveCompletion()
    {
        // Cek setiap spawner apakah masih ada musuh dalam wave
        foreach (EnemySpawner spawner in enemySpawners)
        {
            if (spawner.totalKillWave > 0) return; // Jika masih ada musuh, hentikan pengecekan
        }

        // Jika semua musuh sudah mati, akhiri wave
        EndWave();
    }

    // Fungsi untuk mengakhiri wave saat semua musuh terbunuh
    private void EndWave()
    {
        isWaveActive = false; // Tandai bahwa wave telah selesai

        // Hentikan semua proses spawning di setiap spawner
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.isSpawning = false; // Matikan spawner
        }
    }
}
