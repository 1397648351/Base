/************************************************************************ 
 * 项目名称 ：Test
 * 项目描述 ：
 * 文件名称 ：Actives.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：MyPC
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 12:49:33
 * 更新时间 ：2018/6/8 12:49:33
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORMAttributes;
using System.Reflection;
using Models;
using DBUtility;
using System.Data;

namespace Test
{
    public class Actives
    {
        public void Wr(string str)
        {
            PropertyInfo[] properties = typeof(User).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                string name = ORMAttributes.AttributeProcess.GetColumnName(property);
                Console.WriteLine(property.Name + ": " + name);
            }
        }

        public void sqlTest(string conStr)
        {
            DbFactory Df = new DbFactory(conStr);
            DbHelperBase db = Df.OracleHelper;
            try
            {
                DataSet ds = db.GetDataSet("select * from bimcc_lu");
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
