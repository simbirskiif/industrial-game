using UnityEngine;

public class StorageObject : MonoBehaviour
{
    MasterManager masterManager;
    public ItemStack[] items;
    public ExitPoint exitPoint;
    public EntryPoint entryPoint;
    public int capacity;
    public bool manually = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        masterManager = GameObject.FindGameObjectWithTag("MasterManager").GetComponent<MasterManager>();
        if (!manually)
        {
            items = new ItemStack[capacity];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = ItemStack.nullItem;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ProcessExitPoint();
        ProcessEntryPoint();
    }

    private void ProcessExitPoint()
    {
        if (exitPoint != null)
        {
            int slot = getClosestAny();
            if (slot != -1)
            {
                int item = items[slot].itemInfo;
                if (exitPoint.AddToQueue(item))
                {
                    items[slot].amount--;
                    if (items[slot].amount <= 0)
                    {
                        items[slot] = ItemStack.nullItem;
                    }
                }
                else
                {
                    //Debug.Log("Failed to add item to queue");
                }
            }
        }
    }

    private void ProcessEntryPoint()
    {
        if (entryPoint != null)
        {
            int testItem = entryPoint.WhatItem();
            if (testItem != -1)
            {
                //Debug.Log("Test item: " + masterManager.Items[testItem].Name);
                if (searchForMatchingItem(testItem) != -1)
                {
                    int item = entryPoint.GiveItem();
                    if (item != -1)
                    {
                        //Debug.Log("Item: " + masterManager.Items[item].Name);
                        addItem(item);
                    }
                }
                else if (hasEmptySlot())
                {
                    int item = entryPoint.GiveItem();
                    if (item != -1)
                    {
                        addItem(item);
                    }
                }
            }
        }
    }
    public int getClosestAny() //Получить ближайший не пустой слот
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != ItemStack.nullItem)
            {
                return i;
            }
        }
        return -1;
    }
    public bool addItem(int itemInfo)//Добавить предмет в хранилище (BOOL)
    {
        int index = searchForMatchingItem(itemInfo);
        if (index != -1)
        {
            items[index].amount++;
            return true;
        }
        else
        {
            index = searchEmptySlot();
            if (index != -1)
            {
                items[index] = new ItemStack(itemInfo, 1);
                return true;
            }
        }
        return false;
    }
    public int searchForMatchingItem(int itemInfo)//Ищет предмет в хранилище для добавления его к существующему слоту
    {
        if (itemInfo == -1)
        {
            return -1;
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != ItemStack.nullItem && items[i].itemInfo == itemInfo)
            {
                if (items[i].amount < masterManager.Items[itemInfo].StackSize)
                {
                    return i;
                }
            }
        }
        return -1;
    }
    public int searchEmptySlot()//Ищет пустой слот в хранилище
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == ItemStack.nullItem)
            {
                return i;
            }
        }
        return -1;
    }
    public bool hasEmptySlot()//Проверяет наличие пустого слота в хранилище
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == ItemStack.nullItem)
            {
                return true;
            }
        }
        return false;
    }

}
