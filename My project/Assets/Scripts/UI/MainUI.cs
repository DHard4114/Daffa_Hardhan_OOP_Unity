using UnityEngine;
using UnityEngine.UIElements;

public class MainUI : MonoBehaviour
{
    // Variabel untuk referensi objek player, combat manager, dan health component.
    GameObject player;
    [SerializeField] CombatManager combatManager; // Menghubungkan CombatManager dari Inspector
    HealthComponent healthComponent; // Komponen kesehatan dari player

    // Label untuk menampilkan data pada UI.
    private Label healthLabel;
    private Label pointsLabel;
    private Label waveLabel;
    private Label enemiesLeftLabel;

    // Method ini dipanggil ketika object di-enable (misalnya saat scene dimulai).
    private void OnEnable()
    {
        // Mencari objek player dalam scene dan mendapatkan referensi ke komponen HealthComponent-nya.
        player = GameObject.Find("Player");
        healthComponent = player.GetComponent<HealthComponent>();

        // Mendapatkan root VisualElement dari UIDocument untuk akses UI yang ada.
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // Menghubungkan label UI dengan elemen yang sesuai di dalam root UI.
        healthLabel = root.Q<Label>("Health");
        pointsLabel = root.Q<Label>("Points");
        waveLabel = root.Q<Label>("Wave");
        enemiesLeftLabel = root.Q<Label>("EnemiesWave");
    }

    // Update dipanggil setiap frame.
    void Update()
    {
        // Memperbarui UI dengan data terbaru dari berbagai komponen yang ada.
        UpdateHealth(healthComponent.GetHealth()); // Mengupdate label Health dengan nilai kesehatan pemain.
        UpdatePoints(combatManager.points); // Mengupdate label Points dengan jumlah poin saat ini.
        UpdateWave(combatManager.waveNumber - 1); // Mengupdate label Wave dengan nomor wave (dikurangi 1 untuk menyesuaikan).
        UpdateEnemiesLeft(combatManager.totalEnemies); // Mengupdate label Enemies Left dengan jumlah musuh yang tersisa.
    }

    // Metode ini digunakan untuk memperbarui nilai kesehatan di UI.
    public void UpdateHealth(int health)
    {
        healthLabel.text = $"Health: {health}"; // Menampilkan nilai kesehatan pada label Health.
    }

    // Metode ini digunakan untuk memperbarui nilai poin di UI.
    public void UpdatePoints(int points)
    {
        pointsLabel.text = $"Points: {points}"; // Menampilkan nilai poin pada label Points.
    }

    // Metode ini digunakan untuk memperbarui nomor wave di UI.
    public void UpdateWave(int wave)
    {
        waveLabel.text = $"Wave: {wave}"; // Menampilkan nomor wave pada label Wave.
    }

    // Metode ini digunakan untuk memperbarui jumlah musuh yang tersisa di UI.
    public void UpdateEnemiesLeft(int count)
    {
        enemiesLeftLabel.text = $"Enemies Left: {count}"; // Menampilkan jumlah musuh yang tersisa pada label Enemies Left.
    }
}
