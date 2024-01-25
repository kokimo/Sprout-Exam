using Sprout.Exam.Business.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.Business.Interfaces
{
    public interface IEmployeeRepository
    {
       public Task<EmployeeDto> GetByIdAsync(int id);
       public Task<List<EmployeeDto>> GetListAsync();

       public Task<int> CreateAsync(CreateEmployeeDto input);

       public Task<EmployeeDto> UpdateAsync(EditEmployeeDto input);

        public Task<int> DeleteAsync(int id);
    }
}
