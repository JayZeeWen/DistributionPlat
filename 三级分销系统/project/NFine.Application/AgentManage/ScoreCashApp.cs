/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;

namespace NFine.Application.SystemManage
{
    public class ScoreCashApp
    {
        private IScoreCashRepository service = new ScoreCashRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<ScoreCashEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ScoreCashEntity>();
            
            return service.FindList(expression, pagination);
        }


        public ScoreCashEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(ScoreCashEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, userLogOnEntity, keyValue);
        }
        public void UpdateForm(ScoreCashEntity userEntity)
        {
            service.Update(userEntity);
        }

        public List<ScoreCashEntity> GetCashList(Pagination page)
        {
            return service.GetCashList(page);
        }
        
    }
}
