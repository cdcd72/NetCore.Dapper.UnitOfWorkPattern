namespace Web.Core.Enum
{
    public enum DBProvider 
    {
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,

        /// <summary>
        /// MsSqlServer
        /// </summary>
        MsSqlServer
    }

    /// <summary>
    /// 枚舉擴充方法
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// 取得枚舉
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strValue"></param>
        /// <returns></returns>
        public static T ConvertFromString<T>(this string strValue) where T : struct
        {
            T t = default;

            if (typeof(System.Enum) != typeof(T).BaseType)
                return t;

            return (T)System.Enum.Parse(typeof(T), strValue);
        }
    }
}
