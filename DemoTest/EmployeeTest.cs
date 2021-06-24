using Demo.DAL.Repositories;
using Demo.Models;
using Moq;
using System;
using Xunit;

namespace DemoTest
{
    public class EmployeeTest
    {
        //private readonly Mock<IEmployeeRepository> repository;

        //public EmployeeTest( Mock<IEmployeeRepository> repository)
        //{
        //    this.repository = repository;
        //}

        private Mock<IEmployeeRepository> _mockRepository;

        public EmployeeTest()
        {
            _mockRepository = new Mock<IEmployeeRepository>();
        }

        [Fact]
        public void AddEmployee()
        {
            //Employee employee = new Employee()
            //{
            //    FirstName = "a",
            //    LastName = "b",
            //    BirthDate = new DateTime(2020 - 10 - 10)
            //};
            //var repository = new Mock<IEmployeeRepository>();
            //var res = repository.Object.InsertAsync(employee).ToString();
            //var id = repository.Object.FindOne(null, e => e.FirstName == employee.FirstName).Id.ToString();
            //Assert.Equal(id, res);
            _mockRepository.Setup(mr => mr.Insert(It.IsAny<Employee>())).Returns(1);
        }

        [Fact]
        public void DeleteEmployee()
        {
            //Employee employee = new Employee()
            //{
            //    Id = 2,
            //    FirstName = "a",
            //    LastName = "b",
            //    BirthDate = new DateTime(2020 - 10 - 10)
            //};
            //var res = _mockRepository.Object.Delete(employee);
            //_mockRepository.Verify(r => r.Delete(employee));


            _mockRepository.Setup(mr => mr.Delete(It.IsAny<Employee>())).Returns(1);
        }

        [Fact]
        public void UpadeEmployee()
        {
            //Employee employee = new Employee()
            //{
            //    FirstName = "c",
            //    LastName = "f",
            //    BirthDate = new DateTime(2020 - 10 - 10)
            //};
            //_mockRepository.Setup(c => c.UpdateAsync(employee)).Returns();
            //var res = _mockRepository.Object.UpdateAsync(employee);
            //_mockRepository.Verify(r => r.Update(employee));


            _mockRepository.Setup(mr => mr.Update(It.IsAny<Employee>())).Returns(1);
        }
    }
}
