namespace ASPNETCoreWebAPI.Data
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmetnName { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
