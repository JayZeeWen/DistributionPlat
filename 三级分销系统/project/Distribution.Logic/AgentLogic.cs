using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distribution.DB;
using Distribution.Model;
using System.Linq.Expressions;

namespace Distribution.Logic
{
    public static class AgentLogic
    {
        #region BasicMethod
        public static Agent GetEnityById(int id)
        {
            using (DistributionContext context = new DistributionContext())
            {
                return context.t_agent.Find(id);
            }
        }

        public static Agent FindEntity(Expression<Func<Agent, bool>> predicate)
        {
            return CommLogic.FindEntity<Agent>(predicate);
        }


        public static List<Agent> GetList()
        {
            List<Agent> list = new List<Agent>();
            using (DistributionContext context = new DistributionContext())
            {
                list = context.t_agent.ToList();
            }
            return list;
        }

        public static void InsertNewEntiy(Agent NewAgent)
        {
            using (DistributionContext context = new DistributionContext())
            {
                context.t_agent.Add(NewAgent);
                context.SaveChanges();
            }
        }

        public static void UpdateEntity(Agent UpdateAgent)
        {
            using (DistributionContext context = new DistributionContext())
            {
                Agent ag = context.t_agent.Find(UpdateAgent.c_id);
                CommLogic.DeepClone<Agent>(ag, UpdateAgent);
                context.SaveChanges();
            }
        }

        public static void DeleteEntity(int AgentId)
        {
            using (DistributionContext context = new DistributionContext())
            {
                Agent DelAgent = context.t_agent.Find(AgentId);
                context.t_agent.Remove(DelAgent);
                context.SaveChanges();
            }
        } 
        #endregion

        public static Agent CheckLogin(string mobile, string password)
        {
            Agent userEntity = AgentLogic.FindEntity(t => t.c_mobile == mobile);
            if (userEntity != null)
            {
                if (userEntity.c_state  >= 1)
                {
                    string dbPassword = userEntity.c_login_pwd;
                    if (dbPassword == password)
                    {
                        return userEntity;
                    }
                    else
                    {
                        throw new Exception("密码不正确，请重新输入");
                    }
                }
                else
                {
                    throw new Exception("账户未审核,请等待审核通过");
                }
            }
            else
            {
                throw new Exception("账户不存在，请重新输入");
            }
        }

        public static bool CheckRegist(string reg_Mobile ,string reco_Mobile,string pwd,string voucherPath)
        {
            bool result = true;
            Agent rec_ag = FindEntity(t => t.c_mobile == reco_Mobile);
            if (rec_ag == null)
            {
                throw new Exception("未找到推荐人，请输入正确的推荐人手机号码");
            }
            Agent reg_ag = FindEntity(t => t.c_mobile == reg_Mobile);
            if(reg_ag != null )
            {
                throw new Exception("该手机已被注册，请输入正确的手机号码或联系管理员");
            }
            reg_ag = new Agent();
            reg_ag.c_mobile = reg_Mobile;
            reg_ag.c_login_pwd = pwd;
            reg_ag.c_levle = 1;
            reg_ag.c_state = 0;
            reg_ag.c_create_date = DateTime.Now;
            reg_ag.c_score = 0;
            reg_ag.c_voucher_path = voucherPath;
            InsertNewEntiy(reg_ag);

            AgentRelation ar = new AgentRelation();
            ar.c_parent_id = rec_ag.c_id;
            ar.c_child_id = reg_ag.c_id;
            ar.c_create_date = DateTime.Now;
            AgentRelationLogic.InsertNewEntiy(ar);
            return result;
        }

    }
}
