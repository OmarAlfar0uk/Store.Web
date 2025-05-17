using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositorice
{
    public class CashingRepository(IConnectionMultiplexer connection) : ICashingRepository
    {
        private readonly IDatabase _database  = connection.GetDatabase(); 
        public async Task<string?> GetAsync(string CasheKey)
        {
            var CashValue =await _database.StringGetAsync(CasheKey);
            return CashValue.IsNullOrEmpty ? null : CashValue.ToString();
        }

        public async Task SetAsync(string CasheKey, string CacheValue, TimeSpan TimeToLive)
        {
           await _database.StringSetAsync(CasheKey, CacheValue, TimeToLive);
        }
    }
}
