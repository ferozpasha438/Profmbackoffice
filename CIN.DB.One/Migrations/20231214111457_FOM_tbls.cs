using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CIN.DB.One.Migrations
{
    public partial class FOM_tbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adjs",
                columns: table => new
                {
                    PositiveSum = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NegativeSum = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjs", x => x.PositiveSum);
                });

            migrationBuilder.CreateTable(
                name: "HRM_DEF_EmployeeShift",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    MondayShiftId = table.Column<short>(type: "smallint", nullable: true),
                    TuesdayShiftId = table.Column<short>(type: "smallint", nullable: true),
                    WednesdayShiftId = table.Column<short>(type: "smallint", nullable: true),
                    ThursdayShiftId = table.Column<short>(type: "smallint", nullable: true),
                    FridayShiftId = table.Column<short>(type: "smallint", nullable: true),
                    SaturdayShiftId = table.Column<short>(type: "smallint", nullable: true),
                    SundayShiftId = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRM_DEF_EmployeeShift", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HRM_DEF_PayrollGroup",
                columns: table => new
                {
                    PayrollGroupID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayrollGroupName_EN = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PayrollGroupName_AR = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CountryID = table.Column<long>(type: "bigint", nullable: true),
                    CompanyID = table.Column<long>(type: "bigint", nullable: true),
                    SiteID = table.Column<long>(type: "bigint", nullable: true),
                    ProjectId = table.Column<long>(type: "bigint", nullable: true),
                    BusinessUnitID = table.Column<long>(type: "bigint", nullable: true),
                    DivisionID = table.Column<long>(type: "bigint", nullable: true),
                    BranchID = table.Column<long>(type: "bigint", nullable: true),
                    DepartmentID = table.Column<long>(type: "bigint", nullable: true),
                    IsForAllEmployee = table.Column<bool>(type: "bit", nullable: true),
                    IsForFutureEmployee = table.Column<bool>(type: "bit", nullable: true),
                    StartPayRollDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrentPayRollMonth = table.Column<short>(type: "smallint", nullable: true),
                    EndPayRollDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrentPayRollYear = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRM_DEF_PayrollGroup", x => x.PayrollGroupID);
                });

            migrationBuilder.CreateTable(
                name: "OP_HRM_TEMP_Project",
                columns: table => new
                {
                    ProjectCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectNameEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProjectNameArb = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsResourcesAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsMaterialAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsLogisticsAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsShiftsAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsExpenceOverheadsAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsEstimationCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsSkillSetsMapped = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertedToProposal = table.Column<bool>(type: "bit", nullable: false),
                    IsConvrtedToContract = table.Column<bool>(type: "bit", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OP_HRM_TEMP_Project", x => x.ProjectCode);
                });

            migrationBuilder.CreateTable(
                name: "tblAssignDrivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotesForDriver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAssignDrivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblAssignRoutes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoutePlan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAssignRoutes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolExaminationManagementDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamHeaderId = table.Column<int>(type: "int", nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartingDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolExaminationManagementDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolExaminationManagementHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GradeCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TypeOfExaminationCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreparedBy = table.Column<int>(type: "int", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    DateOfCompletion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsResultDeclared = table.Column<bool>(type: "bit", nullable: false),
                    DateOfResult = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolExaminationManagementHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolStudentMaster",
                columns: table => new
                {
                    StuRegNum = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StuRegDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StuAdmDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StuName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StuName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    GradeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeSectionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LangCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    GenderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PTGroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StuIDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NatCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReligionCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherToungue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeStructCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportationRequired = table.Column<bool>(type: "bit", nullable: false),
                    PickNDropZone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportationFee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VehicleTransport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image1Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image2Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdmissionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortListDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortListedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofAdmission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StuConvDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StuConvBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BloodGroup = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Weight = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PhysicalDisability = table.Column<bool>(type: "bit", nullable: false),
                    PhysicalDisabilityNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WearSpects = table.Column<bool>(type: "bit", nullable: false),
                    SpecialAssistance = table.Column<bool>(type: "bit", nullable: false),
                    SpecialAssistanceNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicsScale = table.Column<int>(type: "int", nullable: false),
                    AttentivenessScale = table.Column<int>(type: "int", nullable: false),
                    HomeWorkScale = table.Column<int>(type: "int", nullable: false),
                    ProjectWorkScale = table.Column<int>(type: "int", nullable: false),
                    SportsPhysicalScale = table.Column<int>(type: "int", nullable: false),
                    DiciplineAttitude = table.Column<int>(type: "int", nullable: false),
                    SignatureImage1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SignatureImage2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxApplicable = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolStudentMaster", x => x.StuRegNum);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolStudentResultDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentResultHeaderId = table.Column<int>(type: "int", nullable: false),
                    StudentAdmNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubCodes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaximumMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QualifiyingMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MarksObtained = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QualifiyingGrade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolStudentResultDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolStudentResultHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamId = table.Column<int>(type: "int", nullable: false),
                    GradeCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsRelease = table.Column<bool>(type: "bit", nullable: false),
                    ReleasedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResultDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolStudentResultHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolSubjectExamsGrade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubjectCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MaximumMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    QualifiyingGrade = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolSubjectExamsGrade", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolTeacherClassMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GradeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsMapped = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolTeacherClassMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolTeacherLanguages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LanguageCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Read = table.Column<int>(type: "int", nullable: false),
                    Write = table.Column<int>(type: "int", nullable: false),
                    Speak = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolTeacherLanguages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolTeacherMaster",
                columns: table => new
                {
                    TeacherCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherShortName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TeacherName1 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    TeacherName2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    MaritalStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Pcity = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PPhone1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PMobile1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Saddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SPhone2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SMobile2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NationalityCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NationalityID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Passport = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HiringType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TotalExperience = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    HighestQualification = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TechnologyCompetence = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ComminicationSkills = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TeachingSkills = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Subjectknowledge = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    AdministrativeSkills = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DisciplineSkills = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThumbNailImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FullImageParh = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PrimaryBranchCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SysLoginId = table.Column<int>(type: "int", nullable: false),
                    AboutTeacher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolTeacherMaster", x => x.TeacherCode);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolTeacherQualification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Institute = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolTeacherQualification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefSchoolTeacherSubjectsMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GradeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubjectCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeachingSkillLevel = table.Column<int>(type: "int", nullable: false),
                    AdminSkillLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefSchoolTeacherSubjectsMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentAcademics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExamName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Result = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassPercent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentAcademics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentAddress",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAddress1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentAddress", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentAttendance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AtnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtnTimeIn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtnTimeOut = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AtnFlag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLeave = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaveCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicYear = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentAttendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentFeeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeStructCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxDiscPer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscPer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetDiscAmt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsLateFee = table.Column<bool>(type: "bit", nullable: false),
                    IsAddedManaully = table.Column<bool>(type: "bit", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    VoidedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoidReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentFeeDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentFeeHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeStructCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PaidTransNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidRemarks1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidRemarks2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JvNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    IsCompletelyPaid = table.Column<bool>(type: "bit", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentFeeHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentGuardiansSiblings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deisgnation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentGuardiansSiblings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentNotices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PosNeg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoticeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReasonCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionItems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionRemarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    AprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsClosed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionTaken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClosedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentNotices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentNoticesReasonCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReasonCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonName1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReasonName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequireAction = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentNoticesReasonCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDefStudentPrevEducation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameOfInstitute = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClassStudied = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageMedium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassPercentage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearofPassing = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDefStudentPrevEducation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblDriverMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DriverName_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IqamaNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Validity = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDriverMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomClientCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientCatCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomClientCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomClientMaster",
                columns: table => new
                {
                    ClientCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientShortName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Area = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondaryAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternameMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AlternateEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandLine1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LandLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VATNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CRNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeOfBusiness = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumOfEmp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LogoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoLocLat = table.Column<float>(type: "real", nullable: false),
                    GeoLocLan = table.Column<float>(type: "real", nullable: false),
                    GeoLocGain = table.Column<float>(type: "real", nullable: false),
                    InActiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LoginPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomClientMaster", x => x.ClientCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomCustomerContract",
                columns: table => new
                {
                    ContractCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustSiteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustContNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContDeptCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContProjManager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContProjSupervisor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContApprAuthorities = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAppreoved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomCustomerContract", x => x.ContractCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomDepartment",
                columns: table => new
                {
                    DeptCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptServType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomDepartment", x => x.DeptCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomDepartmentTypes",
                columns: table => new
                {
                    ServiceTypeCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceTypeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceTypeName_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomDepartmentTypes", x => x.ServiceTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomSubContractor",
                columns: table => new
                {
                    SubContCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptCodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NameArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesgContactPerson1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson1Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DesgContactPerson2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPerson2Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomSubContractor", x => x.SubContCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpFomUserClientLoginMapping",
                columns: table => new
                {
                    UserClientLoginCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    RegMobile = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    LoginType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    LastLoginDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpFomUserClientLoginMapping", x => x.UserClientLoginCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysAcCodeSegment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodeType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Segment = table.Column<short>(type: "smallint", nullable: false),
                    Len = table.Column<short>(type: "smallint", nullable: false),
                    Start = table.Column<short>(type: "smallint", nullable: false),
                    End = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysAcCodeSegment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CompanyAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CompanyAddressAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    VATNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DateFormat = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    GeoLocLatitude = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GeoLocLongitude = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    LogoURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PriceDecimal = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    QuantityDecimal = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Website = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LogoImagePath = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CrNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CcNumber = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysCompany", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysCountryCode",
                columns: table => new
                {
                    CountryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysCountryCode", x => x.CountryCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysFileUpload",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SourceId = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UploadedBy = table.Column<string>(type: "nvarchar(124)", maxLength: 124, nullable: true),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysFileUpload", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysIncidentReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    SiteGeoLatitude = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SiteGeoLongitude = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysIncidentReport", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysMenuOption",
                columns: table => new
                {
                    MenuCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Level1 = table.Column<short>(type: "smallint", nullable: false),
                    Level2 = table.Column<short>(type: "smallint", nullable: false),
                    Level3 = table.Column<short>(type: "smallint", nullable: false),
                    MenuNameEng = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    MenuNameArb = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    IsForm = table.Column<bool>(type: "bit", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysMenuOption", x => x.MenuCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysSystemTaxes",
                columns: table => new
                {
                    TaxCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TaxName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsInterState = table.Column<bool>(type: "bit", nullable: false),
                    TaxComponent01 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Taxper01 = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    InputAcCode01 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OutputAcCode01 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxComponent02 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Taxper02 = table.Column<decimal>(type: "decimal(6,3)", nullable: true),
                    InputAcCode02 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OutputAcCode02 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxComponent03 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Taxper03 = table.Column<decimal>(type: "decimal(6,3)", nullable: true),
                    InputAcCode03 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OutputAcCode03 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxComponent04 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Taxper04 = table.Column<decimal>(type: "decimal(6,3)", nullable: true),
                    InputAcCode04 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OutputAcCode04 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxComponent05 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Taxper05 = table.Column<decimal>(type: "decimal(6,3)", nullable: true),
                    InputAcCode05 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OutputAcCode05 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysSystemTaxes", x => x.TaxCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysTransactionCodes",
                columns: table => new
                {
                    TransactionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysTransactionCodes", x => x.TransactionCode);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysUserSiteMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysUserSiteMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysUserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UerType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysUserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysZoneSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameAR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysZoneSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefMainAccounts",
                columns: table => new
                {
                    FinAcCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FinAcName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FinAcDesc = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FinAcAlias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FinIsPayCode = table.Column<bool>(type: "bit", nullable: false),
                    FinPayCodetype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FinIsIntegrationAC = table.Column<bool>(type: "bit", nullable: false),
                    Fintype = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FinCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FinSubCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FinIsRevenue = table.Column<bool>(type: "bit", nullable: false),
                    FinIsRevenuetype = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    FinActLastSeq = table.Column<short>(type: "smallint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefMainAccounts", x => x.FinAcCode);
                });

            migrationBuilder.CreateTable(
                name: "tblFinSysAccountType",
                columns: table => new
                {
                    TypeCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TypeBal = table.Column<string>(type: "nchar(2)", maxLength: 2, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinSysAccountType", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "tblFinSysBatchSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatchCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BatchName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BatchName2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinSysBatchSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFinSysCostAllocationSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CostCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CostName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CostName2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinSysCostAllocationSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFinSysFinanialSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FYOpenDate = table.Column<DateTime>(type: "date", nullable: false),
                    FYClosingDate = table.Column<DateTime>(type: "date", nullable: false),
                    FYYear = table.Column<short>(type: "smallint", nullable: false),
                    FinAcCatLen = table.Column<short>(type: "smallint", nullable: false),
                    FinAcSubCatLen = table.Column<short>(type: "smallint", nullable: false),
                    FinAcLen = table.Column<short>(type: "smallint", nullable: false),
                    FinBranchPrefixLen = table.Column<short>(type: "smallint", nullable: false),
                    FinAcFormat = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FinAllowNextYearTran = table.Column<bool>(type: "bit", nullable: false),
                    FinTranDateAsPostDate = table.Column<bool>(type: "bit", nullable: false),
                    FInSysGenAcCode = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    PaymentMethod = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    NumOfSeg = table.Column<short>(type: "smallint", nullable: false),
                    UserCostSeg = table.Column<bool>(type: "bit", nullable: false),
                    MinCutOffShortAmt = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    MaxCutOffOverAmr = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    ArDistFlag = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinSysFinanialSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFinSysSegmentSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seg2Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Seg2Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Seg2Name2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinSysSegmentSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFinSysSegmentTwoSetup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seg2Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Seg2Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Seg2Name2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinSysSegmentTwoSetup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCustomerWallet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AdvAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PostedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCustomerWallet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnOverShortAmount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AmtType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnOverShortAmount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnTrialBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AcDesc = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Balance = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrBalance = table.Column<decimal>(type: "decimal(18,3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnTrialBalance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblHRMSysPosition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PositionNameEn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PositionNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblHRMSysPosition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblIMAdjustmentsTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranBarcode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TranItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemName2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemQty = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    TranItemUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranUOMFactor = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    TranItemCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    ItemAttribute1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ItemAttribute2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVADJAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMAdjustmentsTransactionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblIMAdjustmentsTransactionHeader",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranTotalCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotItems = table.Column<int>(type: "int", nullable: false),
                    TranCreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranCreateUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLastEditDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLastEditUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLockStat = table.Column<short>(type: "smallint", nullable: false),
                    TranLockUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranPostStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranPostDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranpostUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranVoidStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranVoidUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranvoidDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAdjAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JVNum = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TranBranch = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMAdjustmentsTransactionHeader", x => x.TranNumber);
                });

            migrationBuilder.CreateTable(
                name: "tblIMReceiptsTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranBarcode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TranItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemName2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemQty = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    TranItemUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranUOMFactor = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    TranItemCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    ItemAttribute1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ItemAttribute2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVADJAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMReceiptsTransactionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblIMReceiptsTransactionHeader",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranTotalCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotItems = table.Column<int>(type: "int", nullable: false),
                    TranCreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranCreateUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLastEditDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLastEditUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLockStat = table.Column<short>(type: "smallint", nullable: false),
                    TranLockUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranPostStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranPostDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranpostUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranVoidStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranVoidUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranvoidDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAdjAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JVNum = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TranBranch = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMReceiptsTransactionHeader", x => x.TranNumber);
                });

            migrationBuilder.CreateTable(
                name: "tblIMStockReconciliationTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranBarcode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TranItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemName2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemQty = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    TranItemUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranUOMFactor = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    TranItemCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    ItemAttribute1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ItemAttribute2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVADJAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMStockReconciliationTransactionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblIMStockReconciliationTransactionHeader",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranTotalCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotItems = table.Column<int>(type: "int", nullable: false),
                    TranCreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranCreateUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLastEditDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLastEditUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLockStat = table.Column<short>(type: "smallint", nullable: false),
                    TranLockUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranPostStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranPostDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranpostUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranVoidStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranVoidUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranvoidDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAdjAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JVNum = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMStockReconciliationTransactionHeader", x => x.TranNumber);
                });

            migrationBuilder.CreateTable(
                name: "tblIMTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranBarcode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TranItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemName2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemQty = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    TranItemUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranUOMFactor = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    TranItemCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    ItemAttribute1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ItemAttribute2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVADJAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMTransactionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblIMTransactionHeader",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranTotalCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotItems = table.Column<int>(type: "int", nullable: false),
                    TranCreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranCreateUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLastEditDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLastEditUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLockStat = table.Column<short>(type: "smallint", nullable: false),
                    TranLockUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranPostStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranPostDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranpostUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranVoidStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranVoidUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranvoidDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAdjAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JVNum = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    TranBranch = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMTransactionHeader", x => x.TranNumber);
                });

            migrationBuilder.CreateTable(
                name: "tblIMTransferTransactionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranBarcode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TranItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemName2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TranItemQty = table.Column<decimal>(type: "decimal(8,3)", nullable: false),
                    TranItemUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranUOMFactor = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    TranItemCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    ItemAttribute1 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ItemAttribute2 = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    INVADJAcc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMTransferTransactionDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefCategory",
                columns: table => new
                {
                    ItemCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ItemCatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemCatDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ItemCatPrefix = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefCategory", x => x.ItemCatCode);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefClass",
                columns: table => new
                {
                    ItemClassCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ItemClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemClassDesce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefClass", x => x.ItemClassCode);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefPurchaseConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoGenCustCode = table.Column<bool>(type: "bit", nullable: false),
                    PrefixCatCode = table.Column<bool>(type: "bit", nullable: false),
                    NewCustIndicator = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VendLength = table.Column<short>(type: "smallint", nullable: false),
                    CategoryLength = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefPurchaseConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefSalesConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutoGenCustCode = table.Column<bool>(type: "bit", nullable: false),
                    PrefixCatCode = table.Column<bool>(type: "bit", nullable: false),
                    NewCustIndicator = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CustLength = table.Column<short>(type: "smallint", nullable: false),
                    CategoryLength = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefSalesConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefSubClass",
                columns: table => new
                {
                    ItemSubClassCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ItemSubClassName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ItemSubClassDesce = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefSubClass", x => x.ItemSubClassCode);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefTracking",
                columns: table => new
                {
                    TrCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TypeCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TrName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefTracking", x => x.TrCode);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefType",
                columns: table => new
                {
                    TypeCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefType", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefUOM",
                columns: table => new
                {
                    UOMCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UOMName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UOMDesc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefUOM", x => x.UOMCode);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefWarehouseTest",
                columns: table => new
                {
                    WHCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    WHName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WHDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WHAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WHCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WHState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WHIncharge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WHBranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvDistGroup = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WhAllowDirectPur = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefWarehouseTest", x => x.WHCode);
                });

            migrationBuilder.CreateTable(
                name: "tblLessonPlanDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonPlanCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GradeCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SectionCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SubCodes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Chapter = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LessonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumOfSessions = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    EstStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EstDays = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EstHrs = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ActStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ActHrs = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Topics = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topics2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignTeacherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ActualTecherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLessonPlanDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblLessonPlanHeader",
                columns: table => new
                {
                    LessonPlanCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeacherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GradeCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SectionCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SubCodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EstEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumOfLessons = table.Column<int>(type: "int", nullable: false),
                    NumOfDays = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLessonPlanHeader", x => x.LessonPlanCode);
                });

            migrationBuilder.CreateTable(
                name: "tblOpAuthorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppAuth = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AppLevel = table.Column<short>(type: "smallint", nullable: false),
                    CanApproveEnquiry = table.Column<bool>(type: "bit", nullable: false),
                    CanAddSurveyorToEnquiry = table.Column<bool>(type: "bit", nullable: false),
                    CanApproveSurvey = table.Column<bool>(type: "bit", nullable: false),
                    CanApproveEstimation = table.Column<bool>(type: "bit", nullable: false),
                    CanApproveProposal = table.Column<bool>(type: "bit", nullable: false),
                    CanApproveContract = table.Column<bool>(type: "bit", nullable: false),
                    CanModifyEstimation = table.Column<bool>(type: "bit", nullable: false),
                    CanConvertEnqToProject = table.Column<bool>(type: "bit", nullable: false),
                    CanConvertEstimationToProposal = table.Column<bool>(type: "bit", nullable: false),
                    CanConvertProposalToContract = table.Column<bool>(type: "bit", nullable: false),
                    CanCreateRoaster = table.Column<bool>(type: "bit", nullable: false),
                    CanEditRoaster = table.Column<bool>(type: "bit", nullable: false),
                    IsFinalAuthority = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CanEditEnquiry = table.Column<bool>(type: "bit", nullable: false),
                    CanEditPvReq = table.Column<bool>(type: "bit", nullable: false),
                    CanApprovePvReq = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpContractClause",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClauseTitleEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseSubTitleEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseDescriptionEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseTitleArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseSubTitleArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseDescriptionArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberArb = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpContractClause", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpContractFormHead",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TitleOfServiceEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDetailsEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDetailsEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreambleEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPartyEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondPartyEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleOfServiceArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDetailsArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDetailsArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreambleArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPartyArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondPartyArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpContractFormHead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpContractTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleOfServiceEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDetailsEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDetailsEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreambleEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPartyEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondPartyEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleOfServiceArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyDetailsArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustomerDetailsArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PreambleArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstPartyArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondPartyArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsForProject = table.Column<bool>(type: "bit", nullable: true),
                    IsForAddingSite = table.Column<bool>(type: "bit", nullable: true),
                    IsForAddingResources = table.Column<bool>(type: "bit", nullable: true),
                    IsForRemovingResources = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpContractTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpContractTermsMapToProject",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ContractTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    isLiabilityAndInsurance = table.Column<bool>(type: "bit", nullable: true),
                    isTerminationClause = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpContractTermsMapToProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpEmployeeAttendance",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttnDate = table.Column<DateTime>(type: "date", nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ShiftCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    OutTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ShiftNumber = table.Column<short>(type: "smallint", nullable: true),
                    isDefaultEmployee = table.Column<bool>(type: "bit", nullable: false),
                    isPrimarySite = table.Column<bool>(type: "bit", nullable: false),
                    isDefShiftOff = table.Column<bool>(type: "bit", nullable: false),
                    isPosted = table.Column<bool>(type: "bit", nullable: false),
                    Attendance = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    AltEmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AltShiftCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RefIdForAlt = table.Column<long>(type: "bigint", nullable: true),
                    IsLate = table.Column<bool>(type: "bit", nullable: true),
                    IsLogoutFromShift = table.Column<bool>(type: "bit", nullable: true),
                    IsOnBreak = table.Column<bool>(type: "bit", nullable: true),
                    IsGeofenseOut = table.Column<bool>(type: "bit", nullable: true),
                    GeofenseOutCount = table.Column<short>(type: "smallint", nullable: true),
                    SkillsetCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpEmployeeAttendance", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpEmployeeLeaves",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AttnDate = table.Column<DateTime>(type: "date", nullable: true),
                    AL = table.Column<bool>(type: "bit", nullable: false),
                    EL = table.Column<bool>(type: "bit", nullable: false),
                    UL = table.Column<bool>(type: "bit", nullable: false),
                    SL = table.Column<bool>(type: "bit", nullable: false),
                    W = table.Column<bool>(type: "bit", nullable: false),
                    STL = table.Column<bool>(type: "bit", nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ShiftCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpEmployeeLeaves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpEmployeesToProjectSite",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmployeeNameAr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpEmployeesToProjectSite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpEmployeeToResourceMap",
                columns: table => new
                {
                    MapId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SkillSet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    isPrimarySite = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpEmployeeToResourceMap", x => x.MapId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpEmployeeTransResign",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AttnDate = table.Column<DateTime>(type: "date", nullable: true),
                    TR = table.Column<bool>(type: "bit", nullable: false),
                    R = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpEmployeeTransResign", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpLogisticsandvehicle",
                columns: table => new
                {
                    VehicleNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VehicleNameInEnglish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VehicleNameInArabic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DailyFuelCost = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    DailyMiscCost = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    EstimatedDailyMaintenanceCost = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    OtherDailyOperationCost = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    TotalDailyServiceCost = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    DailyServicePrice = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    ValueofVehicle = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Vehicletype = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MinMargin = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpLogisticsandvehicle", x => x.VehicleNumber);
                });

            migrationBuilder.CreateTable(
                name: "tblOpMaterialEquipment",
                columns: table => new
                {
                    Code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInArabic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EstimatedCostValue = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    IsDepreciationApplicable = table.Column<bool>(type: "bit", nullable: false),
                    MinimumCostPerUsage = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    DepreciationPerYear = table.Column<decimal>(type: "decimal(17,3)", nullable: true),
                    UsageLifetermsYear = table.Column<short>(type: "smallint", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpMaterialEquipment", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "tblOpMonthlyRoaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Month = table.Column<short>(type: "smallint", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    S1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S3 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S4 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S5 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S6 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S7 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S8 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S9 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S10 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S11 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S12 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S13 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S14 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S15 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S16 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S17 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S18 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S19 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S20 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S21 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S22 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S23 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S24 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S25 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S26 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S27 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S28 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S29 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S30 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S31 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpMonthlyRoaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpMonthlyRoasterForSite",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Month = table.Column<short>(type: "smallint", nullable: false),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    SkillsetCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SkillsetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    S1 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S2 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S3 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S4 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S5 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S6 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S7 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S8 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S9 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S10 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S11 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S12 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S13 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S14 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S15 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S16 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S17 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S18 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S19 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S20 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S21 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S22 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S23 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S24 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S25 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S26 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    S27 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S28 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S29 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S30 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    S31 = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MonthStartDate = table.Column<DateTime>(type: "date", nullable: true),
                    MonthEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    EmployeeID = table.Column<long>(type: "bigint", nullable: false),
                    MapId = table.Column<long>(type: "bigint", nullable: false),
                    IsPrimaryResource = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpMonthlyRoasterForSite", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpOperationExpenseHead",
                columns: table => new
                {
                    CostHead = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CostType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostNameInEnglish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CostNameInArabic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MinServiceCosttoCompany = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    MinServicePrice = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    GrossMargin = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    isApplicableForVehicle = table.Column<bool>(type: "bit", nullable: false),
                    isApplicableForSkillset = table.Column<bool>(type: "bit", nullable: false),
                    isApplicableForMaterial = table.Column<bool>(type: "bit", nullable: false),
                    isApplicableForFinancialExpense = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpOperationExpenseHead", x => x.CostHead);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPaymentTermsToProject",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Particular = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InstDate = table.Column<DateTime>(type: "date", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPaymentTermsToProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectBudgetCosting",
                columns: table => new
                {
                    ProjectBudgetCostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectBudgetEstimationId = table.Column<int>(type: "int", nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectBudgetCosting", x => x.ProjectBudgetCostingId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectBudgetEstimation",
                columns: table => new
                {
                    ProjectBudgetEstimationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PreviousEstimatonId = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectBudgetEstimation", x => x.ProjectBudgetEstimationId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectFinancialExpenseCosting",
                columns: table => new
                {
                    FinancialExpenseCostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectBudgetCostingId = table.Column<int>(type: "int", nullable: false),
                    FinancialExpenseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CostPerUnit = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectFinancialExpenseCosting", x => x.FinancialExpenseCostingId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectLogisticsCosting",
                columns: table => new
                {
                    LogisticsCostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectBudgetCostingId = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VehicleNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    CostPerUnit = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Margin = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectLogisticsCosting", x => x.LogisticsCostingId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectLogisticsSubCosting",
                columns: table => new
                {
                    LogisticsSubCostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogisticsCostingId = table.Column<int>(type: "int", nullable: false),
                    CostHead = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectLogisticsSubCosting", x => x.LogisticsSubCostingId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectMaterialEquipmentCosting",
                columns: table => new
                {
                    MaterialEquipmentCostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectBudgetCostingId = table.Column<int>(type: "int", nullable: false),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    MaterialEquipmentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CostPerUnit = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectMaterialEquipmentCosting", x => x.MaterialEquipmentCostingId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectMaterialEquipmentSubCosting",
                columns: table => new
                {
                    MaterialEquipmentSubCostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialEquipmentCostingId = table.Column<int>(type: "int", nullable: false),
                    CostHead = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectMaterialEquipmentSubCosting", x => x.MaterialEquipmentSubCostingId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectResourceCosting",
                columns: table => new
                {
                    ResourceCostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectBudgetCostingId = table.Column<int>(type: "int", nullable: false),
                    SkillsetCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Margin = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    CostPerUnit = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectResourceCosting", x => x.ResourceCostingId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectResourceSubCosting",
                columns: table => new
                {
                    ResourceSubCostingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ResourceCostingId = table.Column<int>(type: "int", nullable: false),
                    CostHead = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectResourceSubCosting", x => x.ResourceSubCostingId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProjectSites",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectNameEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ProjectNameArb = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: true),
                    EndDate = table.Column<DateTime>(type: "date", nullable: true),
                    ActualEndDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsResourcesAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsMaterialAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsLogisticsAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsShiftsAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsExpenceOverheadsAssigned = table.Column<bool>(type: "bit", nullable: false),
                    IsEstimationCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsSkillSetsMapped = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertedToProposal = table.Column<bool>(type: "bit", nullable: false),
                    IsConvrtedToContract = table.Column<bool>(type: "bit", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdendum = table.Column<bool>(type: "bit", nullable: false),
                    IsInProgress = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsSuspended = table.Column<bool>(type: "bit", nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    IsInActive = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SiteWorkingHours = table.Column<short>(type: "smallint", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProjectSites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProposalCosting",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectBudgetEstimationId = table.Column<int>(type: "int", nullable: false),
                    SkillSetCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemArab = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    IsForAdendum = table.Column<bool>(type: "bit", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProposalCosting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpProposalTemplate",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TitleOfService = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoveringLetter = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Commercial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuingAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitleOfServiceArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoveringLetterArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CommercialArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotesArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IssuingAuthorityArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpProposalTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvAddResource",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddResReqHeadId = table.Column<long>(type: "bigint", nullable: false),
                    Qty = table.Column<int>(type: "int", nullable: false),
                    SkillsetCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PricePerUnit = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    ToDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvAddResource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvAddResourceEmployeeToResourceMap",
                columns: table => new
                {
                    MapId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PvAddResReqId = table.Column<long>(type: "bigint", nullable: false),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SkillSet = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DefShift = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OffDay = table.Column<short>(type: "smallint", nullable: false),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    ToDate = table.Column<DateTime>(type: "date", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvAddResourceEmployeeToResourceMap", x => x.MapId);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvAddResourceReqHead",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsEmpMapped = table.Column<bool>(type: "bit", nullable: false),
                    IsMerged = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvAddResourceReqHead", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvOpenCloseReq",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsSuspendReq = table.Column<bool>(type: "bit", nullable: false),
                    IsCancelReq = table.Column<bool>(type: "bit", nullable: false),
                    IsCloseReq = table.Column<bool>(type: "bit", nullable: false),
                    IsReOpenReq = table.Column<bool>(type: "bit", nullable: false),
                    IsRevokeSuspReq = table.Column<bool>(type: "bit", nullable: false),
                    IsExtendProjReq = table.Column<bool>(type: "bit", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "date", nullable: true),
                    ExtensionDate = table.Column<DateTime>(type: "date", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvOpenCloseReq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvRemoveResourceReq",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsMerged = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvRemoveResourceReq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvReplaceResourceReq",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ResignedEmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReplacedEmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsMerged = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvReplaceResourceReq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvSwapEmployeesReq",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SrcCustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrcSiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrcProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestCustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestSiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrcEmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestEmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsMerged = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvSwapEmployeesReq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvTransferResourceReq",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SrcCustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrcSiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrcProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestCustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestSiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsMerged = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvTransferResourceReq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpPvTransferWithReplacementReq",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SrcCustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrcSiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrcProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestCustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestSiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SrcEmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DestEmployeeNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FromDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsMerged = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    Modified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUploadBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpPvTransferWithReplacementReq", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpReasonCode",
                columns: table => new
                {
                    ReasonCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ReasonCodeNameEng = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReasonCodeNameArb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DescriptionEng = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DescriptionArb = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    IsForCustomerVisit = table.Column<bool>(type: "bit", nullable: true),
                    IsForCustomerComplaint = table.Column<bool>(type: "bit", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpReasonCode", x => x.ReasonCode);
                });

            migrationBuilder.CreateTable(
                name: "tblOpShiftsPlanForProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ShiftCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpShiftsPlanForProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblOpSkillset",
                columns: table => new
                {
                    SkillSetCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SkillType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInEnglish = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameInArabic = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DetailsOfSkillSet = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SkillSetType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PrioryImportanceScale = table.Column<short>(type: "smallint", nullable: false),
                    MinBufferResource = table.Column<short>(type: "smallint", nullable: false),
                    MonthlyCtc = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    CostToCompanyDay = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    MonthlyOverheadCost = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    MonthlyOtherOverHeads = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    TotalSkillsetCost = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    TotalSkillsetCostDay = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    ServicePriceToCompany = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    MinMarginRequired = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    OverrideMarginIfRequired = table.Column<bool>(type: "bit", nullable: false),
                    ResponsibilitiesRoles = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpSkillset", x => x.SkillSetCode);
                });

            migrationBuilder.CreateTable(
                name: "tblOpSkillsetPlanForProject",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SkillsetCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Quantity = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpSkillsetPlanForProject", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblParentAddRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisteredMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StuAdmNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdded = table.Column<bool>(type: "bit", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblParentAddRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblParentMyGallery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegisterMobile = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsVedio = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblParentMyGallery", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblPopDefVendorCategory",
                columns: table => new
                {
                    VenCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VenCatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VenCatDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CatPrefix = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    LastSeq = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopDefVendorCategory", x => x.VenCatCode);
                });

            migrationBuilder.CreateTable(
                name: "tblPopDefVendorPOTermsCode",
                columns: table => new
                {
                    POTermsCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    POTermsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    POTermsDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    POTermsDueDays = table.Column<short>(type: "smallint", nullable: false),
                    POTermDiscDays = table.Column<short>(type: "smallint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopDefVendorPOTermsCode", x => x.POTermsCode);
                });

            migrationBuilder.CreateTable(
                name: "tblPopDefVendorShipment",
                columns: table => new
                {
                    ShipmentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShipmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShipmentDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShipmentType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopDefVendorShipment", x => x.ShipmentCode);
                });

            migrationBuilder.CreateTable(
                name: "tblPurAuthorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppAuth = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AppLevel = table.Column<short>(type: "smallint", nullable: false),
                    PurchaseRequest = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseOrder = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseReturn = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPurAuthorities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRouteMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RouteCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteLongitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteLatitude = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRouteMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRoutePlanDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoutePlanId = table.Column<int>(type: "int", nullable: false),
                    RouteCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Flag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoutePlanDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblRoutePlanHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoutePlanCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RouteNameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RouteNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoutePlanHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSchoolMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mobile = table.Column<int>(type: "int", nullable: false),
                    SentBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadFlag = table.Column<bool>(type: "bit", nullable: false),
                    ReadDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSchoolMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSequenceNumberSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceSeq = table.Column<int>(type: "int", nullable: false),
                    InvCredSeq = table.Column<int>(type: "int", nullable: false),
                    CreditSeq = table.Column<int>(type: "int", nullable: false),
                    VendCreditSeq = table.Column<int>(type: "int", nullable: false),
                    VoucherSeq = table.Column<int>(type: "int", nullable: false),
                    ApVoucherSeq = table.Column<int>(type: "int", nullable: false),
                    JvVoucherSeq = table.Column<int>(type: "int", nullable: false),
                    CvVoucherSeq = table.Column<int>(type: "int", nullable: false),
                    BvVoucherSeq = table.Column<int>(type: "int", nullable: false),
                    ArPaymentNumber = table.Column<int>(type: "int", nullable: false),
                    ApPaymentNumber = table.Column<int>(type: "int", nullable: false),
                    PONumber = table.Column<int>(type: "int", nullable: false),
                    PRNumber = table.Column<int>(type: "int", nullable: false),
                    GRNNumber = table.Column<int>(type: "int", nullable: false),
                    SDQuoteNumber = table.Column<int>(type: "int", nullable: false),
                    SDInvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    SDOrderNumber = table.Column<int>(type: "int", nullable: false),
                    SDDeliveryNumber = table.Column<int>(type: "int", nullable: false),
                    SDInvRetNumber = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSequenceNumberSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblServiceCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName_En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceName_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblServiceCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblServiceProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceProviderCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceProviderName_En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceProviderName_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblServiceProvider", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefCustomerCategory",
                columns: table => new
                {
                    CustCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustCatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustCatDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CatPrefix = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    LastSeq = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefCustomerCategory", x => x.CustCatCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefSalesShipment",
                columns: table => new
                {
                    ShipmentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ShipmentName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShipmentDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShipmentType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefSalesShipment", x => x.ShipmentCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefSalesTermsCode",
                columns: table => new
                {
                    SalesTermsCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SalesTermsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SalesTermsDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SalesTermsDueDays = table.Column<short>(type: "smallint", nullable: false),
                    SalesTermDiscDays = table.Column<short>(type: "smallint", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefSalesTermsCode", x => x.SalesTermsCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefSurveyFormElement",
                columns: table => new
                {
                    FormElementCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ElementEngName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ElementArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ElementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ListValueString = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MinValue = table.Column<int>(type: "int", nullable: true),
                    MaxValue = table.Column<int>(type: "int", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefSurveyFormElement", x => x.FormElementCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefSurveyFormHead",
                columns: table => new
                {
                    SurveyFormCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SurveyFormNameArb = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SurveyFormNameEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefSurveyFormHead", x => x.SurveyFormCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefUnitMaster",
                columns: table => new
                {
                    UnitCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UnitNameEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UnitNameArb = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefUnitMaster", x => x.UnitCode);
                });

            migrationBuilder.CreateTable(
                name: "tblStudentAttnRegDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttnRegHeaderId = table.Column<int>(type: "int", nullable: false),
                    StudentAdmNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    InTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    OutTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    AttnFlag = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    IsLeave = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStudentAttnRegDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblStudentAttnRegHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AttnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeacherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SectionCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    GradeCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblStudentAttnRegHeader", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblStudentHomeWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HomeworkDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GradeCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubCodes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TeacherCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeWorkDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeWorkDescription_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStudentHomeWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysNotificaticationTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Template_En = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Template_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysNotificaticationTemplate", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolAcademicBatches",
                columns: table => new
                {
                    AcademicYear = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AcademicStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AcademicEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolAcademicBatches", x => x.AcademicYear);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolAcademicsSubects",
                columns: table => new
                {
                    SubCodes = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolAcademicsSubects", x => x.SubCodes);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolAcedemicClassGrade",
                columns: table => new
                {
                    GradeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GradeName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsGradeRequired = table.Column<bool>(type: "bit", nullable: true),
                    NoOfGrades = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolAcedemicClassGrade", x => x.GradeCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolBranches",
                columns: table => new
                {
                    BranchCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartAcademicDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndAcademicDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinBranch = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartWeekDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrivacyPolicyUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfWeekDays = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekOff1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WeekOff2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GeoLat = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeoLong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BranchPrefix = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NextStuNum = table.Column<int>(type: "int", nullable: true),
                    NextFeeVoucherNum = table.Column<int>(type: "int", nullable: true),
                    Default_InTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Default_OutTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    BranchNotification_Moderator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolBranches", x => x.BranchCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolBranchesAuthority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Level = table.Column<int>(type: "int", nullable: false),
                    IsApproveLeave = table.Column<bool>(type: "bit", nullable: false),
                    IsApproveDisciPlinaryAction = table.Column<bool>(type: "bit", nullable: false),
                    IsApproveNotification = table.Column<bool>(type: "bit", nullable: false),
                    IsApproveEvent = table.Column<bool>(type: "bit", nullable: false),
                    TeacherCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolBranchesAuthority", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolExaminationTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfExaminationCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ExaminationTypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ExaminationTypeName2 = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolExaminationTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolFeeTerms",
                columns: table => new
                {
                    TermCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TermName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TermName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TermEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeeDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolFeeTerms", x => x.TermCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolFeeType",
                columns: table => new
                {
                    FeeCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FeeName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDiscountable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxDiscPer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TaxApplicable = table.Column<bool>(type: "bit", nullable: false),
                    TaxCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeGLAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeTaxAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolFeeType", x => x.FeeCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolGender",
                columns: table => new
                {
                    GenderCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenderName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolGender", x => x.GenderCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolGradeSectionMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectionCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxStrength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinStrength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AvgStrength = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolGradeSectionMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolGradeSubjectMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemisterCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SubCodes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaximumMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    QualifyingMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolGradeSubjectMapping", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolHolidayCalanderStudent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolHolidayCalanderStudent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolLanguages",
                columns: table => new
                {
                    LangCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LangName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LangName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolLanguages", x => x.LangCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolNationality",
                columns: table => new
                {
                    NatCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NatName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NatName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolNationality", x => x.NatCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolNews",
                columns: table => new
                {
                    NewId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Topic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Topic_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Headlines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Headlines_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewsDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NewTumbnailImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: true),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApproveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolNews", x => x.NewId);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolNotificationFilters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GradeCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NationalityCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SectionCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PTGroupCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    GenderCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PickUpAndDropZone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolNotificationFilters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationType = table.Column<int>(type: "int", nullable: false),
                    AcadamicYear = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    NotificationTitle = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NotificationTitle_Ar = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NotificationMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NotificationMessage_Ar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolNotifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolPayTypes",
                columns: table => new
                {
                    PayCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GLAccount = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowOtherBranchUse = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolPayTypes", x => x.PayCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolPETCategory",
                columns: table => new
                {
                    PETCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PETName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PETName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolPETCategory", x => x.PETCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolPushNotificationParent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MsgNoteId = table.Column<int>(type: "int", nullable: false),
                    NotifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifyMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotifyMessage_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredMobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReadDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotifyTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromUserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolPushNotificationParent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolReligion",
                columns: table => new
                {
                    RegCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolReligion", x => x.RegCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Day1In = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day1Out = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day2In = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day2Out = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day3In = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day3Out = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day4In = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day4Out = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day5In = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day5Out = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day6In = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day6Out = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day7In = table.Column<TimeSpan>(type: "time", nullable: true),
                    Day7Out = table.Column<TimeSpan>(type: "time", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolSectionsSection",
                columns: table => new
                {
                    SectionCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SectionName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolSectionsSection", x => x.SectionCode);
                });

            migrationBuilder.CreateTable(
                name: "TblSysSchoolSemister",
                columns: table => new
                {
                    SemisterCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemisterName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemisterName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SemisterStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SemisterEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSysSchoolSemister", x => x.SemisterCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolStuLeaveType",
                columns: table => new
                {
                    LeaveCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeaveName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LeaveName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaxLeavePerReq = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolStuLeaveType", x => x.LeaveCode);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchooScheduleEvents",
                columns: table => new
                {
                    HDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventNameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDescriptionAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventCreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    NotesOnEvent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchooScheduleEvents", x => x.HDate);
                });

            migrationBuilder.CreateTable(
                name: "tblTranDefProductType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEN = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTranDefProductType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblTranDefTax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    NameEN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Symbol = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TaxTariffPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefault = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTranDefTax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblTranDefUnitType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEN = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    NameAR = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTranDefUnitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblTranFeeTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdmissionNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiptVoucher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FeeTerm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeStructCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TermCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeDueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NetFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiscReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidTransNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidRemarks1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidRemarks2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InvNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JvNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaidOnline = table.Column<bool>(type: "bit", nullable: false),
                    PaidManual = table.Column<bool>(type: "bit", nullable: false),
                    PayCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivedByUser = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcademicYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTranFeeTransaction", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblVehicleBrandMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVehicleBrandMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblVehicleCompanyMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleCompany = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleCompany_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVehicleCompanyMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblVehicleFuelEntry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelQuantity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuellingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Driver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReadingKM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVehicleFuelEntry", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblVehicleMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleCompany = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChassisNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeatingCapacity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegisteredRTORegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationAuthority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleValidityTill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleOwnership = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProcurementLeasedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrentBookValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnnualLeaseValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SalvageBookValue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LeaseEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VehicleOwnerEnglish = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleOwnerArabic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeterReadingOnProcurement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentMeterReading = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleNextMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedMileagePerKM = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VehicleLastMaintenanceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstimatedServiceYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelTankCapacityInLitters = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsVehicleGoodsCarrier = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVehicleMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblVehicleTypeMaster",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VehicleType_Ar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVehicleTypeMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblWebStudentRegistration",
                columns: table => new
                {
                    FullName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GenderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Grade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherPhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EnglishFluencyLevel = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsyourchildPottytrained = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblWebStudentRegistration", x => x.FullName);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysCurrencyCode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BuyingRate = table.Column<float>(type: "real", nullable: false),
                    SellingRate = table.Column<float>(type: "real", nullable: false),
                    Lastupdated = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysCurrencyCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpSysCurrencyCode_tblErpSysCountryCode_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "tblErpSysCountryCode",
                        principalColumn: "CountryCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysStateCode",
                columns: table => new
                {
                    StateCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysStateCode", x => x.StateCode);
                    table.ForeignKey(
                        name: "FK_tblErpSysStateCode_tblErpSysCountryCode_CountryCode",
                        column: x => x.CountryCode,
                        principalTable: "tblErpSysCountryCode",
                        principalColumn: "CountryCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinCenterCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FinCenterName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FinCenterType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinDefCenters_tblFinDefMainAccounts_FinCenterCode",
                        column: x => x.FinCenterCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnDistribution",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    FinAcCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Gl = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    DrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnDistribution", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnDistribution_tblFinDefMainAccounts_FinAcCode",
                        column: x => x.FinAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefDistributionGroup",
                columns: table => new
                {
                    InvDistGroup = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InvAssetAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvNonAssetAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvCashPOAC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvCOGSAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvAdjAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvSalesAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvInTransitAc = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    InvDefaultAPAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvCostCorAc = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    InvWIPAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvWriteOffAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefDistributionGroup", x => x.InvDistGroup);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvAdjAc",
                        column: x => x.InvAdjAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvAssetAc",
                        column: x => x.InvAssetAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvCashPOAC",
                        column: x => x.InvCashPOAC,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvCOGSAc",
                        column: x => x.InvCOGSAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvCostCorAc",
                        column: x => x.InvCostCorAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvDefaultAPAc",
                        column: x => x.InvDefaultAPAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvInTransitAc",
                        column: x => x.InvInTransitAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvNonAssetAc",
                        column: x => x.InvNonAssetAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvSalesAc",
                        column: x => x.InvSalesAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvWIPAc",
                        column: x => x.InvWIPAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefDistributionGroup_tblFinDefMainAccounts_InvWriteOffAc",
                        column: x => x.InvWriteOffAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblInventoryDefDistributionGroup",
                columns: table => new
                {
                    InvDistGroup = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    InvAssetAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvNonAssetAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvCashPOAC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvCOGSAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvAdjAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvSalesAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvInTransitAc = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    InvDefaultAPAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvCostCorAc = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    InvWIPAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvWriteOffAc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInventoryDefDistributionGroup", x => x.InvDistGroup);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvAdjAc",
                        column: x => x.InvAdjAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvAssetAc",
                        column: x => x.InvAssetAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvCashPOAC",
                        column: x => x.InvCashPOAC,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvCOGSAc",
                        column: x => x.InvCOGSAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvCostCorAc",
                        column: x => x.InvCostCorAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvDefaultAPAc",
                        column: x => x.InvDefaultAPAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvInTransitAc",
                        column: x => x.InvInTransitAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvNonAssetAc",
                        column: x => x.InvNonAssetAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvSalesAc",
                        column: x => x.InvSalesAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvWIPAc",
                        column: x => x.InvWIPAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInventoryDefDistributionGroup_tblFinDefMainAccounts_InvWriteOffAc",
                        column: x => x.InvWriteOffAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefAccountCategory",
                columns: table => new
                {
                    FinCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FinCatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FinType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    FinCatLastSeq = table.Column<short>(type: "smallint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefAccountCategory", x => x.FinCatCode);
                    table.ForeignKey(
                        name: "FK_tblFinDefAccountCategory_tblFinSysAccountType_FinType",
                        column: x => x.FinType,
                        principalTable: "tblFinSysAccountType",
                        principalColumn: "TypeCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefSubCategory",
                columns: table => new
                {
                    ItemSubCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SubCatKey = table.Column<string>(type: "nvarchar(41)", maxLength: 41, nullable: true),
                    ItemCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemSubCatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ItemSubCatDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefSubCategory", x => x.ItemSubCatCode);
                    table.ForeignKey(
                        name: "FK_tblInvDefSubCategory_tblInvDefCategory_ItemCatCode",
                        column: x => x.ItemCatCode,
                        principalTable: "tblInvDefCategory",
                        principalColumn: "ItemCatCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblOpContractClausesToContractFormMap",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractFormId = table.Column<long>(type: "bigint", nullable: false),
                    ClauseTitleEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseSubTitleEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseDescriptionEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberEng = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseTitleArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseSubTitleArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClauseDescriptionArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberArb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialNumber = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpContractClausesToContractFormMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOpContractClausesToContractFormMap_tblOpContractFormHead_ContractFormId",
                        column: x => x.ContractFormId,
                        principalTable: "tblOpContractFormHead",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblOpContractTemplateToContractClauseMap",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    ContractClauseId = table.Column<long>(type: "bigint", nullable: false),
                    SerialNumber = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpContractTemplateToContractClauseMap", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOpContractTemplateToContractClauseMap_tblOpContractClause_ContractClauseId",
                        column: x => x.ContractClauseId,
                        principalTable: "tblOpContractClause",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblOpContractTemplateToContractClauseMap_tblOpContractTemplate_ContractTemplateId",
                        column: x => x.ContractTemplateId,
                        principalTable: "tblOpContractTemplate",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblOpCustomerComplaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReasonCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ComplaintBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComplaintDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProofForComplaint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActionRequired = table.Column<bool>(type: "bit", nullable: false),
                    ComplaintDate = table.Column<DateTime>(type: "date", nullable: false),
                    BookedBy = table.Column<int>(type: "int", nullable: false),
                    ClosingDate = table.Column<DateTime>(type: "date", nullable: true),
                    ProofForAction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActionDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    ClosedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsInprogress = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpCustomerComplaints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOpCustomerComplaints_tblOpReasonCode_ReasonCode",
                        column: x => x.ReasonCode,
                        principalTable: "tblOpReasonCode",
                        principalColumn: "ReasonCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefServiceMaster",
                columns: table => new
                {
                    ServiceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ServiceNameEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ServiceNameArb = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SurveyFormCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefServiceMaster", x => x.ServiceCode);
                    table.ForeignKey(
                        name: "FK_tblSndDefServiceMaster_tblSndDefSurveyFormHead_SurveyFormCode",
                        column: x => x.SurveyFormCode,
                        principalTable: "tblSndDefSurveyFormHead",
                        principalColumn: "SurveyFormCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefSurveyFormElementsMapp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SurveyFormCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FormElementCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefSurveyFormElementsMapp", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndDefSurveyFormElementsMapp_tblSndDefSurveyFormElement_FormElementCode",
                        column: x => x.FormElementCode,
                        principalTable: "tblSndDefSurveyFormElement",
                        principalColumn: "FormElementCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefSurveyFormElementsMapp_tblSndDefSurveyFormHead_SurveyFormCode",
                        column: x => x.SurveyFormCode,
                        principalTable: "tblSndDefSurveyFormHead",
                        principalColumn: "SurveyFormCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolFeeStructureHeader",
                columns: table => new
                {
                    FeeStructCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GradeCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FeeStructName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeeStructName2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplyLateFee = table.Column<bool>(type: "bit", nullable: false),
                    LateFeeCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActualFeePayable = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolFeeStructureHeader", x => x.FeeStructCode);
                    table.ForeignKey(
                        name: "FK_tblSysSchoolFeeStructureHeader_tblSysSchoolAcedemicClassGrade_GradeCode",
                        column: x => x.GradeCode,
                        principalTable: "tblSysSchoolAcedemicClassGrade",
                        principalColumn: "GradeCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSysSchoolFeeStructureHeader_tblSysSchoolBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblSysSchoolBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSysSchoolFeeStructureHeader_tblSysSchoolFeeType_LateFeeCode",
                        column: x => x.LateFeeCode,
                        principalTable: "tblSysSchoolFeeType",
                        principalColumn: "FeeCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolNewsMediaLib",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NewId = table.Column<int>(type: "int", nullable: false),
                    Mediapath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MediaNotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolNewsMediaLib", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSysSchoolNewsMediaLib_tblSysSchoolNews_NewId",
                        column: x => x.NewId,
                        principalTable: "tblSysSchoolNews",
                        principalColumn: "NewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblTranDefProduct",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameEN = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    NameAR = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    ProductCode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductTypeId = table.Column<int>(type: "int", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CostPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    UnitTypeId = table.Column<int>(type: "int", nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTranDefProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTranDefProduct_tblTranDefProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "tblTranDefProductType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblTranDefProduct_tblTranDefUnitType_UnitTypeId",
                        column: x => x.UnitTypeId,
                        principalTable: "tblTranDefUnitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysCityCode",
                columns: table => new
                {
                    CityCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StateCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityNameAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysCityCode", x => x.CityCode);
                    table.ForeignKey(
                        name: "FK_tblErpSysCityCode_tblErpSysStateCode_StateCode",
                        column: x => x.StateCode,
                        principalTable: "tblErpSysStateCode",
                        principalColumn: "StateCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefAccountSubCategory",
                columns: table => new
                {
                    FinSubCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FinCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FinSubCatName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FinSubCatLastSeq = table.Column<short>(type: "smallint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefAccountSubCategory", x => x.FinSubCatCode);
                    table.ForeignKey(
                        name: "FK_tblFinDefAccountSubCategory_tblFinDefAccountCategory_FinCatCode",
                        column: x => x.FinCatCode,
                        principalTable: "tblFinDefAccountCategory",
                        principalColumn: "FinCatCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpInvItemMaster",
                columns: table => new
                {
                    ItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    HSNCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ItemDescriptionAr = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ShortName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ShortNameAr = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ItemCat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemSubCat = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemClass = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemSubClass = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemBaseUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ItemAvgCost = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ItemStandardCost = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ItemPrimeVendor = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemStandardPrice1 = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    ItemStandardPrice2 = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    ItemStandardPrice3 = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    ItemType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemTracking = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemWeight = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemTaxCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AllowPriceOverride = table.Column<bool>(type: "bit", nullable: false),
                    AllowDiscounts = table.Column<bool>(type: "bit", nullable: false),
                    AllowQuantityOverride = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpInvItemMaster", x => x.ItemCode);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemMaster_tblErpSysSystemTaxes_ItemTaxCode",
                        column: x => x.ItemTaxCode,
                        principalTable: "tblErpSysSystemTaxes",
                        principalColumn: "TaxCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemMaster_tblInvDefCategory_ItemCat",
                        column: x => x.ItemCat,
                        principalTable: "tblInvDefCategory",
                        principalColumn: "ItemCatCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemMaster_tblInvDefClass_ItemClass",
                        column: x => x.ItemClass,
                        principalTable: "tblInvDefClass",
                        principalColumn: "ItemClassCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemMaster_tblInvDefSubCategory_ItemSubCat",
                        column: x => x.ItemSubCat,
                        principalTable: "tblInvDefSubCategory",
                        principalColumn: "ItemSubCatCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemMaster_tblInvDefSubClass_ItemSubClass",
                        column: x => x.ItemSubClass,
                        principalTable: "tblInvDefSubClass",
                        principalColumn: "ItemSubClassCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemMaster_tblInvDefUOM_ItemBaseUnit",
                        column: x => x.ItemBaseUnit,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefServiceUnitMap",
                columns: table => new
                {
                    ServiceCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    UnitCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefServiceUnitMap", x => new { x.ServiceCode, x.UnitCode });
                    table.ForeignKey(
                        name: "FK_tblSndDefServiceUnitMap_tblSndDefServiceMaster_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "tblSndDefServiceMaster",
                        principalColumn: "ServiceCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefServiceUnitMap_tblSndDefUnitMaster_UnitCode",
                        column: x => x.UnitCode,
                        principalTable: "tblSndDefUnitMaster",
                        principalColumn: "UnitCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSysSchoolFeeStructureDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FeeStructCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TermCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FeeCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaxDiscPer = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ActualFeeAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSysSchoolFeeStructureDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSysSchoolFeeStructureDetails_tblSysSchoolFeeStructureHeader_FeeStructCode",
                        column: x => x.FeeStructCode,
                        principalTable: "tblSysSchoolFeeStructureHeader",
                        principalColumn: "FeeStructCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSysSchoolFeeStructureDetails_tblSysSchoolFeeTerms_TermCode",
                        column: x => x.TermCode,
                        principalTable: "tblSysSchoolFeeTerms",
                        principalColumn: "TermCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSysSchoolFeeStructureDetails_tblSysSchoolFeeType_FeeCode",
                        column: x => x.FeeCode,
                        principalTable: "tblSysSchoolFeeType",
                        principalColumn: "FeeCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysCompanyBranches",
                columns: table => new
                {
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<int>(type: "int", nullable: true),
                    BankName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    BankNameAr = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    BranchName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BranchAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    BranchAddressAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AccountNumber = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    City = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    State = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AuthorityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GeoLocLatitude = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GeoLocLongitude = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Iban = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysCompanyBranches", x => x.BranchCode);
                    table.ForeignKey(
                        name: "FK_tblErpSysCompanyBranches_tblErpSysCityCode_City",
                        column: x => x.City,
                        principalTable: "tblErpSysCityCode",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpSysCompanyBranches_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpSysCompanyBranches_tblErpSysZoneSetting_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "tblErpSysZoneSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefCustomerMaster",
                columns: table => new
                {
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CustArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustAlias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustType = table.Column<short>(type: "smallint", nullable: false),
                    VATNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustCatCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CustRating = table.Column<short>(type: "smallint", nullable: false),
                    SalesTermsCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CustDiscount = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    CustCrLimit = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    CustOutStandBal = table.Column<decimal>(type: "decimal(17,3)", nullable: true),
                    CustAvailCrLimit = table.Column<decimal>(type: "decimal(17,3)", nullable: true),
                    CustSalesRep = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CustSalesArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustARAc = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    CustLastPaidDate = table.Column<DateTime>(type: "date", nullable: false),
                    CustLastSalesDate = table.Column<DateTime>(type: "date", nullable: false),
                    CustLastPayAmt = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    CustAddress1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustCityCode1 = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    CustMobile1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustPhone1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustEmail1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustContact1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustAddress2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustCityCode2 = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustMobile2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustPhone2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustEmail2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustContact2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustUDF1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustUDF2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustUDF3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustAllowCrsale = table.Column<bool>(type: "bit", nullable: false),
                    CustAlloCrOverride = table.Column<bool>(type: "bit", nullable: false),
                    CustOnHold = table.Column<bool>(type: "bit", nullable: false),
                    CustAlloChkPay = table.Column<bool>(type: "bit", nullable: false),
                    CustSetPriceLevel = table.Column<bool>(type: "bit", nullable: false),
                    CustPriceLevel = table.Column<short>(type: "smallint", nullable: false),
                    CustIsVendor = table.Column<bool>(type: "bit", nullable: false),
                    CustArAcBranch = table.Column<bool>(type: "bit", nullable: false),
                    CustArAcCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CustDefExpAcCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CustARAdjAcCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CustARDiscAcCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    CrNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustNameAliasEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustNameAliasAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefCustomerMaster", x => x.CustCode);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblErpSysCityCode_CustCityCode1",
                        column: x => x.CustCityCode1,
                        principalTable: "tblErpSysCityCode",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblErpSysCityCode_CustCityCode2",
                        column: x => x.CustCityCode2,
                        principalTable: "tblErpSysCityCode",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblFinDefMainAccounts_CustARAc",
                        column: x => x.CustARAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblFinDefMainAccounts_CustArAcCode",
                        column: x => x.CustArAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblFinDefMainAccounts_CustARAdjAcCode",
                        column: x => x.CustARAdjAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblFinDefMainAccounts_CustARDiscAcCode",
                        column: x => x.CustARDiscAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblFinDefMainAccounts_CustDefExpAcCode",
                        column: x => x.CustDefExpAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblSndDefCustomerCategory_CustCatCode",
                        column: x => x.CustCatCode,
                        principalTable: "tblSndDefCustomerCategory",
                        principalColumn: "CustCatCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefCustomerMaster_tblSndDefSalesTermsCode_SalesTermsCode",
                        column: x => x.SalesTermsCode,
                        principalTable: "tblSndDefSalesTermsCode",
                        principalColumn: "SalesTermsCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefVendorMaster",
                columns: table => new
                {
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VendName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    VendArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendAlias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendType = table.Column<short>(type: "smallint", nullable: false),
                    VATNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendCatCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    VendRating = table.Column<short>(type: "smallint", nullable: false),
                    PoTermsCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    VendDiscount = table.Column<decimal>(type: "decimal(7,2)", nullable: false),
                    VendCrLimit = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    VendOutStandBal = table.Column<decimal>(type: "decimal(17,3)", nullable: true),
                    VendAvailCrLimit = table.Column<decimal>(type: "decimal(17,3)", nullable: true),
                    VendPoRep = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    VendPoArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VendARAc = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    VendLastPaidDate = table.Column<DateTime>(type: "date", nullable: false),
                    VendLastPoDate = table.Column<DateTime>(type: "date", nullable: false),
                    VendLastPayAmt = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    VendAddress1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VendCityCode1 = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    VendMobile1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendPhone1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendEmail1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VendContact1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendAddress2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VendCityCode2 = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    VendMobile2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendPhone2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendEmail2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VendContact2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendUDF1 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendUDF2 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendUDF3 = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    VendAllowCrsale = table.Column<bool>(type: "bit", nullable: false),
                    VendAlloCrOverride = table.Column<bool>(type: "bit", nullable: false),
                    VendOnHold = table.Column<bool>(type: "bit", nullable: false),
                    VendAlloChkPay = table.Column<bool>(type: "bit", nullable: false),
                    VendSetPriceLevel = table.Column<bool>(type: "bit", nullable: false),
                    VendPriceLevel = table.Column<short>(type: "smallint", nullable: false),
                    VendIsVendor = table.Column<bool>(type: "bit", nullable: false),
                    VendArAcBranch = table.Column<bool>(type: "bit", nullable: false),
                    VendArAcCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    VendDefExpAcCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    VendARAdjAcCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    VendARDiscAcCode = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    CrNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefVendorMaster", x => x.VendCode);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblErpSysCityCode_VendCityCode1",
                        column: x => x.VendCityCode1,
                        principalTable: "tblErpSysCityCode",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblErpSysCityCode_VendCityCode2",
                        column: x => x.VendCityCode2,
                        principalTable: "tblErpSysCityCode",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblFinDefMainAccounts_VendARAc",
                        column: x => x.VendARAc,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblFinDefMainAccounts_VendArAcCode",
                        column: x => x.VendArAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblFinDefMainAccounts_VendARAdjAcCode",
                        column: x => x.VendARAdjAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblFinDefMainAccounts_VendARDiscAcCode",
                        column: x => x.VendARDiscAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblFinDefMainAccounts_VendDefExpAcCode",
                        column: x => x.VendDefExpAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblPopDefVendorCategory_VendCatCode",
                        column: x => x.VendCatCode,
                        principalTable: "tblPopDefVendorCategory",
                        principalColumn: "VenCatCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefVendorMaster_tblPopDefVendorPOTermsCode_PoTermsCode",
                        column: x => x.PoTermsCode,
                        principalTable: "tblPopDefVendorPOTermsCode",
                        principalColumn: "POTermsCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpInvItemNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NoteDates = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpInvItemNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemNotes_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpInvItemsBarcode",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemUOMFlag = table.Column<short>(type: "smallint", nullable: false),
                    ItemBarcode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    ItemUOM = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpInvItemsBarcode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemsBarcode_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemsBarcode_tblInvDefUOM_ItemUOM",
                        column: x => x.ItemUOM,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpInvItemsUOM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemUOMFlag = table.Column<short>(type: "smallint", nullable: false),
                    ItemUOM = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ItemConvFactor = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    ItemUOMPrice1 = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    ItemUOMPrice2 = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    ItemUOMPrice3 = table.Column<decimal>(type: "numeric(10,3)", nullable: false),
                    ItemUOMDiscPer = table.Column<decimal>(type: "numeric(6,3)", nullable: false),
                    ItemUOMPrice4 = table.Column<decimal>(type: "numeric(10,3)", nullable: false),
                    ItemAvgCost = table.Column<decimal>(type: "numeric(11,5)", nullable: false),
                    ItemLastPOCost = table.Column<decimal>(type: "numeric(11,5)", nullable: false),
                    ItemLandedCost = table.Column<decimal>(type: "numeric(11,5)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpInvItemsUOM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemsUOM_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemsUOM_tblInvDefUOM_ItemUOM",
                        column: x => x.ItemUOM,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    SwpireCardId = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PrimaryBranch = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLoginAllow = table.Column<bool>(type: "bit", nullable: false),
                    IsSigned = table.Column<bool>(type: "bit", nullable: false),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LoginType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysLogin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpSysLogin_tblErpSysCompanyBranches_PrimaryBranch",
                        column: x => x.PrimaryBranch,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysTransactionSequence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    LastSeqNum = table.Column<int>(type: "int", nullable: false),
                    PrefixCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PrefixFinYear = table.Column<bool>(type: "bit", nullable: false),
                    ResetAfterFYClosing = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysTransactionSequence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpSysTransactionSequence_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpSysTransactionSequence_tblErpSysTransactionCodes_TransactionCode",
                        column: x => x.TransactionCode,
                        principalTable: "tblErpSysTransactionCodes",
                        principalColumn: "TransactionCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefAccountBranches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinBranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FinBranchPrefix = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FinBranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FinBranchDesc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FinBranchAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FinBranchType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FinBranchIsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefAccountBranches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinDefAccountBranches_tblErpSysCompanyBranches_FinBranchCode",
                        column: x => x.FinBranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefAccountBranchMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinBranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FinBranchName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    InventoryAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CashPurchase = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostofSalesAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InventoryAdjustment = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DefaultSalesAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DefaultSalesReturn = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InventoryTransfer = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DefaultPayable = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostCorrection = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WIPUsageConsumption = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Reserved = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefAccountBranchMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinDefAccountBranchMapping_tblErpSysCompanyBranches_FinBranchCode",
                        column: x => x.FinBranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefAccountlPaycodes",
                columns: table => new
                {
                    FinPayCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FinBranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FinPayType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FinPayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FinPayAcIntgrAC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FinPayPDCClearAC = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UseByOtherBranches = table.Column<bool>(type: "bit", nullable: false),
                    SystemGenCheckBook = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefAccountlPaycodes", x => x.FinPayCode);
                    table.ForeignKey(
                        name: "FK_tblFinDefAccountlPaycodes_tblErpSysCompanyBranches_FinBranchCode",
                        column: x => x.FinBranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinDefAccountlPaycodes_tblFinDefMainAccounts_FinPayAcIntgrAC",
                        column: x => x.FinPayAcIntgrAC,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinDefAccountlPaycodes_tblFinDefMainAccounts_FinPayPDCClearAC",
                        column: x => x.FinPayPDCClearAC,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefBranchesAuthority",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinBranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AppAuth = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AppLevel = table.Column<short>(type: "smallint", nullable: false),
                    AppAuthBV = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthCV = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthJV = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthAP = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthAR = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthFA = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthBR = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthPurcOrder = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthPurcRequest = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthPurcReturn = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthAdj = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthIssue = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthRect = table.Column<bool>(type: "bit", nullable: false),
                    AppAuthTrans = table.Column<bool>(type: "bit", nullable: false),
                    IsFinalAuthority = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefBranchesAuthority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinDefBranchesAuthority_tblErpSysCompanyBranches_FinBranchCode",
                        column: x => x.FinBranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefBranchesMainAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinAcCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FinBranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefBranchesMainAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinDefBranchesMainAccounts_tblErpSysCompanyBranches_FinBranchCode",
                        column: x => x.FinBranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinDefBranchesMainAccounts_tblFinDefMainAccounts_FinAcCode",
                        column: x => x.FinAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnAccountsLedger",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jvnum = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    GlId = table.Column<int>(type: "int", nullable: true),
                    TransDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    AcCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AcDesc = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MainAcCode = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PostedFlag = table.Column<bool>(type: "bit", nullable: false),
                    PostDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExRate = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    VoidFlag = table.Column<bool>(type: "bit", nullable: false),
                    ReverseFlag = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    Batch = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CostAllocation = table.Column<int>(type: "int", nullable: true),
                    CostSegCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnAccountsLedger", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnAccountsLedger_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnBankVoucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpVoucherNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VoucherNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    BankCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TrnMode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChequeNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ChequeDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AccountPayee = table.Column<bool>(type: "bit", nullable: false),
                    PDC = table.Column<bool>(type: "bit", nullable: false),
                    VoucherType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Posted = table.Column<bool>(type: "bit", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Void = table.Column<bool>(type: "bit", nullable: false),
                    CDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnBankVoucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucher_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucher_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCashVoucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpVoucherNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VoucherNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CBookCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VoucherType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Posted = table.Column<bool>(type: "bit", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Void = table.Column<bool>(type: "bit", nullable: false),
                    CDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCashVoucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucher_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucher_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnJournalVoucher",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpVoucherNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VoucherNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Posted = table.Column<bool>(type: "bit", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Void = table.Column<bool>(type: "bit", nullable: false),
                    CDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnJournalVoucher", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucher_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucher_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblIMTransferTransactionHeader",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranUser = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranToLocation = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranReference = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranTotalCost = table.Column<decimal>(type: "decimal(10,3)", nullable: false),
                    TranTotItems = table.Column<int>(type: "int", nullable: false),
                    TranCreateDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranCreateUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLastEditDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLastEditUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranLockStat = table.Column<short>(type: "smallint", nullable: false),
                    TranLockUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranPostStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranPostDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranpostUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranVoidStatus = table.Column<short>(type: "smallint", nullable: false),
                    TranVoidUser = table.Column<string>(type: "nvarchar(25)", nullable: true),
                    TranvoidDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TranInvAdjAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    JVNum = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblIMTransferTransactionHeader", x => x.TranNumber);
                    table.ForeignKey(
                        name: "FK_tblIMTransferTransactionHeader_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefWarehouse",
                columns: table => new
                {
                    WHCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    WHName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WHDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WHAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WHCity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    WHState = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WHIncharge = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    WHBranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvDistGroup = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WhAllowDirectPur = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefWarehouse", x => x.WHCode);
                    table.ForeignKey(
                        name: "FK_tblInvDefWarehouse_tblErpSysCompanyBranches_WHBranchCode",
                        column: x => x.WHBranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblInvDefWarehouse_tblInvDefDistributionGroup_InvDistGroup",
                        column: x => x.InvDistGroup,
                        principalTable: "tblInvDefDistributionGroup",
                        principalColumn: "InvDistGroup",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblOprTrnApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ServiceCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AppAuth = table.Column<int>(type: "int", nullable: false),
                    AppRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOprTrnApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOprTrnApprovals_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPurTrnApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ServiceType = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    ServiceCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AppAuth = table.Column<int>(type: "int", nullable: false),
                    AppRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPurTrnApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPurTrnApprovals_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSchoolTranInvoice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpInvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "date", nullable: true),
                    InvoiceDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    LpoContract = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VatPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsCreditConverted = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    InvoiceModule = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServiceDate1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSchoolTranInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSchoolTranInvoice_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSchoolTranInvoice_tblSndDefSalesTermsCode_PaymentTerms",
                        column: x => x.PaymentTerms,
                        principalTable: "tblSndDefSalesTermsCode",
                        principalColumn: "SalesTermsCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefSurveyor",
                columns: table => new
                {
                    SurveyorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SurveyorNameEng = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SurveyorNameArb = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Branch = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefSurveyor", x => x.SurveyorCode);
                    table.ForeignKey(
                        name: "FK_tblSndDefSurveyor_tblErpSysCompanyBranches_Branch",
                        column: x => x.Branch,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTranInvoice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpInvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "date", nullable: true),
                    InvoiceDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    LpoContract = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VatPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsCreditConverted = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    InvoiceModule = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServiceDate1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTranInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTranInvoice_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblTranInvoice_tblSndDefSalesTermsCode_PaymentTerms",
                        column: x => x.PaymentTerms,
                        principalTable: "tblSndDefSalesTermsCode",
                        principalColumn: "SalesTermsCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblTranPurcInvoice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpCreditNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "date", nullable: true),
                    InvoiceDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    LpoContract = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VatPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsCreditConverted = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    InvoiceModule = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServiceDate1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTranPurcInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblTranPurcInvoice_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblTranPurcInvoice_tblPopDefVendorPOTermsCode_PaymentTerms",
                        column: x => x.PaymentTerms,
                        principalTable: "tblPopDefVendorPOTermsCode",
                        principalColumn: "POTermsCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblTranVenInvoice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpCreditNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "date", nullable: true),
                    InvoiceDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    LpoContract = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PaymentTerms = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VatPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsCreditConverted = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    InvoiceModule = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServiceDate1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTranVenInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblTranVenInvoice_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblTranVenInvoice_tblPopDefVendorPOTermsCode_PaymentTerms",
                        column: x => x.PaymentTerms,
                        principalTable: "tblPopDefVendorPOTermsCode",
                        principalColumn: "POTermsCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblFinTrnAdvanceWallet",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    SNDId = table.Column<int>(type: "int", nullable: false),
                    SNDInvNum = table.Column<int>(type: "int", nullable: false),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AdvAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PostedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PayCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PreparedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblFinTrnAdvanceWallet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblFinTrnAdvanceWallet_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblFinTrnAdvanceWallet_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblFinTrnAdvanceWallet_tblSndDefCustomerMaster_CustCode",
                        column: x => x.CustCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCustomerPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    VoucherNumber = table.Column<int>(type: "int", nullable: false),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PayType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PayCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Checkdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preparedby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCustomerPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerPayment_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerPayment_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerPayment_tblSndDefCustomerMaster_CustCode",
                        column: x => x.CustCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnOpmCustomerPaymentHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    PaymentNumber = table.Column<int>(type: "int", nullable: false),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PayType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PayCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    InvoiceAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CheckNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Checkdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preparedby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Flag1 = table.Column<bool>(type: "bit", nullable: false),
                    Flag2 = table.Column<bool>(type: "bit", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false),
                    VoidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnOpmCustomerPaymentHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmCustomerPaymentHeader_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmCustomerPaymentHeader_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmCustomerPaymentHeader_tblSndDefCustomerMaster_CustCode",
                        column: x => x.CustCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefServiceEnquiryHeader",
                columns: table => new
                {
                    EnquiryNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DateOfEnquiry = table.Column<DateTime>(type: "date", nullable: false),
                    EstimateClosingDate = table.Column<DateTime>(type: "date", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TotalEstPrice = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StusEnquiryHead = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConvertedToProject = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<short>(type: "smallint", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefServiceEnquiryHeader", x => x.EnquiryNumber);
                    table.ForeignKey(
                        name: "FK_tblSndDefServiceEnquiryHeader_tblSndDefCustomerMaster_CustomerCode",
                        column: x => x.CustomerCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefSiteMaster",
                columns: table => new
                {
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SiteName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SiteArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    SiteAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    SiteCityCode = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    SiteGeoLatitude = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    SiteGeoLongitude = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    SiteGeoGain = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    SiteGeoLatitudeMeter = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    SiteGeoLongitudeMeter = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    IsChildCustomer = table.Column<bool>(type: "bit", nullable: false),
                    VATNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefSiteMaster", x => x.SiteCode);
                    table.ForeignKey(
                        name: "FK_tblSndDefSiteMaster_tblErpSysCityCode_SiteCityCode",
                        column: x => x.SiteCityCode,
                        principalTable: "tblErpSysCityCode",
                        principalColumn: "CityCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefSiteMaster_tblSndDefCustomerMaster_CustomerCode",
                        column: x => x.CustomerCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnOpmVendorPaymentHeader",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    PaymentNumber = table.Column<int>(type: "int", nullable: false),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PayType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PayCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    InvoiceAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CheckNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Checkdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preparedby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Flag1 = table.Column<bool>(type: "bit", nullable: false),
                    Flag2 = table.Column<bool>(type: "bit", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    PostedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false),
                    VoidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnOpmVendorPaymentHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmVendorPaymentHeader_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmVendorPaymentHeader_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmVendorPaymentHeader_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnVendorPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    VoucherNumber = table.Column<int>(type: "int", nullable: false),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    PayType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PayCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CheckNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Checkdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Narration = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preparedby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnVendorPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorPayment_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorPayment_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorPayment_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysMenuLoginId",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    IsAllowed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysMenuLoginId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpSysMenuLoginId_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpSysMenuLoginId_tblErpSysMenuOption_MenuCode",
                        column: x => x.MenuCode,
                        principalTable: "tblErpSysMenuOption",
                        principalColumn: "MenuCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpSysUserBranch",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpSysUserBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpSysUserBranch_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpSysUserBranch_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblOpCustomerVisitForm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ProjectCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReasonCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SupervisorRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustomerRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ActionTerms = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CustomerNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: false),
                    VisitedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ScheduleDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VisitedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    IsInprogress = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblOpCustomerVisitForm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblOpCustomerVisitForm_tblErpSysLogin_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblOpCustomerVisitForm_tblOpReasonCode_ReasonCode",
                        column: x => x.ReasonCode,
                        principalTable: "tblOpReasonCode",
                        principalColumn: "ReasonCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPopTrnGRNHeader",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VenCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "date", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "date", nullable: false),
                    CompCode = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvRefNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RFQContractNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TAXId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxInclusive = table.Column<short>(type: "smallint", nullable: false),
                    PONotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TranBuyer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    TranCreateUserDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranCreateUser = table.Column<int>(type: "int", nullable: false),
                    TranLastEditDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLastEditUser = table.Column<int>(type: "int", nullable: false),
                    TranPostStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranPostDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranpostUser = table.Column<int>(type: "int", nullable: false),
                    TranVoidStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranVoidUser = table.Column<DateTime>(type: "date", nullable: false),
                    TranvoidDate = table.Column<int>(type: "int", nullable: false),
                    TranShipMode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranCurrencyCode = table.Column<int>(type: "int", nullable: false),
                    ExRate = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    TranTotalCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranDiscPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    TranDiscAmount = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    OHCharges = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POClosedDate = table.Column<DateTime>(type: "date", nullable: false),
                    ClosedBy = table.Column<int>(type: "int", nullable: false),
                    ForeClosed = table.Column<bool>(type: "bit", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseRequestNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PurchaseOrderNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WHCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopTrnGRNHeader", x => x.TranNumber);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblErpSysCompany_CompCode",
                        column: x => x.CompCode,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblErpSysCurrencyCode_TranCurrencyCode",
                        column: x => x.TranCurrencyCode,
                        principalTable: "tblErpSysCurrencyCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblErpSysLogin_ClosedBy",
                        column: x => x.ClosedBy,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblErpSysLogin_TranCreateUser",
                        column: x => x.TranCreateUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblErpSysLogin_TranLastEditUser",
                        column: x => x.TranLastEditUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblErpSysLogin_TranpostUser",
                        column: x => x.TranpostUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblErpSysLogin_TranvoidDate",
                        column: x => x.TranvoidDate,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblPopDefVendorPOTermsCode_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "tblPopDefVendorPOTermsCode",
                        principalColumn: "POTermsCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblPopDefVendorShipment_TranShipMode",
                        column: x => x.TranShipMode,
                        principalTable: "tblPopDefVendorShipment",
                        principalColumn: "ShipmentCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNHeader_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPopTrnPurchaseOrderHeader",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VenCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "date", nullable: true),
                    CancelDate = table.Column<DateTime>(type: "date", nullable: false),
                    CompCode = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvRefNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RFQContractNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TAXId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxInclusive = table.Column<short>(type: "smallint", nullable: false),
                    PONotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TranBuyer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    TranCreateUserDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranCreateUser = table.Column<int>(type: "int", nullable: false),
                    TranLastEditDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLastEditUser = table.Column<int>(type: "int", nullable: false),
                    TranPostStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranPostDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranpostUser = table.Column<int>(type: "int", nullable: false),
                    TranVoidStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranVoidUser = table.Column<DateTime>(type: "date", nullable: false),
                    TranvoidDate = table.Column<int>(type: "int", nullable: false),
                    TranShipMode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranCurrencyCode = table.Column<int>(type: "int", nullable: false),
                    ExRate = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    TranTotalCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranDiscPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    TranDiscAmount = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    OHCharges = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POClosedDate = table.Column<DateTime>(type: "date", nullable: false),
                    ClosedBy = table.Column<int>(type: "int", nullable: false),
                    ForeClosed = table.Column<bool>(type: "bit", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseRequestNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PurchaseOrderNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WHCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    ISGRN = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopTrnPurchaseOrderHeader", x => x.TranNumber);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblErpSysCompany_CompCode",
                        column: x => x.CompCode,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblErpSysCurrencyCode_TranCurrencyCode",
                        column: x => x.TranCurrencyCode,
                        principalTable: "tblErpSysCurrencyCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblErpSysLogin_ClosedBy",
                        column: x => x.ClosedBy,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblErpSysLogin_TranCreateUser",
                        column: x => x.TranCreateUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblErpSysLogin_TranLastEditUser",
                        column: x => x.TranLastEditUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblErpSysLogin_TranpostUser",
                        column: x => x.TranpostUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblErpSysLogin_TranvoidDate",
                        column: x => x.TranvoidDate,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblPopDefVendorPOTermsCode_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "tblPopDefVendorPOTermsCode",
                        principalColumn: "POTermsCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblPopDefVendorShipment_TranShipMode",
                        column: x => x.TranShipMode,
                        principalTable: "tblPopDefVendorShipment",
                        principalColumn: "ShipmentCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderHeader_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPopTrnPurchaseReturnHeader",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    VenCatCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "date", nullable: false),
                    CancelDate = table.Column<DateTime>(type: "date", nullable: false),
                    CompCode = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    InvRefNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RFQContractNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DocNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentID = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TAXId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TaxInclusive = table.Column<short>(type: "smallint", nullable: false),
                    PONotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TranBuyer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    TranCreateUserDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranCreateUser = table.Column<int>(type: "int", nullable: false),
                    TranLastEditDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranLastEditUser = table.Column<int>(type: "int", nullable: false),
                    TranPostStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranPostDate = table.Column<DateTime>(type: "date", nullable: false),
                    TranpostUser = table.Column<int>(type: "int", nullable: false),
                    TranVoidStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranVoidUser = table.Column<DateTime>(type: "date", nullable: false),
                    TranvoidDate = table.Column<int>(type: "int", nullable: false),
                    TranShipMode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TranCurrencyCode = table.Column<int>(type: "int", nullable: false),
                    ExRate = table.Column<decimal>(type: "decimal(10,5)", nullable: false),
                    TranTotalCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranDiscPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    TranDiscAmount = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    OHCharges = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    Taxes = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POClosedDate = table.Column<DateTime>(type: "date", nullable: false),
                    ClosedBy = table.Column<int>(type: "int", nullable: false),
                    ForeClosed = table.Column<bool>(type: "bit", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    PurchaseReturnNO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WHCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopTrnPurchaseReturnHeader", x => x.TranNumber);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblErpSysCompany_CompCode",
                        column: x => x.CompCode,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblErpSysCurrencyCode_TranCurrencyCode",
                        column: x => x.TranCurrencyCode,
                        principalTable: "tblErpSysCurrencyCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblErpSysLogin_ClosedBy",
                        column: x => x.ClosedBy,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblErpSysLogin_TranCreateUser",
                        column: x => x.TranCreateUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblErpSysLogin_TranLastEditUser",
                        column: x => x.TranLastEditUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblErpSysLogin_TranpostUser",
                        column: x => x.TranpostUser,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblErpSysLogin_TranvoidDate",
                        column: x => x.TranvoidDate,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblPopDefVendorPOTermsCode_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "tblPopDefVendorPOTermsCode",
                        principalColumn: "POTermsCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblPopDefVendorShipment_TranShipMode",
                        column: x => x.TranShipMode,
                        principalTable: "tblPopDefVendorShipment",
                        principalColumn: "ShipmentCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnHeader_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndAuthorities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AppAuth = table.Column<int>(type: "int", nullable: false),
                    AppLevel = table.Column<short>(type: "smallint", nullable: false),
                    CanCreateSndInvoice = table.Column<bool>(type: "bit", nullable: false),
                    CanEditSndInvoice = table.Column<bool>(type: "bit", nullable: false),
                    CanApproveSndInvoice = table.Column<bool>(type: "bit", nullable: false),
                    CanPostSndInvoice = table.Column<bool>(type: "bit", nullable: false),
                    CanSettleSndInvoice = table.Column<bool>(type: "bit", nullable: false),
                    CanVoidSndInvoice = table.Column<bool>(type: "bit", nullable: false),
                    CanCreateSndQuotation = table.Column<bool>(type: "bit", nullable: false),
                    CanEditSndQuotation = table.Column<bool>(type: "bit", nullable: false),
                    CanApproveSndQuotation = table.Column<bool>(type: "bit", nullable: false),
                    CanConvertSndQuotationToOrder = table.Column<bool>(type: "bit", nullable: false),
                    CanConvertSndQuotationToInvoice = table.Column<bool>(type: "bit", nullable: false),
                    CanReviseSndQuotation = table.Column<bool>(type: "bit", nullable: false),
                    CanVoidSndQuotation = table.Column<bool>(type: "bit", nullable: false),
                    CanConvertSndQuotationToDeliveryNote = table.Column<bool>(type: "bit", nullable: false),
                    CanConvertSndDeliveryNoteToInvoice = table.Column<bool>(type: "bit", nullable: false),
                    IsFinalAuthority = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndAuthorities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndAuthorities_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndAuthorities_tblErpSysLogin_AppAuth",
                        column: x => x.AppAuth,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndTrnApprovals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    ServiceType = table.Column<short>(type: "smallint", nullable: false),
                    ServiceCode = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    AppAuth = table.Column<int>(type: "int", nullable: false),
                    AppRemarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndTrnApprovals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndTrnApprovals_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTrnApprovals_tblErpSysLogin_AppAuth",
                        column: x => x.AppAuth,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinDefBankCheckLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FinPayCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    StChkNum = table.Column<int>(type: "int", nullable: false),
                    EndChkNum = table.Column<int>(type: "int", nullable: false),
                    CheckLeavePrefix = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsUsed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    TranSource = table.Column<string>(type: "char(2)", maxLength: 2, nullable: true),
                    UsedByTranNum = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    UsedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsVoided = table.Column<bool>(type: "bit", nullable: false),
                    VoidedOn = table.Column<DateTime>(type: "date", nullable: true),
                    VoidedBy = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsPDC = table.Column<bool>(type: "bit", nullable: false),
                    CheckDate = table.Column<DateTime>(type: "date", nullable: true),
                    IssuedToName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsBounced = table.Column<bool>(type: "bit", nullable: false),
                    BounceReason = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsCleared = table.Column<bool>(type: "bit", nullable: false),
                    ClearedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinDefBankCheckLeaves", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinDefBankCheckLeaves_tblFinDefAccountlPaycodes_FinPayCode",
                        column: x => x.FinPayCode,
                        principalTable: "tblFinDefAccountlPaycodes",
                        principalColumn: "FinPayCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnBankVoucherApproval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankVoucherId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    AppRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnBankVoucherApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherApproval_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherApproval_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherApproval_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherApproval_tblFinTrnBankVoucher_BankVoucherId",
                        column: x => x.BankVoucherId,
                        principalTable: "tblFinTrnBankVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnBankVoucherItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankVoucherId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    FinAcCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Batch2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CostAllocation = table.Column<int>(type: "int", nullable: true),
                    CostSegCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Payment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnBankVoucherItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherItem_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherItem_tblFinDefMainAccounts_FinAcCode",
                        column: x => x.FinAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherItem_tblFinTrnBankVoucher_BankVoucherId",
                        column: x => x.BankVoucherId,
                        principalTable: "tblFinTrnBankVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnBankVoucherStatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankVoucherId = table.Column<int>(type: "int", nullable: true),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnBankVoucherStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherStatement_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnBankVoucherStatement_tblFinTrnBankVoucher_BankVoucherId",
                        column: x => x.BankVoucherId,
                        principalTable: "tblFinTrnBankVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCashVoucherApproval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashVoucherId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    AppRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCashVoucherApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherApproval_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherApproval_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherApproval_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherApproval_tblFinTrnCashVoucher_CashVoucherId",
                        column: x => x.CashVoucherId,
                        principalTable: "tblFinTrnCashVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCashVoucherItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashVoucherId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    FinAcCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Batch2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CostAllocation = table.Column<int>(type: "int", nullable: true),
                    CostSegCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Payment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCashVoucherItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherItem_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherItem_tblFinDefMainAccounts_FinAcCode",
                        column: x => x.FinAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherItem_tblFinTrnCashVoucher_CashVoucherId",
                        column: x => x.CashVoucherId,
                        principalTable: "tblFinTrnCashVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCashVoucherStatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashVoucherId = table.Column<int>(type: "int", nullable: true),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCashVoucherStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherStatement_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCashVoucherStatement_tblFinTrnCashVoucher_CashVoucherId",
                        column: x => x.CashVoucherId,
                        principalTable: "tblFinTrnCashVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnJournalVoucherApproval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalVoucherId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    AppRemarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnJournalVoucherApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherApproval_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherApproval_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherApproval_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherApproval_tblFinTrnJournalVoucher_JournalVoucherId",
                        column: x => x.JournalVoucherId,
                        principalTable: "tblFinTrnJournalVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnJournalVoucherItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalVoucherId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    FinAcCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Batch = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Batch2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CostAllocation = table.Column<int>(type: "int", nullable: true),
                    CostSegCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnJournalVoucherItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherItem_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherItem_tblFinDefMainAccounts_FinAcCode",
                        column: x => x.FinAcCode,
                        principalTable: "tblFinDefMainAccounts",
                        principalColumn: "FinAcCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherItem_tblFinTrnJournalVoucher_JournalVoucherId",
                        column: x => x.JournalVoucherId,
                        principalTable: "tblFinTrnJournalVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnJournalVoucherStatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JournalVoucherId = table.Column<int>(type: "int", nullable: true),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    JvDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Remarks1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnJournalVoucherStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherStatement_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnJournalVoucherStatement_tblFinTrnJournalVoucher_JournalVoucherId",
                        column: x => x.JournalVoucherId,
                        principalTable: "tblFinTrnJournalVoucher",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpInvItemInventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WHCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    QtyOH = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    QtyOnSalesOrder = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    QtyOnPO = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    QtyReserved = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    ItemAvgCost = table.Column<decimal>(type: "numeric(11,5)", nullable: false),
                    ItemLastPOCost = table.Column<decimal>(type: "numeric(11,5)", nullable: false),
                    ItemLandedCost = table.Column<decimal>(type: "numeric(11,5)", nullable: false),
                    MinQty = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    MaxQty = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    EOQ = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpInvItemInventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemInventory_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemInventory_tblInvDefWarehouse_WHCode",
                        column: x => x.WHCode,
                        principalTable: "tblInvDefWarehouse",
                        principalColumn: "WHCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblErpInvItemInventoryHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WHCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TranType = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    TranNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    TranUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    TranQty = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    unitConvFactor = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranTotQty = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranPrice = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    ItemAvgCost = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranRemarks = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErpInvItemInventoryHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemInventoryHistory_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblErpInvItemInventoryHistory_tblInvDefWarehouse_WHCode",
                        column: x => x.WHCode,
                        principalTable: "tblInvDefWarehouse",
                        principalColumn: "WHCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblInvDefInventoryConfig",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CentralWHCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    AutoGenItemCode = table.Column<bool>(type: "bit", nullable: false),
                    PrefixCatCode = table.Column<bool>(type: "bit", nullable: false),
                    NewItemIndicator = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ItemLength = table.Column<short>(type: "smallint", nullable: false),
                    CategoryLength = table.Column<short>(type: "smallint", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblInvDefInventoryConfig", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblInvDefInventoryConfig_tblInvDefWarehouse_CentralWHCode",
                        column: x => x.CentralWHCode,
                        principalTable: "tblInvDefWarehouse",
                        principalColumn: "WHCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndTranInvoice",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpInvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "date", nullable: true),
                    InvoiceDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    LpoContract = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PaymentTermId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    WarehouseCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    InvoiceStatusId = table.Column<int>(type: "int", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VatPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsCreditConverted = table.Column<bool>(type: "bit", nullable: false),
                    InvoiceStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    InvoiceModule = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    InvoiceNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServiceDate1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FooterDiscount = table.Column<short>(type: "smallint", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsPosted = table.Column<bool>(type: "bit", nullable: false),
                    IsSettled = table.Column<bool>(type: "bit", nullable: false),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeleveryRefNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsQtyDeducted = table.Column<bool>(type: "bit", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CustomerCode = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndTranInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndTranInvoice_tblInvDefWarehouse_WarehouseCode",
                        column: x => x.WarehouseCode,
                        principalTable: "tblInvDefWarehouse",
                        principalColumn: "WHCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTranInvoice_tblSndDefCustomerMaster_CustomerCode",
                        column: x => x.CustomerCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTranInvoice_tblSndDefSalesTermsCode_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "tblSndDefSalesTermsCode",
                        principalColumn: "SalesTermsCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndTranQuotation",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpQuotationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QuotationNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    QuotationRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    QuotationDate = table.Column<DateTime>(type: "date", nullable: true),
                    QuotationDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    LpoContract = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PaymentTermId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    WarehouseCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VatPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    QuotationStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    QuotationModule = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    QuotationNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServiceDate1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FooterDiscount = table.Column<short>(type: "smallint", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertedSndQuotationToDeliveryNote = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertedSndQuotationToOrder = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertedSndQuotationToInvoice = table.Column<bool>(type: "bit", nullable: false),
                    IsRevised = table.Column<bool>(type: "bit", nullable: false),
                    RevisedNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    OriginalQuotationId = table.Column<long>(type: "bigint", nullable: false),
                    IsFinalRevision = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndTranQuotation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndTranQuotation_tblInvDefWarehouse_WarehouseCode",
                        column: x => x.WarehouseCode,
                        principalTable: "tblInvDefWarehouse",
                        principalColumn: "WHCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTranQuotation_tblSndDefSalesTermsCode_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "tblSndDefSalesTermsCode",
                        principalColumn: "SalesTermsCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSchoolTranInvoiceItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CreditMemoId = table.Column<long>(type: "bigint", nullable: true),
                    DebitMemoId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceType = table.Column<short>(type: "smallint", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxTariffPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Discount = table.Column<short>(type: "smallint", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSchoolTranInvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSchoolTranInvoiceItem_tblSchoolTranInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "tblSchoolTranInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCustomerApproval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    AppRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCustomerApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerApproval_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerApproval_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerApproval_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerApproval_tblSndDefCustomerMaster_CustCode",
                        column: x => x.CustCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerApproval_tblTranInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "tblTranInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCustomerInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreditDays = table.Column<short>(type: "smallint", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Remarks1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCustomerInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerInvoice_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerInvoice_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerInvoice_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerInvoice_tblSndDefCustomerMaster_CustCode",
                        column: x => x.CustCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerInvoice_tblTranInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "tblTranInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnCustomerStatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PamentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CheckNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Remarks1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    DrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnCustomerStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerStatement_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerStatement_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerStatement_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerStatement_tblSndDefCustomerMaster_CustCode",
                        column: x => x.CustCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnCustomerStatement_tblTranInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "tblTranInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblTranInvoiceItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CreditMemoId = table.Column<long>(type: "bigint", nullable: true),
                    DebitMemoId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceType = table.Column<short>(type: "smallint", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxTariffPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Discount = table.Column<short>(type: "smallint", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblTranInvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblTranInvoiceItem_tblTranInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "tblTranInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblTranPurcInvoiceItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditId = table.Column<long>(type: "bigint", nullable: true),
                    CreditMemoId = table.Column<long>(type: "bigint", nullable: true),
                    DebitMemoId = table.Column<long>(type: "bigint", nullable: true),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxTariffPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Discount = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTranPurcInvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblTranPurcInvoiceItem_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TblTranPurcInvoiceItem_TblTranPurcInvoice_CreditId",
                        column: x => x.CreditId,
                        principalTable: "TblTranPurcInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnVendorApproval",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    AppRemarks = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnVendorApproval", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorApproval_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorApproval_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorApproval_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorApproval_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorApproval_TblTranVenInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "TblTranVenInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnVendorInvoice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreditDays = table.Column<short>(type: "smallint", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    PaidAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: false),
                    Remarks1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnVendorInvoice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorInvoice_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorInvoice_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorInvoice_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorInvoice_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorInvoice_TblTranVenInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "TblTranVenInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnVendorStatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TranSource = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    TranNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Trantype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PamentCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CheckNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Remarks1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Remarks2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LoginId = table.Column<int>(type: "int", nullable: false),
                    DrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnVendorStatement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorStatement_tblErpSysCompany_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorStatement_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorStatement_tblErpSysLogin_LoginId",
                        column: x => x.LoginId,
                        principalTable: "tblErpSysLogin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorStatement_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnVendorStatement_TblTranVenInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "TblTranVenInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TblTranVenInvoiceItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreditNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreditId = table.Column<long>(type: "bigint", nullable: true),
                    CreditMemoId = table.Column<long>(type: "bigint", nullable: true),
                    DebitMemoId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceType = table.Column<short>(type: "smallint", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxTariffPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Discount = table.Column<short>(type: "smallint", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblTranVenInvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblTranVenInvoiceItem_TblTranVenInvoice_CreditId",
                        column: x => x.CreditId,
                        principalTable: "TblTranVenInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnOpmCustomerPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CustCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    NetAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "date", nullable: true),
                    InvoiceDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Flag1 = table.Column<bool>(type: "bit", nullable: false),
                    Flag2 = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnOpmCustomerPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmCustomerPayment_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmCustomerPayment_tblFinTrnOpmCustomerPaymentHeader_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "tblFinTrnOpmCustomerPaymentHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmCustomerPayment_tblSndDefCustomerMaster_CustCode",
                        column: x => x.CustCode,
                        principalTable: "tblSndDefCustomerMaster",
                        principalColumn: "CustCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmCustomerPayment_tblTranInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "tblTranInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefServiceEnquiries",
                columns: table => new
                {
                    EnquiryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnquiryNumber = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    ServiceCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    UnitCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    UnitQuantity = table.Column<int>(type: "int", nullable: false),
                    ServiceQuantity = table.Column<int>(type: "int", nullable: false),
                    PricePerUnit = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    EstimatedPrice = table.Column<decimal>(type: "decimal(17,3)", nullable: false),
                    StatusEnquiry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SurveyorCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAssignedSurveyor = table.Column<bool>(type: "bit", nullable: false),
                    IsSurveyInProgress = table.Column<bool>(type: "bit", nullable: false),
                    IsSurveyCompleted = table.Column<bool>(type: "bit", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefServiceEnquiries", x => x.EnquiryID);
                    table.ForeignKey(
                        name: "FK_tblSndDefServiceEnquiries_tblSndDefServiceEnquiryHeader_EnquiryNumber",
                        column: x => x.EnquiryNumber,
                        principalTable: "tblSndDefServiceEnquiryHeader",
                        principalColumn: "EnquiryNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefServiceEnquiries_tblSndDefServiceMaster_ServiceCode",
                        column: x => x.ServiceCode,
                        principalTable: "tblSndDefServiceMaster",
                        principalColumn: "ServiceCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefServiceEnquiries_tblSndDefSiteMaster_SiteCode",
                        column: x => x.SiteCode,
                        principalTable: "tblSndDefSiteMaster",
                        principalColumn: "SiteCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDefServiceEnquiries_tblSndDefUnitMaster_UnitCode",
                        column: x => x.UnitCode,
                        principalTable: "tblSndDefUnitMaster",
                        principalColumn: "UnitCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblFinTrnOpmVendorPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    PaymentId = table.Column<int>(type: "int", nullable: false),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TranDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    CrAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    NetAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    BalanceAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AppliedAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DocNum = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "date", nullable: true),
                    InvoiceDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false),
                    Flag1 = table.Column<bool>(type: "bit", nullable: false),
                    Flag2 = table.Column<bool>(type: "bit", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFinTrnOpmVendorPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmVendorPayment_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmVendorPayment_tblFinTrnOpmVendorPaymentHeader_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "tblFinTrnOpmVendorPaymentHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmVendorPayment_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblFinTrnOpmVendorPayment_TblTranVenInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "TblTranVenInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPopTrnGRNDetails",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompCode = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranVendorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ItemTracking = table.Column<short>(type: "smallint", nullable: false),
                    TranItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TranItemName2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TranItemQty = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranItemUnitCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TranUOMFactor = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranItemCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranTotCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    DiscPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    DiscAmt = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ItemTax = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ItemTaxPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POQtyReceived = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POQtyReceiving = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POQtyCancel = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POLineCost1 = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLineCost2 = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POOHCostPerItem = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLandedCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLandedCostPerItem = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranVoidStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranPostStatus = table.Column<bool>(type: "bit", nullable: false),
                    ForeClosed = table.Column<bool>(type: "bit", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    ReceivingQty = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    BalQty = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ReceivedQty = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopTrnGRNDetails", x => x.TranNumber);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNDetails_tblErpInvItemMaster_TranItemCode",
                        column: x => x.TranItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNDetails_tblErpSysCompany_CompCode",
                        column: x => x.CompCode,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNDetails_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNDetails_tblInvDefUOM_TranItemUnitCode",
                        column: x => x.TranItemUnitCode,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNDetails_tblPopTrnPurchaseOrderHeader_TranId",
                        column: x => x.TranId,
                        principalTable: "tblPopTrnPurchaseOrderHeader",
                        principalColumn: "TranNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNDetails_tblSndDefVendorMaster_TranVendorCode",
                        column: x => x.TranVendorCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnGRNDetails_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPopTrnPurchaseOrderDetails",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CompCode = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranVendorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ItemTracking = table.Column<short>(type: "smallint", nullable: false),
                    TranItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TranItemName2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TranItemQty = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranItemUnitCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TranUOMFactor = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranItemCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranTotCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    DiscPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    DiscAmt = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ItemTax = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ItemTaxPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POQtyReceived = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POQtyReceiving = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POQtyCancel = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POLineCost1 = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLineCost2 = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POOHCostPerItem = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLandedCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLandedCostPerItem = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranVoidStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranPostStatus = table.Column<bool>(type: "bit", nullable: false),
                    ForeClosed = table.Column<bool>(type: "bit", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    IsGrn = table.Column<bool>(type: "bit", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopTrnPurchaseOrderDetails", x => x.TranNumber);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderDetails_tblErpInvItemMaster_TranItemCode",
                        column: x => x.TranItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderDetails_tblErpSysCompany_CompCode",
                        column: x => x.CompCode,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderDetails_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderDetails_tblInvDefUOM_TranItemUnitCode",
                        column: x => x.TranItemUnitCode,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderDetails_tblPopTrnPurchaseOrderHeader_TranId",
                        column: x => x.TranId,
                        principalTable: "tblPopTrnPurchaseOrderHeader",
                        principalColumn: "TranNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderDetails_tblSndDefVendorMaster_TranVendorCode",
                        column: x => x.TranVendorCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseOrderDetails_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPopTrnPurchaseReturnDetails",
                columns: table => new
                {
                    TranNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranId = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranDate = table.Column<DateTime>(type: "date", nullable: false),
                    VendCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompCode = table.Column<int>(type: "int", nullable: false),
                    BranchCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranVendorCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ItemTracking = table.Column<short>(type: "smallint", nullable: false),
                    TranItemCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TranItemName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TranItemName2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TranItemQty = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranItemUnitCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    TranUOMFactor = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    TranItemCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranTotCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    DiscPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    DiscAmt = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ItemTax = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ItemTaxPer = table.Column<decimal>(type: "decimal(6,3)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POQtyReceived = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POQtyReceiving = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POQtyCancel = table.Column<decimal>(type: "decimal(12,5)", nullable: false),
                    POLineCost1 = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLineCost2 = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POOHCostPerItem = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLandedCost = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    POLandedCostPerItem = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    TranVoidStatus = table.Column<bool>(type: "bit", nullable: false),
                    TranPostStatus = table.Column<bool>(type: "bit", nullable: false),
                    ForeClosed = table.Column<bool>(type: "bit", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false),
                    ReceivedQty = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ReturnedQty = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    BalQty = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    ReceivingQty = table.Column<decimal>(type: "decimal(12,3)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "date", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblPopTrnPurchaseReturnDetails", x => x.TranNumber);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnDetails_tblErpInvItemMaster_TranItemCode",
                        column: x => x.TranItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnDetails_tblErpSysCompany_CompCode",
                        column: x => x.CompCode,
                        principalTable: "tblErpSysCompany",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnDetails_tblErpSysCompanyBranches_BranchCode",
                        column: x => x.BranchCode,
                        principalTable: "tblErpSysCompanyBranches",
                        principalColumn: "BranchCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnDetails_tblInvDefUOM_TranItemUnitCode",
                        column: x => x.TranItemUnitCode,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnDetails_tblPopTrnPurchaseReturnHeader_TranId",
                        column: x => x.TranId,
                        principalTable: "tblPopTrnPurchaseReturnHeader",
                        principalColumn: "TranNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnDetails_tblSndDefVendorMaster_TranVendorCode",
                        column: x => x.TranVendorCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPopTrnPurchaseReturnDetails_tblSndDefVendorMaster_VendCode",
                        column: x => x.VendCode,
                        principalTable: "tblSndDefVendorMaster",
                        principalColumn: "VendCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndTranInvoiceItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: true),
                    CreditMemoId = table.Column<long>(type: "bigint", nullable: true),
                    DebitMemoId = table.Column<long>(type: "bigint", nullable: true),
                    InvoiceType = table.Column<short>(type: "smallint", nullable: true),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    UnitType = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxTariffPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Discount = table.Column<short>(type: "smallint", nullable: true),
                    SiteCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ItemAvgCost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    NetQuantity = table.Column<decimal>(type: "decimal(18,3)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndTranInvoiceItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndTranInvoiceItem_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTranInvoiceItem_tblInvDefUOM_UnitType",
                        column: x => x.UnitType,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTranInvoiceItem_tblSndTranInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "tblSndTranInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndTranInvoicePaymentSettlements",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceId = table.Column<long>(type: "bigint", nullable: false),
                    PaymentCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    SettledAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SettledDate = table.Column<DateTime>(type: "date", nullable: true),
                    DueDate = table.Column<DateTime>(type: "date", nullable: true),
                    SettledBy = table.Column<long>(type: "bigint", nullable: true),
                    IsPaid = table.Column<bool>(type: "bit", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndTranInvoicePaymentSettlements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndTranInvoicePaymentSettlements_tblFinDefAccountlPaycodes_PaymentCode",
                        column: x => x.PaymentCode,
                        principalTable: "tblFinDefAccountlPaycodes",
                        principalColumn: "FinPayCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTranInvoicePaymentSettlements_tblSndTranInvoice_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "tblSndTranInvoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDeliveryNoteHeader",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpQuotationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QuotationNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TaxIdNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    QuotationRefNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    QuotationDate = table.Column<DateTime>(type: "date", nullable: true),
                    QuotationDueDate = table.Column<DateTime>(type: "date", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    LpoContract = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    PaymentTermId = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    CustomerId = table.Column<long>(type: "bigint", nullable: true),
                    WarehouseCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalPayment = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountDue = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    VatPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    QuotationStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    QuotationModule = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    QuotationNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ServiceDate1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CustName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CustArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FooterDiscount = table.Column<short>(type: "smallint", nullable: false),
                    IsApproved = table.Column<bool>(type: "bit", nullable: false),
                    IsVoid = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertedFromQuotation = table.Column<bool>(type: "bit", nullable: false),
                    IsCovertedFromOrder = table.Column<bool>(type: "bit", nullable: false),
                    IsConvertedDeliveryNoteToInvoice = table.Column<bool>(type: "bit", nullable: false),
                    RevisedNumber = table.Column<byte>(type: "tinyint", nullable: false),
                    OriginalQuotationId = table.Column<long>(type: "bigint", nullable: false),
                    QuotationHeadId = table.Column<long>(type: "bigint", nullable: false),
                    ConvertedBy = table.Column<int>(type: "int", nullable: false),
                    ConvertedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsClosed = table.Column<bool>(type: "bit", nullable: false),
                    DeliveryNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDeliveryNoteHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndDeliveryNoteHeader_tblInvDefWarehouse_WarehouseCode",
                        column: x => x.WarehouseCode,
                        principalTable: "tblInvDefWarehouse",
                        principalColumn: "WHCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDeliveryNoteHeader_tblSndDefSalesTermsCode_PaymentTermId",
                        column: x => x.PaymentTermId,
                        principalTable: "tblSndDefSalesTermsCode",
                        principalColumn: "SalesTermsCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDeliveryNoteHeader_tblSndTranQuotation_QuotationHeadId",
                        column: x => x.QuotationHeadId,
                        principalTable: "tblSndTranQuotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndTranQuotationItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QuotationId = table.Column<long>(type: "bigint", nullable: false),
                    CreditMemoId = table.Column<long>(type: "bigint", nullable: true),
                    DebitMemoId = table.Column<long>(type: "bigint", nullable: true),
                    QuotationType = table.Column<short>(type: "smallint", nullable: true),
                    UnitType = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxTariffPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Discount = table.Column<short>(type: "smallint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndTranQuotationItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndTranQuotationItem_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTranQuotationItem_tblInvDefUOM_UnitType",
                        column: x => x.UnitType,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndTranQuotationItem_tblSndTranQuotation_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "tblSndTranQuotation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDefSurveyFormDataEntry",
                columns: table => new
                {
                    EntryID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnquiryID = table.Column<int>(type: "int", nullable: false),
                    ElementEngName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ElementArbName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ElementType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ListValueString = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MinValue = table.Column<int>(type: "int", nullable: true),
                    MaxValue = table.Column<int>(type: "int", nullable: true),
                    EntryValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDefSurveyFormDataEntry", x => x.EntryID);
                    table.ForeignKey(
                        name: "FK_tblSndDefSurveyFormDataEntry_tblSndDefServiceEnquiries_EnquiryID",
                        column: x => x.EnquiryID,
                        principalTable: "tblSndDefServiceEnquiries",
                        principalColumn: "EnquiryID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblSndDeliveryNoteLine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuotationNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    QuotationId = table.Column<long>(type: "bigint", nullable: true),
                    CreditMemoId = table.Column<long>(type: "bigint", nullable: true),
                    DebitMemoId = table.Column<long>(type: "bigint", nullable: true),
                    QuotationType = table.Column<short>(type: "smallint", nullable: true),
                    UnitType = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    ItemCode = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Quantity = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    SubTotal = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    DiscountAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    AmountBeforeTax = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    IsDefaultConfig = table.Column<bool>(type: "bit", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "date", nullable: true),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TaxTariffPercentage = table.Column<decimal>(type: "decimal(18,3)", nullable: true),
                    Discount = table.Column<short>(type: "smallint", nullable: true),
                    DeliveryNoteId = table.Column<long>(type: "bigint", nullable: false),
                    Delivery = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Delivered = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BackOrder = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DelvFlg1 = table.Column<bool>(type: "bit", nullable: false),
                    DelvFlg2 = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSndDeliveryNoteLine", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblSndDeliveryNoteLine_tblErpInvItemMaster_ItemCode",
                        column: x => x.ItemCode,
                        principalTable: "tblErpInvItemMaster",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDeliveryNoteLine_tblInvDefUOM_UnitType",
                        column: x => x.UnitType,
                        principalTable: "tblInvDefUOM",
                        principalColumn: "UOMCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDeliveryNoteLine_tblSndDeliveryNoteHeader_DeliveryNoteId",
                        column: x => x.DeliveryNoteId,
                        principalTable: "tblSndDeliveryNoteHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblSndDeliveryNoteLine_tblSndDeliveryNoteHeader_QuotationId",
                        column: x => x.QuotationId,
                        principalTable: "tblSndDeliveryNoteHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemInventory_ItemCode",
                table: "tblErpInvItemInventory",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemInventory_WHCode",
                table: "tblErpInvItemInventory",
                column: "WHCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemInventoryHistory_ItemCode",
                table: "tblErpInvItemInventoryHistory",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemInventoryHistory_WHCode",
                table: "tblErpInvItemInventoryHistory",
                column: "WHCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemMaster_ItemBaseUnit",
                table: "tblErpInvItemMaster",
                column: "ItemBaseUnit");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemMaster_ItemCat",
                table: "tblErpInvItemMaster",
                column: "ItemCat");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemMaster_ItemClass",
                table: "tblErpInvItemMaster",
                column: "ItemClass");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemMaster_ItemSubCat",
                table: "tblErpInvItemMaster",
                column: "ItemSubCat");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemMaster_ItemSubClass",
                table: "tblErpInvItemMaster",
                column: "ItemSubClass");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemMaster_ItemTaxCode",
                table: "tblErpInvItemMaster",
                column: "ItemTaxCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemNotes_ItemCode",
                table: "tblErpInvItemNotes",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemsBarcode_ItemCode",
                table: "tblErpInvItemsBarcode",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemsBarcode_ItemUOM",
                table: "tblErpInvItemsBarcode",
                column: "ItemUOM");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemsUOM_ItemCode",
                table: "tblErpInvItemsUOM",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpInvItemsUOM_ItemUOM",
                table: "tblErpInvItemsUOM",
                column: "ItemUOM");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysCityCode_StateCode",
                table: "tblErpSysCityCode",
                column: "StateCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysCompanyBranches_City",
                table: "tblErpSysCompanyBranches",
                column: "City");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysCompanyBranches_CompanyId",
                table: "tblErpSysCompanyBranches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysCompanyBranches_ZoneId",
                table: "tblErpSysCompanyBranches",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysCurrencyCode_CountryCode",
                table: "tblErpSysCurrencyCode",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysLogin_PrimaryBranch",
                table: "tblErpSysLogin",
                column: "PrimaryBranch");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysLogin_UserName",
                table: "tblErpSysLogin",
                column: "UserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysMenuLoginId_LoginId",
                table: "tblErpSysMenuLoginId",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysMenuLoginId_MenuCode",
                table: "tblErpSysMenuLoginId",
                column: "MenuCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysStateCode_CountryCode",
                table: "tblErpSysStateCode",
                column: "CountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysTransactionSequence_BranchCode",
                table: "tblErpSysTransactionSequence",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysTransactionSequence_TransactionCode",
                table: "tblErpSysTransactionSequence",
                column: "TransactionCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysUserBranch_BranchCode",
                table: "tblErpSysUserBranch",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysUserBranch_LoginId",
                table: "tblErpSysUserBranch",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysUserType_UerType",
                table: "tblErpSysUserType",
                column: "UerType",
                unique: true,
                filter: "[UerType] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblErpSysZoneSetting_Code",
                table: "tblErpSysZoneSetting",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefAccountBranches_FinBranchCode",
                table: "tblFinDefAccountBranches",
                column: "FinBranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefAccountBranchMapping_FinBranchCode",
                table: "tblFinDefAccountBranchMapping",
                column: "FinBranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefAccountCategory_FinCatCode",
                table: "tblFinDefAccountCategory",
                column: "FinCatCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefAccountCategory_FinType",
                table: "tblFinDefAccountCategory",
                column: "FinType");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefAccountlPaycodes_FinBranchCode",
                table: "tblFinDefAccountlPaycodes",
                column: "FinBranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefAccountlPaycodes_FinPayAcIntgrAC",
                table: "tblFinDefAccountlPaycodes",
                column: "FinPayAcIntgrAC");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefAccountlPaycodes_FinPayPDCClearAC",
                table: "tblFinDefAccountlPaycodes",
                column: "FinPayPDCClearAC");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefAccountSubCategory_FinCatCode",
                table: "tblFinDefAccountSubCategory",
                column: "FinCatCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefBankCheckLeaves_FinPayCode",
                table: "tblFinDefBankCheckLeaves",
                column: "FinPayCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefBranchesAuthority_FinBranchCode",
                table: "tblFinDefBranchesAuthority",
                column: "FinBranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefBranchesMainAccounts_FinAcCode",
                table: "tblFinDefBranchesMainAccounts",
                column: "FinAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefBranchesMainAccounts_FinBranchCode",
                table: "tblFinDefBranchesMainAccounts",
                column: "FinBranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefCenters_FinCenterCode",
                table: "tblFinDefCenters",
                column: "FinCenterCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinDefMainAccounts_FinAcCode",
                table: "tblFinDefMainAccounts",
                column: "FinAcCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnAccountsLedger_BranchCode",
                table: "tblFinTrnAccountsLedger",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_TblFinTrnAdvanceWallet_BranchCode",
                table: "TblFinTrnAdvanceWallet",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_TblFinTrnAdvanceWallet_CompanyId",
                table: "TblFinTrnAdvanceWallet",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_TblFinTrnAdvanceWallet_CustCode",
                table: "TblFinTrnAdvanceWallet",
                column: "CustCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucher_BranchCode",
                table: "tblFinTrnBankVoucher",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucher_CompanyId",
                table: "tblFinTrnBankVoucher",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucher_VoucherNumber",
                table: "tblFinTrnBankVoucher",
                column: "VoucherNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherApproval_BankVoucherId",
                table: "tblFinTrnBankVoucherApproval",
                column: "BankVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherApproval_BranchCode",
                table: "tblFinTrnBankVoucherApproval",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherApproval_CompanyId",
                table: "tblFinTrnBankVoucherApproval",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherApproval_LoginId",
                table: "tblFinTrnBankVoucherApproval",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherApproval_TranNumber",
                table: "tblFinTrnBankVoucherApproval",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherItem_BankVoucherId",
                table: "tblFinTrnBankVoucherItem",
                column: "BankVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherItem_BranchCode",
                table: "tblFinTrnBankVoucherItem",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherItem_FinAcCode",
                table: "tblFinTrnBankVoucherItem",
                column: "FinAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherStatement_BankVoucherId",
                table: "tblFinTrnBankVoucherStatement",
                column: "BankVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherStatement_LoginId",
                table: "tblFinTrnBankVoucherStatement",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnBankVoucherStatement_TranNumber",
                table: "tblFinTrnBankVoucherStatement",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucher_BranchCode",
                table: "tblFinTrnCashVoucher",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucher_CompanyId",
                table: "tblFinTrnCashVoucher",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucher_VoucherNumber",
                table: "tblFinTrnCashVoucher",
                column: "VoucherNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherApproval_BranchCode",
                table: "tblFinTrnCashVoucherApproval",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherApproval_CashVoucherId",
                table: "tblFinTrnCashVoucherApproval",
                column: "CashVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherApproval_CompanyId",
                table: "tblFinTrnCashVoucherApproval",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherApproval_LoginId",
                table: "tblFinTrnCashVoucherApproval",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherApproval_TranNumber",
                table: "tblFinTrnCashVoucherApproval",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherItem_BranchCode",
                table: "tblFinTrnCashVoucherItem",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherItem_CashVoucherId",
                table: "tblFinTrnCashVoucherItem",
                column: "CashVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherItem_FinAcCode",
                table: "tblFinTrnCashVoucherItem",
                column: "FinAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherStatement_CashVoucherId",
                table: "tblFinTrnCashVoucherStatement",
                column: "CashVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherStatement_LoginId",
                table: "tblFinTrnCashVoucherStatement",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCashVoucherStatement_TranNumber",
                table: "tblFinTrnCashVoucherStatement",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerApproval_BranchCode",
                table: "tblFinTrnCustomerApproval",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerApproval_CompanyId",
                table: "tblFinTrnCustomerApproval",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerApproval_CustCode",
                table: "tblFinTrnCustomerApproval",
                column: "CustCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerApproval_InvoiceId",
                table: "tblFinTrnCustomerApproval",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerApproval_LoginId",
                table: "tblFinTrnCustomerApproval",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerApproval_TranNumber",
                table: "tblFinTrnCustomerApproval",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerInvoice_BranchCode",
                table: "tblFinTrnCustomerInvoice",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerInvoice_CompanyId",
                table: "tblFinTrnCustomerInvoice",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerInvoice_CustCode",
                table: "tblFinTrnCustomerInvoice",
                column: "CustCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerInvoice_InvoiceId",
                table: "tblFinTrnCustomerInvoice",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerInvoice_InvoiceNumber",
                table: "tblFinTrnCustomerInvoice",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerInvoice_LoginId",
                table: "tblFinTrnCustomerInvoice",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerPayment_BranchCode",
                table: "tblFinTrnCustomerPayment",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerPayment_CompanyId",
                table: "tblFinTrnCustomerPayment",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerPayment_CustCode",
                table: "tblFinTrnCustomerPayment",
                column: "CustCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerStatement_BranchCode",
                table: "tblFinTrnCustomerStatement",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerStatement_CompanyId",
                table: "tblFinTrnCustomerStatement",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerStatement_CustCode",
                table: "tblFinTrnCustomerStatement",
                column: "CustCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerStatement_InvoiceId",
                table: "tblFinTrnCustomerStatement",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerStatement_LoginId",
                table: "tblFinTrnCustomerStatement",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnCustomerStatement_TranNumber",
                table: "tblFinTrnCustomerStatement",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnDistribution_FinAcCode",
                table: "tblFinTrnDistribution",
                column: "FinAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucher_BranchCode",
                table: "tblFinTrnJournalVoucher",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucher_CompanyId",
                table: "tblFinTrnJournalVoucher",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucher_VoucherNumber",
                table: "tblFinTrnJournalVoucher",
                column: "VoucherNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherApproval_BranchCode",
                table: "tblFinTrnJournalVoucherApproval",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherApproval_CompanyId",
                table: "tblFinTrnJournalVoucherApproval",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherApproval_JournalVoucherId",
                table: "tblFinTrnJournalVoucherApproval",
                column: "JournalVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherApproval_LoginId",
                table: "tblFinTrnJournalVoucherApproval",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherApproval_TranNumber",
                table: "tblFinTrnJournalVoucherApproval",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherItem_BranchCode",
                table: "tblFinTrnJournalVoucherItem",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherItem_FinAcCode",
                table: "tblFinTrnJournalVoucherItem",
                column: "FinAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherItem_JournalVoucherId",
                table: "tblFinTrnJournalVoucherItem",
                column: "JournalVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherStatement_JournalVoucherId",
                table: "tblFinTrnJournalVoucherStatement",
                column: "JournalVoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherStatement_LoginId",
                table: "tblFinTrnJournalVoucherStatement",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnJournalVoucherStatement_TranNumber",
                table: "tblFinTrnJournalVoucherStatement",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmCustomerPayment_BranchCode",
                table: "tblFinTrnOpmCustomerPayment",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmCustomerPayment_CustCode",
                table: "tblFinTrnOpmCustomerPayment",
                column: "CustCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmCustomerPayment_InvoiceId",
                table: "tblFinTrnOpmCustomerPayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmCustomerPayment_PaymentId",
                table: "tblFinTrnOpmCustomerPayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmCustomerPaymentHeader_BranchCode",
                table: "tblFinTrnOpmCustomerPaymentHeader",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmCustomerPaymentHeader_CompanyId",
                table: "tblFinTrnOpmCustomerPaymentHeader",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmCustomerPaymentHeader_CustCode",
                table: "tblFinTrnOpmCustomerPaymentHeader",
                column: "CustCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmVendorPayment_BranchCode",
                table: "tblFinTrnOpmVendorPayment",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmVendorPayment_InvoiceId",
                table: "tblFinTrnOpmVendorPayment",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmVendorPayment_PaymentId",
                table: "tblFinTrnOpmVendorPayment",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmVendorPayment_VendCode",
                table: "tblFinTrnOpmVendorPayment",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmVendorPaymentHeader_BranchCode",
                table: "tblFinTrnOpmVendorPaymentHeader",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmVendorPaymentHeader_CompanyId",
                table: "tblFinTrnOpmVendorPaymentHeader",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnOpmVendorPaymentHeader_VendCode",
                table: "tblFinTrnOpmVendorPaymentHeader",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorApproval_BranchCode",
                table: "tblFinTrnVendorApproval",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorApproval_CompanyId",
                table: "tblFinTrnVendorApproval",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorApproval_InvoiceId",
                table: "tblFinTrnVendorApproval",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorApproval_LoginId",
                table: "tblFinTrnVendorApproval",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorApproval_TranNumber",
                table: "tblFinTrnVendorApproval",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorApproval_VendCode",
                table: "tblFinTrnVendorApproval",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorInvoice_BranchCode",
                table: "tblFinTrnVendorInvoice",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorInvoice_CompanyId",
                table: "tblFinTrnVendorInvoice",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorInvoice_InvoiceId",
                table: "tblFinTrnVendorInvoice",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorInvoice_InvoiceNumber",
                table: "tblFinTrnVendorInvoice",
                column: "InvoiceNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorInvoice_LoginId",
                table: "tblFinTrnVendorInvoice",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorInvoice_VendCode",
                table: "tblFinTrnVendorInvoice",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorPayment_BranchCode",
                table: "tblFinTrnVendorPayment",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorPayment_CompanyId",
                table: "tblFinTrnVendorPayment",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorPayment_VendCode",
                table: "tblFinTrnVendorPayment",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorStatement_BranchCode",
                table: "tblFinTrnVendorStatement",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorStatement_CompanyId",
                table: "tblFinTrnVendorStatement",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorStatement_InvoiceId",
                table: "tblFinTrnVendorStatement",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorStatement_LoginId",
                table: "tblFinTrnVendorStatement",
                column: "LoginId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorStatement_TranNumber",
                table: "tblFinTrnVendorStatement",
                column: "TranNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblFinTrnVendorStatement_VendCode",
                table: "tblFinTrnVendorStatement",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblIMTransferTransactionHeader_BranchCode",
                table: "tblIMTransferTransactionHeader",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvAdjAc",
                table: "tblInvDefDistributionGroup",
                column: "InvAdjAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvAssetAc",
                table: "tblInvDefDistributionGroup",
                column: "InvAssetAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvCashPOAC",
                table: "tblInvDefDistributionGroup",
                column: "InvCashPOAC");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvCOGSAc",
                table: "tblInvDefDistributionGroup",
                column: "InvCOGSAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvCostCorAc",
                table: "tblInvDefDistributionGroup",
                column: "InvCostCorAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvDefaultAPAc",
                table: "tblInvDefDistributionGroup",
                column: "InvDefaultAPAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvInTransitAc",
                table: "tblInvDefDistributionGroup",
                column: "InvInTransitAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvNonAssetAc",
                table: "tblInvDefDistributionGroup",
                column: "InvNonAssetAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvSalesAc",
                table: "tblInvDefDistributionGroup",
                column: "InvSalesAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvWIPAc",
                table: "tblInvDefDistributionGroup",
                column: "InvWIPAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefDistributionGroup_InvWriteOffAc",
                table: "tblInvDefDistributionGroup",
                column: "InvWriteOffAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefInventoryConfig_CentralWHCode",
                table: "tblInvDefInventoryConfig",
                column: "CentralWHCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefSubCategory_ItemCatCode",
                table: "tblInvDefSubCategory",
                column: "ItemCatCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefWarehouse_InvDistGroup",
                table: "tblInvDefWarehouse",
                column: "InvDistGroup");

            migrationBuilder.CreateIndex(
                name: "IX_tblInvDefWarehouse_WHBranchCode",
                table: "tblInvDefWarehouse",
                column: "WHBranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvAdjAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvAdjAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvAssetAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvAssetAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvCashPOAC",
                table: "tblInventoryDefDistributionGroup",
                column: "InvCashPOAC");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvCOGSAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvCOGSAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvCostCorAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvCostCorAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvDefaultAPAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvDefaultAPAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvInTransitAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvInTransitAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvNonAssetAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvNonAssetAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvSalesAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvSalesAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvWIPAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvWIPAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblInventoryDefDistributionGroup_InvWriteOffAc",
                table: "tblInventoryDefDistributionGroup",
                column: "InvWriteOffAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblOpContractClausesToContractFormMap_ContractFormId",
                table: "tblOpContractClausesToContractFormMap",
                column: "ContractFormId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOpContractTemplateToContractClauseMap_ContractClauseId",
                table: "tblOpContractTemplateToContractClauseMap",
                column: "ContractClauseId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOpContractTemplateToContractClauseMap_ContractTemplateId",
                table: "tblOpContractTemplateToContractClauseMap",
                column: "ContractTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOpCustomerComplaints_ReasonCode",
                table: "tblOpCustomerComplaints",
                column: "ReasonCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblOpCustomerVisitForm_ReasonCode",
                table: "tblOpCustomerVisitForm",
                column: "ReasonCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblOpCustomerVisitForm_SupervisorId",
                table: "tblOpCustomerVisitForm",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblOprTrnApprovals_BranchCode",
                table: "tblOprTrnApprovals",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNDetails_BranchCode",
                table: "tblPopTrnGRNDetails",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNDetails_CompCode",
                table: "tblPopTrnGRNDetails",
                column: "CompCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNDetails_TranId",
                table: "tblPopTrnGRNDetails",
                column: "TranId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNDetails_TranItemCode",
                table: "tblPopTrnGRNDetails",
                column: "TranItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNDetails_TranItemUnitCode",
                table: "tblPopTrnGRNDetails",
                column: "TranItemUnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNDetails_TranVendorCode",
                table: "tblPopTrnGRNDetails",
                column: "TranVendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNDetails_VendCode",
                table: "tblPopTrnGRNDetails",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_BranchCode",
                table: "tblPopTrnGRNHeader",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_ClosedBy",
                table: "tblPopTrnGRNHeader",
                column: "ClosedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_CompCode",
                table: "tblPopTrnGRNHeader",
                column: "CompCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_PaymentID",
                table: "tblPopTrnGRNHeader",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_TranCreateUser",
                table: "tblPopTrnGRNHeader",
                column: "TranCreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_TranCurrencyCode",
                table: "tblPopTrnGRNHeader",
                column: "TranCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_TranLastEditUser",
                table: "tblPopTrnGRNHeader",
                column: "TranLastEditUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_TranpostUser",
                table: "tblPopTrnGRNHeader",
                column: "TranpostUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_TranShipMode",
                table: "tblPopTrnGRNHeader",
                column: "TranShipMode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_TranvoidDate",
                table: "tblPopTrnGRNHeader",
                column: "TranvoidDate");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnGRNHeader_VendCode",
                table: "tblPopTrnGRNHeader",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderDetails_BranchCode",
                table: "tblPopTrnPurchaseOrderDetails",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderDetails_CompCode",
                table: "tblPopTrnPurchaseOrderDetails",
                column: "CompCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderDetails_TranId",
                table: "tblPopTrnPurchaseOrderDetails",
                column: "TranId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderDetails_TranItemCode",
                table: "tblPopTrnPurchaseOrderDetails",
                column: "TranItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderDetails_TranItemUnitCode",
                table: "tblPopTrnPurchaseOrderDetails",
                column: "TranItemUnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderDetails_TranVendorCode",
                table: "tblPopTrnPurchaseOrderDetails",
                column: "TranVendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderDetails_VendCode",
                table: "tblPopTrnPurchaseOrderDetails",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_BranchCode",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_ClosedBy",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "ClosedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_CompCode",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "CompCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_PaymentID",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_TranCreateUser",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "TranCreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_TranCurrencyCode",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "TranCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_TranLastEditUser",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "TranLastEditUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_TranpostUser",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "TranpostUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_TranShipMode",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "TranShipMode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_TranvoidDate",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "TranvoidDate");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseOrderHeader_VendCode",
                table: "tblPopTrnPurchaseOrderHeader",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnDetails_BranchCode",
                table: "tblPopTrnPurchaseReturnDetails",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnDetails_CompCode",
                table: "tblPopTrnPurchaseReturnDetails",
                column: "CompCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnDetails_TranId",
                table: "tblPopTrnPurchaseReturnDetails",
                column: "TranId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnDetails_TranItemCode",
                table: "tblPopTrnPurchaseReturnDetails",
                column: "TranItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnDetails_TranItemUnitCode",
                table: "tblPopTrnPurchaseReturnDetails",
                column: "TranItemUnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnDetails_TranVendorCode",
                table: "tblPopTrnPurchaseReturnDetails",
                column: "TranVendorCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnDetails_VendCode",
                table: "tblPopTrnPurchaseReturnDetails",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_BranchCode",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_ClosedBy",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "ClosedBy");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_CompCode",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "CompCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_PaymentID",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_TranCreateUser",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "TranCreateUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_TranCurrencyCode",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "TranCurrencyCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_TranLastEditUser",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "TranLastEditUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_TranpostUser",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "TranpostUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_TranShipMode",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "TranShipMode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_TranvoidDate",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "TranvoidDate");

            migrationBuilder.CreateIndex(
                name: "IX_tblPopTrnPurchaseReturnHeader_VendCode",
                table: "tblPopTrnPurchaseReturnHeader",
                column: "VendCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblPurTrnApprovals_BranchCode",
                table: "tblPurTrnApprovals",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSchoolTranInvoice_BranchCode",
                table: "tblSchoolTranInvoice",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSchoolTranInvoice_PaymentTerms",
                table: "tblSchoolTranInvoice",
                column: "PaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_tblSchoolTranInvoiceItem_InvoiceId",
                table: "tblSchoolTranInvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndAuthorities_AppAuth",
                table: "tblSndAuthorities",
                column: "AppAuth");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndAuthorities_BranchCode",
                table: "tblSndAuthorities",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_CustARAc",
                table: "tblSndDefCustomerMaster",
                column: "CustARAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_CustArAcCode",
                table: "tblSndDefCustomerMaster",
                column: "CustArAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_CustARAdjAcCode",
                table: "tblSndDefCustomerMaster",
                column: "CustARAdjAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_CustARDiscAcCode",
                table: "tblSndDefCustomerMaster",
                column: "CustARDiscAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_CustCatCode",
                table: "tblSndDefCustomerMaster",
                column: "CustCatCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_CustCityCode1",
                table: "tblSndDefCustomerMaster",
                column: "CustCityCode1");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_CustCityCode2",
                table: "tblSndDefCustomerMaster",
                column: "CustCityCode2");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_CustDefExpAcCode",
                table: "tblSndDefCustomerMaster",
                column: "CustDefExpAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefCustomerMaster_SalesTermsCode",
                table: "tblSndDefCustomerMaster",
                column: "SalesTermsCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefServiceEnquiries_EnquiryNumber",
                table: "tblSndDefServiceEnquiries",
                column: "EnquiryNumber");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefServiceEnquiries_ServiceCode",
                table: "tblSndDefServiceEnquiries",
                column: "ServiceCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefServiceEnquiries_SiteCode",
                table: "tblSndDefServiceEnquiries",
                column: "SiteCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefServiceEnquiries_UnitCode",
                table: "tblSndDefServiceEnquiries",
                column: "UnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefServiceEnquiryHeader_CustomerCode",
                table: "tblSndDefServiceEnquiryHeader",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefServiceMaster_SurveyFormCode",
                table: "tblSndDefServiceMaster",
                column: "SurveyFormCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefServiceUnitMap_UnitCode",
                table: "tblSndDefServiceUnitMap",
                column: "UnitCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefSiteMaster_CustomerCode",
                table: "tblSndDefSiteMaster",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefSiteMaster_SiteCityCode",
                table: "tblSndDefSiteMaster",
                column: "SiteCityCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefSurveyFormDataEntry_EnquiryID",
                table: "tblSndDefSurveyFormDataEntry",
                column: "EnquiryID");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefSurveyFormElementsMapp_FormElementCode",
                table: "tblSndDefSurveyFormElementsMapp",
                column: "FormElementCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefSurveyFormElementsMapp_SurveyFormCode",
                table: "tblSndDefSurveyFormElementsMapp",
                column: "SurveyFormCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefSurveyor_Branch",
                table: "tblSndDefSurveyor",
                column: "Branch");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_PoTermsCode",
                table: "tblSndDefVendorMaster",
                column: "PoTermsCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_VendARAc",
                table: "tblSndDefVendorMaster",
                column: "VendARAc");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_VendArAcCode",
                table: "tblSndDefVendorMaster",
                column: "VendArAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_VendARAdjAcCode",
                table: "tblSndDefVendorMaster",
                column: "VendARAdjAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_VendARDiscAcCode",
                table: "tblSndDefVendorMaster",
                column: "VendARDiscAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_VendCatCode",
                table: "tblSndDefVendorMaster",
                column: "VendCatCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_VendCityCode1",
                table: "tblSndDefVendorMaster",
                column: "VendCityCode1");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_VendCityCode2",
                table: "tblSndDefVendorMaster",
                column: "VendCityCode2");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDefVendorMaster_VendDefExpAcCode",
                table: "tblSndDefVendorMaster",
                column: "VendDefExpAcCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDeliveryNoteHeader_PaymentTermId",
                table: "tblSndDeliveryNoteHeader",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDeliveryNoteHeader_QuotationHeadId",
                table: "tblSndDeliveryNoteHeader",
                column: "QuotationHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDeliveryNoteHeader_WarehouseCode",
                table: "tblSndDeliveryNoteHeader",
                column: "WarehouseCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDeliveryNoteLine_DeliveryNoteId",
                table: "tblSndDeliveryNoteLine",
                column: "DeliveryNoteId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDeliveryNoteLine_ItemCode",
                table: "tblSndDeliveryNoteLine",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDeliveryNoteLine_QuotationId",
                table: "tblSndDeliveryNoteLine",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndDeliveryNoteLine_UnitType",
                table: "tblSndDeliveryNoteLine",
                column: "UnitType");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranInvoice_CustomerCode",
                table: "tblSndTranInvoice",
                column: "CustomerCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranInvoice_PaymentTermId",
                table: "tblSndTranInvoice",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranInvoice_WarehouseCode",
                table: "tblSndTranInvoice",
                column: "WarehouseCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranInvoiceItem_InvoiceId",
                table: "tblSndTranInvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranInvoiceItem_ItemCode",
                table: "tblSndTranInvoiceItem",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranInvoiceItem_UnitType",
                table: "tblSndTranInvoiceItem",
                column: "UnitType");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranInvoicePaymentSettlements_InvoiceId",
                table: "tblSndTranInvoicePaymentSettlements",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranInvoicePaymentSettlements_PaymentCode",
                table: "tblSndTranInvoicePaymentSettlements",
                column: "PaymentCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranQuotation_PaymentTermId",
                table: "tblSndTranQuotation",
                column: "PaymentTermId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranQuotation_WarehouseCode",
                table: "tblSndTranQuotation",
                column: "WarehouseCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranQuotationItem_ItemCode",
                table: "tblSndTranQuotationItem",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranQuotationItem_QuotationId",
                table: "tblSndTranQuotationItem",
                column: "QuotationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTranQuotationItem_UnitType",
                table: "tblSndTranQuotationItem",
                column: "UnitType");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTrnApprovals_AppAuth",
                table: "tblSndTrnApprovals",
                column: "AppAuth");

            migrationBuilder.CreateIndex(
                name: "IX_tblSndTrnApprovals_BranchCode",
                table: "tblSndTrnApprovals",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSysSchoolFeeStructureDetails_FeeCode",
                table: "tblSysSchoolFeeStructureDetails",
                column: "FeeCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSysSchoolFeeStructureDetails_FeeStructCode",
                table: "tblSysSchoolFeeStructureDetails",
                column: "FeeStructCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSysSchoolFeeStructureDetails_TermCode",
                table: "tblSysSchoolFeeStructureDetails",
                column: "TermCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSysSchoolFeeStructureHeader_BranchCode",
                table: "tblSysSchoolFeeStructureHeader",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSysSchoolFeeStructureHeader_GradeCode",
                table: "tblSysSchoolFeeStructureHeader",
                column: "GradeCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSysSchoolFeeStructureHeader_LateFeeCode",
                table: "tblSysSchoolFeeStructureHeader",
                column: "LateFeeCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblSysSchoolNewsMediaLib_NewId",
                table: "tblSysSchoolNewsMediaLib",
                column: "NewId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTranDefProduct_ProductTypeId",
                table: "tblTranDefProduct",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTranDefProduct_UnitTypeId",
                table: "tblTranDefProduct",
                column: "UnitTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblTranInvoice_BranchCode",
                table: "tblTranInvoice",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_tblTranInvoice_PaymentTerms",
                table: "tblTranInvoice",
                column: "PaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_tblTranInvoiceItem_InvoiceId",
                table: "tblTranInvoiceItem",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_TblTranPurcInvoice_BranchCode",
                table: "TblTranPurcInvoice",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_TblTranPurcInvoice_PaymentTerms",
                table: "TblTranPurcInvoice",
                column: "PaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_TblTranPurcInvoiceItem_CreditId",
                table: "TblTranPurcInvoiceItem",
                column: "CreditId");

            migrationBuilder.CreateIndex(
                name: "IX_TblTranPurcInvoiceItem_ItemCode",
                table: "TblTranPurcInvoiceItem",
                column: "ItemCode");

            migrationBuilder.CreateIndex(
                name: "IX_TblTranVenInvoice_BranchCode",
                table: "TblTranVenInvoice",
                column: "BranchCode");

            migrationBuilder.CreateIndex(
                name: "IX_TblTranVenInvoice_PaymentTerms",
                table: "TblTranVenInvoice",
                column: "PaymentTerms");

            migrationBuilder.CreateIndex(
                name: "IX_TblTranVenInvoiceItem_CreditId",
                table: "TblTranVenInvoiceItem",
                column: "CreditId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adjs");

            migrationBuilder.DropTable(
                name: "HRM_DEF_EmployeeShift");

            migrationBuilder.DropTable(
                name: "HRM_DEF_PayrollGroup");

            migrationBuilder.DropTable(
                name: "OP_HRM_TEMP_Project");

            migrationBuilder.DropTable(
                name: "tblAssignDrivers");

            migrationBuilder.DropTable(
                name: "tblAssignRoutes");

            migrationBuilder.DropTable(
                name: "tblDefSchoolExaminationManagementDetails");

            migrationBuilder.DropTable(
                name: "tblDefSchoolExaminationManagementHeader");

            migrationBuilder.DropTable(
                name: "tblDefSchoolStudentMaster");

            migrationBuilder.DropTable(
                name: "tblDefSchoolStudentResultDetails");

            migrationBuilder.DropTable(
                name: "tblDefSchoolStudentResultHeader");

            migrationBuilder.DropTable(
                name: "tblDefSchoolSubjectExamsGrade");

            migrationBuilder.DropTable(
                name: "tblDefSchoolTeacherClassMapping");

            migrationBuilder.DropTable(
                name: "tblDefSchoolTeacherLanguages");

            migrationBuilder.DropTable(
                name: "tblDefSchoolTeacherMaster");

            migrationBuilder.DropTable(
                name: "tblDefSchoolTeacherQualification");

            migrationBuilder.DropTable(
                name: "tblDefSchoolTeacherSubjectsMapping");

            migrationBuilder.DropTable(
                name: "tblDefStudentAcademics");

            migrationBuilder.DropTable(
                name: "tblDefStudentAddress");

            migrationBuilder.DropTable(
                name: "tblDefStudentAttendance");

            migrationBuilder.DropTable(
                name: "tblDefStudentFeeDetails");

            migrationBuilder.DropTable(
                name: "tblDefStudentFeeHeader");

            migrationBuilder.DropTable(
                name: "tblDefStudentGuardiansSiblings");

            migrationBuilder.DropTable(
                name: "tblDefStudentNotices");

            migrationBuilder.DropTable(
                name: "tblDefStudentNoticesReasonCode");

            migrationBuilder.DropTable(
                name: "tblDefStudentPrevEducation");

            migrationBuilder.DropTable(
                name: "tblDriverMaster");

            migrationBuilder.DropTable(
                name: "tblErpFomClientCategory");

            migrationBuilder.DropTable(
                name: "tblErpFomClientMaster");

            migrationBuilder.DropTable(
                name: "tblErpFomCustomerContract");

            migrationBuilder.DropTable(
                name: "tblErpFomDepartment");

            migrationBuilder.DropTable(
                name: "tblErpFomDepartmentTypes");

            migrationBuilder.DropTable(
                name: "tblErpFomSubContractor");

            migrationBuilder.DropTable(
                name: "tblErpFomUserClientLoginMapping");

            migrationBuilder.DropTable(
                name: "tblErpInvItemInventory");

            migrationBuilder.DropTable(
                name: "tblErpInvItemInventoryHistory");

            migrationBuilder.DropTable(
                name: "tblErpInvItemNotes");

            migrationBuilder.DropTable(
                name: "tblErpInvItemsBarcode");

            migrationBuilder.DropTable(
                name: "tblErpInvItemsUOM");

            migrationBuilder.DropTable(
                name: "tblErpSysAcCodeSegment");

            migrationBuilder.DropTable(
                name: "tblErpSysFileUpload");

            migrationBuilder.DropTable(
                name: "tblErpSysIncidentReport");

            migrationBuilder.DropTable(
                name: "tblErpSysMenuLoginId");

            migrationBuilder.DropTable(
                name: "tblErpSysTransactionSequence");

            migrationBuilder.DropTable(
                name: "tblErpSysUserBranch");

            migrationBuilder.DropTable(
                name: "tblErpSysUserSiteMapping");

            migrationBuilder.DropTable(
                name: "tblErpSysUserType");

            migrationBuilder.DropTable(
                name: "tblFinDefAccountBranches");

            migrationBuilder.DropTable(
                name: "tblFinDefAccountBranchMapping");

            migrationBuilder.DropTable(
                name: "tblFinDefAccountSubCategory");

            migrationBuilder.DropTable(
                name: "tblFinDefBankCheckLeaves");

            migrationBuilder.DropTable(
                name: "tblFinDefBranchesAuthority");

            migrationBuilder.DropTable(
                name: "tblFinDefBranchesMainAccounts");

            migrationBuilder.DropTable(
                name: "tblFinDefCenters");

            migrationBuilder.DropTable(
                name: "tblFinSysBatchSetup");

            migrationBuilder.DropTable(
                name: "tblFinSysCostAllocationSetup");

            migrationBuilder.DropTable(
                name: "tblFinSysFinanialSetup");

            migrationBuilder.DropTable(
                name: "tblFinSysSegmentSetup");

            migrationBuilder.DropTable(
                name: "tblFinSysSegmentTwoSetup");

            migrationBuilder.DropTable(
                name: "tblFinTrnAccountsLedger");

            migrationBuilder.DropTable(
                name: "TblFinTrnAdvanceWallet");

            migrationBuilder.DropTable(
                name: "tblFinTrnBankVoucherApproval");

            migrationBuilder.DropTable(
                name: "tblFinTrnBankVoucherItem");

            migrationBuilder.DropTable(
                name: "tblFinTrnBankVoucherStatement");

            migrationBuilder.DropTable(
                name: "tblFinTrnCashVoucherApproval");

            migrationBuilder.DropTable(
                name: "tblFinTrnCashVoucherItem");

            migrationBuilder.DropTable(
                name: "tblFinTrnCashVoucherStatement");

            migrationBuilder.DropTable(
                name: "tblFinTrnCustomerApproval");

            migrationBuilder.DropTable(
                name: "tblFinTrnCustomerInvoice");

            migrationBuilder.DropTable(
                name: "tblFinTrnCustomerPayment");

            migrationBuilder.DropTable(
                name: "tblFinTrnCustomerStatement");

            migrationBuilder.DropTable(
                name: "tblFinTrnCustomerWallet");

            migrationBuilder.DropTable(
                name: "tblFinTrnDistribution");

            migrationBuilder.DropTable(
                name: "tblFinTrnJournalVoucherApproval");

            migrationBuilder.DropTable(
                name: "tblFinTrnJournalVoucherItem");

            migrationBuilder.DropTable(
                name: "tblFinTrnJournalVoucherStatement");

            migrationBuilder.DropTable(
                name: "tblFinTrnOpmCustomerPayment");

            migrationBuilder.DropTable(
                name: "tblFinTrnOpmVendorPayment");

            migrationBuilder.DropTable(
                name: "tblFinTrnOverShortAmount");

            migrationBuilder.DropTable(
                name: "tblFinTrnTrialBalance");

            migrationBuilder.DropTable(
                name: "tblFinTrnVendorApproval");

            migrationBuilder.DropTable(
                name: "tblFinTrnVendorInvoice");

            migrationBuilder.DropTable(
                name: "tblFinTrnVendorPayment");

            migrationBuilder.DropTable(
                name: "tblFinTrnVendorStatement");

            migrationBuilder.DropTable(
                name: "tblHRMSysPosition");

            migrationBuilder.DropTable(
                name: "tblIMAdjustmentsTransactionDetails");

            migrationBuilder.DropTable(
                name: "tblIMAdjustmentsTransactionHeader");

            migrationBuilder.DropTable(
                name: "tblIMReceiptsTransactionDetails");

            migrationBuilder.DropTable(
                name: "tblIMReceiptsTransactionHeader");

            migrationBuilder.DropTable(
                name: "tblIMStockReconciliationTransactionDetails");

            migrationBuilder.DropTable(
                name: "tblIMStockReconciliationTransactionHeader");

            migrationBuilder.DropTable(
                name: "tblIMTransactionDetails");

            migrationBuilder.DropTable(
                name: "tblIMTransactionHeader");

            migrationBuilder.DropTable(
                name: "tblIMTransferTransactionDetails");

            migrationBuilder.DropTable(
                name: "tblIMTransferTransactionHeader");

            migrationBuilder.DropTable(
                name: "tblInvDefInventoryConfig");

            migrationBuilder.DropTable(
                name: "tblInvDefPurchaseConfig");

            migrationBuilder.DropTable(
                name: "tblInvDefSalesConfig");

            migrationBuilder.DropTable(
                name: "tblInvDefTracking");

            migrationBuilder.DropTable(
                name: "tblInvDefType");

            migrationBuilder.DropTable(
                name: "tblInvDefWarehouseTest");

            migrationBuilder.DropTable(
                name: "tblInventoryDefDistributionGroup");

            migrationBuilder.DropTable(
                name: "tblLessonPlanDetails");

            migrationBuilder.DropTable(
                name: "tblLessonPlanHeader");

            migrationBuilder.DropTable(
                name: "tblOpAuthorities");

            migrationBuilder.DropTable(
                name: "tblOpContractClausesToContractFormMap");

            migrationBuilder.DropTable(
                name: "tblOpContractTemplateToContractClauseMap");

            migrationBuilder.DropTable(
                name: "tblOpContractTermsMapToProject");

            migrationBuilder.DropTable(
                name: "tblOpCustomerComplaints");

            migrationBuilder.DropTable(
                name: "tblOpCustomerVisitForm");

            migrationBuilder.DropTable(
                name: "tblOpEmployeeAttendance");

            migrationBuilder.DropTable(
                name: "tblOpEmployeeLeaves");

            migrationBuilder.DropTable(
                name: "tblOpEmployeesToProjectSite");

            migrationBuilder.DropTable(
                name: "tblOpEmployeeToResourceMap");

            migrationBuilder.DropTable(
                name: "tblOpEmployeeTransResign");

            migrationBuilder.DropTable(
                name: "tblOpLogisticsandvehicle");

            migrationBuilder.DropTable(
                name: "tblOpMaterialEquipment");

            migrationBuilder.DropTable(
                name: "tblOpMonthlyRoaster");

            migrationBuilder.DropTable(
                name: "tblOpMonthlyRoasterForSite");

            migrationBuilder.DropTable(
                name: "tblOpOperationExpenseHead");

            migrationBuilder.DropTable(
                name: "tblOpPaymentTermsToProject");

            migrationBuilder.DropTable(
                name: "tblOpProjectBudgetCosting");

            migrationBuilder.DropTable(
                name: "tblOpProjectBudgetEstimation");

            migrationBuilder.DropTable(
                name: "tblOpProjectFinancialExpenseCosting");

            migrationBuilder.DropTable(
                name: "tblOpProjectLogisticsCosting");

            migrationBuilder.DropTable(
                name: "tblOpProjectLogisticsSubCosting");

            migrationBuilder.DropTable(
                name: "tblOpProjectMaterialEquipmentCosting");

            migrationBuilder.DropTable(
                name: "tblOpProjectMaterialEquipmentSubCosting");

            migrationBuilder.DropTable(
                name: "tblOpProjectResourceCosting");

            migrationBuilder.DropTable(
                name: "tblOpProjectResourceSubCosting");

            migrationBuilder.DropTable(
                name: "tblOpProjectSites");

            migrationBuilder.DropTable(
                name: "tblOpProposalCosting");

            migrationBuilder.DropTable(
                name: "tblOpProposalTemplate");

            migrationBuilder.DropTable(
                name: "tblOpPvAddResource");

            migrationBuilder.DropTable(
                name: "tblOpPvAddResourceEmployeeToResourceMap");

            migrationBuilder.DropTable(
                name: "tblOpPvAddResourceReqHead");

            migrationBuilder.DropTable(
                name: "tblOpPvOpenCloseReq");

            migrationBuilder.DropTable(
                name: "tblOpPvRemoveResourceReq");

            migrationBuilder.DropTable(
                name: "tblOpPvReplaceResourceReq");

            migrationBuilder.DropTable(
                name: "tblOpPvSwapEmployeesReq");

            migrationBuilder.DropTable(
                name: "tblOpPvTransferResourceReq");

            migrationBuilder.DropTable(
                name: "tblOpPvTransferWithReplacementReq");

            migrationBuilder.DropTable(
                name: "tblOprTrnApprovals");

            migrationBuilder.DropTable(
                name: "tblOpShiftsPlanForProject");

            migrationBuilder.DropTable(
                name: "tblOpSkillset");

            migrationBuilder.DropTable(
                name: "tblOpSkillsetPlanForProject");

            migrationBuilder.DropTable(
                name: "tblParentAddRequest");

            migrationBuilder.DropTable(
                name: "tblParentMyGallery");

            migrationBuilder.DropTable(
                name: "tblPopTrnGRNDetails");

            migrationBuilder.DropTable(
                name: "tblPopTrnGRNHeader");

            migrationBuilder.DropTable(
                name: "tblPopTrnPurchaseOrderDetails");

            migrationBuilder.DropTable(
                name: "tblPopTrnPurchaseReturnDetails");

            migrationBuilder.DropTable(
                name: "tblPurAuthorities");

            migrationBuilder.DropTable(
                name: "tblPurTrnApprovals");

            migrationBuilder.DropTable(
                name: "tblRouteMaster");

            migrationBuilder.DropTable(
                name: "tblRoutePlanDetails");

            migrationBuilder.DropTable(
                name: "tblRoutePlanHeader");

            migrationBuilder.DropTable(
                name: "tblSchoolMessages");

            migrationBuilder.DropTable(
                name: "tblSchoolTranInvoiceItem");

            migrationBuilder.DropTable(
                name: "tblSequenceNumberSetting");

            migrationBuilder.DropTable(
                name: "tblServiceCode");

            migrationBuilder.DropTable(
                name: "tblServiceProvider");

            migrationBuilder.DropTable(
                name: "tblSndAuthorities");

            migrationBuilder.DropTable(
                name: "tblSndDefSalesShipment");

            migrationBuilder.DropTable(
                name: "tblSndDefServiceUnitMap");

            migrationBuilder.DropTable(
                name: "tblSndDefSurveyFormDataEntry");

            migrationBuilder.DropTable(
                name: "tblSndDefSurveyFormElementsMapp");

            migrationBuilder.DropTable(
                name: "tblSndDefSurveyor");

            migrationBuilder.DropTable(
                name: "tblSndDeliveryNoteLine");

            migrationBuilder.DropTable(
                name: "tblSndTranInvoiceItem");

            migrationBuilder.DropTable(
                name: "tblSndTranInvoicePaymentSettlements");

            migrationBuilder.DropTable(
                name: "tblSndTranQuotationItem");

            migrationBuilder.DropTable(
                name: "tblSndTrnApprovals");

            migrationBuilder.DropTable(
                name: "tblStudentAttnRegDetails");

            migrationBuilder.DropTable(
                name: "TblStudentAttnRegHeader");

            migrationBuilder.DropTable(
                name: "tblStudentHomeWork");

            migrationBuilder.DropTable(
                name: "tblSysNotificaticationTemplate");

            migrationBuilder.DropTable(
                name: "tblSysSchoolAcademicBatches");

            migrationBuilder.DropTable(
                name: "tblSysSchoolAcademicsSubects");

            migrationBuilder.DropTable(
                name: "tblSysSchoolBranchesAuthority");

            migrationBuilder.DropTable(
                name: "tblSysSchoolExaminationTypes");

            migrationBuilder.DropTable(
                name: "tblSysSchoolFeeStructureDetails");

            migrationBuilder.DropTable(
                name: "tblSysSchoolGender");

            migrationBuilder.DropTable(
                name: "tblSysSchoolGradeSectionMapping");

            migrationBuilder.DropTable(
                name: "tblSysSchoolGradeSubjectMapping");

            migrationBuilder.DropTable(
                name: "tblSysSchoolHolidayCalanderStudent");

            migrationBuilder.DropTable(
                name: "tblSysSchoolLanguages");

            migrationBuilder.DropTable(
                name: "tblSysSchoolNationality");

            migrationBuilder.DropTable(
                name: "tblSysSchoolNewsMediaLib");

            migrationBuilder.DropTable(
                name: "tblSysSchoolNotificationFilters");

            migrationBuilder.DropTable(
                name: "tblSysSchoolNotifications");

            migrationBuilder.DropTable(
                name: "tblSysSchoolPayTypes");

            migrationBuilder.DropTable(
                name: "tblSysSchoolPETCategory");

            migrationBuilder.DropTable(
                name: "tblSysSchoolPushNotificationParent");

            migrationBuilder.DropTable(
                name: "tblSysSchoolReligion");

            migrationBuilder.DropTable(
                name: "tblSysSchoolSchedule");

            migrationBuilder.DropTable(
                name: "tblSysSchoolSectionsSection");

            migrationBuilder.DropTable(
                name: "TblSysSchoolSemister");

            migrationBuilder.DropTable(
                name: "tblSysSchoolStuLeaveType");

            migrationBuilder.DropTable(
                name: "tblSysSchooScheduleEvents");

            migrationBuilder.DropTable(
                name: "tblTranDefProduct");

            migrationBuilder.DropTable(
                name: "tblTranDefTax");

            migrationBuilder.DropTable(
                name: "tblTranFeeTransaction");

            migrationBuilder.DropTable(
                name: "tblTranInvoiceItem");

            migrationBuilder.DropTable(
                name: "TblTranPurcInvoiceItem");

            migrationBuilder.DropTable(
                name: "TblTranVenInvoiceItem");

            migrationBuilder.DropTable(
                name: "tblVehicleBrandMaster");

            migrationBuilder.DropTable(
                name: "tblVehicleCompanyMaster");

            migrationBuilder.DropTable(
                name: "tblVehicleFuelEntry");

            migrationBuilder.DropTable(
                name: "tblVehicleMaster");

            migrationBuilder.DropTable(
                name: "tblVehicleTypeMaster");

            migrationBuilder.DropTable(
                name: "TblWebStudentRegistration");

            migrationBuilder.DropTable(
                name: "tblErpSysMenuOption");

            migrationBuilder.DropTable(
                name: "tblErpSysTransactionCodes");

            migrationBuilder.DropTable(
                name: "tblFinDefAccountCategory");

            migrationBuilder.DropTable(
                name: "tblFinTrnBankVoucher");

            migrationBuilder.DropTable(
                name: "tblFinTrnCashVoucher");

            migrationBuilder.DropTable(
                name: "tblFinTrnJournalVoucher");

            migrationBuilder.DropTable(
                name: "tblFinTrnOpmCustomerPaymentHeader");

            migrationBuilder.DropTable(
                name: "tblFinTrnOpmVendorPaymentHeader");

            migrationBuilder.DropTable(
                name: "tblOpContractFormHead");

            migrationBuilder.DropTable(
                name: "tblOpContractClause");

            migrationBuilder.DropTable(
                name: "tblOpContractTemplate");

            migrationBuilder.DropTable(
                name: "tblOpReasonCode");

            migrationBuilder.DropTable(
                name: "tblPopTrnPurchaseOrderHeader");

            migrationBuilder.DropTable(
                name: "tblPopTrnPurchaseReturnHeader");

            migrationBuilder.DropTable(
                name: "tblSchoolTranInvoice");

            migrationBuilder.DropTable(
                name: "tblSndDefServiceEnquiries");

            migrationBuilder.DropTable(
                name: "tblSndDefSurveyFormElement");

            migrationBuilder.DropTable(
                name: "tblSndDeliveryNoteHeader");

            migrationBuilder.DropTable(
                name: "tblFinDefAccountlPaycodes");

            migrationBuilder.DropTable(
                name: "tblSndTranInvoice");

            migrationBuilder.DropTable(
                name: "tblSysSchoolFeeStructureHeader");

            migrationBuilder.DropTable(
                name: "tblSysSchoolFeeTerms");

            migrationBuilder.DropTable(
                name: "tblSysSchoolNews");

            migrationBuilder.DropTable(
                name: "tblTranDefProductType");

            migrationBuilder.DropTable(
                name: "tblTranDefUnitType");

            migrationBuilder.DropTable(
                name: "tblTranInvoice");

            migrationBuilder.DropTable(
                name: "tblErpInvItemMaster");

            migrationBuilder.DropTable(
                name: "TblTranPurcInvoice");

            migrationBuilder.DropTable(
                name: "TblTranVenInvoice");

            migrationBuilder.DropTable(
                name: "tblFinSysAccountType");

            migrationBuilder.DropTable(
                name: "tblErpSysCurrencyCode");

            migrationBuilder.DropTable(
                name: "tblErpSysLogin");

            migrationBuilder.DropTable(
                name: "tblPopDefVendorShipment");

            migrationBuilder.DropTable(
                name: "tblSndDefVendorMaster");

            migrationBuilder.DropTable(
                name: "tblSndDefServiceEnquiryHeader");

            migrationBuilder.DropTable(
                name: "tblSndDefServiceMaster");

            migrationBuilder.DropTable(
                name: "tblSndDefSiteMaster");

            migrationBuilder.DropTable(
                name: "tblSndDefUnitMaster");

            migrationBuilder.DropTable(
                name: "tblSndTranQuotation");

            migrationBuilder.DropTable(
                name: "tblSysSchoolAcedemicClassGrade");

            migrationBuilder.DropTable(
                name: "tblSysSchoolBranches");

            migrationBuilder.DropTable(
                name: "tblSysSchoolFeeType");

            migrationBuilder.DropTable(
                name: "tblErpSysSystemTaxes");

            migrationBuilder.DropTable(
                name: "tblInvDefClass");

            migrationBuilder.DropTable(
                name: "tblInvDefSubCategory");

            migrationBuilder.DropTable(
                name: "tblInvDefSubClass");

            migrationBuilder.DropTable(
                name: "tblInvDefUOM");

            migrationBuilder.DropTable(
                name: "tblPopDefVendorCategory");

            migrationBuilder.DropTable(
                name: "tblPopDefVendorPOTermsCode");

            migrationBuilder.DropTable(
                name: "tblSndDefSurveyFormHead");

            migrationBuilder.DropTable(
                name: "tblSndDefCustomerMaster");

            migrationBuilder.DropTable(
                name: "tblInvDefWarehouse");

            migrationBuilder.DropTable(
                name: "tblInvDefCategory");

            migrationBuilder.DropTable(
                name: "tblSndDefCustomerCategory");

            migrationBuilder.DropTable(
                name: "tblSndDefSalesTermsCode");

            migrationBuilder.DropTable(
                name: "tblErpSysCompanyBranches");

            migrationBuilder.DropTable(
                name: "tblInvDefDistributionGroup");

            migrationBuilder.DropTable(
                name: "tblErpSysCityCode");

            migrationBuilder.DropTable(
                name: "tblErpSysCompany");

            migrationBuilder.DropTable(
                name: "tblErpSysZoneSetting");

            migrationBuilder.DropTable(
                name: "tblFinDefMainAccounts");

            migrationBuilder.DropTable(
                name: "tblErpSysStateCode");

            migrationBuilder.DropTable(
                name: "tblErpSysCountryCode");
        }
    }
}
