using DataAccess.Entities;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests;

public class EmployeeControllerTest
{
    [Fact]
    public async void GetListAsync_ReturnsAJsonResult_WithEmployeeList()
    {
        //Arange
        var mockRepo = new Mock<IEmployeeService>();
        mockRepo.Setup(repo => repo.GetAll())
           .Returns(GetEmployees());

        var controller = new EmployeeController(mockRepo.Object);

        // Act
        var result = await controller.GetListAsync();

        //Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Employee>>(jsonResult.Value);
        Assert.Equal(2, model.Count());
    }


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async void GetOne_ReturnsAJsonResult_WithAnEmployeeIfIdExists(int id)
    {
        //Arange
        var mockRepo = new Mock<IEmployeeService>();
        mockRepo.Setup(repo => repo.GetOne(id))
           .Returns(GetEmployees().Where(p => p.ID == id).FirstOrDefault());

        var controller = new EmployeeController(mockRepo.Object);

        // Act
        var result = await controller.GetOne(id);

        //Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        var model = Assert.IsAssignableFrom<Employee>(jsonResult.Value);
        Assert.NotNull(model);

    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    public async void GetOne_ReturnsAJsonResult_WithNullIfIdDoesNotExists(int id)
    {
        //Arange
        var mockRepo = new Mock<IEmployeeService>();
        mockRepo.Setup(repo => repo.GetOne(id))
           .Returns(GetEmployees().Where(p => p.ID == id).FirstOrDefault());

        var controller = new EmployeeController(mockRepo.Object);

        // Act
        var result = await controller.GetOne(id);

        //Assert
        var jsonResult = Assert.IsType<JsonResult>(result);
        Assert.Null(jsonResult.Value);

    }

    private List<Employee> GetEmployees()
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee(){ ID = 1, FirstName = "Bob", LastName = "Mbassa", Gender = "Male", DepartmentId = 1},
            new Employee(){ ID = 2, FirstName = "Ashraf", LastName = "Miiro", Gender = "Male", DepartmentId = 2},
        };
        return employees;
    }
}