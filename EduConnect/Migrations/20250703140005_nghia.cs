using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EduConnect.Migrations
{
    /// <inheritdoc />
    public partial class nghia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    messageID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    responseText = table.Column<string>(type: "text", nullable: true),
                    messageText = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Message__4808B873C2F34A8E", x => x.messageID);
                });

            migrationBuilder.CreateTable(
                name: "SchoolYear",
                columns: table => new
                {
                    schoolYearID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYear", x => x.schoolYearID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    passwordHash = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__CB9A1CDFEE67CF80", x => x.userID);
                });

            migrationBuilder.CreateTable(
                name: "Semester",
                columns: table => new
                {
                    semeID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    startDate = table.Column<DateOnly>(type: "date", nullable: true),
                    endDate = table.Column<DateOnly>(type: "date", nullable: true),
                    semesterName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    schoolYearID = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Semester", x => x.semeID);
                    table.ForeignKey(
                        name: "FK_Semester_SchoolYear",
                        column: x => x.schoolYearID,
                        principalTable: "SchoolYear",
                        principalColumn: "schoolYearID");
                });

            migrationBuilder.CreateTable(
                name: "Parent",
                columns: table => new
                {
                    parentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Parent__90658CB8D9406FDE", x => x.parentID);
                    table.ForeignKey(
                        name: "FK__Parent__userID__3E52440B",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Subject",
                columns: table => new
                {
                    subjectID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    subjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    semeID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Subject__ACF9A740AF98EE05", x => x.subjectID);
                    table.ForeignKey(
                        name: "FK__Subject__termID__3B75D760",
                        column: x => x.semeID,
                        principalTable: "Semester",
                        principalColumn: "semeID");
                });

            migrationBuilder.CreateTable(
                name: "ChatBotLog",
                columns: table => new
                {
                    chatID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    messageID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    parentID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime", nullable: true),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ChatBotL__19BDBDB3C0C4419E", x => x.chatID);
                    table.ForeignKey(
                        name: "FK__ChatBotLo__messa__6383C8BA",
                        column: x => x.messageID,
                        principalTable: "Message",
                        principalColumn: "messageID");
                    table.ForeignKey(
                        name: "FK__ChatBotLo__paren__656C112C",
                        column: x => x.parentID,
                        principalTable: "Parent",
                        principalColumn: "parentID");
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    teacherID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    userID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    subjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    fcm_token = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    platform = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teacher", x => x.teacherID);
                    table.ForeignKey(
                        name: "FK_Teacher_Subject",
                        column: x => x.subjectID,
                        principalTable: "Subject",
                        principalColumn: "subjectID");
                    table.ForeignKey(
                        name: "FK_Teacher_User",
                        column: x => x.userID,
                        principalTable: "Users",
                        principalColumn: "userID");
                });

            migrationBuilder.CreateTable(
                name: "Classroom",
                columns: table => new
                {
                    classID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    className = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    teacherID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classroom", x => x.classID);
                    table.ForeignKey(
                        name: "FK_Classroom_Teacher",
                        column: x => x.teacherID,
                        principalTable: "Teacher",
                        principalColumn: "teacherID");
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    courseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    classID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    teacherID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    semeID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    dayOfWeek = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    status = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    subjectName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Course__2AA84FF184342A62", x => x.courseID);
                    table.ForeignKey(
                        name: "FK__Course__classID__4F7CD00D",
                        column: x => x.classID,
                        principalTable: "Classroom",
                        principalColumn: "classID");
                    table.ForeignKey(
                        name: "FK__Course__teacherI__5070F446",
                        column: x => x.teacherID,
                        principalTable: "Teacher",
                        principalColumn: "teacherID");
                    table.ForeignKey(
                        name: "FK__Course__termID__5165187F",
                        column: x => x.semeID,
                        principalTable: "Semester",
                        principalColumn: "semeID");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    teacherID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    termID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    classID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    teacherName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    className = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Report__04C97FDB86006B0D", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK__Report__class__59063A47",
                        column: x => x.classID,
                        principalTable: "Classroom",
                        principalColumn: "classID");
                    table.ForeignKey(
                        name: "FK__Report__teach__5812160E",
                        column: x => x.teacherID,
                        principalTable: "Teacher",
                        principalColumn: "teacherID");
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    studentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    parentID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    dateOfBirth = table.Column<DateOnly>(type: "date", nullable: true),
                    fullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    classID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Student__4D11D65C2495EB66", x => x.studentID);
                    table.ForeignKey(
                        name: "FK_Student_Classroom",
                        column: x => x.classID,
                        principalTable: "Classroom",
                        principalColumn: "classID");
                    table.ForeignKey(
                        name: "FK__Student__parentI__412EB0B6",
                        column: x => x.parentID,
                        principalTable: "Parent",
                        principalColumn: "parentID");
                });

            migrationBuilder.CreateTable(
                name: "Term",
                columns: table => new
                {
                    termID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    mode = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReportId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term", x => x.termID);
                    table.ForeignKey(
                        name: "FK_Term_Report",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "ReportId");
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    studentID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    courseID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    participation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    note = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    homework = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    focus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.id);
                    table.ForeignKey(
                        name: "FK__Attendanc__cours__5535A963",
                        column: x => x.courseID,
                        principalTable: "Course",
                        principalColumn: "courseID");
                    table.ForeignKey(
                        name: "FK__Attendanc__stude__5441852A",
                        column: x => x.studentID,
                        principalTable: "Student",
                        principalColumn: "studentID");
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    scoreID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    subjectID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    studentID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    score = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Score__B56A0D6D900EDC0A", x => x.scoreID);
                    table.ForeignKey(
                        name: "FK_Score_Student",
                        column: x => x.studentID,
                        principalTable: "Student",
                        principalColumn: "studentID");
                    table.ForeignKey(
                        name: "FK__Score__subjectID__4CA06362",
                        column: x => x.subjectID,
                        principalTable: "Subject",
                        principalColumn: "subjectID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_courseID",
                table: "Attendance",
                column: "courseID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_studentID",
                table: "Attendance",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBotLog_messageID",
                table: "ChatBotLog",
                column: "messageID");

            migrationBuilder.CreateIndex(
                name: "IX_ChatBotLog_parentID",
                table: "ChatBotLog",
                column: "parentID");

            migrationBuilder.CreateIndex(
                name: "IX_Classroom_teacherID",
                table: "Classroom",
                column: "teacherID",
                unique: true,
                filter: "[teacherID] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Course_classID",
                table: "Course",
                column: "classID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_semeID",
                table: "Course",
                column: "semeID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_teacherID",
                table: "Course",
                column: "teacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Parent_userID",
                table: "Parent",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_classID",
                table: "Report",
                column: "classID");

            migrationBuilder.CreateIndex(
                name: "IX_Report_teacherID",
                table: "Report",
                column: "teacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Score_studentID",
                table: "Score",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_Score_subjectID",
                table: "Score",
                column: "subjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Semester_schoolYearID",
                table: "Semester",
                column: "schoolYearID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_classID",
                table: "Student",
                column: "classID");

            migrationBuilder.CreateIndex(
                name: "IX_Student_parentID",
                table: "Student",
                column: "parentID");

            migrationBuilder.CreateIndex(
                name: "IX_Subject_semeID",
                table: "Subject",
                column: "semeID");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_subjectID",
                table: "Teacher",
                column: "subjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_userID",
                table: "Teacher",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Term_ReportId",
                table: "Term",
                column: "ReportId",
                unique: true,
                filter: "[ReportId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "ChatBotLog");

            migrationBuilder.DropTable(
                name: "Score");

            migrationBuilder.DropTable(
                name: "Term");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Parent");

            migrationBuilder.DropTable(
                name: "Classroom");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Subject");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Semester");

            migrationBuilder.DropTable(
                name: "SchoolYear");
        }
    }
}
