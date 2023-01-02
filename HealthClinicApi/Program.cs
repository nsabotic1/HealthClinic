using HealthClinicApi.Data;
using HealthClinicApi.Helpers;
using HealthClinicApi.Services;
using HealthClinicApi.Services.AdmissionRecordService;
using HealthClinicApi.Services.DoctorService;
using HealthClinicApi.Services.MedicalFindingRecordService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IAdmissionRecordService, AdmissionRecordService>();
builder.Services.AddScoped<IMedicalFindingRecordService, MedicalFindingRecordService>();
builder.Services.AddScoped<IHelperMethodsService, HelperMethodsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
