using AstroBackEnd.Models;
using AstroBackEnd.RequestModels;
using AstroBackEnd.RequestModels.TopicRequest;
using AstroBackEnd.Services.Core;
using AstroBackEnd.ViewsModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/topics")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private ITopicService topicService;
        public TopicController(ITopicService topicService)
        {
            this.topicService = topicService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult CreateTopic([FromBody] CreateTopicRequest request)
        {
            try
            {
                return Ok(topicService.CreateTopic(request));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetTopic(int id)
        {
            try
            {
                Topic topic = topicService.GetTopic(id);
                return Ok(topic);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult FindTopic(int id, string name, int houseId, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                int total = 0;
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindTopicRequest request = new FindTopicRequest()
                {
                    Id = id,
                    Name = name,
                    HouseId = houseId,
                    PagingRequest = pagingRequest
                };
                var findResult = topicService.FindTopic(request, out total);
                PagingView pagingView = new PagingView()
                {
                    Payload = findResult,
                    Total = total
                };
                return Ok(pagingView);
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateTopic(int id, [FromBody] UpdateTopicRequest request)
        {
            try
            {
                return Ok(topicService.UpdateTopic(id, request));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteTopic(int id)
        {
            try
            {
                return Ok(topicService.DeleteTopic(id));
            }
            catch (ArgumentException ex)
            {
                if (ex.Message.ToLower().Contains("not found"))
                    return NotFound(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
