using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoCrudAPI.DbContext.Document;
using MongoCrudAPI.DbContext.Repository;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoCrudAPI.Application
{
    [ApiController]
    [Route("[controller]")]
    public class MongoCrudApiController<TMongoDocument, TMongoDocumentDto> : ControllerBase, IMongoCrudApiController<TMongoDocument, TMongoDocumentDto> where TMongoDocument : IMongoDocument
    {
        private readonly IMongoRepository<TMongoDocument> _repository;

        public MongoCrudApiController(IMongoRepository<TMongoDocument> repository)
        {
            _repository = repository;
        }

        protected TMongoDocumentDto MapToDocumentDto(TMongoDocument document)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMongoDocument, TMongoDocumentDto>());
            var mapper = new Mapper(config);

            return mapper.Map<TMongoDocumentDto>(document);
        }

        protected TMongoDocument MapToDocument(TMongoDocumentDto document)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<TMongoDocumentDto, TMongoDocument>());
            var mapper = new Mapper(config);

            return mapper.Map<TMongoDocument>(document);
        }

        [HttpGet]
        public Task<IEnumerable<TMongoDocumentDto>> GetAllAsync()
        {
            var documents = _repository.GetAll();

            return Task.FromResult(documents.Select(MapToDocumentDto));
        }

        [HttpGet("{id:length(24)}", Name = "Get")]
        public async Task<TMongoDocumentDto> GetAsync(string id)
        {
            var document = await _repository.FindByIdAsync(id);

            return MapToDocumentDto(document);
        }

        [HttpPost]
        public async Task<TMongoDocumentDto> CreateAsync(TMongoDocumentDto createInput)
        {
            var document = MapToDocument(createInput);

            await _repository.InsertOneAsync(document);

            return createInput;
        }

        [HttpPut("{id:length(24)}")]
        public async Task<TMongoDocumentDto> UpdateAsync(string id, TMongoDocumentDto updateInput)
        {
            var document = MapToDocument(updateInput);

            await _repository.UpdateByIdAsync(id, document);

            return updateInput;
        }

        [HttpDelete("{id:length(24)}")]
        public async Task DeleteAsync(string id)
        {
            await _repository.DeleteByIdAsync(id);
        }
    }
}
