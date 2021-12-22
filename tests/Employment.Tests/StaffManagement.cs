using NUnit.Framework;
using System;

namespace PersonnelManagementSystem.Employment.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CheckForBaseSalary()
    {
        Employee employee1 = new("John", "Doe", Guid.NewGuid().ToString(), DateTime.Today);
        Assert.AreEqual(3000M, employee1.BaseSalary);

        Employee employee2 = new Sales("John", "Doe", Guid.NewGuid().ToString(), DateTime.Today);
        Assert.AreEqual(4500M, employee2.BaseSalary);

        Employee employee3 = new Manager("John", "Doe", Guid.NewGuid().ToString(), DateTime.Today);
        Assert.AreEqual(7000M, employee3.BaseSalary);
    }

    [Test]
    public void CheckForSalaryCalculation()
    {
        Employee employee1 = new("John", "Doe", Guid.NewGuid().ToString(), new DateTime(2019, 12, 22));
        Assert.AreEqual(3270M, employee1.Salary());

        Employee employee2 = new Sales("John", "Doe", Guid.NewGuid().ToString(), new DateTime(2019, 12, 22));
        Assert.AreEqual(5175M, employee2.Salary());

        Employee employee3 = new Manager("John", "Doe", Guid.NewGuid().ToString(), new DateTime(2019, 12, 22));
        Assert.AreEqual(7210M, employee3.Salary());
    }

    private string GenerateId() => Guid.NewGuid().ToString();

    [Test]
    public void CheckForSalary()
    {
        StaffManager staffManager = new();

        // Employees.
        var employeeId1 = GenerateId();
        var employeeDateOfEmployment1 = DateTime.Today;

        var employeeId2 = GenerateId();
        var employeeDateOfEmployment2 = new DateTime(2019, 12, 5);

        var employeeId3 = GenerateId();
        var employeeDateOfEmployment3 = new DateTime(2019, 2, 21);

        var employeeId4 = GenerateId();
        var employeeDateOfEmployment4 = new DateTime(2021, 6, 9);

        var employeeId5 = GenerateId();
        var employeeDateOfEmployment5 = new DateTime(2020, 5, 2);

        var employeeId6 = GenerateId();
        var employeeDateOfEmployment6 = new DateTime(2021, 5, 2);

        var employeeId7 = GenerateId();
        var employeeDateOfEmployment7 = new DateTime(2021, 5, 7);

        // Saleses.
        var salesId1 = GenerateId();
        var salesDateOfEmployment1 = new DateTime(2016, 12, 5);

        var salesId2 = GenerateId();
        var salesDateOfEmployment2 = new DateTime(2017, 12, 5);

        var salesId3 = GenerateId();
        var salesDateOfEmployment3 = new DateTime(2018, 3, 2);

        // Managers.
        var managerId1 = GenerateId();
        var managerDateOfEmployment1 = new DateTime(2017, 9, 5);

        var managerId2 = GenerateId();
        var managerDateOfEmployment2 = new DateTime(2016, 12, 5);

        staffManager.Add("John", "Doe", employeeId1, employeeDateOfEmployment1, EmployeeType.Employee);
        staffManager.Add("John", "Doe", employeeId2, employeeDateOfEmployment2, EmployeeType.Employee);

        staffManager.Add("John", "Doe", salesId1, salesDateOfEmployment1, EmployeeType.Sales);
        staffManager.Add("John", "Doe", salesId2, salesDateOfEmployment2, EmployeeType.Sales);
        staffManager.TryAddSubordinate("John", "Doe", employeeId3, employeeDateOfEmployment3, employeeId2, EmployeeType.Employee);

        staffManager.Add("John", "Doe", managerId1, managerDateOfEmployment1, EmployeeType.Manager);
        staffManager.TryAddSubordinate("John", "Doe", employeeId4, employeeDateOfEmployment4, managerId1, EmployeeType.Employee);
        staffManager.TryAddSubordinate("John", "Doe", salesId3, salesDateOfEmployment3, managerId1, EmployeeType.Sales);
        staffManager.TryAddSubordinate("John", "Doe", employeeId5, employeeDateOfEmployment5, salesId3, EmployeeType.Employee);
        staffManager.TryAddSubordinate("John", "Doe", employeeId6, employeeDateOfEmployment6, salesId3, EmployeeType.Employee);

        staffManager.Add("John", "Doe", managerId2, managerDateOfEmployment2, EmployeeType.Manager);
        staffManager.TryAddSubordinate("John", "Doe", employeeId7, employeeDateOfEmployment7, managerId2, EmployeeType.Employee);

        Assert.AreEqual(3000M, staffManager.GetSalary(employeeId1));
        Assert.AreEqual(3270M, staffManager.GetSalary(employeeId2));
        //Assert.AreEqual(0M, staffManager.GetSalary(employeeId3));
        Assert.AreEqual(3000M, staffManager.GetSalary(employeeId4));
        Assert.AreEqual(3090M, staffManager.GetSalary(employeeId5));
        Assert.AreEqual(3000M, staffManager.GetSalary(employeeId6));
        Assert.AreEqual(3000M, staffManager.GetSalary(employeeId7));

        Assert.AreEqual(7875M, staffManager.GetSalary(salesId1));
        Assert.AreEqual(7875M, staffManager.GetSalary(salesId2));
        Assert.AreEqual(6105.45M, staffManager.GetSalary(salesId3));

        Assert.AreEqual(8077.31635, staffManager.GetSalary(managerId1));
        Assert.AreEqual(9179M, staffManager.GetSalary(managerId2));

        Assert.AreEqual(57471.76635M, staffManager.GetAllSalary());
    }

    [Test]
    public void CheckForCorrectAddEmployeesAndSubordinates()
    {
        StaffManager staffManager = new();

        // Employees.
        var employeeId1 = GenerateId();
        var employeeId2 = GenerateId();
        var employeeId3 = GenerateId();
        var employeeId4 = GenerateId();
        var employeeId5 = GenerateId();
        var employeeId6 = GenerateId();
        var employeeId7 = GenerateId();

        // Saleses.
        var salesId1 = GenerateId();
        var salesId2 = GenerateId();
        var salesId3 = GenerateId();

        // Managers.
        var managerId1 = GenerateId();
        var managerId2 = GenerateId();

        staffManager.Add("John", "Doe", employeeId1, DateTime.Today, EmployeeType.Employee);
        staffManager.Add("John", "Doe", employeeId2, DateTime.Today, EmployeeType.Employee);

        staffManager.Add("John", "Doe", salesId1, DateTime.Today, EmployeeType.Sales);
        staffManager.Add("John", "Doe", salesId2, DateTime.Today, EmployeeType.Sales);

        // Add imposible employee.
        var result1 = staffManager.TryAddSubordinate("John", "Doe", employeeId3, DateTime.Today, employeeId2, EmployeeType.Employee);
        Assert.IsFalse(result1);

        Assert.AreEqual(4, staffManager.EmployeeCount());

        // Add imposible employee.
        var result2 = staffManager.TryAddSubordinate("John", "Doe", GenerateId(), DateTime.Today, employeeId2, EmployeeType.Sales);
        Assert.IsFalse(result2);

        Assert.AreEqual(4, staffManager.EmployeeCount());

        staffManager.Add("John", "Doe", managerId1, DateTime.Today, EmployeeType.Manager);
        var result3 = staffManager.TryAddSubordinate("John", "Doe", employeeId4, DateTime.Today, managerId1, EmployeeType.Employee);
        Assert.IsTrue(result3);

        var result4 = staffManager.TryAddSubordinate("John", "Doe", salesId3, DateTime.Today, managerId1, EmployeeType.Sales);
        Assert.IsTrue(result4);

        // Check for add imposimble subordinate.
        var result5 = staffManager.TryAddSubordinate("John", "Doe", GenerateId(), DateTime.Today, employeeId2, EmployeeType.Manager);
        Assert.IsFalse(result5);

        var result6 = staffManager.TryAddSubordinate("John", "Doe", employeeId5, DateTime.Today, salesId3, EmployeeType.Employee);
        Assert.IsTrue(result6);

        staffManager.TryAddSubordinate("John", "Doe", employeeId6, DateTime.Today, salesId3, EmployeeType.Employee);

        staffManager.Add("John", "Doe", managerId2, DateTime.Today, EmployeeType.Manager);
        staffManager.TryAddSubordinate("John", "Doe", employeeId7, DateTime.Today, managerId2, EmployeeType.Employee);

        Assert.AreEqual(11, staffManager.EmployeeCount());
    }

    [Test]
    public void CheckForCorrectRemoveEmployeesAndSubordinates()
    {
        StaffManager staffManager = new();

        // Employees.
        var employeeId1 = GenerateId();
        var employeeDateOfEmployment1 = DateTime.Today;

        var employeeId2 = GenerateId();
        var employeeDateOfEmployment2 = new DateTime(2019, 12, 5);

        var employeeId3 = GenerateId();
        var employeeDateOfEmployment3 = new DateTime(2019, 2, 21);

        var employeeId4 = GenerateId();
        var employeeDateOfEmployment4 = new DateTime(2021, 6, 9);

        var employeeId5 = GenerateId();
        var employeeDateOfEmployment5 = new DateTime(2020, 5, 2);

        var employeeId6 = GenerateId();
        var employeeDateOfEmployment6 = new DateTime(2021, 5, 2);

        var employeeId7 = GenerateId();
        var employeeDateOfEmployment7 = new DateTime(2021, 5, 7);

        // Saleses.
        var salesId1 = GenerateId();
        var salesDateOfEmployment1 = new DateTime(2016, 12, 5);

        var salesId2 = GenerateId();
        var salesDateOfEmployment2 = new DateTime(2017, 12, 5);

        var salesId3 = GenerateId();
        var salesDateOfEmployment3 = new DateTime(2018, 3, 2);

        // Managers.
        var managerId1 = GenerateId();
        var managerDateOfEmployment1 = new DateTime(2017, 9, 5);

        var managerId2 = GenerateId();
        var managerDateOfEmployment2 = new DateTime(2016, 12, 5);

        staffManager.Add("John", "Doe", employeeId1, employeeDateOfEmployment1, EmployeeType.Employee);
        staffManager.Add("John", "Doe", employeeId2, employeeDateOfEmployment2, EmployeeType.Employee);
        staffManager.Add("John", "Doe", salesId1, salesDateOfEmployment1, EmployeeType.Sales);

        Assert.AreEqual(3, staffManager.EmployeeCount());

        staffManager.Add("John", "Doe", salesId2, salesDateOfEmployment2, EmployeeType.Sales);
        staffManager.TryAddSubordinate("John", "Doe", employeeId3, employeeDateOfEmployment3, salesId2, EmployeeType.Employee);

        Assert.AreEqual(5, staffManager.EmployeeCount());

        staffManager.Add("John", "Doe", managerId1, managerDateOfEmployment1, EmployeeType.Manager);
        staffManager.TryAddSubordinate("John", "Doe", employeeId4, employeeDateOfEmployment4, managerId1, EmployeeType.Employee);
        staffManager.TryAddSubordinate("John", "Doe", salesId3, salesDateOfEmployment3, managerId1, EmployeeType.Sales);
        staffManager.TryAddSubordinate("John", "Doe", employeeId5, employeeDateOfEmployment5, salesId3, EmployeeType.Employee);
        staffManager.TryAddSubordinate("John", "Doe", employeeId6, employeeDateOfEmployment6, salesId3, EmployeeType.Employee);

        Assert.AreEqual(10, staffManager.EmployeeCount());

        staffManager.Add("John", "Doe", managerId2, managerDateOfEmployment2, EmployeeType.Manager);
        staffManager.TryAddSubordinate("John", "Doe", employeeId7, employeeDateOfEmployment7, managerId2, EmployeeType.Employee);

        Assert.AreEqual(12, staffManager.EmployeeCount());

        var result1 = staffManager.TryRemove(employeeId1);
        var result2 = staffManager.TryRemove(employeeId2);

        Assert.IsTrue(result1 && result2);
        Assert.AreEqual(10, staffManager.EmployeeCount());

        var result3 = staffManager.TryRemove(salesId2);

        Assert.IsTrue(result3);
        Assert.AreEqual(8, staffManager.EmployeeCount());

        var result4 = staffManager.TryRemove(managerId1);

        Assert.IsTrue(result4);
        Assert.AreEqual(3, staffManager.EmployeeCount());
    }
}
