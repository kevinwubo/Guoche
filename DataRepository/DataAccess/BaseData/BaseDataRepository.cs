﻿/* ==============================================================================
 * Copyright (C) CtripCorpBiz OR Author. All rights reserved.
 * 
 * 类名称：MenuRepository
 * 类描述：
 * 创建人：Ethen Shen
 * 创建时间：4/28/2018 9:56:46 AM
 * 修改人：
 * 修改时间：
 * 修改备注：
 * 代码请保留相关关键处的注释
 * ==============================================================================*/

using DataRepository.DataModel;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    public class BaseDataRepository : DataAccessBase
    {

        public List<CityInfo> GetAllCity()
        {
            List<CityInfo> result = new List<CityInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetAllCity, "Text"));
            result = command.ExecuteEntityList<CityInfo>();
            return result;
        }

        public List<ProvinceInfo> GetAllProvince()
        {
            List<ProvinceInfo> result = new List<ProvinceInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetAllProvince, "Text"));
            result = command.ExecuteEntityList<ProvinceInfo>();
            return result;
        }


        public List<BaseDataInfo> GetAllData()
        {
            List<BaseDataInfo> result = new List<BaseDataInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetAllBaseData, "Text"));
            result = command.ExecuteEntityList<BaseDataInfo>();
            return result;
        }

        public List<BaseDataInfo> GetAllDataByPCode(string pcode)
        {
            List<BaseDataInfo> result = new List<BaseDataInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetBaseDataByPCode, "Text"));
            command.AddInputParameter("@PCode", DbType.String, pcode);
            result = command.ExecuteEntityList<BaseDataInfo>();
            return result;
        }

        public List<BaseDataInfo> GetDataByType(string typeCode)
        {
            List<BaseDataInfo> result = new List<BaseDataInfo>();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetBaseDataByType, "Text"));
            command.AddInputParameter("@TypeCode", DbType.String, typeCode);
            result = command.ExecuteEntityList<BaseDataInfo>();
            return result;
        }

        public List<BaseDataInfo> GetBaseDataByRule(string desc)
        {
            List<BaseDataInfo> result = new List<BaseDataInfo>();
            string sqlText = BaseDataStatement.GetAllBaseDataByRule;
            if (!string.IsNullOrEmpty(desc))
            {
                sqlText += " AND Description LIKE '%'+@key+'%'";
            }


            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(sqlText, "Text"));
            if (!string.IsNullOrEmpty(desc))
            {
                command.AddInputParameter("@key", DbType.String, desc);
            }

            result = command.ExecuteEntityList<BaseDataInfo>();
            return result;
        }

        public BaseDataInfo GetBaseDataByKey(int id)
        {
            BaseDataInfo result = new BaseDataInfo();
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.GetBaseDataByKey, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, id);
            result = command.ExecuteEntity<BaseDataInfo>();
            return result;
        }

        public int CreateNew(BaseDataInfo data)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.CreateNewData, "Text"));
           command.AddInputParameter("@TypeCode",DbType.String,data.TypeCode);
		   command.AddInputParameter("@PCode",DbType.String,data.PCode);
		   command.AddInputParameter("@ValueInfo",DbType.String,data.ValueInfo);
		   command.AddInputParameter("@Description",DbType.String,data.Description);
		   command.AddInputParameter("@Status",DbType.Int32,data.Status);
           command.AddInputParameter("@CreateDate", DbType.DateTime, data.CreateDate);

            return command.ExecuteNonQuery();
        }

        public int ModifyData(BaseDataInfo data)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.ModifyBaseData, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, data.ID);
            command.AddInputParameter("@TypeCode", DbType.String, data.TypeCode);
            command.AddInputParameter("@PCode", DbType.String, data.PCode);
            command.AddInputParameter("@ValueInfo", DbType.String, data.ValueInfo);
            command.AddInputParameter("@Description", DbType.String, data.Description);
            command.AddInputParameter("@Status", DbType.Int32, data.Status);

            return command.ExecuteNonQuery();
        }

        public int Remove(int id)
        {
            DataCommand command = new DataCommand(ConnectionString, GetDbCommand(BaseDataStatement.Remove, "Text"));
            command.AddInputParameter("@ID", DbType.Int32, id);
            int result=command.ExecuteNonQuery();
            return result;
        }
    }
}
