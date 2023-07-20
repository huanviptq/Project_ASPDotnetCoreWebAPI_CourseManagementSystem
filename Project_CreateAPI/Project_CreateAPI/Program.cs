using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.ModelBuilder;
using Project_CreateAPI.Models;
using Project_CreateAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddOData(option =>
{
    option.Filter().Select().OrderBy().Expand().Count();
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IStudentAssignmentRepository, StudentAssignmentRepository>();
builder.Services.AddScoped<IQuizRepository, QuizRepository>();
builder.Services.AddScoped<IQuestionRepository, QuestionRepository>();
builder.Services.AddScoped<IQuizAttendanceRepository, QuizAttendanceRepository>();
builder.Services.AddScoped<CMSContext>();
builder.Services.AddDbContext<CMSContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
