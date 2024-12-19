using UnityEngine;

[System.Serializable]
public class ItemStack
{
    public int itemInfo;
    public int amount;
    static public ItemStack nullItem = new ItemStack(-1);
    public ItemStack(int itemInfo, int amount)
    {
        this.itemInfo = itemInfo;
        this.amount = amount;
    }
    public ItemStack(int itemInfo)
    {
        this.itemInfo = itemInfo;
    }
}
