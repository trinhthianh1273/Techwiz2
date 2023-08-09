namespace Api.IRepository;

public interface IGenericRepository<T, DTO>
    where T : class
    where DTO : class
{
    IEnumerable<T> GetAll();
    IEnumerable<DTO> GetAllDto();
    T getById(int id);
    DTO getDTOById(int id);
    T Insert(T obj);
    T Update(T obj);
    int Delete(int id);
    int Save();
}
