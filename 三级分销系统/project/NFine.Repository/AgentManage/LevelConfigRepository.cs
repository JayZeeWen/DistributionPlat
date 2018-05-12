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
    public class LevelConfigRepository : RepositoryBase<LevelConfigEntity>, ILevelConfigRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<LevelConfigEntity>(t => t.F_Id == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(LevelConfigEntity userEntity, string keyValue)
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




        public List<LevelConfigEntity> GetLevelConfigList(string state)
        {
            throw new System.NotImplementedException();
        }
    }
}
