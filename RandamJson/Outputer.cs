using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RandamJson
{
    public class Outputer
    {
        /// <summary>
        /// JTokenで組み立てられたデータをJSONファイルを出力します。
        /// </summary>
        /// <param name="obj">出力対象となるデータ。</param>
        /// <param name="filePath">出力先のファイルパス。</param>
        /// <returns>非同期の書き込み操作を表すタスク。</returns>
        public async Task OutputFile(JToken obj, string filePath, Formatting formatting = Formatting.Indented)
        {
            await using (var stream = File.CreateText(filePath))
            {
                var writer = new JsonTextWriterForProgress(stream) { Formatting = formatting };

                writer.WroteEvent += (sender, e) => WroteDataEvent?.Invoke(null, e);

                await obj.WriteToAsync(writer);
            }
        }
        /// <summary>
        /// Createrで生成されたデータが書き込まれたときに発生します。
        /// </summary>
        public event EventHandler WroteDataEvent;

        /// <summary>
        /// 進行報告のためにオブジェクト出力ごとのイベント機能を追加したJsonTextWriterです。
        /// Createrクラスと連動する用途のみを考えているため、RandamDataCreaterクラスで利用しない機能については、適切に動作しません。
        /// </summary>
        class JsonTextWriterForProgress : JsonTextWriter
        {
            /// <summary>
            /// JsonTextWriterForProgressクラスの新しいインスタンスを初期化します。
            /// /// </summary>
            /// <param name="textWriter">The System.IO.TextWriter to write to.</param>
            public JsonTextWriterForProgress(TextWriter textWriter) : base(textWriter)
            {
            }

            /// <summary>
            /// Asynchronously writes a System.Boolean value.
            /// </summary>
            /// <param name="value">The System.Boolean value to write.</param>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
            /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
            /// <remarks>Derived classes must override this method to get asynchronous behaviour. Otherwise it will execute synchronously, returning an already-completed task.</remarks>
            public override async Task WriteValueAsync(bool value, CancellationToken cancellationToken = default)
            {
                await base.WriteValueAsync(value, cancellationToken);
                WroteEvent?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Asynchronously writes a System.Double value.
            /// </summary>
            /// <param name="value">The System.Double value to write.</param>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
            /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
            /// <remarks>Derived classes must override this method to get asynchronous behaviour. Otherwise it will execute synchronously, returning an already-completed task.</remarks>
            public override async Task WriteValueAsync(double value, CancellationToken cancellationToken = default)
            {
                await base.WriteValueAsync(value, cancellationToken);
                WroteEvent?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Asynchronously writes a System.String value.
            /// </summary>
            /// <param name="value">The System.String value to write.</param>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
            /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
            /// <remarks>Derived classes must override this method to get asynchronous behaviour. Otherwise it will execute synchronously, returning an already-completed task.</remarks>
            public override async Task WriteValueAsync(string value, CancellationToken cancellationToken = default)
            {
                await base.WriteValueAsync(value, cancellationToken);
                WroteEvent?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Asynchronously writes a null value.
            /// </summary>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
            /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
            /// <remarks>Derived classes must override this method to get asynchronous behaviour. Otherwise it will execute synchronously, returning an already-completed task.</remarks>
            public override async Task WriteNullAsync(CancellationToken cancellationToken = default)
            {
                await base.WriteNullAsync(cancellationToken);
                WroteEvent?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Asynchronously writes the end of an array.
            /// </summary>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
            /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
            /// <remarks>Derived classes must override this method to get asynchronous behaviour. Otherwise it will execute synchronously, returning an already-completed task.</remarks>
            public override async Task WriteEndArrayAsync(CancellationToken cancellationToken = default)
            {
                await base.WriteEndArrayAsync(cancellationToken);
                WroteEvent?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// Asynchronously writes the end of a JSON object.
            /// </summary>
            /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is System.Threading.CancellationToken.None.</param>
            /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
            /// <remarks>Derived classes must override this method to get asynchronous behaviour. Otherwise it will execute synchronously, returning an already-completed task.</remarks>
            public override async Task WriteEndObjectAsync(CancellationToken cancellationToken = default)
            {
                await base.WriteEndObjectAsync(cancellationToken);
                WroteEvent?.Invoke(this, new EventArgs());
            }

            /// <summary>
            /// RandamDataCreaterで生成されたデータが書き込まれたときに発生します。
            /// </summary>
            public event EventHandler WroteEvent;
        }
    }
}
