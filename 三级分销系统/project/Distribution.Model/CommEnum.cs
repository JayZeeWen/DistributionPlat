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
}
