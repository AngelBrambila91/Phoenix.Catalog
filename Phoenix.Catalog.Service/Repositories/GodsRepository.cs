using MongoDB.Driver;

public class GodsRepository
{
    private const string collectionName = "gods";
    private readonly IMongoCollection<God>? dbCollection;
    private readonly FilterDefinitionBuilder<God> filterBuilder = Builders<God>.Filter;

    public GodsRepository()
    {
        var mongoClient = new MongoClient($"mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("Catalog");
        dbCollection = database.GetCollection<God>(collectionName);
    }

    public async Task<IReadOnlyCollection<God>> GetAllAsync()
    {
        return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
    }

    public async Task<God> GetAsync(Guid id)
    {
        FilterDefinition<God> filter = filterBuilder.Eq(entity => entity.Id, id);
        return await dbCollection.Find<God>(filter).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(God entity)
    {
        if(entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        await dbCollection!.InsertOneAsync(entity);
    }

    public async Task UpdateAsync (God entity)
    {
        if(entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        FilterDefinition<God> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, entity.Id);
        await dbCollection!.ReplaceOneAsync(filter, entity);
    }

    public async Task RemoveAsync(Guid id)
    {
        FilterDefinition<God> filter = filterBuilder.Eq(existingEntity => existingEntity.Id, id);
        await dbCollection!.DeleteOneAsync(filter);
    }
}