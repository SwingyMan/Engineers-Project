using Application.Commands;
using Application.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engineers_Project.Server.Controllers;

[Route("api/v1/[controller]/[action]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Retrieves a role by its Guid.
    /// </summary>
    /// <param name="id">Role Guid</param>
    /// <returns>The retrieved role, if found.</returns>
    /// <response code="200">Returns the role if found.</response>
    /// <response code="404">If the role is not found.</response>
    // GET api/role/get/5
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var role = await _mediator.Send(new GenericGetByIdQuery<Role>(id));
        if (role == null) return NotFound();
        return Ok(role);
    }

    /// <summary>
    ///     Creates a role.
    /// </summary>
    /// <param name="genericAddCommand">Role DTO</param>
    /// <returns>The updated role.</returns>
    // POST api/role/role
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] GenericAddCommand<RoleDTO, Role> genericAddCommand)
    {
        return Ok(await _mediator.Send(genericAddCommand));
    }

    /// <summary>
    ///     Updates a role.
    /// </summary>
    /// <param name="genericUpdateCommand">Update command</param>
    /// <returns>The updated role.</returns>
    // PUT api/role/put
    [HttpPut("{id}")]
    public async Task<IActionResult> Put([FromBody] GenericUpdateCommand<RoleDTO, Role> genericUpdateCommand)
    {
        return Ok(await _mediator.Send(genericUpdateCommand));
    }

    /// <summary>
    ///     Deletes a role.
    /// </summary>
    /// <param name="id">Role Guid</param>
    /// <response code="200">If the role was found.</response>
    /// <response code="404">If the role was not found.</response>
    // DELETE api/role/delete/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new GenericDeleteCommand<Role>(id));
        return Ok();
    }

    /// <summary>
    ///     Retrieves all roles.
    /// </summary>
    /// <param name="query"></param>
    /// <returns>All tags</returns>
    // POST api/role/getall
    [HttpPost]
    public async Task<IActionResult> GetAll([FromBody] GenericGetAllQuery<Role> query)
    {
        return Ok(await _mediator.Send(query));
    }
}