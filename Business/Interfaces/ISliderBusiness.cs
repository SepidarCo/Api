using Sepidar.BlobManagement;
using Sepidar.Framework;

namespace Sepidar.Business.Interfaces
{
    public interface ISliderBusiness
    {
        dynamic List(long languageId, ListOptions listOptions);

        dynamic SliderList(long languageId);

        void Create(Blob picturelBlob, long languageId, string title, bool isActive, long orderNumber, string description);

        void Update(long id, Blob? picturelBlob, long languageId, string title, bool isActive, long orderNumber, string description);

        dynamic Get(long id);

        void Delete(long id);
    }
}
