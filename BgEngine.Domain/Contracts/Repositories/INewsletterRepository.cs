using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BgEngine.Domain.RepositoryContracts;
using BgEngine.Domain.EntityModel;

namespace BgEngine.Domain.RepositoryContracts
{
    public interface INewsletterRepository : IRepository<Newsletter>
    {
        void DeleteNewsletterTask(NewsletterTask task);
        void UpdateNewsletterTask(NewsletterTask task);
    }
}
