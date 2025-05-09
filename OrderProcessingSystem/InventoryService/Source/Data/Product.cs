using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OPS.InventoryService.Data
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string ID { get; set; }
        
        [BsonElement("sku")]
        public string SKU { get; set; }
        
        [BsonElement("name")]
        public string Name { get; set; }
        
        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("priceaud")]
        public double PriceAUD { get; set; }
        
        [BsonElement("createdat")]
        public DateTime CreatedAt { get; set; }
        
        [BsonElement("updatedat")]
        public DateTime UpdatedAt { get; set; }
    }
}
