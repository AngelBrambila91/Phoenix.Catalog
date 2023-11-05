using System.ComponentModel.DataAnnotations;

namespace Phoenix.Catalog.Service.Dtos;

public record GodDto (Guid Id, string? Name, string? Description, decimal Price, DateTimeOffset CreatedDate);
public record CreateGodDto([Required]string? Name, string? Description, [Range(0,1000)]decimal Price);
public record UpdateGodDto([Required]string? Name, string? Description, [Range(0,1000)]decimal Price);