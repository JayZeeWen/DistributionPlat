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
    public class ProductApp
    {
        private IProductRepository service = new ProductRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<ProductEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<ProductEntity>();
            
            return service.FindList(expression, pagination);
        }
        


        public ProductEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(ProductEntity userEntity, string keyValue)
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
        public void UpdateForm(ProductEntity userEntity)
        {
            service.Update(userEntity);
        }
        

    }
}
