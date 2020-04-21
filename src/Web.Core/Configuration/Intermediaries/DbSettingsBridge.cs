using Microsoft.Extensions.Options;
using System;
using Web.Core.Enum;

namespace Web.Core.Configuration
{
    /// <summary>
    /// 橋接 DbSettings
    /// 描述：在已透過 DI 將組態 DbSettings 區段綁定至 DbSettings 物件情況下，對其物件進行其它操作(ex. 驗證、解密...)
    /// </summary>
    public class DbSettingsBridge : IDbSettingsResolved
    {
        private readonly IOptions<DbSettings> _dbSettings;

        public DbSettingsBridge(IOptionsSnapshot<DbSettings> dbSettings, ISettingsValidator validator) 
        {
            _dbSettings = dbSettings ?? throw new ArgumentNullException(nameof(dbSettings));

            // 若無驗證器則拋出例外
            if (validator == null) throw new ArgumentNullException(nameof(validator));

            if (!validator.TryValidate(dbSettings.Value, out var validationException))
                throw validationException;
        }

        public DBProvider ConnectionType => _dbSettings.Value.ConnectionType.ConvertFromString<DBProvider>();

        public string ConnectionString => _dbSettings.Value.ConnectionStrings[_dbSettings.Value.ConnectionType];
    }
}
