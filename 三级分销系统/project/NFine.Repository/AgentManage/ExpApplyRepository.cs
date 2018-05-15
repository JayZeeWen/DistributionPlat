/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace NFine.Repository.SystemManage
{
    public class ExpApplyRepository : RepositoryBase<ExpApplyEntity>, IExpApplyRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<ExpApplyEntity>(t => t.F_Id == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(ExpApplyEntity userEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Update(userEntity);
                
                db.Commit();
            }
        }

    }
}
