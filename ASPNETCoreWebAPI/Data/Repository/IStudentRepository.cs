namespace ASPNETCoreWebAPI.Data.Repository
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        Task<List<Student>> GetStudentsByFeesStatusAsync(int feesStatus);
    }
}
