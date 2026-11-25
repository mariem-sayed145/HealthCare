# Health Care System Web API

A Web API for managing a simple Health Care System. The API supports CRUD operations for patients, doctors, and appointments.

## Technologies

- ASP.NET Core Web API
- Entity Framework Core (Code First)
- SQL Server
- C# `async` / `await`
- Swagger for API testing

## Data Models

### Patient
- `Id`
- `Name`
- `DateOfBirth`

### Doctor
- `Id`
- `Name`
- `Specialization`

### Appointment
- `Id`
- `PatientId`
- `DoctorId`
- `AppointmentDate`

A patient can have multiple appointments, and a doctor can have multiple appointments. Each appointment is associated with one patient and one doctor.

## Implemented Tasks

###  Web API Setup
- Created the ASP.NET Core Web API project.
- Configured Entity Framework Core to use SQL Server.
- Created the Patient, Doctor, and Appointment models.
- Configured the database relationships using Entity Framework Core.
- Used the Code First approach to create and update the database.

### Task 1,2 — CRUD Operations
Implemented Create, Read, Update, and Delete operations for:

- Patients
- Doctors
- Appointments

The API uses asynchronous methods with `async` and `await` for database operations.

## API Endpoints

### Patients

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Patients` | Get all patients |
| GET | `/api/Patients/{id}` | Get a patient by ID |
| POST | `/api/Patients` | Create a patient |
| PUT | `/api/Patients/{id}` | Update a patient |
| DELETE | `/api/Patients/{id}` | Delete a patient |

### Doctors

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Doctors` | Get all doctors |
| GET | `/api/Doctors/{id}` | Get a doctor by ID |
| POST | `/api/Doctors` | Create a doctor |
| PUT | `/api/Doctors/{id}` | Update a doctor |
| DELETE | `/api/Doctors/{id}` | Delete a doctor |

### Appointments

| Method | Endpoint | Description |
|---|---|---|
| GET | `/api/Appointments` | Get all appointments |
| GET | `/api/Appointments/{id}` | Get an appointment by ID |
| POST | `/api/Appointments` | Create an appointment |
| PUT | `/api/Appointments/{id}` | Update an appointment |
| DELETE | `/api/Appointments/{id}` | Delete an appointment |

## Setup and Run

1. Clone or download the project.
2. Open the solution in Visual Studio.
3. Update the SQL Server connection string in `appsettings.json` to match your local SQL Server instance.
4. Open the Package Manager Console and run:

   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

5. Run the project from Visual Studio.
6. Use the Swagger UI page to test the API endpoints.

## Example Connection String

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=HealthCareDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

If your SQL Server instance is `SQLEXPRESS`, update the server value accordingly.

## Notes

- The database is created and managed using Entity Framework Core Code First migrations.
- CRUD database operations use asynchronous methods.
- Appointment creation validates that the referenced patient and doctor exist.
