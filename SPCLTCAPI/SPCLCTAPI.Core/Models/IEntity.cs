using System;
using System.Collections.Generic;
using System.Text;

namespace SPCLCTAPI.Core.Models
{
    public interface IEntity
    {
        TKey Id { get; set; }
    }
}
