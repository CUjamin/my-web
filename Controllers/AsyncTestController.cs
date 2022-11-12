using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace my_web.Controllers
{
    [Route("api/async")]
    public class AsyncTestController : Controller
    {
        private readonly ILogger<AsyncTestController> _logger;

        private readonly HttpClient httpClient = new HttpClient();

        public AsyncTestController(ILogger<AsyncTestController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet("test")]
        public async Task<string> AddTodoListAsync()
        {
            var stopwatch = Stopwatch.StartNew();
            var response = await DownLoadWebsitesAsync();
            // var response = await DownLoadWebsites();
            response += $"Elapse time : {stopwatch.Elapsed}{Environment.NewLine}";
            return response;
        }

        private static readonly IEnumerable<string> WEB_SITES = new string[]{
            "https://www.zhihu.com/signin?next=%2F",
            "https://www.baidu.com/",
            "https://www.bilibili.com/",
            "https://www.zhihu.com/signin?next=%2F",
            "https://www.baidu.com/",
            "https://www.bilibili.com/",
            "https://www.zhihu.com/signin?next=%2F",
            "https://www.baidu.com/",
            "https://www.bilibili.com/",
            "https://www.zhihu.com/signin?next=%2F",
            "https://www.baidu.com/",
            "https://www.bilibili.com/",
        };

        private async Task<string> DownLoadWebsitesAsync()
        {
            List<Task<string>> taskList = new List<Task<string>>();

            foreach (var site in WEB_SITES)
            {
                taskList.Add(DownLoadWebsiteAsync(site));
            }
            var results = await Task.WhenAll(taskList);
            var response = "";
            foreach (var result in results)
            {
                response += result;
            }
            return response;
        }
        private async Task<string> DownLoadWebsites()
        {
            var response = "";
            foreach (var site in WEB_SITES)
            {
                response += await Task.Run(() => DownLoadWebsite(site));
            }
            return response;
        }

        public string DownLoadWebsite(string url)
        {
            var response = httpClient.GetAsync(url).GetAwaiter().GetResult();
            var responsePayloadBytes = response.Content.ReadAsByteArrayAsync().GetAwaiter().GetResult();
            return $"Finish download data form {url}. Total bytes returned {responsePayloadBytes.Length}. {Environment.NewLine}";
        }
        public async Task<string> DownLoadWebsiteAsync(string url)
        {
            var response = await httpClient.GetAsync(url);
            var responsePayloadBytes = await response.Content.ReadAsByteArrayAsync();
            return $"Finish download data form {url}. Total bytes returned {responsePayloadBytes.Length}. {Environment.NewLine}";
        }
    }
}