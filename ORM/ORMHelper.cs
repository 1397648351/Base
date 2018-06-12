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
using DBUtility;

namespace ORM
{
    public class ORMHelper
    {
        private DbHelperBase DbHelper;

        protected Dictionary<string, object> options;

        public ORMHelper(string connStr, string type = "mysql")
        {
            switch (type.ToLower())
            {
                case "oracle":
                    DbHelper = new OracleHelper(connStr);
                    break;
                case "sqlserver":
                    DbHelper = new SQLHelper(connStr);
                    break;
                case "mysql":
                default:
                    DbHelper = new MysqlHelper(connStr);
                    break;
            }
            options = new Dictionary<string, object>();
        }

        public ORMHelper Where(object field, string op = null, string condition = null)//查询逻辑 and or xor
        {
            this.ParseWhereExp("AND", field, op, condition);
            return this;
        }

        public ORMHelper WhereOr()
        {
            return this;
        }

        protected void ParseWhereExp(string logic, object field, string op, string condition)
        {
            if (field is string)
            {

            }
            else if (field is Dictionary<string, string>)
            {

            }
        }

        /// <summary>
        /// 创建子查询SQL
        /// </summary>
        /// <returns></returns>
        public string BuildSql()
        {
            return string.Empty;
        }

        /// <summary>
        /// 查找单条记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql指令</param>
        /// <returns></returns>
        public T Find<T>(string sql = "")
        {
            if (string.IsNullOrWhiteSpace(sql)) sql = this.BuildSql();
            DbDataReader reader = DbHelper.GetDataReader(sql, System.Data.CommandType.Text);
            Type type = typeof(T);
            if (type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type.IsEnum)
            {
                if (type.IsEnum)
                {
                    return (T)Enum.ToObject(type, reader.GetValue(0));
                }
                else
                {
                    return (T)Convert.ChangeType(reader.GetValue(0), type);
                }
            }
            else
            {
                T result = Activator.CreateInstance<T>();
                PropertyInfo[] properyies = type.GetProperties();
                string columName;
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
                return result;
            }
        }

        /// <summary>
        /// 查找记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql指令</param>
        /// <returns></returns>
        public List<T> Select<T>(string sql = "")
        {
            if (string.IsNullOrWhiteSpace(sql)) sql = this.BuildSql();
            return Query<T>(sql);
        }

        /// <summary>
        /// 执行查询 返回数据集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">sql指令</param>
        /// <returns></returns>
        public List<T> Query<T>(string sql = "")
        {
            if (string.IsNullOrWhiteSpace(sql)) sql = this.BuildSql();
            DbDataReader reader = DbHelper.GetDataReader(sql, System.Data.CommandType.Text);
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
                        list.Add(result);
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(sql, ex);
            }
        }

        /// <summary>
        /// 执行语句
        /// </summary>
        /// <param name="sql">sql指令</param>
        public int Execute(string sql)
        {
            try
            {
                return DbHelper.ExecNonQuery(sql, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception(sql, ex);
            }
        }

        /// <summary>
        /// 批处理执行SQL语句
        /// </summary>
        /// <param name="sqlList">SQL批处理指令</param>
        /// <returns>bool</returns>
        public bool BatchQuery(List<string> sqlList)
        {
            this.StartTrans();
            int i = 0;
            try
            {
                for (i = 0; i < sqlList.Count; i++)
                {
                    this.Execute(sqlList[i]);
                }
                this.Commit();
            }
            catch
            {
                this.Rollback();
                throw;
            }
            return true;
        }

        /// <summary>
        /// 启动事务
        /// </summary>
        public void StartTrans()
        {
            DbHelper.TransStart();
        }

        /// <summary>
        /// 用于非自动提交状态下面的查询提交
        /// </summary>
        public void Commit()
        {
            DbHelper.TransCommit();
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        public void Rollback()
        {
            DbHelper.TransRollback();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private bool ReaderExists(DbDataReader dr, string columnName)
        {
            dr.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" + columnName + "'";
            return (dr.GetSchemaTable().DefaultView.Count > 0);
        }
    }

    public class Where
    {
        public string filed { get; set; }
        public string logic { get; set; }
        public string condition { get; set; }
    }
}
