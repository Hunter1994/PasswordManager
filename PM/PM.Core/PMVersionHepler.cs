using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Reflection.Extensions;

namespace PM.Core
{
    /// <summary>
    /// 应用版本中心
    /// </summary>
    public class PMVersionHepler
    {
        /// <summary>
        /// 获取应用程序当前版本
        /// 显示在网页中
        /// </summary>
        public const string Version = "1.0.0.0";

        /// <summary>
        /// 获取应用程序最后一次发布的时间
        /// 它显示在网页中
        /// </summary>
        public static DateTime ReleaseDate
        {
            get { return new FileInfo(typeof(PMVersionHepler).GetAssembly().Location).LastWriteTime; }
        }

    }
}
