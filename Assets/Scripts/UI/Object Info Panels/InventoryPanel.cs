using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : MonoBehaviour
{
    public GameObject target;
    public GameObject itemPrefab;
    [SerializeField] GameObject itemsPanel;
    MasterManager masterManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        masterManager = GameObject.FindGameObjectWithTag("MasterManager").GetComponent<MasterManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setTarget(GameObject target)
    {
        this.target = target;
        UpdateInventory();
    }
    public void UpdateInventory()
    {
        if (target == null || target.GetComponent<ItemInfo>() == null)
        {
            return;
        }
        ObjectInfo objectInfo = target.GetComponent<ObjectInfo>();
        if (objectInfo.objectType != ObjectType.Storage || target.GetComponent<StorageObject>() == null)
        {
            return;
        }
        StorageObject storageObject = target.GetComponent<StorageObject>();
        int capacity = storageObject.capacity;
        clearInventory();
        for (int i = 0; i < capacity; i++)
        {
            ItemStack item = storageObject.items[i];
            setPrefab(item);
        }
    }
    void clearInventory()
    {
        foreach (Transform child in itemsPanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
    void setPrefab(ItemStack item)
    {
        GameObject itemObject = Instantiate(itemPrefab, itemsPanel.transform);
        ItemInfo itemInfo = masterManager.Items[item.itemInfo];
        itemObject.transform.Find("Name").GetComponent<Text>().text = itemInfo.Name;
        itemObject.transform.Find("Amount").GetComponent<Text>().text = item.amount.ToString();
    }
}
