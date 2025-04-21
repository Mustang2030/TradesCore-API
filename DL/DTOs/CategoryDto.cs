using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.DTOs
{
    public class CategoryDto
    {
        /// <summary>
        /// Unique identifier for the category.
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Name of the category.
        /// </summary>
        public string Name { get; set; }
    }
}
