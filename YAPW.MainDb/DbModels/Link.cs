using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAPW.MainDb.DbModels
{
    public class Link : EntityBase
    {
        [MaxLength(200)]
        public string LinkId { get; set; }
    }
}
