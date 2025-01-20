using System;
using System.Collections.Generic;

namespace apiHorus.Models;

public partial class Producto
{
    public int ProductoId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal? PriceAnterior { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string Categoria { get; set; } = null!;
}
