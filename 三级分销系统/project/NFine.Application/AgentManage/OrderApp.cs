/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using NFine.Code;
using NFine.Domain.Entity;
using NFine.Domain.Entity.SystemManage;
using NFine.Domain.IRepository.SystemManage;
using NFine.Repository.SystemManage;
using System;
using System.Collections.Generic;

namespace NFine.Application.SystemManage
{
    public class OrderApp
    {
        private IOrderRepository service = new OrderRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();

        public List<OrderEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<OrderEntity>();
            
            return service.FindList(expression, pagination);
        }

        public List<OrderViewEntity> GetViewList(Pagination pagination, string keyword)
        {
            AgentApp app = new AgentApp();
            var expression = ExtLinq.True<OrderEntity>();

            var list =  service.FindList(expression, pagination);

            List<OrderViewEntity> viewList = new List<OrderViewEntity>();
            foreach (var item in list)
            {
                OrderViewEntity entity = new OrderViewEntity();
                var agent = app.GetForm(item.c_agent_id);
                if(agent != null )
                {
                    entity.c_agent_name = agent.c_name;
                }
                entity.F_Id = item.F_Id;
                entity.c_agent_id = item.c_agent_id;
                entity.c_order_num = item.c_order_num;
                entity.c_total = item.c_total;
                entity.c_state = item.c_state;
                entity.c_express_num = item.c_express_num;
                entity.c_express_name = item.c_express_name;
                entity.c_rec_person = item.c_rec_person;
                entity.c_mobile = item.c_mobile;
                entity.c_address = item.c_address;
                entity.F_CreatorTime = item.F_CreatorTime;
                viewList.Add(entity);
            }
            return viewList;
        }


        public OrderEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(OrderEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, userLogOnEntity, keyValue);
        }
        public void UpdateForm(OrderEntity userEntity)
        {
            service.Update(userEntity);
        }
        

    }
}
