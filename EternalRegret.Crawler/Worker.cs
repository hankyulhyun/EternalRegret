using EternalRegret.Cosmos.Context;
using EternalRegret.Cosmos.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EternalRegret.Crawler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var filePath = @"c:\users\hyunh\downloads\data.csv";
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines.Skip(1))
            {
                if (stoppingToken.IsCancellationRequested == true)
                    break;

                var splited = line.Split(',');
                CrawlPricePage(int.Parse(splited[1]));
                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        private string GetPageString(string uri)
        {
            string sRes = string.Empty;

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(uri);
                request.Method = "GET";

                var res = request.GetResponse();
                Stream dataStream = res.GetResponseStream();

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding euckr = System.Text.Encoding.GetEncoding(949);

                StreamReader reader = new StreamReader(dataStream, euckr);
                sRes = reader.ReadToEnd();
                reader.Dispose();
                dataStream.Dispose();
                res.Dispose();

                sRes = Encoding.UTF8.GetString(
                    Encoding.Convert(
                        Encoding.GetEncoding(949),
                        Encoding.UTF8,
                        Encoding.GetEncoding(949).GetBytes(sRes)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Get page string error {e.Message}");
                Thread.Sleep(10000);
                return GetPageString(uri);
            }
            return sRes;
        }

        private void CrawlPricePage(int codeNum)
        {
            var code = codeNum.ToString("D6");

            string uri = $"https://fchart.stock.naver.com/sise.nhn?symbol={code}&timeframe=day&count=5000&requestType=0";
            var pageString = GetPageString(uri);
            var root = XElement.Parse(pageString.Trim());
            if (root.HasElements == false)
                return;

            var chartdata = root.Element("chartdata");
            var name = chartdata.Attribute("name").Value;

            _logger.LogInformation($"Insert: {name} [{code}]");

            var stock = new Stock()
            {
                StockName = name,
                StockCode = code,
            };

            foreach (var dayPrice in root.Element("chartdata").Elements("item"))
            {
                //<item data="20200407|7300|7450|6930|7430|178992" />
                //날짜, 시가, 고가, 저가, 종가, 거래량
                var splitedData = dayPrice.Attribute("data").Value.Split('|');

                var dateInfo = DateTime.ParseExact(splitedData[0], "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
                var price = new Price()
                {
                    PriceDate = dateInfo,
                    StartPrice = Int32.Parse(splitedData[1]),
                    HighPrice = Int32.Parse(splitedData[2]),
                    LowPrice = Int32.Parse(splitedData[3]),
                    EndPrice = Int32.Parse(splitedData[4]),
                    Volumn = Int32.Parse(splitedData[5]),
                };

                if (price.PriceDate < DateTime.ParseExact("20100101", "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture).ToUniversalTime())
                    continue;

                stock.Prices.Add(price);
            }


            using (var context = new StockContext())
            {
                context.Database.EnsureCreated();
                context.Add(stock);
                context.SaveChanges();
            }
        }
    }
}
