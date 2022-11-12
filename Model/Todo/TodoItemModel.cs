using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_web.Model.Todo;
public class TodoItemModel : ITodoItemModel
{
    public string Id { get; set; }
    public string Name { get; set; }
}
