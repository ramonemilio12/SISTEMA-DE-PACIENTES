using Microsoft.AspNetCore.Mvc;
using SistemaPacientes.Application.Dtos;
using SistemaPacientes.Application.Interfaces;

namespace SistemaPacientes.Api.Controllers;

[ApiController]
[Route("api/diagnosticos")]
public sealed class DiagnosticosController : ControllerBase
{
    private readonly IDiagnosticoService _service;

    public DiagnosticosController(IDiagnosticoService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<DiagnosticoDto>>> GetAll(CancellationToken cancellationToken)
        => Ok(await _service.GetAllAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<DiagnosticoDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _service.GetByIdAsync(id, cancellationToken);
        return dto is null ? NotFound() : Ok(dto);
    }

    [HttpPost]
    public async Task<ActionResult<DiagnosticoDto>> Create([FromBody] DiagnosticoWriteDto dto, CancellationToken cancellationToken)
    {
        var created = await _service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<DiagnosticoDto>> Update(Guid id, [FromBody] DiagnosticoWriteDto dto, CancellationToken cancellationToken)
    {
        var updated = await _service.UpdateAsync(id, dto, cancellationToken);
        return updated is null ? NotFound() : Ok(updated);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        => await _service.DeleteAsync(id, cancellationToken) ? NoContent() : NotFound();
}
