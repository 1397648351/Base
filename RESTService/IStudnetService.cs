using Model;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace RESTService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IStudnetService”。
    [ServiceContract]
    public interface IStudnetService
    {

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetStudentById/Id={Id}"
            )]
        Student GetStudentById(string Id);

        [OperationContract]
        [WebInvoke(
             Method = "GET",
             RequestFormat = WebMessageFormat.Json,
             ResponseFormat = WebMessageFormat.Json,
             UriTemplate = "GetStudentList"
             )]
        IList<Student> GetStudentList();

        [OperationContract]
        [WebInvoke(
            Method = "GET",
            RequestFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetStudent"
            )]
        IList<Student> GetStudent();
    }
}
