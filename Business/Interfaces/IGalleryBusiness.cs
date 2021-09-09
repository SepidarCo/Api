using Sepidar.BlobManagement;
using Sepidar.Framework;

namespace Sepidar.Business.Interfaces
{
    public interface IGalleryBusiness
    {
        dynamic List(long languageId, ListOptions listOptions);

        dynamic GalleryList();

        void Create(Blob picturelBlob, long languageId, string alt, string title, bool isActive, long orderNumber);

        void Update(long id, Blob? picturelBlob, long languageId, string alt, string title, bool isActive, long orderNumber);

        dynamic Get(long id);

        void Delete(long id);
    }
}
