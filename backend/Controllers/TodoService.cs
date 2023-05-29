using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace TodoApi
{
    public class TodoService
    {
        // .. FIELDS

        private readonly IMongoCollection<TodoItem> todoCollection;

        // .. CONSTRUCTION

        public TodoService(IOptions<TodoDatabaseSettings> databaseSettings)
        {
            var mongoClient = new MongoClient(databaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(databaseSettings.Value.DatabaseName);
            this.todoCollection = mongoDatabase.GetCollection<TodoItem>(databaseSettings.Value.BooksCollectionName);
        }

        // .. PUBLIC

        public async Task<List<TodoItem>> GetAsync()
        {
            return  await todoCollection.Find(_ => true).ToListAsync();
        }

        public async Task<TodoItem?> GetAsync(int id) 
        {
            return await this.todoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(TodoItem newItem)
        {
            await this.todoCollection.InsertOneAsync(newItem);
        }

        public async Task UpdateAsync(int id, TodoItem updatedItem)
        {
            await this.todoCollection.ReplaceOneAsync(x => x.Id == id, updatedItem);
        }

        public async Task RemoveAsync(int id)
        {
            await this.todoCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}