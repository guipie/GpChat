using GuXin.Core.BaseProvider;
using GuXin.Core.Utilities;
using GuXin.Entity.DomainModels;
using System.Threading.Tasks;

namespace GuXin.System.IServices
{
    public partial interface ISys_UserService
    {

        Task<WebResponseContent> Login(LoginInfo loginInfo, bool verificationCode = true);
        Task<WebResponseContent> ReplaceToken();
        Task<WebResponseContent> ModifyPwd(string oldPwd, string newPwd);
        Task<WebResponseContent> GetCurrentUserInfo();

        object GetTodayStatistic();
        object GetWeekStatistic();
        object GetStatistic();
        object GetUserTop5();
    }
}

