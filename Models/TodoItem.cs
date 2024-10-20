using Microsoft.EntityFrameworkCore;
using Kotarski_Wiktor_ToDo_API.Models;
using System.ComponentModel.DataAnnotations;
namespace Kotarski_Wiktor_ToDo_API.Models
{
    public class TodoItem
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public System.DateTime DateOfExpiry { get; set; }
        public string? Description { get; set; }
        public float PercentComplete { get; set; }
    }
}
