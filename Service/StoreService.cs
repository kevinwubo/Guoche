﻿/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：StoreService
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：5/2/2018 2:19:17 PM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using DataRepository.DataAccess.Store;
using DataRepository.DataModel;
using Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Helper;

namespace Service.BaseBiz
{
    public class StoreService:BaseService
    {
        private static StoreInfo TranslateStoreInfo(StoreEntity storeEntity)
        {
            StoreInfo storeInfo = new StoreInfo();
            if (storeEntity != null)
            {
                 string[] attachmentids = storeEntity.AttachmentIDs.Split(',');
                  storeInfo.SupplierID=storeEntity.SupplierID;
                  storeInfo.SupplierName=storeEntity.SupplierName;
                  storeInfo.SupplierCode=storeEntity.SupplierCode;
                  storeInfo.SupplierType=storeEntity.SupplierType;
                  storeInfo.CityID=storeEntity.CityID;
                  storeInfo.Address=storeEntity.Address;
                  storeInfo.Telephone=storeEntity.Telephone;
                  storeInfo.Mobile=storeEntity.Mobile;
                  storeInfo.StartTime=storeEntity.StartTime;
                  storeInfo.EndTime=storeEntity.EndTime;
                  storeInfo.Coordinate=storeEntity.Coordinate;
                  storeInfo.Status=storeEntity.Status;
                  storeInfo.AttachmentIDs=storeEntity.AttachmentIDs;
                  storeInfo.CreateDate=storeEntity.CreateDate;
                  storeInfo.ModifyDate=storeEntity.ModifyDate;
                  storeInfo.Operator = storeEntity.Operator;
            }


            return storeInfo;
        }

        private static StoreEntity TranslateStoreEntity(StoreInfo storeInfo)
        {
            StoreEntity storeEntity = new StoreEntity();
            if (storeInfo != null)
            {
                storeEntity.SupplierID = storeInfo.SupplierID;
                storeEntity.SupplierName = storeInfo.SupplierName;
                storeEntity.SupplierCode = storeInfo.SupplierCode;
                storeEntity.SupplierType = storeInfo.SupplierType;
                storeEntity.CityID = storeInfo.CityID;
                storeEntity.Address = storeInfo.Address;
                storeEntity.Telephone = storeInfo.Telephone;
                storeEntity.Mobile = storeInfo.Mobile;
                storeEntity.StartTime = storeInfo.StartTime;
                storeEntity.EndTime = storeInfo.EndTime;
                storeEntity.Coordinate = storeInfo.Coordinate;
                storeEntity.Status = storeInfo.Status;
                storeEntity.AttachmentIDs = storeInfo.AttachmentIDs;
                storeEntity.CreateDate = storeInfo.CreateDate;
                storeEntity.ModifyDate = storeInfo.ModifyDate;
                storeEntity.Operator = storeInfo.Operator;
            }


            return storeEntity;
        }

        public static bool ModifyStore(StoreEntity storeEntity)
        {
            int result = 0;
            if (storeEntity != null)
            {
                StoreRepository mr = new StoreRepository();

                StoreInfo storeInfo = TranslateStoreInfo(storeEntity);


                if (storeEntity.SupplierID > 0)
                {
                    result = mr.ModifyStore(storeInfo);
                }
                else
                {
                    storeInfo.CreateDate = DateTime.Now;
                    storeInfo.ModifyDate = DateTime.Now;
                    result = mr.CreateNew(storeInfo);
                }

                List<StoreInfo> miList = mr.GetAllStore();//刷新缓存
                Cache.Add("StoreALL", miList);
            }
            return result > 0;
        }

        public static StoreEntity GetStoreById(int sid)
        {
            StoreEntity result = new StoreEntity();
            StoreRepository mr = new StoreRepository();
            StoreInfo info = mr.GetStoreByKey(sid);
            result = TranslateStoreEntity(info);
            return result;
        }

        public static List<StoreEntity> GetStoreAll()
        {
            List<StoreEntity> all = new List<StoreEntity>();
            StoreRepository mr = new StoreRepository();
            List<StoreInfo> miList = Cache.Get<List<StoreInfo>>("StoreALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllStore();
                Cache.Add("StoreALL", miList);
            }
            if (!miList.IsEmpty())
            {
                foreach (StoreInfo mInfo in miList)
                {
                    StoreEntity StoreEntity = TranslateStoreEntity(mInfo);
                    all.Add(StoreEntity);
                }
            }

            return all;

        }


        public static List<StoreEntity> GetStoreByRule(string name, int status)
        {
            List<StoreEntity> all = new List<StoreEntity>();
            StoreRepository mr = new StoreRepository();
            List<StoreInfo> miList = mr.GetStoresByRule(name, status);

            if (!miList.IsEmpty())
            {
                foreach (StoreInfo mInfo in miList)
                {
                    StoreEntity storeEntity = TranslateStoreEntity(mInfo);
                    all.Add(storeEntity);
                }
            }

            return all;

        }

        public static void Remove(int sid)
        {
            StoreRepository mr = new StoreRepository();
            mr.RemoveStore(sid);
        }
    }
}
