
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ASPNETCoreWebAPI.Data.Repository
{
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dBContext) : base(dBContext) { }

        public Task<List<Student>> GetStudentsByFeesStatusAsync(int feesStatus)
        {
            //write code to return students having fee status pending
            return null;
        }
    }
}
