namespace AspNetCoreTemplate.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using AspNetCoreTemplate.Data.Common.Models;

    public class Art : BaseDeletableModel<string>
    {
        public string Make { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }
    }
}
