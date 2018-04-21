/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using System;

namespace NFine.Domain.Entity.SystemManage
{
    public class AgentEntity : IEntity<AgentEntity> 
    {
        public int c_id { get; set; }

        
        public string c_name { get; set; }

        
        public string c_mobile { get; set; }

        
        public string c_login_pwd { get; set; }

        
        public string c_safe_pwd { get; set; }

        
        public string c_bank_name { get; set; }

      
        public string c_bank_account { get; set; }

       
        public string c_address { get; set; }

        public int? c_state { get; set; }

        public int? c_score { get; set; }

        public int? c_levle { get; set; }

        public int? c_agent_level { get; set; }

        public DateTime? c_create_date { get; set; }
    }
}
