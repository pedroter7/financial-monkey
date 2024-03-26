using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.Entities;

public abstract class BaseEntity
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public DateTime CreationDateTime { get; } = DateTime.UtcNow;
}
