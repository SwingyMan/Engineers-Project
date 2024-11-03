using Domain.Entities;
using Infrastructure.Persistence;
using Infrastructure.IRepositories;
using Moq;
using Task = System.Threading.Tasks.Task;

namespace InfrastructureTests;


[TestFixture]
public class GenericRepositoryTests
{
    private Mock<IGenericRepository<Role>> _mockRepository;
    private Role _sampleEntity;

    [SetUp]
    public void SetUp()
    {
        _mockRepository = new Mock<IGenericRepository<Role>>();
        _sampleEntity = new Role
        {
            Id = Guid.NewGuid(),
            Name = "Test Entity"
        };
    }

    [Test]
    public async Task GetAll_ShouldReturnEntities()
    {
        // Arrange
        var entities = new List<Role> { _sampleEntity };
        _mockRepository.Setup(repo => repo.GetAll())
                       .ReturnsAsync(entities);

        // Act
        var result = await _mockRepository.Object.GetAll();

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(entities, result);
    }

    [Test]
    public async Task GetByID_ShouldReturnEntity()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetByID(It.IsAny<Guid>()))
                       .ReturnsAsync(_sampleEntity);

        // Act
        var result = await _mockRepository.Object.GetByID(_sampleEntity.Id);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(_sampleEntity, result);
    }

    [Test]
    public async Task Update_ShouldReturnUpdatedEntity()
    {
        // Arrange
        _sampleEntity.Name = "Updated Entity";
        _mockRepository.Setup(repo => repo.Update(It.IsAny<Guid>(), It.IsAny<Role>()))
                       .ReturnsAsync(_sampleEntity);

        // Act
        var result = await _mockRepository.Object.Update(_sampleEntity.Id, _sampleEntity);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(_sampleEntity, result);
    }

    [Test]
    public void Delete_ShouldComplete()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                       .Returns(Task.CompletedTask);

        // Act
        var task = _mockRepository.Object.Delete(_sampleEntity.Id);

        // Assert
        Assert.DoesNotThrowAsync(() => task);
    }

    [Test]
    public async Task Add_ShouldReturnAddedEntity()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.Add(It.IsAny<Role>()))
                       .ReturnsAsync(_sampleEntity);

        // Act
        var result = await _mockRepository.Object.Add(_sampleEntity);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(_sampleEntity, result);
    }
}