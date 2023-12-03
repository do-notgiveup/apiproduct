using System;
using System.Collections.Generic;

namespace API.Models;

public partial class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int CateId { get; set; }

    public double? Price { get; set; }

    public virtual Category Cate { get; set; } = null!;
}
