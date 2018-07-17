/************************************************************************ 
 * 项目名称 ：Common
 * 项目描述 ：
 * 文件名称 ：LogHelper.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：MyPC
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/7/9 16:01:03
 * 更新时间 ：2018/7/9 16:01:03
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;

namespace Common
{
    public static class LogHelper
    {
        private static readonly log4net.ILog logInfo = log4net.LogManager.GetLogger("loginfo");
        private static readonly log4net.ILog logError = log4net.LogManager.GetLogger("logerror");

        static LogHelper()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\log4net.config";
            log4net.Config.XmlConfigurator.Configure(new FileInfo(path));
        }

        /// <summary>
        /// 导出基本信息
        /// </summary>
        /// <param name="message">消息文本</param>
        public static void Info(string message)
        {
            logInfo.Info(message);
        }

        /// <summary>
        /// 导出异常信息
        /// </summary>
        /// <param name="message">消息文本</param>
        public static void Error(string message)
        {
            logError.Error(message);
        }
    }
}
