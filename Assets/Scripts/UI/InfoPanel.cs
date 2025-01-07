using UnityEngine;

public class InfoPanel : MonoBehaviour, IOnClosePanel
{
    PanelController panelController;
    bool updating = false;
    public GameObject targetObject;
    ObjectInfo objectInfo;
    [Header("UI Elements")]
    public GameObject infoPanel;
    public GameObject errorPanel;
    public GameObject collectStatusPanel;
    public GameObject inventoryPanel;

    void Start()
    {
        panelController = GetComponent<PanelController>();
        panelController.setCloseListener(this);
    }
    void Update()
    {
        if (updating)
        {
            if (targetObject != null & targetObject.GetComponent<ObjectInfo>() != null)
            {
                ObjectInfo info = targetObject.GetComponent<ObjectInfo>();
                if (info.objectType == ObjectType.Storage)
                {
                    updateTypeStorage();
                }
            }
        }
    }
    void OpenPanel(ObjectInfo info)
    {
        targetObject = info.gameObject;
        objectInfo = info;
        updating = true;
        panelController.OpenPanel();
        autoSetType();
    }
    void OpenPanel(GameObject info)
    {
        targetObject = info;
        if (info.GetComponent<ObjectInfo>()) { objectInfo = info.GetComponent<ObjectInfo>(); }
        updating = true;
        panelController.OpenPanel();
        autoSetType();
    }
    void OpenPanel()
    {
        updating = true;
        panelController.OpenPanel();
    }
    void ClosePanel()
    {
        updating = false;
        panelController.ClosePanel();
    }
    void autoSetType()
    {
        if (targetObject != null & targetObject.GetComponent<ObjectInfo>() != null)
        {
            ObjectInfo info = targetObject.GetComponent<ObjectInfo>();
            if (info.objectType == ObjectType.Storage)
            {
                setTypeStorage();
            }
        }
    }
    void setTypeStorage()
    {
        InventoryPanel inventory = inventoryPanel.GetComponent<InventoryPanel>();
        inventory.setTarget(targetObject);
    }
    void updateTypeStorage()
    {
        updateInventoryStorage();
    }
    void updateInventoryStorage()
    {
        InventoryPanel inventory = inventoryPanel.GetComponent<InventoryPanel>();
        inventory.UpdateInventory();
    }
    void IOnClosePanel.OnClosePanel()
    {
        ClosePanel();
    }
}
