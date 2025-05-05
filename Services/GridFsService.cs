using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace imagenes_ms.Services
{
    public class GridFsService
    {
        private readonly IMongoDatabase _database;
        private readonly GridFSBucket _bucket;

        public GridFsService(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
            _bucket = new GridFSBucket(_database);
        }

        public async Task<ObjectId> UploadImageAsync(IFormFile file, string mascotaId)
        {
            using var stream = file.OpenReadStream();
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument { { "mascotaId", mascotaId }, { "contentType", file.ContentType } }
            };
            return await _bucket.UploadFromStreamAsync(file.FileName, stream, options);
        }

        public async Task<byte[]> GetImageAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            return await _bucket.DownloadAsBytesAsync(objectId);
        }

        public async Task<List<string>> GetImagesByMascota(string mascotaId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq("metadata.mascotaId", mascotaId);
            var files = await _bucket.FindAsync(filter);
            return files.ToList().Select(f => f.Id.ToString()).ToList();
        }
    }
}
