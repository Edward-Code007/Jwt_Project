using System.ComponentModel.DataAnnotations;

namespace TodoEntity;

 public record TodoItem
{
   public Guid Id {get;set;}
   public string Description {get;set;}
   public DateTime CreatedAt{get;set;} 

   public DateTime UpdatedAt{get;set;}
   public bool isCompleted{get;set;} = false;
}