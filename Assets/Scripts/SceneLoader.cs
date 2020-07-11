using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static void LoadScene(string name) => SceneManager.LoadScene(name);
    public static void ReloadScene() => LoadScene(SceneManager.GetActiveScene().name);
}
