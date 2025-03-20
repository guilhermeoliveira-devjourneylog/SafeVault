# SafeVault Application

# 1. Project Objective

The SafeVault is a secure web application designed to manage sensitive data, including user credentials and financial records. The main objective of the project is to ensure security against attacks, such as SQL Injection and XSS, by following best practices for secure coding.

# 2. Requirements

# ✅ Functional Requirements

Input validation in the form to prevent malicious injections.

Use of parameterized queries to prevent SQL Injection.

Implementation of unit tests to validate code security.

Secure data storage in a MySQL database.

Compatibility with .NET 8 and Entity Framework Core.

# Non-Functional Requirements

The system must be fast and efficient in data validation.

It must be scalable to support future authentication and authorization implementations.

Adoption of best practices in development and code organization.

# 3. Design (Specification and Architecture)

## 📌 Application Structure

The application was built using:

Front-end: HTML for the input form.

Back-end: ASP.NET Core MVC.

Database: MySQL (using Pomelo.EntityFrameworkCore.MySql).

Testing: NUnit to validate SQL Injection and XSS.

```plaintext
[Frontend] -> [Controllers] -> [Service Layer] -> [Database]
```