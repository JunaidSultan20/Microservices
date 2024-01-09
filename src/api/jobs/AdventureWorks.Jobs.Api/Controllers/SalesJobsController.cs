namespace AdventureWorks.Jobs.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SalesJobsController : ControllerBase
{
    [HttpGet]
    [Route("RecurringJob")]
    public string RecurringJobs()
    {
        //Recurring Jobs
        //Recurring jobs fire many times on the specified CRON schedule.
        RecurringJob.AddOrUpdate(() => Console.WriteLine("Welcome user in Recurring Job Demo!"), Cron.Minutely);

        return "Welcome user in Recurring Job Demo!";
    }
}