using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Actives act = new Actives();
            act.Wr("cao");
            //act.sqlTest("Data Source=121.43.104.82;port=3306;Initial Catalog=base;user id=root;password=root;Charset=utf8");
            act.sqlTest("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=121.43.104.82)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=orcl)));User Id=njbosa;Password=njbosa");
            Console.ReadLine();
        }
    }
}