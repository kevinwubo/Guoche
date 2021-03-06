﻿using DataRepository.DataAccess.BaseData;
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
    public class BaseDataService : BaseService
    {
        private static BaseDataInfo TranslateBaseDataInfo(BaseDataEntity baseDataEntity)
        {
            BaseDataInfo baseDataInfo = new BaseDataInfo();
            if (baseDataEntity != null)
            {
                baseDataInfo.ID = baseDataEntity.ID;
                baseDataInfo.TypeCode = baseDataEntity.TypeCode ?? "";
                baseDataInfo.PCode = baseDataEntity.PCode??"";
                baseDataInfo.ValueInfo = baseDataEntity.ValueInfo ?? "";
                baseDataInfo.Description = baseDataEntity.Description ?? "";
                baseDataInfo.Status = baseDataEntity.Status;
            }

            return baseDataInfo;
        }

        private static BaseDataEntity TranslateBaseDataEntity(BaseDataInfo baseDataInfo)
        {
            BaseDataEntity baseDataEntity = new BaseDataEntity();
            if (baseDataInfo != null)
            {
                baseDataEntity.ID = baseDataInfo.ID;
                baseDataEntity.TypeCode = baseDataInfo.TypeCode;
                baseDataEntity.PCode = baseDataInfo.PCode;
                baseDataEntity.ValueInfo = baseDataInfo.ValueInfo;
                baseDataEntity.Description = baseDataInfo.Description;
                baseDataEntity.Status = baseDataInfo.Status;
            }

            return baseDataEntity;
        }

        public static bool ModifyBaseData(BaseDataEntity baseDataEntity)
        {
            int result = 0;
            if (baseDataEntity != null)
            {
                BaseDataRepository mr = new BaseDataRepository();

                BaseDataInfo baseDataInfo = TranslateBaseDataInfo(baseDataEntity);


                if (baseDataEntity.ID > 0)
                {
                    result = mr.ModifyData(baseDataInfo);
                }
                else
                {
                    baseDataInfo.CreateDate = DateTime.Now;
                    result = mr.CreateNew(baseDataInfo);
                }
                List<BaseDataInfo> miList = mr.GetAllData();//刷新缓存
                Cache.Add("BaseDataALL", miList);
            }
            return result > 0;
        }

        public static List<Province> GetAllProvince()
        {
            List<Province> result = Cache.Get<List<Province>>("ProvinceALL")??new List<Province>();

            if (result.IsEmpty())
            {
               BaseDataRepository mr = new BaseDataRepository();
               List<ProvinceInfo> pInfo = mr.GetAllProvince();

               foreach (var item in pInfo)
               {
                   Province p = new Province();
                   p.ProvinceID = item.ProvinceID;
                   p.ProvinceName = item.ProvinceName;
                   result.Add(p);
               }
               Cache.Add("ProvinceALL", result);
            }

            return result;
        }

        public static List<City> GetAllCity()
        {
            List<City> result = Cache.Get<List<City>>("CityALL") ?? new List<City>();
            List<Province> allProvinces = GetAllProvince();
            if (result.IsEmpty())
            {
                BaseDataRepository mr = new BaseDataRepository();
                List<CityInfo> cInfo = mr.GetAllCity();

                foreach (var item in cInfo)
                {
                    City c= new City();
                    c.CityID = item.CityID;
                    c.CityName = item.CityName;
                    c.ProvinceID = item.ProvinceID;
                    c.ProvinceInfo = allProvinces.FirstOrDefault(t => t.ProvinceID == item.ProvinceID) ?? new Province();
                    result.Add(c);
                }
                Cache.Add("CityALL", result);
            }

            return result;
        }

        public static BaseDataEntity GetBaseDataById(int id)
        {
            BaseDataEntity result = new BaseDataEntity();
            BaseDataRepository mr = new BaseDataRepository();
            BaseDataInfo info = mr.GetBaseDataByKey(id);
            result = TranslateBaseDataEntity(info);
            return result;
        }

        public static List<BaseDataEntity> GetBaseDataAll()
        {
            List<BaseDataEntity> all = new List<BaseDataEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<BaseDataInfo> miList = Cache.Get<List<BaseDataInfo>>("BaseDataALL");
            if (miList.IsEmpty())
            {
                miList = mr.GetAllData();
                Cache.Add("BaseDataALL", miList);
            }

            if (!miList.IsEmpty())
            {
                foreach (BaseDataInfo mInfo in miList)
                {
                    BaseDataEntity BaseDataEntity = TranslateBaseDataEntity(mInfo);
                    all.Add(BaseDataEntity);
                }
            }

            return all;
        }



        

        public static List<BaseDataEntity> GetBaseDataByType(string tcode)
        {
            List<BaseDataEntity> all = new List<BaseDataEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<BaseDataInfo> miList = mr.GetDataByType(tcode);

            if (!miList.IsEmpty())
            {
                foreach (BaseDataInfo mInfo in miList)
                {
                    BaseDataEntity BaseDataEntity = TranslateBaseDataEntity(mInfo);
                    all.Add(BaseDataEntity);
                }
            }

            return all;

        }

        public static List<BaseDataEntity> GetBaseDataByPCode(string pcode)
        {
            List<BaseDataEntity> all = new List<BaseDataEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<BaseDataInfo> miList = mr.GetAllDataByPCode(pcode);

            if (!miList.IsEmpty())
            {
                foreach (BaseDataInfo mInfo in miList)
                {
                    BaseDataEntity BaseDataEntity = TranslateBaseDataEntity(mInfo);
                    all.Add(BaseDataEntity);
                }
            }

            return all;

        }

        public static List<BaseDataEntity> GetBaseDataByRule(string desc)
        {
            List<BaseDataEntity> all = new List<BaseDataEntity>();
            BaseDataRepository mr = new BaseDataRepository();
            List<BaseDataInfo> miList = mr.GetBaseDataByRule(desc);

            if (!miList.IsEmpty())
            {
                foreach (BaseDataInfo mInfo in miList)
                {
                    BaseDataEntity BaseDataEntity = TranslateBaseDataEntity(mInfo);
                    all.Add(BaseDataEntity);
                }
            }

            return all;

        }


        public static void Remove(int id)
        {
            BaseDataRepository mr = new BaseDataRepository();
            mr.Remove(id);
        }
    }
}
