/************************************************
 * 项目名称 ：DBUtility   
 * 项目描述 ：
 * 文件名称 ：DbFactory.cs
 * 版 本 号 ：v1.0.0.0  
 * 说    明 ：
 * 作    者 ：MyPC 
 * 创建时间 ：2018/6/9 0:35:28 
 * 更新时间 ：2018/6/9 0:35:28 
*************************************************/

namespace DBUtility
{
    public class DbFactory
    {
        private string _ConnStr;
        public DbFactory(string conStr)
        {
            _ConnStr = conStr;
        }

        public DbHelperBase MysqlHelper
        {
            get
            {
                return new MysqlHelper(_ConnStr);
            }
        }

        public DbHelperBase OracleHelper
        {
            get
            {
                return new OracleHelper(_ConnStr);
            }
        }

        public DbHelperBase SQLHelper
        {
            get
            {
                return new OracleHelper(_ConnStr);
            }
        }
    }
}
