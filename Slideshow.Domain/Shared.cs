using System.Configuration;

namespace Slideshow.Domain
{
    /// <summary>
    /// Shared
    /// </summary>
    public static class Shared
    {
        /// <summary>
        /// Fakeの時Trure（1:Fake）
        /// </summary>
        public static bool IsFake { get; } = (ConfigurationManager.AppSettings["IsFake"] == "1");

        /// <summary>
        /// SQLite 接続子
        /// </summary>
        public static string? SQLiteConnectionString { get; } = ConfigurationManager.AppSettings["SQLiteConnectionString"];

        /// <summary>
        /// ダイアログを開く際の初期ディレクトリ
        /// </summary>
        public static string? DialogInitialDirectory { get; } = ConfigurationManager.AppSettings["DialogInitialDirectory"];

        /// <summary>
        /// 画像ファイルの拡張子
        /// </summary>
        public static string? ImageExtension { get; } = ConfigurationManager.AppSettings["ImageExtension"];
    }
}