using MongoDB.Bson;
using MongoDB.Driver;
using PedroTer7.FinancialMonkey.FinancialProductsService.Entities;

namespace PedroTer7.FinancialMonkey.FinancialProductsService.Repository;

public class FinancialProductsRepository(IMongoClient mongoClient) : IFinancialProductsRepository
{
    private readonly IMongoCollection<FinancialProduct> _collection
        = mongoClient
            .GetDatabase("financial-monkey-financialproducts-service")
            .GetCollection<FinancialProduct>("financial-products");

    public async Task<FinancialProduct> CreateProduct(FinancialProduct product)
    {
        await _collection.InsertOneAsync(product);
        return product;
    }

    public async Task<FinancialProduct?> Delete(string id)
    {
        if (ObjectId.TryParse(id, out var oId))
        {
            return await _collection.FindOneAndDeleteAsync(BuildFindByIdFilter(id));
        }
        return null;
    }

    public async Task<FinancialProduct?> GetProduct(string id)
    {
        if (ObjectId.TryParse(id, out var oId))
        {
            return (await _collection.FindAsync(BuildFindByIdFilter(oId.ToString()))).FirstOrDefault();
        }
        return null;
    }

    public async Task<Tuple<ICollection<FinancialProduct>, string?>> GetProductsPage(string? firstId, uint pageSize)
    {
        var filter = BuildPageSearchFilter(firstId);
        var sort = BuildPageSearchSort();
        var qRes = await _collection
                        .Find(filter)
                        .Sort(sort)
                        .Limit((int)pageSize + 1)
                        .ToListAsync();

        var page = qRes.Take((int)pageSize).ToList();
        var nId = qRes.Count == pageSize + 1 ? qRes[(int)pageSize].Id : null;
        return new Tuple<ICollection<FinancialProduct>, string?>(page, nId);
    }

    private static FilterDefinition<FinancialProduct> BuildPageSearchFilter(string? firstId)
    {
        FilterDefinition<FinancialProduct> filter = Builders<FinancialProduct>.Filter.Empty;
        if (!string.IsNullOrEmpty(firstId))
            filter = Builders<FinancialProduct>.Filter.Gte(p => p.Id, firstId);
        return filter;
    }

    private static SortDefinition<FinancialProduct> BuildPageSearchSort()
        => Builders<FinancialProduct>.Sort.Ascending(p => p.Id);

    public async Task<FinancialProduct?> UpdateProduct(string id, FinancialProduct product)
    {
        if (ObjectId.TryParse(id, out var oId))
        {
            product.Id = oId.ToString();
            var res = await _collection.ReplaceOneAsync(BuildFindByIdFilter(id), product);
            if (res.MatchedCount == 1) return product;
        }
        return null;
    }

    private static FilterDefinition<FinancialProduct> BuildFindByIdFilter(string id)
        => Builders<FinancialProduct>.Filter.Eq(p => p.Id, id);
}
