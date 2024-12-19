using UnityEngine;

public class SystemManager : MonoBehaviour, IOnChangePositionInSelector
{
    private float deltaTime = 0.0f;
    public int targetFrameRate = 60;
    PanelController debugPanel;
    bool allowDebug = false;
    [SerializeField] Selector targetFpsSelector;
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
        if (GameObject.FindGameObjectWithTag("DebugPanel").GetComponent<PanelController>())
        {
            debugPanel = GameObject.FindGameObjectWithTag("DebugPanel").GetComponent<PanelController>();
            allowDebug = true;
        }
        initFpsChanger();
    }
    void initFpsChanger()
    {
        if (targetFpsSelector != null)
        {
            SelectorOptions[] options = new SelectorOptions[3];
            targetFpsSelector.setListener(this);
            options[0] = new SelectorOptions("30", 30);
            options[1] = new SelectorOptions("60", 60);
            options[2] = new SelectorOptions("120", 120);
            targetFpsSelector.SetData("Target FPS", options);
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
    void IOnChangePositionInSelector.OnChangePositionInSelector(int position)
    {
        switch (position)
        {
            case 0:
                targetFrameRate = 30;
                Application.targetFrameRate = 30;
                break;
            case 1:
                targetFrameRate = 60;
                Application.targetFrameRate = 60;
                break;
            case 2:
                targetFrameRate = 120;
                Application.targetFrameRate = 120;
                break;
        }
    }
}
