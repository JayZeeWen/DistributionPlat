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

        public List<CommConfigEntity> GetList(Pagination pagination, int  categoryId,string key )
        {
            var expression = ExtLinq.True<CommConfigEntity>();
            if (categoryId != 0 )
            {
                expression = expression.And(t => t.c_category_id == categoryId);
            }
            if(!string.IsNullOrEmpty(key))
            {
                expression = expression.And(t => t.c_desc.Contains(key));
            }
            return service.FindList(expression, pagination);
        }

        public CommConfigEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }

        public List<CommConfigEntity> GetItemList(Pagination pagination,string enCode)
        {
            var list =  service.GetItemList(enCode);
            pagination.records = list.Count;
            return list;
        }
        public void SubmitForm(CommConfigEntity userEntity,  string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, keyValue);
        }
        public void UpdateForm(CommConfigEntity userEntity)
        {
            service.Update(userEntity);
        }
    }
}
