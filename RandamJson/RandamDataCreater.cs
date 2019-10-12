using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RandamJson
{
    /// <summary>
    /// JSONに対応するデータをランダムに生成します。
    /// </summary>
    public class RandamDataCreater
    {
        /// <summary>
        /// オブジェクトプロパティの最初の文字として使える文字の種類。
        /// </summary>
        const string CharKindTheFirst = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_";

        /// <summary>
        /// オブジェクトプロパティの最初以外の文字として使える文字の種類。
        /// </summary>
        const string CharKindNotTheFirst = "0123456789" + CharKindTheFirst;

        /// <summary>
        /// 乱数を発生させます。
        /// </summary>
        Random random { get; }

        /// <summary>
        /// 指定された設定です。
        /// </summary>
        Settings Settings { get; }

        /// <summary>
        /// RandamJsonCreaterの新しいインスタンスを
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="seed"></param>
        public RandamDataCreater(Settings settings = null, int seed = 0)
        {
            Settings = settings;
            random = new Random(seed);
        }

        /// <summary>
        /// JSONに対応するデータをランダムに生成します。
        /// </summary>
        /// <returns>生成されたデータ。</returns>
        public async Task<JObject> CreateAsync()
        {
            await Task.CompletedTask;
            var returnValue = new JObject();
            var createdContaner = new List<JToken> { returnValue };

            foreach (var i in Enumerable.Range(0, Settings.DataCount))
            {
                var value = RandamValue(RandomType());
                var container = createdContaner[random.Next(createdContaner.Count)];

                switch (value)
                {
                    case JArray array:
                    case JObject obj:
                        createdContaner.Add(value);
                        break;
                }
                switch (container)
                {
                    case JArray array:
                        array.Add(value);
                        break;
                    case JObject obj:
                        var key = RandomIdentifier();
                        while (obj.ContainsKey(key))
                        {
                            key = RandomIdentifier();
                        }
                        obj.Add(key, value);
                        break;
                }
                DataCreatedEvent?.Invoke(this, new EventArgs());
            }
            return returnValue;
        }

        /// <summary>
        /// 種類を指定し、ランダムな値を生成します。
        /// </summary>
        /// <param name="type">生成する値の種類。</param>
        /// <returns>生成された値。</returns>
        JToken RandamValue(JTokenType type)
        {
            return type switch
            {
                JTokenType.String => new JValue(new string(Enumerable.Range(0, random.Next(Settings.MaxStringLength + 1)).Select(x => RandomChar(Settings.StringCharactorKinds)).ToArray())),
                JTokenType.Float => new JValue(random.NextDouble() * Settings.MaxNumber),
                JTokenType.Boolean => new JValue(random.Next(2) == 0),
                JTokenType.Null => JValue.CreateNull(),
                JTokenType.Array => new JArray(),
                _ => new JObject()
            };
        }

        /// <summary>
        /// オブジェクトプロパティのキーとなる識別子をランダムに生成します。
        /// </summary>
        /// <returns>生成されたキー。</returns>
        string RandomIdentifier()
            => $"{RandomChar(CharKindTheFirst)}{RandomString(CharKindNotTheFirst, Settings.MaxKeyLength - 1)}";

        /// <summary>
        /// 文字列データをランダムに生成します。
        /// </summary>
        /// <param name="kinds">文字列に許される文字の種類。</param>
        /// <param name="maxLength">文字列に許される最大長。</param>
        /// <returns>生成された文字列データ。</returns>
        string RandomString(string kinds, int maxLength) =>
            new string(
                (from i in Enumerable.Range(0, Math.Max(0, random.Next(1, maxLength + 1)))
                 select RandomChar(kinds)
                ).ToArray());

        /// <summary>
        /// 文字をランダムに生成します。
        /// </summary>
        /// <param name="kinds">許される文字の種類。</param>
        /// <returns>生成された文字。</returns>
        char RandomChar(string kinds) => kinds[random.Next(kinds.Length)];

        /// <summary>
        /// 生成するデータの種類をランダムに決めて取得します。
        /// </summary>
        /// <returns>ランダムに決めたデータの種類。</returns>
        JTokenType RandomType()
        {
            return random.Next(5 + 1) switch
            {
                0 => JTokenType.String,
                1 => JTokenType.Float,
                2 => JTokenType.Boolean,
                3 => JTokenType.Null,
                4 => JTokenType.Array,
                _ => JTokenType.Object,
            };
        }

        /// <summary>
        /// データが生成されたときに発生します。
        /// </summary>
        public event EventHandler DataCreatedEvent;
    }
}
