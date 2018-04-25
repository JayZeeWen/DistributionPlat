using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Distribution.Model
{
    /// <summary>
    /// 奖励类型
    /// </summary>
    public enum RewartType
    {
        /// <summary>
        /// 推荐代理商奖励
        /// </summary>
        Recommend = 1,

        /// <summary>
        /// 购买产品奖励
        /// </summary>
        Purchase = 2
    }

    /// <summary>
    /// 代理商等级
    /// </summary>
    public enum AgentLevel
    {
        /// <summary>
        /// 加盟店
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 市级代理
        /// </summary>
        CityAgent = 2,

        /// <summary>
        /// 省级代理
        /// </summary>
        ProvieceAgent = 3
    }

    /// <summary>
    /// 积分提现处理状态
    /// </summary>
    public enum CashScoreState
    {
        /// <summary>
        /// 受理中
        /// </summary>
        Dealing = 0,

        /// <summary>
        /// 已受理
        /// </summary>
        Succ = 1
    }

    /// <summary>
    /// 公共配置目录id
    /// </summary>
    public enum ConfigCategory
    {
        /// <summary>
        /// 职位等级
        /// </summary>
        PostitionLevel = 1 ,

        /// <summary>
        /// 代理商等级
        /// </summary>
        AgentLevel = 2 
    }
}
