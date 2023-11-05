/*
The class we use for this should not be confused with
our DTOs because we want to have the freedom of updating how we store the items in
the database at any given point, regardless of the contract that we need to honor with
our service clients.
*/

using Phoenix.Common;

namespace Phoenix.Catalog.Service.Entities;
public class God : IEntity
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
}