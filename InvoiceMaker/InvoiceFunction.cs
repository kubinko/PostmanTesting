using Kros.KORM;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;

namespace InvoiceMaker
{
    public static class InvoiceFunction
    {
        [FunctionName(nameof(InvoiceFunction))]
        public static void Run([TimerTrigger("*/15 * * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"Invoice check started at: {DateTime.Now}");

            var db = Database.Builder.UseConnection(Environment.GetEnvironmentVariable("ConnectionString")).Build();
            int invoicesToGenerate = int.Parse(db.ExecuteScalar("SELECT COUNT(*) FROM Attendance WHERE InvoiceGenerated = 0"));

            if (invoicesToGenerate > 0)
            {
                db.ExecuteNonQuery("UPDATE Attendance SET InvoiceGenerated = -1 WHERE InvoiceGenerated = 0");
                log.LogInformation($"{invoicesToGenerate} invoices generated.");
            }
            else
            {
                log.LogInformation("No invoices waiting for generating.");
            }

            log.LogInformation($"Invoice check finished at: {DateTime.Now}");
        }
    }
}
