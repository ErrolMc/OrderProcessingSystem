using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OPS.InventoryService.Data
{
    public class Inventory
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ID { get; set; }
        
        [BsonElement("productid")]
        public string ProductID { get; set; }
        
        [BsonElement("locationid")]
        public string LocationID { get; set; }
        
        [BsonElement("quantityonhand")]
        public int QuantityOnHand { get; set; }
        
        [BsonElement("quantityreserved")]
        public int QuantityReserved { get; set; }
        
        [BsonElement("quantityallocated")]
        public int QuantityAllocated { get; set; }
        
        [BsonElement("createdat")]
        public DateTime CreatedAt { get; set; }
        
        [BsonElement("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}

