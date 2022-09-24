/*
 *Author：jxx
 *Contact：283591387@qq.com
 *代码由框架生成,此处任何更改都可能导致被代码生成器覆盖
 *所有业务编写全部应在Partial文件夹下ComplaintService与IComplaintService中编写
 */
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;
using GuXin.Business.IRepositories;
using GuXin.Business.IServices;
using GuXin.Core.Utilities;
using Microsoft.AspNetCore.Http;

namespace GuXin.Business.Services
{
    public partial class ComplaintService : ServiceBase<Complaint, IComplaintRepository>
    , IComplaintService, IDependency
    {
        public ComplaintService(IComplaintRepository repository)
        : base(repository)
        {
            Init(repository);
        }
        public static IComplaintService Instance
        {
            get { return AutofacContainerModule.GetService<IComplaintService>(); }
        }


    }
}
