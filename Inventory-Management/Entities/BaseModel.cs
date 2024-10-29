namespace Inventory_Management.Entities
{
    public class BaseModel
    {
       public BaseModel() { 

        IsDeleted = false;

        }
        public int ID { get; set; }

        public bool IsDeleted { get; set; }


    }
}
