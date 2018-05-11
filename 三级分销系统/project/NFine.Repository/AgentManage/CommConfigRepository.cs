/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace NFine.Repository.SystemManage
{
    public class CommConfigRepository : RepositoryBase<CommConfigEntity>, ICommConfigRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<CommConfigEntity>(t => t.F_Id == keyValue);
                db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(CommConfigEntity userEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                else
                {
                    db.Insert(userEntity);
                }
                db.Commit();
            }
        }

        public List<CommConfigEntity> GetItemList(string categoryId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format( @"SELECT  d.*
                            FROM    t_common_config d
                            WHERE   1 = 1
                                    AND d.c_category_id = {0}
                                    AND d.F_DeleteMark = 0", categoryId));
            DbParameter[] parameter = 
            {
                 new SqlParameter("@enCode",categoryId)
            };
            return this.FindList(strSql.ToString(), parameter);
        }

    }
}
