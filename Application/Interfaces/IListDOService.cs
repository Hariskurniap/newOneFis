using Domain.Entities;
using System.Linq;

namespace Application.Interfaces;

public interface IListDOService
{
    IQueryable<ListDO> GetListDO();
}
