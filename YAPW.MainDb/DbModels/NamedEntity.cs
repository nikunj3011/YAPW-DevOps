using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAPW.MainDb.Interfaces;

namespace YAPW.MainDb.DbModels
{
    public class NamedEntity : EntityBase, INamedEntity
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(200)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}
