using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DOMAIN.Common
{
    public abstract class AuditableBaseEntity
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public virtual Guid Id { get; set; }
        public string CreatedBy  { get; set; }
        public DateTime Created{ get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

    }
}
