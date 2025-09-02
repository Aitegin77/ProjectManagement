using DAL.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IDocumentService
    {
        Task AddAsync(ICollection<IFormFile> documents, Project project);
        Task UpdateAsync(ICollection<IFormFile> documents, Project project);
        void Delete(ICollection<Document> documents);
    }
}
