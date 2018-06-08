/************************************************************************ 
 * 项目名称 ：DBUtility
 * 项目描述 ：
 * 文件名称 ：MysqlHelper.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：WUZE
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 17:29:12
 * 更新时间 ：2018/6/8 17:29:12
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DBUtility
{
    public class MysqlHelper : DbHelperBase
    {
        public MysqlHelper(string connStr)
            : base(connStr)
        { }
        private MySqlConnection _DBConnectionObj;
        private MySqlCommand _DbCommandObj;
        private MySqlDataAdapter _DbDataAdapterObj;

        protected override DbConnection DBConnectionObj
        {
            get
            {
                if (_DBConnectionObj == null)
                {
                    _DBConnectionObj = new MySqlConnection(_ConnStr);
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
                    _DbCommandObj = new MySqlCommand();
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
                    _DbDataAdapterObj = new MySqlDataAdapter();
                }
                return _DbDataAdapterObj;
            }
        }
    }
}
