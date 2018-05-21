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
using System.Linq;

namespace NFine.Application.SystemManage
{
    public class AgentApp
    {
        private IAgentRepository service = new AgentRepository();
        private UserLogOnApp userLogOnApp = new UserLogOnApp();
        private AgentRelationApp arApp = new AgentRelationApp();

        public List<AgentEntity> GetList(Pagination pagination, string keyword)
        {
            var expression = ExtLinq.True<AgentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.c_name.Contains(keyword));
                expression = expression.Or(t => t.c_bank_name.Contains(keyword));
                expression = expression.Or(t => t.c_mobile.Contains(keyword));
            }
            expression = expression.And(t => t.c_name != "admin");
            return service.FindList(expression, pagination);
        }

        public List<AgentEntity> GetList(Pagination pagination, string keyword,int level,int  agentLevel,int state)
        {
            var expression = ExtLinq.True<AgentEntity>();
            if (!string.IsNullOrEmpty(keyword))
            {
                expression = expression.And(t => t.c_name.Contains(keyword));
                expression = expression.Or(t => t.c_bank_name.Contains(keyword));
                expression = expression.Or(t => t.c_mobile.Contains(keyword));
            }
            if (level != 0 )
            {
                if(level == -1 )
                {
                    expression = expression.And(t => t.c_agnet_type == 0 );
                }
                else
                {
                    expression = expression.And(t => t.c_levle == level);
                }
                
            }
            if (agentLevel != 0 )
            {
                expression = expression.And(t => t.c_agent_level == agentLevel);
            }
            if(state != -1 )
            {
                expression = expression.And(t => t.c_state == state);
            }
            expression = expression.And(t => t.c_name != "admin");
            return service.FindList(expression, pagination);
        }

        public List<AgentViewModel> GetViewList(Pagination pagination, string keyword, int level, int agentLevel, int state)
        {
            List<AgentViewModel> viewList = new List<AgentViewModel>();
            var list = GetList(pagination, keyword, level, agentLevel, state);
            foreach (var item in list )
            {
                AgentViewModel entity = new AgentViewModel();
                SetViewEntity(item, entity);
                viewList.Add(entity);
            }
            int i = 0;
            List<string> pid = new List<string>();
            pid.Add(Configs.GetValue("RootAgent"));
            GetListGen(viewList,pid, ref i);
            return viewList;
        }

        public void SetViewEntity(AgentEntity entity , AgentViewModel viewEntity)
        {
            viewEntity.F_Id = entity.F_Id;
            viewEntity.c_name = entity.c_name;
            viewEntity.c_mobile = entity.c_mobile;
            viewEntity.c_login_pwd = entity.c_login_pwd;
            viewEntity.c_safe_pwd = entity.c_safe_pwd;
            viewEntity.c_bank_name = entity.c_bank_name;
            viewEntity.c_bank_account = entity.c_bank_account;
            viewEntity.c_address = entity.c_address;
            viewEntity.c_state = entity.c_state;
            viewEntity.c_score = entity.c_score;
            viewEntity.c_levle = entity.c_levle;
            viewEntity.c_agent_level = entity.c_agent_level;
            viewEntity.c_had_reward = entity.c_had_reward;
            viewEntity.c_agnet_type = entity.c_agnet_type;
            viewEntity.c_exp_state = entity.c_exp_state;
            viewEntity.c_rec_person = entity.c_rec_person;
            viewEntity.c_rec_mobile = entity.c_rec_mobile;
            viewEntity.c_bank_person = entity.c_bank_person;
            viewEntity.c_voucher_path = entity.c_voucher_path;
            viewEntity.c_create_date = entity.c_create_date;
            viewEntity.F_CreatorUserId = entity.F_CreatorUserId;
            viewEntity.F_LastModifyTime = entity.F_LastModifyTime;
            viewEntity.F_CreatorTime = entity.F_CreatorTime;
            viewEntity.F_LastModifyUserId = entity.F_LastModifyUserId;
            viewEntity.F_DeleteTime = entity.F_DeleteTime;
            viewEntity.F_DeleteUserId = entity.F_DeleteUserId;
            viewEntity.F_DeleteMark = entity.F_DeleteMark;
            
        }
        
        public void GetListGen(List<AgentViewModel> viewList,List<string> parentIds , ref int genNum  )
        {
            genNum += 1;
            if (parentIds.Count() == 0 )
            {
                return;
            }
            if (viewList.Where(t => t.agentGen == null ).Count() > 0 )
            {
                var pList =  arApp.GetListByParentId(parentIds);
                List<string> cIds = pList.Select(f => f.c_child_id).ToList();
                if (viewList.Where(f => cIds.Contains(f.F_Id)).Count() > 0 )
                {
                    foreach (var item in viewList)
                    {
                        if (cIds.Contains(item.F_Id))
                        {
                            item.agentGen = genNum;
                        }
                    }
                }
                GetListGen(viewList, cIds,ref  genNum);

            }
            else
            {
                return;
            }
        }

        public List<AgentEntity> GetAgentList(string state)
        {
            return service.GetAgentList(state);
        }

        public int GetTotalScore()
        {
            var list =  service.FindList(@"select * from t_agent ");
            int sum = (int)list.Sum(t => t.c_score);
            return sum;
        }

        public AgentEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public AgentEntity GetFormWithoutTrack(string keyValue)
        {
            return service.FindEntity(keyValue);
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }
        public void SubmitForm(AgentEntity userEntity, UserLogOnEntity userLogOnEntity, string keyValue)
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
        public void UpdateForm(AgentEntity userEntity)
        {
            service.Update(userEntity);
        }

        public bool hadReward(string agentId)
        {
            var result  = service.GetAgentHadReward(agentId);
            bool hadReward = result == true ? true : false;
            return hadReward;
        }
        public AgentEntity CheckLogin(string username, string password)
        {
            AgentEntity userEntity = service.FindEntity(t => t.c_name == username);
            if (userEntity != null)
            {
                if (userEntity.c_state == 1)
                {
                    UserLogOnEntity userLogOnEntity = userLogOnApp.GetForm(userEntity.F_Id);
                    string dbPassword = Md5.md5(DESEncrypt.Encrypt(password.ToLower(), userLogOnEntity.F_UserSecretkey).ToLower(), 32).ToLower();
                    if (dbPassword == userLogOnEntity.F_UserPassword)
                    {
                        DateTime lastVisitTime = DateTime.Now;
                        int LogOnCount = (userLogOnEntity.F_LogOnCount).ToInt() + 1;
                        if (userLogOnEntity.F_LastVisitTime != null)
                        {
                            userLogOnEntity.F_PreviousVisitTime = userLogOnEntity.F_LastVisitTime.ToDate();
                        }
                        userLogOnEntity.F_LastVisitTime = lastVisitTime;
                        userLogOnEntity.F_LogOnCount = LogOnCount;
                        userLogOnApp.UpdateForm(userLogOnEntity);
                        return userEntity;
                    }
                    else
                    {
                        throw new Exception("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户还未审核通过,请联系管理员");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }
    }
}
