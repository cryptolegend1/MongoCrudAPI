using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoCrudAPI.Application
{
    public interface IMongoCrudApiController<TMongoDocument, TMongoDocumentDto>
    {
        Task<IEnumerable<TMongoDocumentDto>> GetAllAsync();

        Task<TMongoDocumentDto> GetAsync(string id);

        Task<TMongoDocumentDto> CreateAsync(TMongoDocumentDto createInput);

        Task<TMongoDocumentDto> UpdateAsync(string id, TMongoDocumentDto updateInput);

        Task DeleteAsync(string id);
    }
}
