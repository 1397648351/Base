/************************************************************************ 
 * 项目名称 ：ORMAttributes
 * 项目描述 ：
 * 文件名称 ：ColumnAttribute.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：MyPC
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 12:36:29
 * 更新时间 ：2018/6/8 12:36:29
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMAttributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class ColumnAttribute : Attribute
    {
        public ColumnAttribute(string columnName)
        {
            this.Value = columnName;
        }

        public string Value { get; protected set; }
    }
}
