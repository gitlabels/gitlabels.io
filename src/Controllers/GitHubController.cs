using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebHooks;
using Newtonsoft.Json.Linq;

namespace gitlabels.Controllers
{
    // Route: /api/webhooks/incoming/github/{id}
    [Route("api/github")]
    public class GitHubController : ControllerBase
    {
        [HttpGet]
        [Route("setup")]
        public IActionResult Setup(int installation_id, string setup_action)
        {
            return Ok($"installation_id={installation_id}&setup_action={setup_action}");
        }

        [GitHubWebHook(EventName = "ping")]
        public IActionResult HandlePing(string id, string @event, JObject data)
        {
            return Ok(new
            {
                app = "gitlabels",
                version = "0.1.0",
                handler = "ping",
                debug = new
                {
                    id = id,
                    @event = @event,
                    data = data
                }
            });
        }

        [GitHubWebHook]
        public IActionResult GitHubHandler(string id, string @event, JObject data)
        {
            return Ok(new {
                app = "gitlabels",
                version = "0.1.0",
                handler = "default",
                debug = new {
                    id = id,
                    @event = @event,
                    data = data
                }
            });
        }
        [GeneralWebHook]
        public IActionResult FallbackHandler(string receiverName, string id, string eventName)
        {
            return Ok(new
            {
                app = "gitlabels",
                version = "0.1.0",
                handler = "fallback",
                debug = new
                {
                    id,
                    receiverName,
                    eventName
                }
            });
        }
    }
}
