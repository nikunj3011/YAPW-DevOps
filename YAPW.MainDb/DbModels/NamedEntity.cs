using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class NamedEntity : EntityBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(100)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
