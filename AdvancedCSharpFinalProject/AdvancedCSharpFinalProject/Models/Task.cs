namespace AdvancedCSharpFinalProject.Models
{
    public class Task
    {
        public Project Project { get; set; }
        public enum Priority { Low, Medium, High }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
