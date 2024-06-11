using System;
using System.Collections.Generic;

namespace WebApplication1.Models;

public partial class Image
{
    public int Id { get; set; }

    public string Identifier { get; set; } = null!;

    public byte[] Imagebase64 { get; set; } = null!;
}
