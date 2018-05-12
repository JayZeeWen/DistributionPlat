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
    public class OrderRepository : RepositoryBase<OrderEntity>, IOrderRepository
    {
        public void DeleteForm(string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                db.Delete<OrderEntity>(t => t.F_Id == keyValue);
                db.Commit();
            }
        }
        public void SubmitForm(OrderEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            using (var db = new RepositoryBase().BeginTrans())
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    db.Update(userEntity);
                }
                db.Commit();
            }
        }

        public List<OrderDetailEntity> GetDetailList(string orderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(string.Format(@" select * from t_order_detail 
                                        where c_order_id = '{0}'",orderId));
            DbParameter[] parameter = { };
            return FindList<OrderDetailEntity>(strSql.ToString(), parameter);
        }
    }
}
