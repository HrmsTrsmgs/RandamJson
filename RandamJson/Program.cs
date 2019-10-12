using CommandLine;
using Newtonsoft.Json.Linq;
using ShellProgressBar;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RandamJson
{
    /// <summary>
    /// エントリーポイントです。
    /// </summary>
    class Program
    {
        /// <summary>
        /// プログラムのエントリーポイントとなります。
        /// </summary>
        /// <param name="args">コマンドライン引数。</param>
        /// <returns>プログラムの戻り値</returns>
        static async Task<int> Main(string[] args)
        {
            try
            {   
                var settings =
                    Parser.Default.ParseArguments<Settings>(args) switch
                    {
                        Parsed<Settings> success => success.Value,
                        _ => throw new ArgumentException("コマンドライン引数が適切ではありません")
                    };
                var magnification = (int)Math.Ceiling(Math.Log(settings.DataCount)* 0.5 + settings.DataCount * 0.0000007); //適当
                var outputTickCount = settings.DataCount / magnification + 1;
                using (var pbar = new ProgressBar(settings.DataCount + outputTickCount, "", new ProgressBarOptions { ProgressCharacter = '-' }))
                {
                    var creater = new RandamDataCreater(settings);
                    var outputer = new Outputer();

                    creater.DataCreatedEvent += (sender, e) => pbar.Tick();
                    int i = 0;
                    outputer.WroteDataEvent += (sender, e) => { if (i++ % magnification == 0) pbar.Tick(); };
                    await outputer.OutputFile(await creater.CreateAsync(), settings.FilePath, settings.Formatting);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return 0;
        }
    }
}
