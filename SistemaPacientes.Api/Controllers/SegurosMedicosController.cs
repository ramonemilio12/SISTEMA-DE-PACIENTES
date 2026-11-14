using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.Api.Controllers;

[ApiController]
[Route("api/seguros-medicos")]
public sealed class SegurosMedicosController : ControllerBase
{
    private readonly ISeguroMedicoService _service;

    public SegurosMedicosController(ISeguroMedicoService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<SeguroMedicoDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SeguroMedicoDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _service.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<SeguroMedicoDto>> Create([FromBody] SeguroMedicoWriteDto dto, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<SeguroMedicoDto>> Update(Guid id, [FromBody] SeguroMedicoWriteDto dto, CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(id, dto, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await _service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
