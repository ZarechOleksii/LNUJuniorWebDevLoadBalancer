using LoadBalancer.Hubs;
using LoadBalancer.Models;
using LoadBalancer.Models.Entities;
using LoadBalancer.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHubContext<StatusHub> _hub;

        public DataController(UserManager<User> userManager, ApplicationContext applicationContext, IHubContext<StatusHub> hubContext)
        {
            _context = applicationContext;
            _userManager = userManager;
            _hub = hubContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> StartJobAsync([FromBody] DataTransformViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var result = await _context.DataResults.FirstOrDefaultAsync(v => v.InProgress && v.UserId == user.Id);
                if (result is null)
                {
                    await _hub.Clients.Client(model.SignalRConnectionId).SendAsync("ReceiveMessage", $"Working machine: { Environment.MachineName }");
                    await _hub.Clients.Client(model.SignalRConnectionId).SendAsync("ReceiveMessage", "Received Data on Server");

                    var DataResult = new DataResult(user.Id);

                    DataResult.SourceName = model.FileName;
                    DataResult.Actions = model.ToDo;
                    DataResult.Before = model.Data;
                    await _context.SaveChangesAsync();

                    await _hub.Clients.Client(model.SignalRConnectionId).SendAsync("ReceiveMessage", "Started transforming");

                    string content = DataResult.Before;

                    try
                    {
                        foreach (var x in DataResult.Actions.OrderBy(v => v.Order))
                        {
                            await _hub.Clients.Client(model.SignalRConnectionId).SendAsync("ReceiveMessage", $"Started action {x.Action}, number {x.Order}");
                            content = x.DoAction(content);
                            await _hub.Clients.Client(model.SignalRConnectionId).SendAsync("ReceiveMessage", $"Finished action {x.Action}, number {x.Order}");
                        }

                        DataResult.After = content;
                        DataResult.TimeSpan = DateTime.Now - DataResult.DateTime;
                        DataResult.InProgress = false;
                    }
                    catch (OutOfMemoryException)
                    {
                        DataResult.After = "";
                        DataResult.TimeSpan = DateTime.Now - DataResult.DateTime;
                        DataResult.InProgress = false;
                        DataResult.Errored = true;
                    }

                    await _context.DataResults.AddAsync(DataResult);

                    await _context.SaveChangesAsync();

                    await _hub.Clients.Client(model.SignalRConnectionId).SendAsync("ReceiveMessage", $"Finished transforming!");

                    return Ok(DataResult);
                }
                else
                {
                    return StatusCode(503);
                }
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> GetJobsAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var jobs = await _context.DataResults
                    .Where(v => v.UserId == user.Id)
                    .OrderByDescending(v => v.DateTime)
                    .Select(v => new
                    {
                        v.SourceName,
                        v.DateTime,
                        v.TimeSpan,
                        v.InProgress,
                        v.Errored
                    })
                    .Select(v => new DataResultsViewModel()
                    {
                        SourceName = v.SourceName,
                        DateTime = v.DateTime,
                        TimeSpan = v.TimeSpan,
                        InProgress = v.InProgress,
                        Errored = v.Errored
                    })
                    .ToListAsync();

                return View(jobs);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
