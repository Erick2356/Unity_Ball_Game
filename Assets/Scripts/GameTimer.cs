using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance;

    private float levelStartTime;
    private float gameStartTime;
    private string currentSceneName;
    private bool isFirstLoad = true;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        gameStartTime = Time.time;
        levelStartTime = Time.time;
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
       
        if (isFirstLoad)
        {
            isFirstLoad = false;
            currentSceneName = scene.name;
            levelStartTime = Time.time;
            return;
        }

        float levelTime = Time.time - levelStartTime;
        Debug.Log($"Tiempo en {currentSceneName}: {levelTime:F2} segundos");


        currentSceneName = scene.name;
        levelStartTime = Time.time;
    }

    public void RegisterMazeCompleted()
{
    float totalTime = Time.time - gameStartTime;
    Debug.Log($"Tiempo total (ambos niveles): {totalTime:F2} segundos");

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
}
}