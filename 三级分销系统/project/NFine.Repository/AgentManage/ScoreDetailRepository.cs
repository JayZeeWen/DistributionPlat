
/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System;
using System.Linq;

namespace NFine.Repository.SystemManage
{
    public class ScoreDetailRepository : RepositoryBase<ScoreDetailEntity>, IScoreDetailRepository
    {
        public List<ScoreDetailEntity> GetDetailList(Pagination page)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format(@" select * from (
                        select row_number() over( order by score.{0} ) as rowNum
	                        ,agent.c_name
	                        ,score.* 
	                        from t_score_detail as score
	                        left join t_agent as agent
		                        on agent.f_id = score.c_user_id 
		                        )  as t 
", page.sidx));
            DbParameter[] parameter =
            {

            };

            var list = FindList(strSql.ToString(), parameter);
            page.records = list.Count;
            list =  list.Skip((page.page - 1) * page.rows).Take(page.rows).ToList();
            return list;
        }

        void IScoreDetailRepository.DeleteForm(string keyValue)
        {
            throw new NotImplementedException();
        }

        void IScoreDetailRepository.SubmitForm(ScoreDetailEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            throw new NotImplementedException();
        }
    }
}
