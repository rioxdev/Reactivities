using Application.Activities;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : BaseController
    {


        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetAll() => Ok(await Mediator.Send(new List.Query()));


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id) => HandleResult(await Mediator.Send(new Details.Query() { Id = id }));

        [HttpPost]
        public async Task<ActionResult> Create(Activity activity) => Ok(await Mediator.Send(new Create.Command() { Activity = activity }));


        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, Activity activity)
        {
            activity.Id = id;
            return Ok(await Mediator.Send(new Update.Command() { Activity = activity }));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id) => Ok(await Mediator.Send(new Delete.Command() { Id = id }));


    }
}
