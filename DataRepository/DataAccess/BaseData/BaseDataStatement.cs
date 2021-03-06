﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataRepository.DataAccess.BaseData
{
    public class BaseDataStatement
    {
        public static string GetAllCity = @"SELECT * FROM City(NOLOCK)";

        public static string GetAllProvince = @"SELECT ProvinceID,ProvinceName FROM Province(NOLOCK)";

        public static string GetAllBaseData = @"SELECT * FROM BaseData(NOLOCK)";

        public static string GetAllBaseDataByRule = @"SELECT * FROM BaseData(NOLOCK) WHERE 1=1 ";

        public static string GetBaseDataByKey = @"SELECT * FROM BaseData(NOLOCK) WHERE ID=@ID";

        public static string Remove = @"UPDATE BaseData SET Status=0 WHERE ID=@ID";

        public static string GetBaseDataByType = @"SELECT * FROM BaseData(NOLOCK) WHERE TypeCode=@TypeCode AND Status=1";

        public static string GetBaseDataByPCode = @"SELECT * FROM BaseData(NOLOCK) WHERE PCode=@PCode AND Status=1";

        public static string CreateNewData = @"INSERT INTO [BaseData]([TypeCode],[PCode],[ValueInfo],[Description],[Status],[CreateDate]) VALUES (@TypeCode,@PCode,@ValueInfo,@Description,@Status,@CreateDate)";

        public static string ModifyBaseData = @"UPDATE [BaseData]
                                               SET [TypeCode] =@TypeCode,[PCode]=@PCode,[ValueInfo]=@ValueInfo
                                                  ,[Description]=@Description
                                                  ,[Status] = @Status
                                             WHERE ID=@ID";
    }
}
