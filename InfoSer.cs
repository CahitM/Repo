using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using AddressBookP;

namespace AddressBookP
{
    public class InfoSer
    {
        private readonly IMongoCollection<Information> _informations;

        public InfoSer(IOptions<PhoneBookDatabaseSet> options)
        {
            var mongoClient = new MongoClient(options.Value.ConnectionString);

            _informations = mongoClient.GetDatabase(options.Value.DatabaseName)
                .GetCollection<Information>(options.Value.PhoneBookName);
        }

        public async Task<List<Information>> Get(string v) =>
            await _informations.Find(_ => true).ToListAsync();
        public async Task<Information> Get(int UUID) =>
            await _informations.Find(m => m.UUID == UUID).FirstOrDefaultAsync();

        public async Task Create(Information newInformation) =>
            await _informations.InsertOneAsync(newInformation);
        public async Task Update(int UUID, Information updateInformation) =>
            await _informations.ReplaceOneAsync(m => m.UUID == UUID, updateInformation);
        public async Task Remove(int UUID) =>
            await _informations.DeleteManyAsync(m => m.UUID == UUID);

        internal Task Get()
        {
            throw new NotImplementedException();
        }
    }
}
