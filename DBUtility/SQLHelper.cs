/************************************************************************ 
 * 项目名称 ：DBUtility
 * 项目描述 ：
 * 文件名称 ：SQLHelper.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：MyPC
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 11:08:28
 * 更新时间 ：2018/6/8 11:08:28
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using System.Data.Common;
using System.Data.SqlClient;

namespace DBUtility
{
    public class SQLHelper : DbHelperBase
    {
        public SQLHelper(string connStr)
            : base(connStr)
        { }
        private SqlConnection _DBConnectionObj;
        private SqlCommand _DbCommandObj;
        private SqlDataAdapter _DbDataAdapterObj;

        protected override DbConnection DBConnectionObj
        {
            get
            {
                if (_DBConnectionObj == null)
                {
                    _DBConnectionObj = new SqlConnection(_ConnStr);
                }
                return _DBConnectionObj;
            }
        }

        protected override DbCommand DbCommandObj
        {
            get
            {
                if (_DbCommandObj == null)
                {
                    _DbCommandObj = new SqlCommand();
                }
                return _DbCommandObj;
            }
        }

        protected override DbDataAdapter DbDataAdapterObj
        {
            get
            {
                if (_DbDataAdapterObj == null)
                {
                    _DbDataAdapterObj = new SqlDataAdapter();
                }
                return _DbDataAdapterObj;
            }
        }
    }
}
