using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    // Objek GameObject untuk mereferensikan pemain
    GameObject player;

    // Referensi ke script CombatManager untuk mengambil data pertempuran
    [SerializeField] CombatManager combatManager;

    // Komponen kesehatan pemain
    HealthComponent healthComponent;

    // Label untuk menampilkan informasi di UI
    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label enemiesLeftLabel;

    // Fungsi yang dipanggil saat script diaktifkan
    private void OnEnable()
    {
        // Mencari GameObject pemain dengan nama "Player"
        player = GameObject.Find("Player");

        // Mengambil komponen HealthComponent dari pemain
        healthComponent = player.GetComponent<HealthComponent>();

        // Mengambil elemen root dari UIDocument
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Mencari elemen UI berdasarkan nama elemen
        healthLabel = root.Q<Label>("Health");
        pointsLabel = root.Q<Label>("Points");
        waveLabel = root.Q<Label>("Wave");
        enemiesLeftLabel = root.Q<Label>("EnemiesWave");
    }

    // Fungsi yang dipanggil setiap frame untuk memperbarui UI
    void Update()
    {
        // Memperbarui informasi kesehatan
        UpdateHealth(healthComponent.GetHealth());

        // Memperbarui informasi poin
        UpdatePoints(combatManager.points);

        // Memperbarui informasi gelombang musuh
        UpdateWave(combatManager.waveNumber - 1);

        // Memperbarui jumlah musuh yang tersisa
        UpdateEnemiesLeft(combatManager.totalEnemies);
    }

    // Fungsi untuk memperbarui teks kesehatan di UI
    public void UpdateHealth(int health)
    {
        healthLabel.text = $"Health: {health}";
    }

    // Fungsi untuk memperbarui teks poin di UI
    public void UpdatePoints(int points)
    {
        pointsLabel.text = $"Points: {points}";
    }

    // Fungsi untuk memperbarui teks gelombang di UI
    public void UpdateWave(int wave)
    {
        waveLabel.text = $"Wave: {wave}";
    }

    // Fungsi untuk memperbarui teks jumlah musuh yang tersisa di UI
    public void UpdateEnemiesLeft(int count)
    {
        enemiesLeftLabel.text = $"Enemies Left: {count}";
    }
}
