using Phoenix.Catalog.Service.Dtos;

public static class Extensions
{
    public static GodDto AsDto(this God god)
    {
        return new GodDto(god.Id, god.Name, god.Description, god.Price, god.CreatedDate);
    }
}