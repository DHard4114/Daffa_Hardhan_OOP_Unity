using UnityEngine;
using UnityEngine.Assertions;

public class EnemyClickSpawner : MonoBehaviour
{
    // Array untuk menyimpan varian prefab Enemy yang bisa dipilih
    [SerializeField] private Enemy[] enemyVariants;
    
    // Indeks varian Enemy yang dipilih untuk di-spawn
    [SerializeField] private int selectedVariant = 0;

    // Fungsi Start dipanggil sekali saat permainan dimulai
    void Start()
    {
        // Memastikan bahwa array enemyVariants memiliki setidaknya 1 prefab Enemy
        Assert.IsTrue(enemyVariants.Length > 0, "Tambahkan setidaknya 1 Prefab Enemy terlebih dahulu!");
    }

    // Fungsi Update dipanggil sekali per frame
    private void Update()
    {
        // Melakukan iterasi untuk memeriksa tombol angka 1 hingga jumlah varian enemy
        for (int i = 1; i <= enemyVariants.Length; i++)
        {
            // Jika tombol angka ditekan, pilih varian Enemy yang sesuai
            if (Input.GetKeyDown(i.ToString()))
            {
                selectedVariant = i - 1; // Set indeks varian yang dipilih
            }
        }

        // Jika tombol klik kanan mouse ditekan, panggil fungsi SpawnEnemy untuk spawn enemy
        if (Input.GetMouseButtonDown(1))
        {
            SpawnEnemy();
        }
    }

    // Fungsi untuk spawn varian Enemy yang dipilih pada posisi mouse
    private void SpawnEnemy()
    {
        // Memastikan indeks varian yang dipilih valid sebelum melakukan instansiasi
        if (selectedVariant < enemyVariants.Length)
        {
            Instantiate(enemyVariants[selectedVariant]); // Spawn enemy yang dipilih
        }
    }
}
