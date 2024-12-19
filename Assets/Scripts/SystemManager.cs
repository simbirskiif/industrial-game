using UnityEngine;

public class SystemManager : MonoBehaviour
{
    private float deltaTime = 0.0f;
    public int targetFrameRate = 60;
    PanelController debugPanel;
    bool allowDebug = false;
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        if (GameObject.FindGameObjectWithTag("DebugPanel").GetComponent<PanelController>())
        {
            debugPanel = GameObject.FindGameObjectWithTag("DebugPanel").GetComponent<PanelController>();
            allowDebug = true;
        }
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
        if (allowDebug)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 24;
            style.normal.textColor = Color.green;
            float fps = 1.0f / deltaTime;
            GUI.Label(new Rect(10, 20, 100, 20), $"FPS: {fps}", style);
            if (GUI.Button(new Rect(10, 135, 200, 50), "Open Debug Panel"))
            {
                debugPanel.OpenPanel();
            }
        }
    }
}
