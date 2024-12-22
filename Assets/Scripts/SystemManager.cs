using UnityEngine;

public class SystemManager : MonoBehaviour, IOnChangePositionInSelector, IOnChangeCheckbox
{
    private float deltaTime = 0.0f;
    public int targetFrameRate = 60;
    PanelController debugPanel;
    bool allowDebug = false;
    bool visibleFps = false;
    [SerializeField] Selector targetFpsSelector;
    [SerializeField] Checkbox fpsCheckbox;
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
        if (fpsCheckbox != null)
        {
            fpsCheckbox.setListener(this);
            fpsCheckbox.setData("Visible Fps", visibleFps);
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
            if (visibleFps)
            {
                GUI.Label(new Rect(10, 10, 200, 50), "FPS: " + Mathf.Ceil(fps).ToString(), style);
            }
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
    void IOnChangeCheckbox.OnChangeCheckbox(bool isChecked)
    {
        visibleFps = isChecked;
        Debug.Log(isChecked);
    }
}
