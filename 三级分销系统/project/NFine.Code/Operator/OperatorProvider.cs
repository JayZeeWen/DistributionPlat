/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: NFine
 * Description: 三级分销平台
 * Website：http://www.nfine.cn
*********************************************************************************/
namespace NFine.Code
{
    public class OperatorProvider
    {
        public static OperatorProvider Provider
        {
            get { return new OperatorProvider(); }
        }
        private string LoginUserKey = Configs.GetValue("LoginUserKey");
        private string LoginProvider = Configs.GetValue("LoginProvider");
        private string AgentInfoKey = Configs.GetValue("AgentInfoKey");

        

        public OperatorModel GetCurrent()
        {
            OperatorModel operatorModel = new OperatorModel();
            if (LoginProvider == "Cookie")
            {
                operatorModel = DESEncrypt.Decrypt(WebHelper.GetCookie(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }
            else
            {
                operatorModel = DESEncrypt.Decrypt(WebHelper.GetSession(LoginUserKey).ToString()).ToObject<OperatorModel>();
            }
            return operatorModel;
        }
        public void AddCurrent(OperatorModel operatorModel,string loginKey = "")
        {
            if (string.IsNullOrEmpty(loginKey))
            {
                loginKey = LoginUserKey;
            }
            if (LoginProvider == "Cookie")
            {
                WebHelper.WriteCookie(loginKey, DESEncrypt.Encrypt(operatorModel.ToJson()), 60);
            }
            else
            {
                WebHelper.WriteSession(loginKey, DESEncrypt.Encrypt(operatorModel.ToJson()));
            }
            WebHelper.WriteCookie("nfine_mac", Md5.md5(Net.GetMacByNetworkInterface().ToJson(), 32));
            WebHelper.WriteCookie("nfine_licence", Licence.GetLicence());
        }

        public void AddCurrentAgentInfo(AgentInfo ag ,string key)
        {
            string k = AgentInfoKey + key;
            if (LoginProvider == "Cookie")
            {
                WebHelper.WriteCookie(k , ag.ToJson(), 60);
            }
            else
            {
                WebHelper.WriteSession(k , ag.ToJson());
            }
        }
        public AgentInfo GetAgentInfo(string key)
        {
            key = AgentInfoKey + key;
            AgentInfo s = new AgentInfo();
            if (LoginProvider == "Cookie")
            {
                s = WebHelper.GetCookie(key).ToString().ToObject<AgentInfo>();
            }
            else
            {
                s = WebHelper.GetSession(key).ToString().ToObject<AgentInfo>();
            }
            return s;
        }
        public void RemoveCurrent()
        {
            if (LoginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(LoginUserKey.Trim());
            }
            else
            {
                WebHelper.RemoveSession(LoginUserKey.Trim());
            }
        }
        public void RemoveAgentInfo(string key )
        {
            key = AgentInfoKey + key;
            if (LoginProvider == "Cookie")
            {
                WebHelper.RemoveCookie(key);
            }
            else
            {
                WebHelper.RemoveSession(key);
            }
        }
    }
}
