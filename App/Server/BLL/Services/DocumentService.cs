using BLL.Interfaces;
using Common.Enums;
using DAL.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Document = DAL.Entities.Document;

namespace BLL.Services
{
    public class DocumentService : IDocumentService
    {
        private IDocumentRepository DocumentRepository { get; set; }

        public DocumentService(IDocumentRepository documentRepository) =>
            DocumentRepository = documentRepository;

        /// <summary>
        /// Asynchronously adds uploaded documents to the specified project.
        /// Saves files to the storage directory and creates corresponding records in the repository.
        /// </summary>
        /// <param name="documents">The collection of uploaded documents to add.</param>
        /// <param name="project">The project to which the documents belong.</param>
        /// <exception cref="ArgumentException">Thrown when documents are null or empty.</exception>
        public async Task AddAsync(ICollection<IFormFile> documents, Project project)
        {
            Validate(documents);

            Directory.CreateDirectory(AppConstants.DocumentDir);

            foreach (var document in documents)
            {
                var documentName = $"{document.FileName}-{Guid.NewGuid()}{Path.GetExtension(document.FileName)}";
                var path = Path.Combine(AppConstants.DocumentDir, documentName);

                using var stream = new FileStream(path, FileMode.CreateNew);
                await document.CopyToAsync(stream);

                await DocumentRepository.AddAsync(new Document()
                {
                    Name = document.FileName,
                    Path = path,
                    ProjectId = project.Id,
                    Project = project
                });
            }

            await DocumentRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Asynchronously updates the documents of the specified project.
        /// </summary>
        /// <param name="documents">The new collection of uploaded documents.</param>
        /// <param name="project">The project whose documents will be updated.</param>
        public async Task UpdateAsync(ICollection<IFormFile> documents, Project project)
        {
            Validate(documents);

            if (project.Documents.Count == documents.Count)
                if (project.Documents.Any(pd => documents.Any(d => pd.Name == d.FileName)))
                    return;

            foreach (var document in project.Documents)
            {
                DeleteFile(document);

                DocumentRepository.Delete(document);
            }

            project.Documents.Clear();

            await AddAsync(documents, project);
            await DocumentRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes the physical files of the specified documents from storage.
        /// Does not remove the document records from the database.
        /// </summary>
        /// <param name="documents">The collection of documents whose files will be deleted.</param>
        public void Delete(ICollection<Document> documents)
        {
            foreach (var document in documents)
                DeleteFile(document);
        }

        /// <summary>
        /// Deletes the physical file of the specified document from storage if it exists.
        /// </summary>
        /// <param name="document">The document whose file will be deleted.</param>
        private void DeleteFile(Document document)
        {
            var path = Path.Combine(AppConstants.DocumentDir, Path.GetFileName(document.Path));

            if (File.Exists(path))
                File.Delete(path);
        }

        /// <summary>
        /// Validates that the provided collection of documents is not null or empty.
        /// </summary>
        /// <typeparam name="T">The type of the documents in the collection.</typeparam>
        /// <param name="documents">The collection of documents to validate.</param>
        /// <exception cref="ArgumentException">Thrown when the collection is null or empty.</exception>
        private void Validate<T>(ICollection<T> documents)
        {
            if (documents == null || documents.Count == 0)
                throw new ArgumentException("Documents cannot be null or empty.");
        }
    }
}
