using Core.Utilities.Results;
using System.Collections.Generic;

namespace Core.Business
{
    public interface IRepositoryService<T>
    {
        IDataResult<List<T>> GetAll();
        IDataResult<T> GetById(int id);
        IResult Add(T entity);
        IResult Delete(T entity);
        IResult Update(T entity);
    }
}
