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
    public class ScoreCashRepository : RepositoryBase<ScoreCashEntity>, IScoreCashRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<ScoreCashEntity>(t => t.F_Id == keyValue);
                db.Delete<UserLogOnEntity>(t => t.F_UserId == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(ScoreCashEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
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

        public List<ScoreCashEntity> GetCashList(Pagination page)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format(@" select * from (
                        select row_number() over( order by score.{0} ) as rowNum
	                        ,agent.c_name
	                        ,score.* 
	                        from t_score_cash as score
	                        left join t_agent as agent
		                        on agent.f_id = score.c_user_id 
		                        )  as t 
",page.sidx));
            DbParameter[] parameter = 
            {
                 
            };

            return FindList(strSql.ToString(), parameter);
        }
    }
}
