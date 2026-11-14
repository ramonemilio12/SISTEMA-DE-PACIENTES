using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.Api.Controllers;

[ApiController]
[Route("api/receta-detalles")]
public sealed class RecetaDetallesController : ControllerBase
{
    private readonly IRecetaDetalleService _service;

    public RecetaDetallesController(IRecetaDetalleService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<RecetaDetalleDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RecetaDetalleDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _service.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<RecetaDetalleDto>> Create([FromBody] RecetaDetalleWriteDto dto, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RecetaDetalleDto>> Update(Guid id, [FromBody] RecetaDetalleWriteDto dto, CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(id, dto, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await _service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
