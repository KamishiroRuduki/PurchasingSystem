using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankJsonData
{
    public class BankJsonData
    {
        public static  List<string> ReadData()
        {
            //------------------------------從URL抓銀行資料的JSON------------------------
            //string url = "https://raw.githubusercontent.com/wsmwason/taiwan-bank-code/master/data/taiwanBankCodeATM.json";
            //WebClient client = new WebClient();
            //byte[] sourceByte2 = client.DownloadData(url);
            //string jsontext2 = Encoding.UTF8.GetString(sourceByte2);        
            string url = "https://raw.githubusercontent.com/KamishiroRuduki/PurchasingSystem/master/BankJsonData/Data/taiwanBankCodeATM.json";
            WebClient client = new WebClient();
            byte[] sourceByte2 = client.DownloadData(url);
            string jsontext2 = Encoding.UTF8.GetString(sourceByte2);

            //------------------------------從資料夾抓抓銀行資料JSON------------------------
            //StreamReader r = new StreamReader("C:\\Users\\p4786\\C sharp\\PurchasingSystem\\BankJsonData\\Data\\taiwanBankCodeATM.json");
            //string jsonString = r.ReadToEnd();
            //byte[] sourceByte = Encoding.UTF8.GetBytes(jsonString);
            //string jsontext = Encoding.UTF8.GetString(sourceByte);


            var obj = JsonConvert.DeserializeObject<List<Rootobject>>(jsontext2);
            var list = new List<string>();
            for (int i = 0; i < obj.Count; i++)
            {
                string test = obj[i].code + obj[i].name;
                list.Add(test);
            }
            return list;

        }
    }
}

public class Rootobject
{
    public string code { get; set; }
    public string name { get; set; }
}

