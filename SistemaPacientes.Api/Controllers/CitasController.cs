using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.Api.Controllers;

[ApiController]
[Route("api/citas")]
public sealed class CitasController : ControllerBase
{
    private readonly ICitaService _service;

    public CitasController(ICitaService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CitaDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CitaDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _service.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<CitaDto>> Create([FromBody] CitaWriteDto dto, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<CitaDto>> Update(Guid id, [FromBody] CitaWriteDto dto, CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(id, dto, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await _service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
