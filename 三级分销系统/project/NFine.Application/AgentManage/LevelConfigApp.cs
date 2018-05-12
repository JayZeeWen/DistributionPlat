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

namespace NFine.Application.SystemManage
{
    public class LevelConfigApp
    {
        private ILevelConfigRepository service = new LevelConfigRepository();
        private CommConfigApp configApp = new CommConfigApp();



        public LevelConfigEntity GetForm(string keyValue)
        {
            return service.FindEntity(keyValue);
        }

        public LevelDetailEntity GetDetailForm(string keyValue)
        {
            LevelConfigEntity entity = GetForm(keyValue);
            LevelDetailEntity detail = new LevelDetailEntity();
            SetDetialFromOrg(detail, entity);
            return detail;
        }

        public void SetDetialFromOrg(LevelDetailEntity detail , LevelConfigEntity entity)
        {
            Pagination page = new Pagination();
            var configList = configApp.GetItemList(page,"1");
            detail.F_Id = entity.F_Id;
            detail.c_level = entity.c_level;
            detail.LevelName = configList.Find(t => t.c_key == detail.c_level).c_value;
            detail.c_need_nums = entity.c_need_nums;
            detail.c_need_level = entity.c_need_level;
            var needLeve = configList.Find(t => t.c_key == detail.c_need_level);
            if(needLeve != null)
            {
                detail.needLevelName = needLeve.c_value;
            }
            
            detail.c_level_num = entity.c_level_num;
            detail.c_recomm_reward = entity.c_recomm_reward;
            detail.c_buy_reward = entity.c_buy_reward;
            detail.c_is_delete = entity.c_is_delete;
            detail.c_create_date = entity.c_create_date;
            detail.F_CreatorTime = entity.F_CreatorTime;
            detail.F_CreatorUserId = entity.F_CreatorUserId;
            detail.F_LastModifyTime = entity.F_LastModifyTime;
            detail.F_LastModifyUserId = entity.F_LastModifyUserId;
            detail.F_DeleteTime = entity.F_DeleteTime;
            detail.F_DeleteUserId = entity.F_DeleteUserId;
            detail.F_DeleteMark = entity.F_DeleteMark;
        }
        public void DeleteForm(string keyValue)
        {
            service.DeleteForm(keyValue);
        }

        public List<LevelConfigEntity> GetItemList(Pagination pagination)
        {
            return service.FindList(pagination);
        }

        public void SubmitForm(LevelConfigEntity userEntity,  string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                userEntity.Modify(keyValue);
            }
            else
            {
                userEntity.Create();
            }
            service.SubmitForm(userEntity, keyValue);
        }
        public void UpdateForm(LevelConfigEntity userEntity)
        {
            service.Update(userEntity);
        }
    }
}
