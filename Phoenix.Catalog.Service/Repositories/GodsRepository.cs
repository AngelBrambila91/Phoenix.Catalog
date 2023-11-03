using MongoDB.Driver;

/*
MongoDB is a document-oriented NoSQL database which stores data in JSON-like documents with
dynamic schema
We will prefer a NoSQL solution (as opposed to a relational database) for our microservices because:
• We won’t need relationships across the data, because each microservice manages its own database
• We don’t need ACID guarantees, where ACID stands for atomicity, consistency, isolation and
durability, which are properties of database transactions that we won’t need in our services.
• We won’t need to write complex queries, since most of our service queries will be able to find
everything they need in a single document type
• Need low latency, high availability and high scalability, which are classic features of NoSQL
databases
*/
public class GodsRepository : IGodsRepository
{
    //Implemnt Dependecy Injection
    private readonly IGodsRepository godsRepository;
    private const string collectionName = "gods";
    private readonly IMongoCollection<God>? dbCollection;
    private readonly FilterDefinitionBuilder<God> filterBuilder = Builders<God>.Filter;
    public GodsRepository(IMongoDatabase database)
    {
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
        if (entity is null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        await dbCollection!.InsertOneAsync(entity);
    }

    public async Task UpdateAsync(God entity)
    {
        if (entity is null)
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