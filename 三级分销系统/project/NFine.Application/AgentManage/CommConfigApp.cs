/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;

namespace NFine.Application.SystemManage
{
    public class CommConfigApp
    {
        private ICommConfigRepository service = new CommConfigRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        //public List<CommConfigEntity> GetList(Pagination pagination, string keyword)
        //{
        //    var expression = ExtLinq.True<CommConfigEntity>();
        //    if (!string.IsNullOrEmpty(keyword))
        //    {
        //        expression = expression.And(t => t.c_name.Contains(keyword));
        //        expression = expression.Or(t => t.c_bank_name.Contains(keyword));
        //        expression = expression.Or(t => t.c_mobile.Contains(keyword));
        //    }
        //    expression = expression.And(t => t.c_name != "admin");
        //    return service.FindList(expression, pagination);
        //}


        public CommConfigEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }

        public List<CommConfigEntity> GetItemList(string enCode)
        {
            return service.GetItemList(enCode);
        }
        public void SubmitForm(CommConfigEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
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
        public void UpdateForm(CommConfigEntity userEntity)
        {
            service.Update(userEntity);
        }
    }
}
