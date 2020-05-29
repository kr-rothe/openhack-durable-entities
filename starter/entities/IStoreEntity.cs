namespace InventoryManagement
{
    using System.Threading.Tasks;

    public interface IStoreEntity
    {
        void updateInventoryCount(EventSchema count);
        Task<Item> getStoreEntityItem(string id);
    
    }

    
}
