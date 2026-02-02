# 🌐 Introduction to ASP.NET Core Web API

A comprehensive guide to understanding Web APIs, their evolution, and practical implementation using ASP.NET Core.

---

## 📖 Table of Contents

1. [Introduction](#1-introduction)
2. [Evolution of Web API](#2-evolution-of-web-api)
3. [What is Web API?](#3-what-is-web-api)
4. [Why Web API?](#4-why-web-api)
5. [Web API Request & Response](#5-web-api-request--response)
6. [HTTP Verbs](#6-http-verbs)
7. [Creating Your First Endpoint](#7-creating-your-first-endpoint)
8. [Why and How to Use DTOs](#8-why-and-how-to-use-dtos)

---

## 1. Introduction

Welcome to the world of **Web APIs**! 🚀

A **Web API (Application Programming Interface)** is a set of rules and protocols that allows different software applications to communicate with each other over the internet. Think of it as a waiter in a restaurant – it takes your order (request), communicates with the kitchen (server), and brings back your food (response).

In this guide, you'll learn:

- How APIs evolved over time
- The difference between traditional web apps and Web APIs
- How to build your own API endpoints

---

## 2. Evolution of Web API

### 🕰️ The SOAP Era (Before REST)

In the early days, applications communicated using **SOAP (Simple Object Access Protocol)**:

```
┌─────────────────┐         WSDL/Proxy           ┌─────────────────┐
│   .NET App      │  ────────────────────────▶   │   Java App      │
│                 │                               │                 │
│  ┌───────────┐  │      XML Serialization       │  ┌───────────┐  │
│  │   Data    │──┼─────────────────────────────▶│──│   Data    │  │
│  └───────────┘  │                               │  └───────────┘  │
│                 │      XML Deserialization      │                 │
└─────────────────┘  ◀────────────────────────────└─────────────────┘
```

**How SOAP Works:**

1. A **.NET application** creates data
2. Uses **WSDL (Web Services Description Language)** to define the service contract
3. Data is **serialized into XML** format
4. Sent over the network using SOAP protocol
5. **Java application** receives and **deserializes** the XML back to objects

> ⚠️ **Problem with SOAP:** XML is verbose, heavy, and slow. Every piece of data carries a lot of extra tags!

---

### 🚀 The REST Revolution

**REST (Representational State Transfer)** emerged as a simpler, lighter alternative:

```
┌─────────────────┐                              ┌─────────────────┐
│   Any Client    │         HTTP + JSON          │   Web API       │
│ (Mobile/Web/IoT)│  ◀─────────────────────────▶ │   Server        │
└─────────────────┘                              └─────────────────┘
```

**Why REST Won:**
| Feature | SOAP | REST |
|---------|------|------|
| Data Format | XML only | JSON (mainly), XML, others |
| Weight | Heavy | Lightweight |
| Speed | Slower | Faster |
| Complexity | Complex | Simple |
| Readability | Difficult | Easy |

**JSON Example:**

```json
{
  "id": 1,
  "studentName": "Kartik",
  "email": "Kartik123@gmail.com"
}
```

---

## 3. What is Web API?

### 🏗️ Traditional Web Application Architecture

```
┌────────────────────────────────────────────────────────────┐
│                    TRADITIONAL WEB APP                      │
│                                                             │
│   ┌─────────┐     ┌─────────┐     ┌─────────┐     ┌──────┐ │
│   │   UI    │────▶│   BLL   │────▶│   DAL   │────▶│  DB  │ │
│   │ (Views) │◀────│(Business│◀────│ (Data   │◀────│      │ │
│   │         │     │ Logic)  │     │ Access) │     │      │ │
│   └─────────┘     └─────────┘     └─────────┘     └──────┘ │
│        │                                                    │
│        ▼                                                    │
│   ┌─────────────────┐                                      │
│   │   HTML Page     │  ◀─── Rendered to Web Browser        │
│   └─────────────────┘                                      │
└────────────────────────────────────────────────────────────┘
```

**Components:**

- **UI (User Interface):** Razor Views, HTML pages
- **BLL (Business Logic Layer):** Rules, calculations, validations
- **DAL (Data Access Layer):** Database operations
- **DB (Database):** Data storage

> 🔴 **Problem:** The UI is tightly coupled with the backend. If you want a mobile app, you'd need to rebuild everything!

---

### 🌐 Web API Architecture

```
                    ┌─────────────────────────────────────────┐
                    │              WEB API                     │
                    │                                          │
                    │   ┌─────────┐     ┌─────────┐     ┌────┐│
                    │   │   BLL   │────▶│   DAL   │────▶│ DB ││
                    │   │(Business│◀────│ (Data   │◀────│    ││
                    │   │ Logic)  │     │ Access) │     │    ││
                    │   └────┬────┘     └─────────┘     └────┘│
                    │        │                                 │
                    └────────┼─────────────────────────────────┘
                             │
              ┌──────────────┼──────────────┐
              │              │              │
              ▼              ▼              ▼
        ┌──────────┐   ┌──────────┐   ┌──────────┐
        │  React   │   │  Mobile  │   │  Angular │
        │   App    │   │   App    │   │   App    │
        └──────────┘   └──────────┘   └──────────┘
```

**Key Difference:** Web API exposes only **BLL** and **DAL** – no UI! This allows multiple frontends to consume the same backend.

---

## 4. Why Web API?

### 🤔 The Problem with Traditional Apps

```
Traditional App:
┌──────────────────────────────────────────┐
│  Web Browser  ◀──────▶  ASP.NET MVC App  │
└──────────────────────────────────────────┘
        ❌ Can't reuse for mobile!
        ❌ Can't reuse for desktop!
        ❌ Tightly coupled UI
```

### ✅ The Web API Solution

```
Web API Approach:
┌──────────────────┐
│   Web Browser    │────┐
└──────────────────┘    │
                        │
┌──────────────────┐    │      ┌────────────────┐
│   Mobile App     │────┼─────▶│    WEB API     │
└──────────────────┘    │      │   (Backend)    │
                        │      └────────────────┘
┌──────────────────┐    │
│   Desktop App    │────┘
└──────────────────┘
```

### 💡 Benefits of Web API:

| Benefit                | Description                              |
| ---------------------- | ---------------------------------------- |
| **Reusability**        | One backend, multiple frontends          |
| **Flexibility**        | Change UI without touching backend       |
| **Scalability**        | Scale frontend and backend independently |
| **Technology Freedom** | Use React, Angular, Flutter – anything!  |
| **Mobile Ready**       | Same API for web and mobile apps         |

---

## 5. Web API Request & Response

### 📤 HTTP Request Structure

Every API request has three main parts:

```
┌────────────────────────────────────────────────────────┐
│                     HTTP REQUEST                        │
├────────────────────────────────────────────────────────┤
│  VERB      │  GET, POST, PUT, DELETE                   │
├────────────────────────────────────────────────────────┤
│  HEADERS   │  Content-Type: application/json           │
│            │  Authorization: Bearer token123           │
│            │  Accept: application/json                 │
├────────────────────────────────────────────────────────┤
│  CONTENT   │  { "name": "Kartik", "email": "..." }    │
│  (Body)    │                                           │
└────────────────────────────────────────────────────────┘
```

---

### 📥 HTTP Response Structure

```
┌────────────────────────────────────────────────────────┐
│                     HTTP RESPONSE                       │
├────────────────────────────────────────────────────────┤
│  STATUS    │  200 OK, 404 Not Found, 500 Error         │
├────────────────────────────────────────────────────────┤
│  HEADERS   │  Content-Type: application/json           │
│            │  Cache-Control: no-cache                  │
├────────────────────────────────────────────────────────┤
│  CONTENT   │  { "id": 1, "studentName": "Kartik" }    │
│  (Body)    │                                           │
└────────────────────────────────────────────────────────┘
```

### 📊 Common HTTP Status Codes

| Code  | Meaning               | Usage                         |
| ----- | --------------------- | ----------------------------- |
| `200` | OK                    | Request successful            |
| `201` | Created               | Resource created successfully |
| `400` | Bad Request           | Invalid request data          |
| `404` | Not Found             | Resource doesn't exist        |
| `500` | Internal Server Error | Server-side error             |

---

## 6. HTTP Verbs

HTTP verbs (methods) define what action to perform on a resource:

### 🔵 GET – Retrieve Data

```http
GET /api/student/all
```

> Fetches all students or a specific student. **Safe & Idempotent.**

---

### 🟢 POST – Create Data

```http
POST /api/student
Content-Type: application/json

{
    "studentName": "New Student",
    "email": "new@email.com"
}
```

> Creates a new resource. **Not idempotent.**

---

### 🟡 PUT – Update Data

```http
PUT /api/student/1
Content-Type: application/json

{
    "id": 1,
    "studentName": "Updated Name"
}
```

> Updates an existing resource completely. **Idempotent.**

---

### 🔴 DELETE – Remove Data

```http
DELETE /api/student/1
```

> Removes a resource. **Idempotent.**

---

### 📋 HTTP Verbs Summary

| Verb   | Action | Safe?  | Idempotent? |
| ------ | ------ | ------ | ----------- |
| GET    | Read   | ✅ Yes | ✅ Yes      |
| POST   | Create | ❌ No  | ❌ No       |
| PUT    | Update | ❌ No  | ✅ Yes      |
| DELETE | Delete | ❌ No  | ✅ Yes      |

---

## 7. Creating Your First Endpoint

Let's look at a real implementation from this project!

### 📁 Project Structure

```
CollegeApp/
├── Controllers/
│   └── StudentController.cs    ◀── API Endpoints
├── Model/
│   ├── Student.cs              ◀── Data Model
│   └── CollegeRepository.cs    ◀── Data Storage
└── Program.cs
```

---

### 📦 Student Model

```csharp
namespace CollegeApp.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
```

---

### 💾 College Repository (Data Layer)

```csharp
namespace CollegeApp.Model
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>
        {
            new Student
            {
                Id = 1,
                StudentName = "Kartik",
                Email = "Kartik123@gmail.com",
                Address = "Hyd, India"
            },
            new Student
            {
                Id = 2,
                StudentName = "Aryan",
                Email = "Aryan123@gmail.com",
                Address = "Banglore, India"
            }
        };
    }
}
```

---

### 🎮 Student Controller (API Endpoints)

```csharp
using CollegeApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/student/all
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(CollegeRepository.Students);
        }

        // GET: api/student/{id}
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public ActionResult<Student> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students
                .Where(s => s.Id == id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {id} not found!.");

            return Ok(student);
        }

        // GET: api/student/{name}
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students
                .Where(s => s.StudentName == name).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with name {name} not found!.");

            return Ok(student);
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}", Name = "DeleteStudentById")]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students
                .Where(s => s.Id == id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {id} not found!.");

            CollegeRepository.Students.Remove(student);
            return Ok(true);
        }
    }
}
```

---

### 🧪 Testing Your Endpoints

| Endpoint                  | Method | Description          |
| ------------------------- | ------ | -------------------- |
| `GET /api/student/all`    | GET    | Get all students     |
| `GET /api/student/1`      | GET    | Get student by ID    |
| `GET /api/student/Kartik` | GET    | Get student by name  |
| `DELETE /api/student/1`   | DELETE | Delete student by ID |

---

### 🎯 Key Takeaways

1. **`[ApiController]`** – Marks the class as a Web API controller
2. **`[Route("api/[controller]")]`** – Defines the base URL route
3. **`[HttpGet]`, `[HttpPost]`, etc.** – Maps methods to HTTP verbs
4. **`ActionResult<T>`** – Allows returning data with HTTP status codes
5. **Response Helpers:**
   - `Ok()` – Returns 200 with data
   - `BadRequest()` – Returns 400
   - `NotFound()` – Returns 404

---

## 8. Why and How to Use DTOs

### 🤔 What is a DTO?

**DTO (Data Transfer Object)** is a design pattern used to transfer data between layers of an application. It's a simple object that carries data without any business logic.

---

### ❌ The Problem: Returning Database Entities Directly

When working with an **in-memory repository** or any database, you might be tempted to return database entities directly:

```csharp
// ❌ BAD PRACTICE: Returning database entity directly
public ActionResult<Student> GetStudentById(int id)
{
    var student = CollegeRepository.Students.Find(s => s.Id == id);
    return Ok(student);  // Exposing database entity!
}
```

> ⚠️ **Why is this bad?**
>
> - Exposes internal database structure to clients
> - Can leak sensitive fields (passwords, internal IDs)
> - Tightly couples API responses to database schema
> - Hard to add calculated fields or transform data

---

### ✅ The Solution: Use DTOs

```
┌─────────────────────────────────────────────────────────────────────────┐
│                        DATA FLOW WITH DTOs                               │
│                                                                          │
│   ┌────────┐      ┌─────────┐      ┌─────────┐      ┌──────────────┐   │
│   │   DB   │─────▶│   DAL   │─────▶│   BLL   │─────▶│  Controller  │   │
│   │        │      │         │      │         │      │              │   │
│   │ Student│      │ Student │      │ Student │      │  StudentDTO  │   │
│   │ Entity │      │ Entity  │      │ → DTO   │      │  (Response)  │   │
│   └────────┘      └─────────┘      └─────────┘      └──────────────┘   │
│                                         │                    │          │
│                                         ▼                    ▼          │
│                                  ┌─────────────────────────────────┐   │
│                                  │ Transform + Add Calculated Data  │   │
│                                  └─────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────────┘
```

---

### 🛒 Real-World Example: Shopping Cart

Imagine a shopping cart system:

**Step 1: DAL Layer reads from Database**

```csharp
// Database Entity (what's stored in DB)
public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
```

**Step 2: BLL Layer calculates and creates DTO**

```csharp
// DTO (what's sent to UI)
public class OrderItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // ✅ Calculated fields added by BLL
    public decimal TotalPrice { get; set; }      // Quantity × UnitPrice
    public decimal TaxAmount { get; set; }       // Calculated tax
    public decimal TaxPercentage { get; set; }   // Tax %
}
```

**Step 3: Controller returns DTO to UI**

```csharp
public ActionResult<OrderItemDTO> GetOrderItem(int id)
{
    var item = _repository.GetById(id);  // Get entity from DB

    var dto = new OrderItemDTO
    {
        Id = item.Id,
        Name = item.Name,
        Quantity = item.Quantity,
        UnitPrice = item.UnitPrice,
        TotalPrice = item.Quantity * item.UnitPrice,
        TaxAmount = (item.Quantity * item.UnitPrice) * 0.18m,
        TaxPercentage = 18
    };

    return Ok(dto);  // ✅ Return DTO, not entity!
}
```

---

### 📁 Updated Project Structure

```
CollegeApp/
├── Controllers/
│   └── StudentController.cs    ◀── Returns StudentDTO
├── Model/
│   ├── Student.cs              ◀── Database Entity
│   ├── StudentDTO.cs           ◀── Data Transfer Object (NEW!)
│   └── CollegeRepository.cs    ◀── Data Storage
└── Program.cs
```

---

### 📦 StudentDTO Model

```csharp
namespace CollegeApp.Model
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
```

---

### � Updated Student Controller (Using DTOs)

```csharp
using CollegeApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/student/all
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            // Using LINQ to transform Entity → DTO
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address
            });

            return Ok(students);
        }

        // GET: api/student/{id}
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students
                .Where(s => s.Id == id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {id} not found!.");

            // Create DTO from entity
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };

            return Ok(studentDTO);
        }
    }
}
```

---

### 💡 Benefits of Using DTOs

| Benefit             | Description                                           |
| ------------------- | ----------------------------------------------------- |
| **Security**        | Hide sensitive database fields from API responses     |
| **Flexibility**     | Shape data for specific UI needs                      |
| **Decoupling**      | API responses independent of database schema          |
| **Calculated Data** | Add computed fields (totals, percentages) in BLL      |
| **Versioning**      | Maintain different DTO versions for API compatibility |

---

### 🎯 Key Takeaways for DTOs

1. **Never return database entities directly** – Always use DTOs
2. **Create DTOs in the Model folder** – Keep them separate from entities
3. **Transform in BLL/Controller** – Convert Entity → DTO before returning
4. **Add calculated fields** – DTOs can include computed values
5. **Use LINQ for transformation** – Efficient way to map collections

---

## 🎉 Conclusion

You've learned:

- ✅ How Web APIs evolved from SOAP to REST
- ✅ The difference between traditional apps and Web APIs
- ✅ Why Web APIs are essential for modern development
- ✅ HTTP request/response structure
- ✅ HTTP verbs and their purposes
- ✅ How to create API endpoints in ASP.NET Core
- ✅ Why and how to use DTOs in Web APIs

**Happy Coding!** 🚀

---

## 📚 Resources

- [ASP.NET Core Web API Documentation](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [REST API Best Practices](https://restfulapi.net/)
- [HTTP Status Codes](https://httpstatuses.com/)
