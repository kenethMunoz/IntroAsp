using System;
using System.Collections.Generic;

namespace IntroAsp.Models;

public partial class Beer
{
    public int IdBeer { get; set; }

    public string? Name { get; set; }

    public int ID_Brand { get; set; }

    public virtual Brand? ID_BrandNavigation { get; set; }
}
