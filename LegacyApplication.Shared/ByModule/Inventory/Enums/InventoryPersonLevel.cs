namespace LegacyApplication.Shared.ByModule.Inventory.Enums
{
    public enum InventoryPersonLevel
    {
        //无权限设定 = -1,
        仓库管理员 = 0,
        //区域经理 = 10,
        //总经理 = 100,
        超级管理员 = 1000
    }
}
