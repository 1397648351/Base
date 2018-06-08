/************************************************
 * 项目名称 ：DBUtility   
 * 项目描述 ：
 * 文件名称 ：OracleHelper.cs
 * 版 本 号 ：v1.0.0.0  
 * 说    明 ：
 * 作    者 ：WUZE 
 * 创建时间 ：2018/6/9 0:31:17 
 * 更新时间 ：2018/6/9 0:31:17 
*************************************************/

using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

namespace DBUtility
{
    public class OracleHelper : DbHelperBase
    {
        public OracleHelper(string connStr)
            : base(connStr)
        { }
        private OracleConnection _DBConnectionObj;
        private OracleCommand _DbCommandObj;
        private OracleDataAdapter _DbDataAdapterObj;

        protected override DbConnection DBConnectionObj
        {
            get
            {
                if (_DBConnectionObj == null)
                {
                    _DBConnectionObj = new OracleConnection(_ConnStr);
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
                    _DbCommandObj = new OracleCommand();
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
                    _DbDataAdapterObj = new OracleDataAdapter();
                }
                return _DbDataAdapterObj;
            }
        }
    }
}
