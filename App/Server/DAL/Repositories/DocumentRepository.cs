using DAL.Context;
using DAL.Repositories.Abstract;
using DAL.Repositories.Interfaces;
using Document = DAL.Entities.Document;

namespace DAL.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(AppDbContext dbContext) : base(dbContext) { }
    }
}
