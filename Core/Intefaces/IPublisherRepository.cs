using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Intefaces
{
    public interface IPublisherRepository
    {
        Task<IReadOnlyList<Publisher>> GetAllPublishersSync();
    }
}