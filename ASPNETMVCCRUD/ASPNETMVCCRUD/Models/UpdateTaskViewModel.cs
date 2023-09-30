namespace ASPNETMVCCRUD.Models
{
    public class UpdateTaskViewModel
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string AssignedTo { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Priorty { get; set; }



    }
}
