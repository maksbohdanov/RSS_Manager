using DAL.Entities;

namespace DAL.Interfaces
{
    public interface INewsRepository: IRepository<News, string>
    {
    }
}
