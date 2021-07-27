using System.Threading.Tasks;
using colonist_extension.Models;

namespace colonist_extension.Repositories
{
    public interface IEventRepository
    {
         Task<int> PostEvent(BaseEvent evt);
    }
}