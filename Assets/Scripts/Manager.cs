using UnityEngine;

public class Manager : MonoBehaviour
{
    private float deltaTime = 0.0f;
    public int targetFrameRate = 60;
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }

    void Update()
    {
        // Вычисляем время между кадрами
        calculateFPS();
    }
    void calculateFPS()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 24;
        style.normal.textColor = Color.green;
        float fps = 1.0f / deltaTime;
        GUI.Label(new Rect(10, 20, 100, 20), $"FPS: {fps}", style);
    }
}
