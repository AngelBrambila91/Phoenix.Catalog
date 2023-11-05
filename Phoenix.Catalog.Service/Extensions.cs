using Phoenix.Catalog.Service.Dtos;
using Phoenix.Catalog.Service.Entities;

namespace Phoenix.Catalog.Service;

public static class Extensions
{
    public static GodDto AsDto(this God god)
    {
        return new GodDto(god.Id, god.Name, god.Description, god.Price, god.CreatedDate);
    }
}