using UnityEngine;

public static class Bootstrap
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Init()
    {
        GameObject root = new GameObject("Root");
        Object.DontDestroyOnLoad(root);

        root.AddComponent<InputManager>();
    }
}
