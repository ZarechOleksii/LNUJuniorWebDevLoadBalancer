using LoadBalancer.Models;
using LoadBalancer.Models.Entities;
using LoadBalancer.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoadBalancer.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<User> _userManager;

        public DataController(UserManager<User> userManager, ApplicationContext applicationContext)
        {
            _context = applicationContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        public async Task<IActionResult> StartJobAsync([FromBody] DataTransformViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);

                var DataResult = new DataResult(user.Id);

                DataResult.Actions = model.ToDo;
                DataResult.Before = model.Data;

                string content = DataResult.Before;

                foreach (var x in DataResult.Actions.OrderBy(v => v.Order))
                {
                    content = x.DoAction(content);
                }

                DataResult.After = content;
                DataResult.TimeSpan = DateTime.Now - DataResult.DateTime;
                DataResult.InProgress = false;

                await _context.DataResults.AddAsync(DataResult);

                await _context.SaveChangesAsync();

                return Ok(DataResult);
            }
            else
            {
                return BadRequest();
            }
        }

/*        [HttpPost]
        public async Task<IActionResult> DoActionAsync(DataTransformViewModel model)
        {
            if (ModelState.IsValid)
            {
                var action = model.ToDo;
                action.DataResult.Actions.Add(action);
                await _context.ReplaceActions.AddAsync(action);
                _context.DataResults.Update(action.DataResult);
                await _context.SaveChangesAsync();
                var result = action.DoAction(model.Data);
                _context.ReplaceActions.Update(action);
                await _context.SaveChangesAsync();
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<IActionResult> DataResultDoneAsync(JobDoneViewModel model)
        {
            if (ModelState.IsValid)
            {
                model.DataResult.After = model.Data;
                model.DataResult.TimeSpan = DateTime.Now - model.DataResult.DateTime;
                model.DataResult.InProgress = false;
                _context.DataResults.Update(model.DataResult);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }*/
    }
}
