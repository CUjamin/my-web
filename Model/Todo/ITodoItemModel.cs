using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my_web.Model.Todo
{
    internal interface ITodoItemModel
    {
        internal string Id { get; set; }
        internal string Name { get; set; }
    }
}