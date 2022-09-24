/*
 *所有关于Content_Intro类的业务代码应在此处编写
*可使用repository.调用常用方法，获取EF/Dapper等信息
*如果需要事务请使用repository.DbContextBeginTransaction
*也可使用DBServerProvider.手动获取数据库相关信息
*用户信息、权限、角色等使用UserContext.Current操作
*Content_IntroService对增、删、改查、导入、导出、审核业务代码扩展参照ServiceFunFilter
*/
using GuXin.Core.BaseProvider;
using GuXin.Core.Extensions.AutofacManager;
using GuXin.Entity.DomainModels;
using System.Linq;
using GuXin.Core.Utilities;
using System.Linq.Expressions;
using GuXin.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using GuXin.Business.IRepositories;
using System;
using GuXin.Entity.AppDto.Input;
using System.Collections.Generic;
using GuXin.Core.ManageUser;

namespace GuXin.Business.Services
{
    public partial class Content_IntroService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IContent_IntroRepository _repository;//访问数据库
        private readonly IContent_Intro_PraiseRepository _Intro_PraiseRepository;//访问数据库
        private readonly IContent_FileRepository _FileRepository;

        [ActivatorUtilitiesConstructor]
        public Content_IntroService(
            IContent_IntroRepository dbRepository, IContent_FileRepository content_FileRepository, IContent_Intro_PraiseRepository content_Intro_PraiseRepository,
            IHttpContextAccessor httpContextAccessor
            )
        : base(dbRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _repository = dbRepository;
            _FileRepository = content_FileRepository;
            _Intro_PraiseRepository = content_Intro_PraiseRepository;
            //多租户会用到这init代码，其他情况可以不用
            //base.Init(dbRepository);
        }


        public WebResponseContent Del(Guid guid)
        {
            if (!_repository.Exists(M => M.IntroGuid == guid && M.CreateID == UserContext.Current.UserId))
                return WebResponseContent.Instance.Error("您没有权限删除此条简介");
            return _repository.DbContextBeginTransaction(() =>
            {
                _repository.Delete(m => m.IntroGuid == guid);
                _FileRepository.Delete(m => m.ContentGuid == guid);
                return WebResponseContent.Instance.Info(_repository.SaveChanges() > 0, "简介删除成功");
            });
        }
        public WebResponseContent Praise(Guid guid)
        {
            WebResponseContent webResponse = WebResponseContent.Instance;
            var model = _repository.FindAsIQueryable(m => m.IntroGuid == guid).AsTracking().FirstOrDefault();
            var praiseModel = _Intro_PraiseRepository.FindFirst(m => m.IntroGuid == guid && m.CreateID == UserContext.Current.UserId);
            if (praiseModel != null)
            {
                model.PraiseCount--;
                _Intro_PraiseRepository.DeleteWithKeys(new object[1] { praiseModel.Id });
                webResponse.SuccessMessage = "已取消点赞";
                webResponse.Data = -1;
                return webResponse.Info(_repository.SaveChanges());
            }
            else
            {
                praiseModel = new Content_Intro_Praise() { IntroGuid = guid }.SetCreateDefaultVal();
                _Intro_PraiseRepository.Add(praiseModel);
                model.PraiseCount++;
                webResponse.Data = 1;
                return webResponse.Info(_repository.SaveChanges());
            }
        }
        public bool Post(List<ContentIntroInput> inputDtos)
        {
            int result = 0;
            inputDtos.ForEach(inputDto =>
            {
                Content_Intro entity = new Content_Intro().SetCreateDefaultVal();
                inputDto.MapValueToEntity<ContentIntroInput, Content_Intro>(entity);
                _repository.Add(entity);
                if (inputDto.Pics?.Count > 0)
                {
                    entity.PicCount = inputDto.Pics.Count;
                    inputDto.Pics.ForEach(m =>
                    {
                        m.ContentGuid = entity.IntroGuid;
                        _FileRepository.Add(m.SetCreateDefaultVal());
                    });
                }
                result += _repository.SaveChanges();
            });
            return result > 0;
        }

        public bool Put(ContentIntroInput inputDto)
        {
            var entity = _repository.FindAsIQueryable(m => m.IntroGuid == inputDto.IntroGuid).AsTracking().FirstOrDefault();
            entity.PicCount = entity.PicCount + inputDto.Pics.Count;
            entity.Location = inputDto.Location;
            entity.BeginDate = inputDto.BeginDate;
            entity.EndDate = inputDto.EndDate;
            entity.Introduction = inputDto.Introduction;
            entity.ModifyDate = DateTime.Now;
            if (inputDto.DelFiles?.Length > 0)
            {
                _FileRepository.Delete(m => inputDto.DelFiles.Contains(m.Guid));
                entity.PicCount -= inputDto.DelFiles.Length;
            }
            if (inputDto.Pics.Count > 0)
                inputDto.Pics.Where(m => m.Guid.IsNullOrEmpty() || m.Guid == Guid.Empty).ToList().ForEach(m =>
                      {
                          m.ContentGuid = inputDto.IntroGuid.ToGuid();
                          _FileRepository.Add(m.SetCreateDefaultVal());
                      });
            return _repository.SaveChanges() > 0;
        }

        public IQueryable<object> Get(int userId, DateTime searchDate)
        {
            userId = userId == 0 ? UserContext.Current.UserId : userId;
            var result = _repository.FindAsIQueryable(m => m.CreateID == userId && m.CreateDate <= searchDate)
                .Select(m => new
                {
                    m.IntroGuid,
                    m.PraiseCount,
                    m.BeginDate,
                    m.EndDate,
                    m.CreateDate,
                    m.Introduction,
                    m.Location,
                    pics = m.ContentFiles.Where(X => X.CreateID == userId && X.ContentGuid == m.IntroGuid).Select(X => new { X.UploadPath, X.Length, X.Guid })
                })
                .OrderByDescending(m => m.CreateDate).Take(5);

            return result;
        }
    }
}
