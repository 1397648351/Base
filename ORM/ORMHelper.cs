/************************************************************************ 
 * 项目名称 ：ORM
 * 项目描述 ：
 * 文件名称 ：ORMHelper.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 15:58:24
 * 更新时间 ：2018/6/8 15:58:24
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using ORMAttributes;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class ORMHelper
    {
        private const string Str_Ex = "ORM异常";
        public List<T> ExecuteLsit<T>(string sql)
        {
            DbDataReader reader = null;
            string columName = string.Empty;
            try
            {
                List<T> list = new List<T>();
                Type type = typeof(T);
                if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type.IsEnum)
                {
                    while (reader.Read())
                    {
                        if (type.IsEnum)
                        {
                            list.Add((T)Enum.ToObject(type, reader.GetValue(0)));
                        }
                        else
                        {
                            list.Add((T)Convert.ChangeType(reader.GetValue(0), type));
                        }
                    }
                }
                else
                {
                    while (reader.Read())
                    {
                        T result = Activator.CreateInstance<T>();
                        PropertyInfo[] properyies = type.GetProperties();
                        foreach (PropertyInfo property in properyies)
                        {
                            columName = AttributeProcess.GetColumnName(property);
                            if (!ReaderExists(reader, columName)) continue;
                            var value = reader.GetValue(reader.GetOrdinal(columName));
                            if (property.PropertyType.IsPrimitive && value.Equals(DBNull.Value))
                                value = Activator.CreateInstance(property.PropertyType);
                            if (property.PropertyType.IsEnum)
                                property.SetValue(result, Enum.ToObject(property.PropertyType, value), null);
                            else
                                property.SetValue(result, Convert.ChangeType(value, property.PropertyType), null);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(Str_Ex, ex);
            }
        }

        private bool ReaderExists(DbDataReader dr, string columnName)
        {

            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (dr.GetSchemaTable().DefaultView.Count > 0);

        }
    }
}
