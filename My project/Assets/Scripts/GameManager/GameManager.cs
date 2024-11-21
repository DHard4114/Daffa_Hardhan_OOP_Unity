using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelManager LevelManager { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        LevelManager = GetComponentInChildren<LevelManager>();

        DontDestroyOnLoad(gameObject);

        
        GameObject camera = GameObject.FindWithTag("MainCamera");
        if (camera != null)
        {
            DontDestroyOnLoad(camera);
        }

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            DontDestroyOnLoad(player);
        }
    }

    public void ClearScene()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("MainCamera") || obj.CompareTag("Player"))
                continue;

            Destroy(obj);
        }
    }
}