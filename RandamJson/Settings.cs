using CommandLine;
using Newtonsoft.Json;

namespace RandamJson
{
    /// <summary>
    /// 出力するJSONファイルを生成する条件についての設定です。
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// ランダムに作成したjsonファイルを出力するパスを取得、設定します。
        /// </summary>
        [Value(0, Required = true, HelpText ="ランダムに作成したJSONファイルを出力するパス。")]
        public string FilePath { get; set; }

        /// <summary>
        /// 出力するJSONファイルに含まれるデータの数を取得、設定します。
        /// </summary>
        [Option('c', "count", Default = 100, HelpText = "出力するJSONファイルに含まれるデータの数。")]
        public int DataCount { get; set; } = 100;

        /// <summary>
        /// ランダムに生成されるオブジェクトのキーの最大長を取得、設定します。
        /// </summary>
        [Option('k', "maxkeylength", Default = 20, HelpText = "ランダムに生成されるオブジェクトのキーの最大長。")]
        public int MaxKeyLength { get; set; } = 20;

        /// <summary>
        /// ランダムに生成される文字列データの最大長を取得、設定します。
        /// </summary>
        [Option('s', "maxstringlength", Default = 20, HelpText = "ランダムに生成される文字列データの最大長。")]
        public int MaxStringLength { get; set; } = 20;

        /// <summary>
        /// ランダムに生成される文字列データに含まれる文字を取得、設定します。
        /// </summary>
        [Option("chars", Default = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ", HelpText = "ランダムに生成される文字列データに含まれる文字。")]
        public string StringCharactorKinds { get; set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_ ";

        /// <summary>
        /// ランダムに生成される数値データの最大値を取得、設定します。
        /// </summary>
        [Option('n', "maxnumber", Default = 99999, HelpText = "ランダムに生成される数値データの最大値。")]
        public int MaxNumber { get; set; } = 99999;

        /// <summary>
        /// JSON出力をどのようにフォーマットするかを取得、設定します。
        /// </summary>
        [Option('f', "Fromatting", Default = Formatting.Indented, HelpText = "JSON出力をどのようにフォーマットするか。")]
        public Formatting Formatting { get; set; } = Formatting.Indented;
    }
}
