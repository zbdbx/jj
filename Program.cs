using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace jj
{
    class Program
    {
        static string FmtUrl = "https://fundmobapi.eastmoney.com/FundMNewApi/FundMNRankNewList?fundtype=0&SortColumn=RZDF&Sort=desc&pageIndex={0}&pagesize=30&companyid=&deviceid=Wap&plat=Wap&product=EFund&version=2.0.0&Uid=&_=1622897970081";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            GetData();
        }


        static void GetData()
        {
            var list = new ArrayList();
            for (var i = 1;/*i<=10*/ ; i++)
            {
                var wc = new WebClient();
                var jsonStr = wc.DownloadString(string.Format(FmtUrl, i));
                var jobj = JObject.Parse(jsonStr);
                Console.WriteLine(i + "-" + jobj["Datas"].Count());
                if (jobj["Datas"].Count() > 0)
                {
                    var dataList = jobj["Datas"].ToList();
                    list.AddRange(dataList);
                }
                else{
                    break;
                }
            }

            Console.WriteLine(list.Count);
            var json = JsonConvert.SerializeObject(list);
            Console.WriteLine("准备写入文件");
            File.WriteAllText("datas.json",json);
            Console.WriteLine("写入文件完成");

        }
    }
}
