using EduCanin.Models.Entities;
using EduCanin.Service;
using EduCanin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Educanin.Tests;



[TestClass]
public class AccountServiceTests
{
    // Déclarations des mocks nécessaires
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<RoleManager<IdentityRole>> _roleManagerMock;
    private Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private AccountService _accountService;

    [TestInitialize]
    public void Setup()
    {
        // On crée un mock pour UserManager (nécessite un IUserStore fictif)
        var store = new Mock<IUserStore<ApplicationUser>>();
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

        // On crée un mock pour RoleManager (nécessite un IRoleStore fictif)
        var roleStore = new Mock<IRoleStore<IdentityRole>>();
        _roleManagerMock = new Mock<RoleManager<IdentityRole>>(roleStore.Object, null, null, null, null);

        // Création du SignInManager mocké (utilise UserManager + IHttpContextAccessor + ClaimsFactory)
        var contextAccessor = new Mock<IHttpContextAccessor>();
        var claimsFactory = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            contextAccessor.Object,
            claimsFactory.Object,
            null,
            null,
            null,
            null
        );

        // On injecte les mocks dans le service à tester
        _accountService = new AccountService(_userManagerMock.Object, _roleManagerMock.Object, _signInManagerMock.Object);
    }

    [TestMethod]
    public async Task LoginAsync_ShouldReturnSuccessResult_WhenCredentialsAreCorrect()
    {
        // Arrange : on prépare les données d'entrée
        var model = new LoginViewModel { Email = "test@mail.com", Password = "P@ssword1", RememberMe = false };

        // On configure le mock pour retourner un succès à la connexion
        _signInManagerMock
            .Setup(s => s.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, true))
            .ReturnsAsync(SignInResult.Success);

        // Act : on appelle la méthode à tester
        var result = await _accountService.LoginAsync(model);

        // Assert : on vérifie que la connexion a réussi
        Assert.IsTrue(result.Succeeded);
    }

    [TestMethod]
    public async Task RegisterAsync_ShouldReturnSuccess_WhenUserCreatedAndRoleAssigned()
    {
        // Arrange : on prépare les données d’inscription
        var model = new RegisterViewModel
        {
            Email = "newuser@mail.com",
            Password = "P@ssword1",
            FirstName = "John",
            LastName = "Doe"
        };

        // On simule que l’utilisateur est bien créé
        _userManagerMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), model.Password))
            .ReturnsAsync(IdentityResult.Success);

        // On simule que le rôle "User" n'existe pas
        _roleManagerMock.Setup(x => x.RoleExistsAsync("User"))
            .ReturnsAsync(false);

        // On simule que le rôle est créé avec succès
        _roleManagerMock.Setup(x => x.CreateAsync(It.IsAny<IdentityRole>()))
            .ReturnsAsync(IdentityResult.Success);

        // On simule que l’ajout au rôle "User" réussit
        _userManagerMock.Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
            .ReturnsAsync(IdentityResult.Success);

        // On simule que la connexion de l’utilisateur réussit après l'inscription
        _signInManagerMock
            .Setup(x => x.SignInAsync(It.IsAny<ApplicationUser>(), false, null))
            .Returns(Task.CompletedTask);

        // Act : on appelle RegisterAsync
        var result = await _accountService.RegisterAsync(model);

        // Assert : on s’assure que l’inscription est un succès
        Assert.IsTrue(result.Succeeded);
    }

    [TestMethod]
    public async Task LogoutAsync_ShouldCallSignOutAsync()
    {
        // Arrange : on simule la méthode de déconnexion
        _signInManagerMock.Setup(x => x.SignOutAsync()).Returns(Task.CompletedTask).Verifiable();

        // Act : on appelle la méthode LogoutAsync
        await _accountService.LogoutAsync();

        // Assert : on vérifie que SignOutAsync a bien été appelé une fois
        _signInManagerMock.Verify(x => x.SignOutAsync(), Times.Once);
    }
}
