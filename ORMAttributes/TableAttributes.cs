/************************************************************************ 
 * 项目名称 ：ORMAttributes
 * 项目描述 ：
 * 文件名称 ：TableAttributes.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：MyPC
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 12:35:29
 * 更新时间 ：2018/6/8 12:35:29
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using System;

namespace ORMAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string tableName)
        {
            this.Value = tableName;
        }

        public string Value { get; protected set; }
    }
}
