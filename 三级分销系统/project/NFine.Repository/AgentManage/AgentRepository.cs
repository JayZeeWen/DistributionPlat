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
    public class AgentRepository : RepositoryBase<AgentEntity>, IAgentRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<AgentEntity>(t => t.F_Id == keyValue);
                db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(AgentEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                else
                {
                    userLogOnEntity.F_Id = userEntity.F_Id;
                    userLogOnEntity.F_UserId = userEntity.F_Id;
                    userLogOnEntity.F_UserSecretkey = Md5.md5(Common.CreateNo(), 16).ToLower();
                    userLogOnEntity.F_UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.F_UserPassword, 32).ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    db.Insert(userEntity);
                    db.Insert(userLogOnEntity);
                }
                db.Commit();
            }
        }

        public List<AgentEntity> GetAgentList(string state)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select ap.c_name as parent_name,a.* 
                             from t_agent a 
                             left join t_agent_relation ar 
                              on a.f_id = ar.c_child_id 
                             left join t_agent ap 
                              on ap.f_id = ar.c_parent_id
                              where 1=1
                                    and a.c_state=@state
                              order by a.c_create_date asc;");
            DbParameter[] parameter = 
            {
                 new SqlParameter("@state",state)
            };
            return this.FindList(strSql.ToString(), parameter);
        }

        public bool? GetAgentHadReward(string agentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select * from t_agent  where F_id = '"+ agentId +"'");
            DbParameter[] parameter =
            {
            };
            var entity =  this.FindList(strSql.ToString(), parameter).Find(t => t.F_Id == agentId);
            return entity.c_had_reward;
        }
    }
}
