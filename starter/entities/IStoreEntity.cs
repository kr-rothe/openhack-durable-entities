namespace InventoryManagement
{
    using System.Threading.Tasks;

    public interface IStoreEntity
    {
        void updateInventoryCount(EventSchema count);
    
    }
}
