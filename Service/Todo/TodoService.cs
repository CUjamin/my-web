using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my_web.Model.Todo;

namespace my_web.Service.Todo;

public class TodoService
{
    private IDictionary<string, ITodoItemModel> idToItemMap = new Dictionary<string, ITodoItemModel>();

    internal TodoService(IDictionary<string, ITodoItemModel> idToItemMap)
    {
        this.idToItemMap = idToItemMap;
    }
    internal Boolean AddTodo(ITodoItemModel todoItem)
    {
        idToItemMap.Add(todoItem.Id, todoItem);
        return true;
    }

    internal ITodoItemModel GetTodoItem(string id)
    {
        ITodoItemModel value;
        if (idToItemMap.TryGetValue(id, out value))
        {
            return value;
        }
        throw new KeyNotFoundException("can not found id=" + id);
    }

    internal Boolean RemoveTodoItem(string id){
        return idToItemMap.Remove(id);
    }

    public IList<TodoItemModel> GetALl(){
        IList<TodoItemModel> todoItemModels = new List<TodoItemModel>();
        todoItemModels.Add((TodoItemModel)idToItemMap.Values);
        return todoItemModels;
    }
}