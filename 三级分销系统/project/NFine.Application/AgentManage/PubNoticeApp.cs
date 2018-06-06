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
    public class PubNoticeApp
    {
        private IPubNoticeRepository service = new PubNoticeRepository();

        public List<PubNoticeEntity> GetList(Pagination pagination)
        {
            var expression = ExtLinq.True<PubNoticeEntity>();
            
            
            return service.FindList(expression, pagination);
        }
        


        public PubNoticeEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(PubNoticeEntity userEntity, string keyValue)
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
        public void UpdateForm(PubNoticeEntity userEntity)
        {
            service.Update(userEntity);
        }
        

    }
}
