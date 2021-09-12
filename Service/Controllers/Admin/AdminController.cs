using Sepidar.Api;
using Sepidar.BaseApi;
using Sepidar.BaseApi.Filters;

namespace Sepidar.Service.Controllers.Admin
{
    [NeedsLogin]
    [ShouldBeAdmin]
    public class AdminController : GeneralController
    {

    }
}