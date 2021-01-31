using System;
using System.Collections.Generic;
using System.Text;

namespace Yms.Services.Common.Abstractions
{
    public interface IDocumentService
    {
        string GetFileNameByDocumentId(Guid id);
    }
}
