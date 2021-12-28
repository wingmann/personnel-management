using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Employment.Tests;

public class Tests
{
    [Test]
    public void CheckForBaseEmployeeSalary()
    {
        Employee employee1 = new(new Person("John", "Doe"), Guid.NewGuid().ToString(), DateTime.Today);
        Assert.AreEqual(3000M, employee1.BaseSalary);
    }
    
    [Test]
    public void CheckForBaseSalesSalary()
    {
        Employee sales = new Sales(new Person("John", "Doe"), Guid.NewGuid().ToString(), DateTime.Today);
        Assert.AreEqual(4500M, sales.BaseSalary);
    }
    
    [Test]
    public void CheckForBaseManagerSalary()
    {
        Employee manager = new Manager(new Person("John", "Doe"), Guid.NewGuid().ToString(), DateTime.Today);
        Assert.AreEqual(7000M, manager.BaseSalary);
    }

    [Test]
    public void CheckForEmployeeSalaryCalculation()
    {
        // Arrange.
        Person person = new("John", "Doe");
        var id = Guid.NewGuid().ToString();
        var dateOfEmployment = new DateTime(2019, 12, 22);

        Employee employee = new(person, id, dateOfEmployment);

        // Act.
        var salary = employee.Salary();

        // Assert;
        Assert.AreEqual(3270M, salary);
    }
    
    [Test]
    public void CheckForSalesSalaryCalculation()
    {
        // Arrange.
        Person person = new("John", "Doe");
        var id = Guid.NewGuid().ToString();
        var dateOfEmployment = new DateTime(2019, 12, 22);

        Employee employee = new Sales(person, id, dateOfEmployment);

        // Act.
        var salary = employee.Salary();

        // Assert;
        Assert.AreEqual(5175M, salary);
    }
    
    [Test]
    public void CheckForManagerSalaryCalculation()
    {
        // Arrange.
        Person person = new("John", "Doe");
        var id = Guid.NewGuid().ToString();
        var dateOfEmployment = new DateTime(2019, 12, 22);

        Employee employee = new Manager(person, id, dateOfEmployment);

        // Act.
        var salary = employee.Salary();

        // Assert;
        Assert.AreEqual(7210M, salary);
    }
    
    private static string GenerateId() => Guid.NewGuid().ToString();

    [Test]
    public void CheckForSalary()
    {
        // Arrange.
        StaffManager staffManager = new();
        Person person = new("John", "Doe");

        List<(string, DateTime)> employees = new()
        {
            (GenerateId(), DateTime.Now),
            (GenerateId(), new DateTime(2019, 12, 5)),
            (GenerateId(), new DateTime(2019, 2, 21)),
            (GenerateId(), new DateTime(2021, 6, 9)),
            (GenerateId(), new DateTime(2020, 5, 2)),
            (GenerateId(), new DateTime(2021, 5, 2)),
            (GenerateId(), new DateTime(2021, 5, 7))
        };

        List<(string, DateTime)> sales = new()
        {
            (GenerateId(), new DateTime(2016, 12, 5)),
            (GenerateId(), new DateTime(2017, 12, 5)),
            (GenerateId(), new DateTime(2018, 3, 2))
        };

        List<(string, DateTime)> managers = new()
        {
            (GenerateId(), new DateTime(2017, 9, 5)),
            (GenerateId(), new DateTime(2016, 12, 5))
        };
        
        // Act.
        staffManager.Add(person, employees[0].Item1, employees[0].Item2, EmployeeType.Employee);
        staffManager.Add(person, employees[1].Item1, employees[1].Item2, EmployeeType.Employee);

        staffManager.Add(person, sales[0].Item1, sales[0].Item2, EmployeeType.Sales);
        staffManager.Add(person, sales[1].Item1, sales[1].Item2, EmployeeType.Sales);
        staffManager.TryAddSubordinate(person, employees[2].Item1, employees[2].Item2, sales[1].Item1, EmployeeType.Employee);

        staffManager.Add(person, managers[0].Item1, managers[0].Item2, EmployeeType.Manager);
        staffManager.TryAddSubordinate(person, employees[2].Item1, employees[2].Item2, managers[0].Item1, EmployeeType.Employee);
        staffManager.TryAddSubordinate(person, sales[2].Item1, sales[2].Item2, managers[0].Item1, EmployeeType.Sales);
        staffManager.TryAddSubordinate(person, employees[4].Item1, employees[4].Item2, sales[2].Item1, EmployeeType.Employee);
        staffManager.TryAddSubordinate(person, employees[5].Item1, employees[5].Item2, sales[2].Item1, EmployeeType.Employee);

        staffManager.Add(person, managers[1].Item1, managers[1].Item2, EmployeeType.Manager);
        staffManager.TryAddSubordinate(person, employees[6].Item1, employees[6].Item2, managers[1].Item1, EmployeeType.Employee);

        // Assert.
        Assert.AreEqual(3000M, staffManager.GetSalary(employees[0].Item1));
        Assert.AreEqual(3270M, staffManager.GetSalary(employees[1].Item1));
        Assert.AreEqual(3090M, staffManager.GetSalary(employees[4].Item1));
        Assert.AreEqual(7875M, staffManager.GetSalary(sales[0].Item1));
        Assert.AreEqual(7891.35M, staffManager.GetSalary(sales[1].Item1));
        Assert.AreEqual(6105.45M, staffManager.GetSalary(sales[2].Item1));
        Assert.AreEqual(8078.12635M, staffManager.GetSalary(managers[0].Item1));
        Assert.AreEqual(9179M, staffManager.GetSalary(managers[1].Item1));
        Assert.AreEqual(61028.92635M, staffManager.GetAllSalary());
    }

    private static List<string> GetIds(int count)
    {
        List<string> list = new();

        for (var i = 0; i < count; i++)
        {
            list.Add(GenerateId());
        }

        return list;
    }
    
    [Test]
    public void CheckForCorrectAddEmployeesAndSubordinates()
    {
        // Arrange.
        StaffManager staffManager = new();
        Person person = new("John", "Doe");
        
        var employeeIds = GetIds(7);
        var salesIds = GetIds(3);
        var managerIds= GetIds(2);
        
        // Act.
        staffManager.Add(person, employeeIds[0], DateTime.Today, EmployeeType.Employee);
        staffManager.Add(person, employeeIds[1], DateTime.Today, EmployeeType.Employee);

        staffManager.Add(person, salesIds[0], DateTime.Today, EmployeeType.Sales);
        staffManager.Add(person, salesIds[1], DateTime.Today, EmployeeType.Sales);

        // Add impossible employee.
        var result1 = staffManager.TryAddSubordinate(person, employeeIds[2], DateTime.Today, employeeIds[1], EmployeeType.Employee);

        // Add impossible employee.
        var result2 = staffManager.TryAddSubordinate(person, GenerateId(), DateTime.Today, employeeIds[1], EmployeeType.Sales);

        staffManager.Add(person, managerIds[0], DateTime.Today, EmployeeType.Manager);
        var result3 = staffManager.TryAddSubordinate(person, employeeIds[3], DateTime.Today, managerIds[0], EmployeeType.Employee);

        var result4 = staffManager.TryAddSubordinate(person, salesIds[2], DateTime.Today, managerIds[0], EmployeeType.Sales);
        
        // Check for add impossible subordinate.
        var result5 = staffManager.TryAddSubordinate(person, GenerateId(), DateTime.Today, employeeIds[1], EmployeeType.Manager);

        var result6 = staffManager.TryAddSubordinate(person, employeeIds[4], DateTime.Today, salesIds[2], EmployeeType.Employee);

        staffManager.TryAddSubordinate(person, employeeIds[5], DateTime.Today, salesIds[2], EmployeeType.Employee);

        staffManager.Add(person, managerIds[1], DateTime.Today, EmployeeType.Manager);
        staffManager.TryAddSubordinate(person, employeeIds[6], DateTime.Today, managerIds[1], EmployeeType.Employee);

        var employeeCount = staffManager.EmployeeCount();
        
        // Assert.
        Assert.IsFalse(result1);
        Assert.IsFalse(result2);
        Assert.IsFalse(result5);
        
        Assert.IsTrue(result3 && result4 && result6);

        Assert.AreEqual(11, employeeCount);
    }

    [Test]
    public void CheckForCorrectRemoveEmployeesAndSubordinates()
    {
        // Arrange.
        StaffManager staffManager = new();
        Person person = new("John", "Doe");

        List<(string, DateTime)> employees = new()
        {
            (GenerateId(), DateTime.Today),
            (GenerateId(), new DateTime(2019, 12, 5)),
            (GenerateId(), new DateTime(2019, 2, 21)),
            (GenerateId(), new DateTime(2021, 6, 9)),
            (GenerateId(), new DateTime(2020, 5, 2)),
            (GenerateId(), new DateTime(2021, 5, 2)),
            (GenerateId(), new DateTime(2021, 5, 7))
        };

        List<(string, DateTime)> sales = new()
        {
            (GenerateId(), new DateTime(2016, 12, 5)),
            (GenerateId(), new DateTime(2017, 12, 5)),
            (GenerateId(), new DateTime(2018, 3, 2))
        };
        
        List<(string, DateTime)> managers = new()
        {
            (GenerateId(), new DateTime(2017, 9, 5)),
            (GenerateId(), new DateTime(2016, 12, 5))
        };
        
        // Act.
        staffManager.Add(person, employees[0].Item1, employees[0].Item2, EmployeeType.Employee);
        staffManager.Add(person, employees[1].Item1, employees[1].Item2, EmployeeType.Employee);
        staffManager.Add(person, sales[0].Item1, sales[0].Item2, EmployeeType.Sales);

        var count1 = staffManager.EmployeeCount();

        staffManager.Add(person, sales[1].Item1, sales[1].Item2, EmployeeType.Sales);
        staffManager.TryAddSubordinate(person, employees[2].Item1, employees[2].Item2, sales[1].Item1, EmployeeType.Employee);

        var count2 = staffManager.EmployeeCount();

        staffManager.Add(person, managers[0].Item1, managers[0].Item2, EmployeeType.Manager);
        staffManager.TryAddSubordinate(person, employees[3].Item1, employees[3].Item2, managers[0].Item1, EmployeeType.Employee);
        staffManager.TryAddSubordinate(person, sales[2].Item1, sales[2].Item2, managers[0].Item1, EmployeeType.Sales);
        staffManager.TryAddSubordinate(person, employees[4].Item1, employees[4].Item2, sales[2].Item1, EmployeeType.Employee);
        staffManager.TryAddSubordinate(person, employees[5].Item1, employees[5].Item2, sales[2].Item1, EmployeeType.Employee);

        var count3 = staffManager.EmployeeCount();

        staffManager.Add(person, managers[1].Item1, managers[1].Item2, EmployeeType.Manager);
        staffManager.TryAddSubordinate(person, employees[6].Item1, employees[6].Item2, managers[1].Item1, EmployeeType.Employee);

        var count4 = staffManager.EmployeeCount();
        
        var result1 = staffManager.TryRemove(employees[0].Item1);
        var result2 = staffManager.TryRemove(employees[1].Item1);
        var count5 = staffManager.EmployeeCount();
        
        var result3 = staffManager.TryRemove(sales[1].Item1);
        var count6 = staffManager.EmployeeCount();
        
        var result4 = staffManager.TryRemove(managers[0].Item1);
        var count7 = staffManager.EmployeeCount();
        
        // Assert.
        Assert.AreEqual(3, count1);
        Assert.AreEqual(5, count2);
        Assert.AreEqual(10, count3);
        Assert.AreEqual(12, count4);
        Assert.AreEqual(10, count5);
        Assert.AreEqual(8, count6);
        Assert.AreEqual(3, count7);
        
        Assert.IsTrue(result1 && result2 && result3 && result4);
    }
}
