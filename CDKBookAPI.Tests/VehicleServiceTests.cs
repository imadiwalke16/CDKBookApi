using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Domain.Entities;
using Application.Interfaces;
using Application.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Application.Tests.Services
{
    public class VehicleServiceTests
    {
        private readonly Mock<IVehicleRepository> _mockRepo;
        private readonly IVehicleService _vehicleService;

        public VehicleServiceTests()
        {
            _mockRepo = new Mock<IVehicleRepository>();
            _vehicleService = new VehicleService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetVehiclesByUserIdAsync_ShouldReturnVehicleList_WhenVehiclesExist()
        {
            // Arrange
            var userId = 1;
            var vehicles = new List<Vehicle>
            {
                new Vehicle { Id = 1, Make = "Toyota", Model = "Camry", Year = "2020", UserId = userId },
                new Vehicle { Id = 2, Make = "Honda", Model = "Civic", Year = "2019", UserId = userId }
            };

            _mockRepo.Setup(r => r.GetVehiclesByUserIdAsync(userId))
                     .ReturnsAsync(vehicles);

            // Act
            var result = await _vehicleService.GetVehiclesByUserIdAsync(userId);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(v => v.Make == "Toyota");
        }

        [Fact]
        public async Task GetVehicleByIdAsync_ShouldReturnVehicle_WhenVehicleExists()
        {
            // Arrange
            var vehicleId = 1;
            var expectedVehicle = new Vehicle
            {
                Id = vehicleId,
                Make = "Ford",
                Model = "Focus",
                Year = "2022",
                UserId = 10
            };

            _mockRepo.Setup(r => r.GetVehicleByIdAsync(vehicleId))
                     .ReturnsAsync(expectedVehicle);

            // Act
            var result = await _vehicleService.GetVehicleByIdAsync(vehicleId);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(vehicleId);
            result.Make.Should().Be("Ford");
        }

        [Fact]
        public async Task AddVehicleAsync_ShouldCallRepositoryWithCorrectVehicle()
        {
            // Arrange
            var newVehicle = new Vehicle
            {
                Id = 0,
                Make = "Hyundai",
                Model = "Elantra",
                Year = "2021",
                LicensePlate = "ABC123",
                UserId = 5
            };

            // Act
            await _vehicleService.AddVehicleAsync(newVehicle);

            // Assert
            _mockRepo.Verify(r => r.AddVehicleAsync(It.Is<Vehicle>(v =>
                v.Make == "Hyundai" &&
                v.Model == "Elantra" &&
                v.Year == "2021" &&
                v.LicensePlate == "ABC123" &&
                v.UserId == 5
            )), Times.Once);
        }

        [Fact]
        public async Task UpdateVehicleAsync_ShouldCallRepositoryWithCorrectVehicle()
        {
            // Arrange
            var updatedVehicle = new Vehicle
            {
                Id = 3,
                Make = "Nissan",
                Model = "Altima",
                Year = "2023",
                LicensePlate = "XYZ789",
                UserId = 7
            };

            // Act
            await _vehicleService.UpdateVehicleAsync(updatedVehicle);

            // Assert
            _mockRepo.Verify(r => r.UpdateVehicleAsync(It.Is<Vehicle>(v =>
                v.Id == 3 &&
                v.Make == "Nissan" &&
                v.Model == "Altima" &&
                v.Year == "2023" &&
                v.LicensePlate == "XYZ789" &&
                v.UserId == 7
            )), Times.Once);
        }

        [Fact]
        public async Task DeleteVehicleAsync_ShouldCallDelete_WhenVehicleExists()
        {
            // Arrange
            var vehicleId = 10;
            var vehicle = new Vehicle
            {
                Id = vehicleId,
                Make = "BMW",
                Model = "X5",
                Year = "2022",
                UserId = 3
            };

            _mockRepo.Setup(r => r.GetVehicleByIdAsync(vehicleId))
                     .ReturnsAsync(vehicle);

            // Act
            await _vehicleService.DeleteVehicleAsync(vehicleId);

            // Assert
            _mockRepo.Verify(r => r.DeleteVehicleAsync(vehicle), Times.Once);
        }

    }
}


