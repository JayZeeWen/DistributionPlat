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
        Succ = 1 ,

        /// <summary>
        /// 已回退
        /// </summary>
         Back = 2,
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
        AgentLevel = 2 ,

        /// <summary>
        /// 积分奖励配置
        /// </summary>
        ScoreConfigCate = 3 
    }

    public enum OrderState
    {
        /// <summary>
        /// 未下单
        /// </summary>
        InShoppingCard = 0 ,

        /// <summary>
        /// 未发货
        /// </summary>
        NoDeliver = 1 ,

        /// <summary>
        /// 已处理
        /// </summary>
        HadDeal = 2 
    }

    public enum AgentType
    {
        /// <summary>
        /// 体验店
        /// </summary>
        Exp =  0 ,

        /// <summary>
        /// 加盟店
        /// </summary>
        Fran = 1 
    }

    public enum OrderType
    {
        /// <summary>
        /// 产品订单
        /// </summary>
        Pro = 1 ,

        /// <summary>
        /// 加盟商订单
        /// </summary>
        Agent = 2 
    }

    public enum RewardConfigKey
    {
        /// <summary>
        /// 直推奖励
        /// </summary>
        firstRecomm = 1 ,
        
        /// <summary>
        /// 二代奖励
        /// </summary>
        secondRecomm = 2 ,

        /// <summary>
        /// 直推购买奖励
        /// </summary>
        firstBuy = 3 ,

        /// <summary>
        /// 二代购买奖励
        /// </summary>
        secondBuy = 4,

        /// <summary>
        /// 省级代理推荐奖励
        /// </summary>
        provinceRecomm = 5 ,


        /// <summary>
        /// 体验店直推奖励
        /// </summary>
        expFirstRecomm = 6,


        /// <summary>
        /// 体验店二代奖励
        /// </summary>
        expSecondRecomm = 7,


        /// <summary>
        /// 体验店直推购买奖励
        /// </summary>
        expFirstBuy = 8,


        /// <summary>
        /// 体验店二代购买奖励
        /// </summary>
        expSecondBuy = 9,


        /// <summary>
        /// 体验店升级积分
        /// </summary>
        expLevelUpScore = 10,


        /// <summary>
        /// 产品起购数量
        /// </summary>
        productAmount = 11,
    }
}
