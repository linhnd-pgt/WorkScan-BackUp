using BaseApp.Constants;
using BaseApp.Data;
using BaseApp.DTO;
using BaseApp.Models;
using BaseApp.Repository.Base;
using BaseApp.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseApp.Tests.Service
{
    public class ApplicationServiceTest
    {

        private readonly IRepositoryManager _mockRepositoryManager;
        private readonly DbContextOptions<BaseAppDBContext> _mockDbContextOptions;
        private readonly BaseAppDBContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ApplicationService _applicationService;

        public ApplicationServiceTest()
        {
            _mockDbContextOptions = new DbContextOptionsBuilder<BaseAppDBContext>()
                                    .UseInMemoryDatabase(databaseName: "ApplicationTestBase")
                                    .EnableSensitiveDataLogging()
                                    .Options;
            _httpContextAccessor = new HttpContextAccessor();
            _dbContext = new BaseAppDBContext(_mockDbContextOptions, _httpContextAccessor);
            _mockRepositoryManager = new RepositoryManager(_dbContext);
            _applicationService = new ApplicationService(_mockRepositoryManager);
        }

        private async Task ResetDbContext()
        {
            _dbContext.EmployeeList.RemoveRange(_dbContext.EmployeeList);
            _dbContext.ApplicationList.RemoveRange(_dbContext.ApplicationList);
            await _dbContext.SaveChangesAsync();
        }

        private async Task PrepareData()
        {
            await ResetDbContext(); 

            // set up an existed employee in db
            EmployeeModel employee = new EmployeeModel
            {
                Id = 1,
                Code = "Code1",
                FullName = "superadmin",
                Password = BCrypt.Net.BCrypt.HashPassword("123456"),
                Email = "superadmin@gmail.com",
                PhoneNum = "+84388124368",
                Avatar = "https://res.cloudinary.com/duylinhmedia/image/upload/v1725848701/rje2a5ibauortais1xrl.jpg",
                Address = "Ha Noi",
                CreatedDate = DateTime.Now,
                CreatedBy = 1.ToString(),
                UpdatedDate = DateTime.Now,
                UpdatedBy = 1.ToString()
            };

            _dbContext.EmployeeList.Add(employee);

            for (long i = 2; i < 15; i++)
            {
                ApplicationModel applicationModel = new ApplicationModel
                {
                    Id = i,
                    Name = "Name " + i,
                    Type = EnumTypes.ApplicationType.LEAVE,
                    Status = EnumTypes.ApplicationStatus.REQUESTED,
                    StartDate = DateTime.Now.AddDays(1),
                    EndDate = DateTime.Now.AddDays(2),
                    Note = "Note " + i,
                    EmployeeId = 1
                };
                _dbContext.ApplicationList.Add(applicationModel);
            }

            await _dbContext.SaveChangesAsync();

            _dbContext.Entry(employee).State = EntityState.Detached;

        }

        [Fact]
        public async Task GivenCorrectRequestApplicationDTO_ThenCreateApplication_ShouldReturnTrue()
        {
            // Arrange
            await PrepareData();

            RequestApplicationDTO requestApplicationDTO = new RequestApplicationDTO
            { 
                Title = "Đơn xin nghỉ",
                ApplicationType = EnumTypes.ApplicationType.LEAVE,
                StartedDate = DateTime.Now.AddDays(1),
                EndedDate = DateTime.Now.AddDays(2),
                EmpId = 1,
                Note = "Note123456"
            };

            // Act
            bool createFunctionResult;
            try
            {
                createFunctionResult = await _applicationService.CreateApplication(requestApplicationDTO);
            }
            catch
            {
                createFunctionResult = false;
            }

            // Assert
            Assert.True(createFunctionResult);

            ApplicationModel createApplicationList = _dbContext.ApplicationList
                                                  .FirstOrDefault(e =>
                                                  e.EmployeeId == requestApplicationDTO.EmpId
                                                  && e.Name.Equals(requestApplicationDTO.Title)
                                                  && e.Type.Equals(requestApplicationDTO.ApplicationType)
                                                  && e.StartDate.Equals(requestApplicationDTO.StartedDate)
                                                  && e.EndDate.Equals(requestApplicationDTO.EndedDate));

            Assert.NotNull(createApplicationList);
        }

        [Fact]
        public async Task GivenWrongRequestApplicationDTO_ThenCreateApplication_ShouldReturnFalse()
        {
            // Arrange
            await PrepareData();

            RequestApplicationDTO requestApplicationDTO = new RequestApplicationDTO
            {
                Title = null,
                ApplicationType = EnumTypes.ApplicationType.LEAVE,
                EmpId = 1,
                Note = "Note123456"
            };

            // Act
            bool createFunctionResult;
            try
            {
                createFunctionResult = await _applicationService.CreateApplication(requestApplicationDTO);
            }
            catch
            {
                createFunctionResult = false;
            }

            // Assert
            Assert.False(createFunctionResult);

            ApplicationModel createApplicationList = _dbContext.ApplicationList
                                                  .FirstOrDefault(e =>
                                                  e.EmployeeId == requestApplicationDTO.EmpId
                                                  && e.Name.Equals(requestApplicationDTO.Title)
                                                  && e.Type.Equals(requestApplicationDTO.ApplicationType)
                                                  && e.StartDate.Equals(requestApplicationDTO.StartedDate)
                                                  && e.EndDate.Equals(requestApplicationDTO.EndedDate));

            Assert.Null(createApplicationList);
        }

        [Fact]
        public async Task GivenWrongEmloyeeId_ThenCreateApplication_ShouldThrowError()
        {
            // Arrange
            await PrepareData();

            RequestApplicationDTO requestApplicationDTO = new RequestApplicationDTO
            {
                Title = "Đơn xin nghỉ",
                ApplicationType = EnumTypes.ApplicationType.LEAVE,
                StartedDate = DateTime.Now.AddDays(1),
                EndedDate = DateTime.Now.AddDays(2),
                EmpId = 4444,
                Note = "Note123456"
            };

            var expectedResult = new Exception(DevMessageConstants.OBJECT_NOT_FOUND);

            // Act
            var createFunctionResult = await Assert.ThrowsAsync<Exception>(() => _applicationService.CreateApplication(requestApplicationDTO));

            // Assert
            Assert.Equal(createFunctionResult.Message, expectedResult.Message);

            ApplicationModel createApplicationList = _dbContext.ApplicationList
                                                  .FirstOrDefault(e =>
                                                  e.EmployeeId == requestApplicationDTO.EmpId
                                                  && e.Name.Equals(requestApplicationDTO.Title)
                                                  && e.Type.Equals(requestApplicationDTO.ApplicationType)
                                                  && e.StartDate.Equals(requestApplicationDTO.StartedDate)
                                                  && e.EndDate.Equals(requestApplicationDTO.EndedDate));

            Assert.Null(createApplicationList);
        }

        [Fact]
        public async Task GivenWrongPageOrSize_ThenGetApplicationList_ShouldReturnList()
        {
            // Arrange
            await PrepareData();

            // Act
            var applicationList = await _applicationService.GetApplicationList(1, 0, 0, 0, DateTime.MinValue, DateTime.MinValue);

            // Assert
            Assert.NotNull(applicationList);

            Assert.Equal(10, applicationList.PageSize);

            Assert.Equal(2, applicationList.TotalPages);

            Assert.Equal(13, applicationList.TotalRecords);
        }

        [Fact]
        public async Task GivenWrongEmpId_ThenGetApplicationList_ShouldReturnEmptyApplicationList()
        {
            // Arrange
            await PrepareData();

            // Act
            var applicationList = await _applicationService.GetApplicationList(4444, 0, 0, 0, DateTime.MinValue, DateTime.MinValue);

            // Assert
            Assert.NotNull(applicationList);

            Assert.Equal(0, applicationList.TotalPages);

            Assert.Equal(0, applicationList.TotalRecords);
        }

        [Fact]
        public async Task GivenCorrectInput_ThenGetApplicationList_ShouldReturnList()
        {
            // Arrange
            await PrepareData();

            // Act
            var applicationList = await _applicationService.GetApplicationList(1, 0, 1, 5, new DateTime(2024, 11, 10), new DateTime(2024, 11, 30));

            // Assert
            Assert.NotNull(applicationList);

            Assert.Equal(5, applicationList.PageSize);

            Assert.Equal(3, applicationList.TotalPages);

            Assert.Equal(13, applicationList.TotalRecords);
        }

        [Fact]
        public async Task GivenWrongApplicationID_ThenUpdateApplicationStatus_ShouldThrowException()
        {
            // Arrange
            await PrepareData();

            RequestUpdateApplicationStatusDTO requestApplicationDTO = new RequestUpdateApplicationStatusDTO
            {
                ApplicationId = 4444
            };

            // Act
            var updateApplicationStatusReport = await Assert.ThrowsAsync<Exception>(() => _applicationService.UpdateApplicationStatus(requestApplicationDTO));

            // Assert
            Assert.Equal(updateApplicationStatusReport.Message, DevMessageConstants.OBJECT_IS_EMPTY);
        }

        [Fact]
        public async Task GivenCorrect_ThenUpdateApplicationStatus_ShouldThrowException()
        {
            // Arrange
            await PrepareData();

            RequestUpdateApplicationStatusDTO requestApplicationDTO = new RequestUpdateApplicationStatusDTO
            {
                ApplicationId = 1,
                ApplicationStatus = EnumTypes.ApplicationStatus.CANCELLED
            };

            // Act
            await _applicationService.UpdateApplicationStatus(requestApplicationDTO);

            // Assert
            ApplicationModel applicationModel = await _dbContext.ApplicationList.Where(app => app.Id == requestApplicationDTO.ApplicationId
                                                && app.Status.Equals(requestApplicationDTO.ApplicationStatus)).FirstOrDefaultAsync();

            Assert.NotNull(applicationModel);

        }


    }
}
