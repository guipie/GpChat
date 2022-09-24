using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuXin.Core.BaseProvider;
using GuXin.Entity.DomainModels;
using GuXin.Core.Extensions.AutofacManager;
namespace GuXin.System.IRepositories
{
    public partial interface ISys_LogRepository : IDependency,IRepository<Sys_Log>
    {
    }
}

