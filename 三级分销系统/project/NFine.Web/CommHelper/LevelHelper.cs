using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Distribution.DB;

namespace  ComHelper
{
    public static class LevelHelper
    {
        /// <summary>
        /// 代理等级提升判断并执行
        /// </summary>
        /// <param name="RecomAgent">代理商</param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool  IsLevelUpWithCondition(Agent RecomAgent,DistributionContext context)
        {
            bool result = false;
            int currentLevel = (int)RecomAgent.c_levle;//当前等级
            int maxLevel = (int)context.t_level_config.Where(t => t.c_is_delete == 0).Max(t => t.c_level);//当前系统的最高级别
            if(currentLevel == maxLevel)
            {
                return result;
            }
            
            var list = context.t_level_config.Where(f => f.c_is_delete == 0 && f.c_level == (currentLevel + 1));
            if(list.Count() > 0 )
            {
                //升级所需条件配置
                LevelConfig config = list.First();
                var list_direct = context.t_agent_relation.Where(f => f.c_parent_id == RecomAgent.c_id);//直推人数
                if (list_direct.Count() < config.c_need_nums)
                {
                    return false;
                }
                //达到相应等级的人数
                int levelCount = list_direct.Where(f => f.ChildrenAgent.c_levle >= config.c_need_level).Count();
                if (config.c_need_level == null)
                {
                    RecomAgent.c_levle = currentLevel + 1;
                    context.SaveChanges();
                    return true;
                }
                
                if(levelCount >= config.c_level_num)
                {
                    RecomAgent.c_levle = currentLevel + 1;
                    context.SaveChanges();
                    return true;
                }
            }
            return result;
        }
    }
}
