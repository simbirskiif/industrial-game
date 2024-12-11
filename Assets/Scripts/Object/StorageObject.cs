using UnityEngine;

public class StorageObject : MonoBehaviour
{
    public ItemStack[] items;
    public ExitPoint exitPoint;
    public EntryPoint entryPoint;
    public int capacity;
    public bool manually = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!manually)
        {
            items = new ItemStack[capacity];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (exitPoint != null)    //Если есть точка выхода, пытается пропихнуть предмет в нее
        {
            int slot = getClosestAny();
            if (slot != -1)
            {
                ItemInfo item = items[slot].itemInfo;
                if (exitPoint.AddToQueue(item))    //Если удалось, уменьшить стак или обнулить слот в хранилище
                {
                    items[slot].amount--;
                    if (items[slot].amount == 0)
                    {
                        items[slot] = null;
                    }
                }
                else
                {
                    Debug.Log("Failed to add item to queue");
                }
            }
        }
        if (entryPoint != null)    //Если есть точка входа, пытается принять предмет
        {
            ItemInfo testItem = entryPoint.WhatItem();
            if (testItem != null)
            {
                Debug.Log("Test item: " + testItem.Name);
                if (searchForMatchingItem(testItem) != -1)    //Если есть такой предмет в хранилище, добавить к стаку
                {
                    ItemInfo item = entryPoint.GiveItem();
                    Debug.Log("Item: " + item.Name);
                    if (item != null)
                    {
                        addItem(item);
                    }
                }
                else if (hasEmptySlot())    //Если нет, добавить новый стак
                {
                    ItemInfo item = entryPoint.GiveItem();
                    if (item != null)
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
            if (items[i] != null)
            {
                return i;
            }
        }
        return -1;
    }
    public bool addItem(ItemInfo itemInfo)//Добавить предмет в хранилище (BOOL)
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
                items[index] = new ItemStack();
                items[index].itemInfo = itemInfo;
                items[index].amount = 1;
                return true;
            }
        }
        return false;
    }
    public int searchForMatchingItem(ItemInfo itemInfo)//Ищет предмет в хранилище для добавления его к существующему слоту
    {
        if (itemInfo == null)
        {
            return -1;
        }
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null && items[i].itemInfo == itemInfo)
            {
                if (items[i].amount < items[i].itemInfo.StackSize)
                {
                    return i;
                }
                return -1;
            }
        }
        return -1;
    }
    public int searchEmptySlot()//Ищет пустой слот в хранилище
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
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
            if (items[i] == null)
            {
                return true;
            }
        }
        return false;
    }

}
