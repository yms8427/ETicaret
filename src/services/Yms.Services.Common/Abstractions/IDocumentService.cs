using System;
using System.IO;

namespace Yms.Services.Common.Abstractions
{
    public interface IDocumentService
    {
        string GetFileNameByDocumentId(Guid id);

        Guid UploadFile(Stream stream, string fileName);
    }
}