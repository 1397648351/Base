/************************************************************************ 
 * 项目名称 ：Models
 * 项目描述 ：
 * 文件名称 ：User.cs
 * 版 本 号 ：v1.0.0.0
 * 说    明 ：
 * 作    者 ：MyPC
 * IDE 环境 ：Visual Studio 2013
 * 创建时间 ：2018/6/8 12:42:43
 * 更新时间 ：2018/6/8 12:42:43
************************************************************************
 * Copyright @ Njbosa 2018. All rights reserved.
************************************************************************/

using ORMAttributes;

namespace Models
{
    [Table("tab_user")]
    public class User
    {
        [Column("id")]
        public string Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("sex")]
        public int Sex { get; set; }
        [Column("phone")]
        public string Phone { get; set; }
        [Column("mail")]
        public string Mail { get; set; }
        [Column("role")]
        public int Role { get; set; }
    }
}
