using APIV2.Mark.Database;
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIV2.Mark.Entities.SeedData
{
    public class UtilitiyContextSeed
    {
        public static async Task SeedAsync(UtilitiyContext context, ILoggerFactory loggerFactory)
        {
			try
			{
				if (!context.ChartOfAccount.Any())
				{
					var accountsData = File.ReadAllText("../APIV2.Mark.Entities/SeedData/ChartOfAccount.json");
					var accounts = JsonSerializer.Deserialize<List<ChartOfAccount>>(accountsData);
					foreach (var item in accounts)
					{
						context.ChartOfAccount.Add(item);
                        await context.SaveChangesAsync();
                    } 
                }
			}
			catch (Exception ex)
			{
                var logger = loggerFactory.CreateLogger<UtilitiyContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
