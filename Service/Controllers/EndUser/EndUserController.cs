using Sepidar.BaseApi;
using Sepidar.BaseApi.Filters;

namespace Sepidar.Service.Controllers.EndUser
{
    [NeedsLogin]
    [ShouldBeEndUser]
    public class EndUserController : GeneralController
    {
    }
}