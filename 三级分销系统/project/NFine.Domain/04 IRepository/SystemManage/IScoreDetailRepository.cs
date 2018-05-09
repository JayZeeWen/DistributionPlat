/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Data;
using NFine.Domain.Entity;
using NFine.Domain.Entity.AgentManage;
using NFine.Domain.Entity.SystemManage;
using System.Collections.Generic;
using NFine.Code;

namespace NFine.Domain.IRepository.SystemManage
{
    public interface IScoreDetailRepository : IRepositoryBase<ScoreDetailEntity>
    {
        void DeleteForm(string keyValue);
        void SubmitForm(ScoreDetailEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue);
        List<ScoreDetailEntity> GetDetailList(Pagination page);
    }
}
