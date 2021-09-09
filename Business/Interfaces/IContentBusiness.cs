using Sepidar.BlobManagement;

namespace Sepidar.Business.Interfaces
{
    public interface IContentBusiness
    {
        dynamic List(long languageId);

        dynamic ContentList(long languageId);

        void Update(long id, Blob picturelBlob, long languageId, string title, string description, bool isActive, long orderNumber);

        dynamic Get(long id);

    }
}
