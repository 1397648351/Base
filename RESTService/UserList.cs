using Model;
using System.Collections.Generic;

namespace RESTService
{
    public class UserList
    {
        #region 饿汉式单例，实例化UserList类
        private static readonly UserList _Instance = new UserList();
        private UserList() { }
        public static UserList Instance
        {
            get { return _Instance; }
        }
        #endregion

        public IList<Student> Users
        {
            get { return _Users; }
        }

        private IList<Student> _Users = new List<Student>{
            new Student {Id = 1, Name = "张三" },
            new Student {Id = 2, Name = "李四" },
            new Student {Id = 3, Name = "王五" }
        };
    }
}