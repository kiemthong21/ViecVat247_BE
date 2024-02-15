using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussinessObject.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    Cid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cemail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Descrition = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Epoint = table.Column<long>(type: "bigint", nullable: true),
                    CV = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Voting = table.Column<double>(type: "float", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: true),
                    VerifyCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FrofileStatus = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Cid);
                });

            migrationBuilder.CreateTable(
                name: "JobsCategory",
                columns: table => new
                {
                    JobCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobCategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobsCategory", x => x.JobCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    RoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NumberCustommer = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "SkillCategory",
                columns: table => new
                {
                    SkillCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillCategoryName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCategory", x => x.SkillCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TypeManager",
                columns: table => new
                {
                    TypeManagerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeManagerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeManager", x => x.TypeManagerId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Uid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Uemail = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Role = table.Column<int>(type: "int", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Avatar = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    JobsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobAssignerId = table.Column<int>(type: "int", nullable: true),
                    JobCategoryId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Image = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Job_Overview = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Required_Skills = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Preferred_Skills = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NoticeToJobSeeker = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkingTime = table.Column<double>(type: "float", nullable: true),
                    Money = table.Column<double>(type: "float", nullable: true),
                    NumberPerson = table.Column<int>(type: "int", nullable: true),
                    TypeJobs = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    IsSendMail = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.JobsId);
                    table.ForeignKey(
                        name: "FK_Job_Customer_JobAssignerId",
                        column: x => x.JobAssignerId,
                        principalTable: "Customer",
                        principalColumn: "Cid");
                    table.ForeignKey(
                        name: "FK_Job_JobsCategory_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobsCategory",
                        principalColumn: "JobCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerRoom",
                columns: table => new
                {
                    CustomerRoomId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerRoom", x => x.CustomerRoomId);
                    table.ForeignKey(
                        name: "FK_CustomerRoom_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Cid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerRoom_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: true),
                    MessageContent = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    MessageType = table.Column<int>(type: "int", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_Customer_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Customer",
                        principalColumn: "Cid");
                    table.ForeignKey(
                        name: "FK_Message_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "RoomId");
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillCategoryId = table.Column<int>(type: "int", nullable: true),
                    SkillName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    TypeSkill = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skill_SkillCategory_SkillCategoryId",
                        column: x => x.SkillCategoryId,
                        principalTable: "SkillCategory",
                        principalColumn: "SkillCategoryId");
                });

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cid = table.Column<int>(type: "int", nullable: true),
                    Uid = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeFeedback = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Feedback = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Report_Customer_Cid",
                        column: x => x.Cid,
                        principalTable: "Customer",
                        principalColumn: "Cid");
                    table.ForeignKey(
                        name: "FK_Report_User_Uid",
                        column: x => x.Uid,
                        principalTable: "User",
                        principalColumn: "Uid");
                });

            migrationBuilder.CreateTable(
                name: "TypeManagerUser",
                columns: table => new
                {
                    TypeManagerUserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeManagerId = table.Column<int>(type: "int", nullable: false),
                    Uid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeManagerUser", x => x.TypeManagerUserId);
                    table.ForeignKey(
                        name: "FK_TypeManagerUser_TypeManager_TypeManagerId",
                        column: x => x.TypeManagerId,
                        principalTable: "TypeManager",
                        principalColumn: "TypeManagerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TypeManagerUser_User_Uid",
                        column: x => x.Uid,
                        principalTable: "User",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    Aid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    Feedback = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    ApplicationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Money = table.Column<double>(type: "float", nullable: true),
                    Voting = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Aid);
                    table.ForeignKey(
                        name: "FK_Application_Customer_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Customer",
                        principalColumn: "Cid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Application_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true),
                    Aid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Cid");
                    table.ForeignKey(
                        name: "FK_Notification_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobsId");
                });

            migrationBuilder.CreateTable(
                name: "ReasonForEditingJobStatus",
                columns: table => new
                {
                    Rid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NumberProcessingTime = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReasonForEditingJobStatus", x => x.Rid);
                    table.ForeignKey(
                        name: "FK_ReasonForEditingJobStatus_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReasonForEditingJobStatus_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    ReceiverId = table.Column<int>(type: "int", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: true),
                    Epoint = table.Column<long>(type: "bigint", nullable: true),
                    OldBalance = table.Column<long>(type: "bigint", nullable: true),
                    NewBalance = table.Column<long>(type: "bigint", nullable: true),
                    Detail = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    Paymentdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(1255)", maxLength: 1255, nullable: true),
                    TransactionType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BankCode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BankTranNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TransactionNo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TransactionStatus = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    TxnRef = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SecureHash = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_Transaction_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Cid");
                    table.ForeignKey(
                        name: "FK_Transaction_Customer_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "Customer",
                        principalColumn: "Cid");
                    table.ForeignKey(
                        name: "FK_Transaction_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobsId");
                });

            migrationBuilder.CreateTable(
                name: "CustomerSkill",
                columns: table => new
                {
                    CustomerSkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSkill", x => x.CustomerSkillId);
                    table.ForeignKey(
                        name: "FK_CustomerSkill_Customer_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Cid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CustomerSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobsSkill",
                columns: table => new
                {
                    JobsSkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillId = table.Column<int>(type: "int", nullable: false),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobsSkill", x => x.JobsSkillId);
                    table.ForeignKey(
                        name: "FK_JobsSkill_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "JobsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobsSkill_Skill_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skill",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReportImage",
                columns: table => new
                {
                    ReportImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportId = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    ImageType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportImage", x => x.ReportImageId);
                    table.ForeignKey(
                        name: "FK_ReportImage_Report_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Report",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_ApplicantId",
                table: "Application",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_JobId",
                table: "Application",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRoom_CustomerId",
                table: "CustomerRoom",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerRoom_RoomId",
                table: "CustomerRoom",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSkill_CustomerId",
                table: "CustomerSkill",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSkill_SkillId",
                table: "CustomerSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobAssignerId",
                table: "Job",
                column: "JobAssignerId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_JobCategoryId",
                table: "Job",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobsSkill_JobId",
                table: "JobsSkill",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobsSkill_SkillId",
                table: "JobsSkill",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_RoomId",
                table: "Message",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Message_SenderId",
                table: "Message",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CustomerId",
                table: "Notification",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_JobId",
                table: "Notification",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonForEditingJobStatus_JobId",
                table: "ReasonForEditingJobStatus",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ReasonForEditingJobStatus_UserId",
                table: "ReasonForEditingJobStatus",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Cid",
                table: "Report",
                column: "Cid");

            migrationBuilder.CreateIndex(
                name: "IX_Report_Uid",
                table: "Report",
                column: "Uid");

            migrationBuilder.CreateIndex(
                name: "IX_ReportImage_ReportId",
                table: "ReportImage",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_SkillCategoryId",
                table: "Skill",
                column: "SkillCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_CustomerId",
                table: "Transaction",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_JobId",
                table: "Transaction",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ReceiverId",
                table: "Transaction",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeManagerUser_TypeManagerId",
                table: "TypeManagerUser",
                column: "TypeManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_TypeManagerUser_Uid",
                table: "TypeManagerUser",
                column: "Uid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropTable(
                name: "CustomerRoom");

            migrationBuilder.DropTable(
                name: "CustomerSkill");

            migrationBuilder.DropTable(
                name: "JobsSkill");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "ReasonForEditingJobStatus");

            migrationBuilder.DropTable(
                name: "ReportImage");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "TypeManagerUser");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Room");

            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "TypeManager");

            migrationBuilder.DropTable(
                name: "SkillCategory");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "JobsCategory");
        }
    }
}
