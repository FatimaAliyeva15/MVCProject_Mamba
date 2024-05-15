using Mamba_Business.Exceptions.Employee;
using Mamba_Business.Services.Abstracts;
using Mamba_Core.Models;
using Mamba_Core.ReposirotyAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mamba_Business.Services.Concretes
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void AddEmployee(Employee employee)
        {
            if (employee == null) throw new NullReferenceException("Employee not found");

            if (!employee.ImgFile.ContentType.Contains("image/"))
                throw new FileContentTypeException("ImageFile", "File content type error");
            if (employee.ImgFile.Length > 2097152)
                throw new FileSizeException("ImageFile", "File size error");

            string filename = employee.ImgFile.FileName;
            string path = @"C:\Users\Asus\source\repos\Mamba\Mamba\wwwroot\upload\employee\" + filename;
            using(FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                employee.ImgFile.CopyTo(fileStream);
            }
            employee.ImgUrl = filename;

            _employeeRepository.Add(employee);
            _employeeRepository.Commit();
        }

        public void DeleteEmployee(int id)
        {
            var existEmployee = _employeeRepository.Get(x => x.Id == id);
            if (existEmployee == null)
                    throw new EntityNotFoundException("", "Entity not found");

            string path = @"C:\\Users\\Asus\\source\\repos\\Mamba\\Mamba\\wwwroot\\upload\\employee\\" + existEmployee.ImgUrl;

            if (!File.Exists(path))
                throw new FileNotFoundException("File not found");

            File.Delete(path);

            _employeeRepository.Delete(existEmployee);
            _employeeRepository.Commit();
        }

        public List<Employee> GetAllEmployees(Func<Employee, bool>? func = null)
        {
            return _employeeRepository.GetAll(func);
        }

        public Employee GetEmployee(Func<Employee, bool>? func = null)
        {
            return _employeeRepository.Get(func);
        }

        public void UpdateEmployee(int id, Employee employee)
        {
            var existEmployee = _employeeRepository.Get(x => x.Id == id);
            if(existEmployee == null) throw new NullReferenceException("Employee not found");

            if(employee.ImgFile != null)
            {
                if (!employee.ImgFile.ContentType.Contains("image/"))
                    throw new FileContentTypeException("ImageFile", "File content type error");
                if (employee.ImgFile.Length > 2097152)
                    throw new FileSizeException("ImageFile", "File size error");

                string filename = employee.ImgFile.FileName;
                string path = @"C:\\Users\\Asus\\source\\repos\\Mamba\\Mamba\\wwwroot\\upload\\employee\\" + filename;
                using(FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    employee.ImgFile.CopyTo(fileStream);
                }
                employee.ImgUrl = filename;
                
                existEmployee.ImgUrl = employee.ImgUrl;
            }

            existEmployee.Fullname = employee.Fullname;
            existEmployee.Position = employee.Position;

            _employeeRepository.Commit();

        }
    }
}
