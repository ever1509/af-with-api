using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Click
{
    public int ClickId { get; set; }
    public string Platform { get; set; }
    public string Browser { get; set; }
    public Guid UrlId { get; set; }
    public DateTime? CreatedDateClick { get; set; }
    public virtual Url Url { get; set; }
}
